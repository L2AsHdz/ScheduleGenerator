using ScheduleCore.Models;
using ScheduleCore.Models.ViewModels;
using ScheduleCore.Services.Parametro;

namespace ScheduleCore.Services.Core;

public class CoreService
{
    private readonly ScheduleDB context;
    private readonly ParametroService parametroService;
    private readonly ParameterManager param;

    public CoreService(ScheduleDB context, ParametroService parametroService)
    {
        this.context = context;
        this.parametroService = parametroService;
        this.param = ParameterManager.Instance;
    }

    public IEnumerable<CursoHorarioDTO> Execute()
    {
        var horario = new List<CursoHorarioDTO>();


        //---------------------------------------------Obtener Datos---------------------------------------------

        // Obtener parametros
        parametroService.GetParametros();

        // Obtener ultimo id Horario generado
        var lastScheduleId = (from ch in context.CursoHorario
            select ch.CodigoHorario).OrderByDescending(ch => ch).FirstOrDefault();

        // Obtener todos  los cursos
        var queryCursos = (from c in context.Curso select c);
        var cursos = queryCursos.ToList();

        // Obtener cursos a aperturar
        var queryCursosPorAsignar = (from cc in context.CursoCarrera select cc);
        var cursosPorAsignar = queryCursosPorAsignar.ToList();

        // Obtener carreras
        var queryCarreras = (from c in context.Carrera select c);
        var carreras = queryCarreras.ToList();

        // Obtener catedraticos
        var queryCatedraticos = (from c in context.Catedratico select c);
        var catedraticos = queryCatedraticos.ToList();

        // Obtener prioridades de catedraticos
        var queryCursosCatedratico = (from cc in context.CursoCatedratico select cc);
        var cursosCatedraticos = queryCursosCatedratico.ToList();

        // Obtener salones
        var querySalones = (from s in context.Salon select s);
        var salones = querySalones.ToList();

        // Obtener prioridades de salones

        //---------------------------------------Filtros y Ordenamiento---------------------------------------

        // Filtrar cursos por semestre
        List<CursoCarrera> filteredCourses;

        filteredCourses = param.EvenSemester
            ? cursosPorAsignar.Where(c => c.Semestre % 2 == 0).ToList()
            : cursosPorAsignar.Where(c => c.Semestre % 2 != 0).ToList();

        // Filtrar por minimo asignaciones
        if (!param.IgnoreMinAssignments)
            filteredCourses =
                filteredCourses.Where(c => c.CantidadAsignaciones >= param.MinAssignments).ToList();

        // Ordenar dependiendo del valor del parametro LastSemesterPriority

        filteredCourses = param.LastSemesterPriority
            ? filteredCourses.OrderByDescending(c => c.Semestre).ToList()
            : filteredCourses.OrderBy(c => c.Semestre).ToList();

        //---------------------------------------------Asignaciones---------------------------------------------

        var assignedCourses = new List<CursoHorarioDTO>();
        // assignedCourses.Add(new CursoHorarioDTO()
        // {
        //     Curso = new CursoDTO() { Semestre = 1, Carrera = new Carrera() { CodigoCarrera = 1 } },
        //     Salon = new Salon() { NoSalon = 1 }, HoraInicio = TimeOnly.Parse("14:00:00")
        // });
        // assignedCourses.Add(new CursoHorarioDTO()
        // {
        //     Curso = new CursoDTO() { Semestre = 1, Carrera = new Carrera() { CodigoCarrera = 1 } },
        //     Salon = new Salon() { NoSalon = 2 }, HoraInicio = TimeOnly.Parse("14:00:00")
        // });
        // assignedCourses.Add(new CursoHorarioDTO()
        // {
        //     Curso = new CursoDTO() { Semestre = 4, Carrera = new Carrera() { CodigoCarrera = 1 } },
        //     Salon = new Salon() { NoSalon = 5 }, HoraInicio = TimeOnly.Parse("16:00:00")
        // });

        var notAssignedCourses = new List<CursoConAdvertencia>();

        var horaActual = param.StartHour;

        //Listado de horarios disponibles
        var horarios = Enumerable.Range(0, (int)((param.EndHour - horaActual).TotalMinutes / param.DurationPeriod))
            .Select(i => horaActual.Add(TimeSpan.FromMinutes(i * param.DurationPeriod)))
            .ToList();


        foreach (var c in filteredCourses)
        {
            var cursoHorario = new CursoHorario();
            var salonHorario = new { salon = 0, hora = horaActual};

            var salonesHorario = (from s in salones
                from h in horarios
                select new
                {
                    salon = s.NoSalon, hora = h, carreraPreferida = s.CodigoCarreraPreferida ?? 0,
                    ocupacionRecomendada = (c.CantidadAsignaciones / (decimal)s.CapacidadRecomendada),
                    ocupacionMaxima = (c.CantidadAsignaciones / (decimal)s.CapacidadMaxima)
                }).ToList();

            salonesHorario = salonesHorario.Where(sh =>
                    !assignedCourses.Any(sc =>
                        (sh.salon == sc.Salon.NoSalon &&
                         sh.hora == sc.HoraInicio) ||
                        (sh.hora == sc.HoraInicio && c.Semestre == sc.Curso.Semestre &&
                         c.CodigoCarrera == sc.Curso.Carrera.CodigoCarrera)
                    )
                )
                .Select(sh => sh).ToList();


            if (param.CapacityPriority)
            {
                salonHorario = salonesHorario.Where(sh => sh.ocupacionRecomendada <= 1)
                    .OrderByDescending(sh => sh.ocupacionRecomendada)
                    .ThenBy(sh => sh.salon)
                    .ThenBy(sh => sh.hora)
                    .Select(sh => new { sh.salon, sh.hora})
                    .FirstOrDefault() ?? new {salon = 0, hora = horaActual};

                if (salonHorario.salon <= 0)
                    salonHorario = salonesHorario.Where(sh => sh.ocupacionMaxima <= 1)
                        .OrderByDescending(sh => sh.ocupacionMaxima)
                        .ThenBy(sh => sh.salon)
                        .ThenBy(sh => sh.hora)
                        .Select(sh => new { sh.salon, sh.hora})
                        .FirstOrDefault() ?? new {salon = 0, hora = horaActual};

                if (salonHorario.salon <= 0 && param.IgnoreMaxCapacity)
                    salonHorario = salonesHorario.OrderBy(sh => sh.salon).ThenBy(sh => sh.hora)
                        .Select(sh => new { sh.salon, sh.hora})
                        .FirstOrDefault() ?? new {salon = 0, hora = horaActual};
            }
            else if (param.CareerPriority)
            {
                var salonesHorarioTemp = salonesHorario.Where(sh => sh.ocupacionRecomendada <= 1).ToList();

                var filtro =
                    salonesHorarioTemp.Any(sh => sh.carreraPreferida > 0 && sh.carreraPreferida == c.CodigoCarrera);

                salonesHorarioTemp = filtro
                        ? salonesHorarioTemp.Where(sh =>
                            sh.carreraPreferida > 0 && sh.carreraPreferida == c.CodigoCarrera).ToList()
                        : salonesHorarioTemp
                    ;

                salonHorario = salonesHorarioTemp.OrderBy(sh => sh.salon).ThenBy(sh => sh.hora)
                    .Select(sh => new { sh.salon, sh.hora})
                    .FirstOrDefault() ?? new {salon = 0, hora = horaActual};

                if (salonHorario.salon <= 0)
                {
                    salonesHorarioTemp = salonesHorario.Where(sh => sh.ocupacionMaxima <= 1).ToList();

                    salonesHorarioTemp = filtro
                            ? salonesHorarioTemp.Where(sh =>
                                sh.carreraPreferida > 0 && sh.carreraPreferida == c.CodigoCarrera).ToList()
                            : salonesHorarioTemp
                        ;

                    salonHorario = salonesHorarioTemp.OrderBy(sh => sh.salon).ThenBy(sh => sh.hora)
                        .Select(sh => new { sh.salon, sh.hora})
                        .FirstOrDefault() ?? new {salon = 0, hora = horaActual};
                }

                if (salonHorario.salon <= 0 && param.IgnoreMaxCapacity)
                {
                    salonesHorarioTemp = salonesHorario;
                    salonesHorarioTemp = filtro
                            ? salonesHorarioTemp.Where(sh =>
                                sh.carreraPreferida > 0 && sh.carreraPreferida == c.CodigoCarrera).ToList()
                            : salonesHorarioTemp
                        ;
                    salonHorario = salonesHorarioTemp.OrderBy(sh => sh.salon).ThenBy(sh => sh.hora)
                        .Select(sh => new { sh.salon, sh.hora})
                        .FirstOrDefault() ?? new {salon = 0, hora = horaActual};
                }
            }
            else
            {
                salonHorario = salonesHorario.Where(sh => sh.ocupacionRecomendada <= 1)
                    .OrderBy(sh => sh.salon)
                    .ThenBy(sh => sh.hora)
                    .Select(sh => new { sh.salon, sh.hora})
                    .FirstOrDefault() ?? new {salon = 0, hora = horaActual};

                if (salonHorario.salon <= 0)
                    salonHorario = salonesHorario.Where(sh => sh.ocupacionMaxima <= 1)
                        .OrderBy(sh => sh.salon)
                        .ThenBy(sh => sh.hora)
                        .Select(sh => new { sh.salon, sh.hora})
                        .FirstOrDefault() ?? new {salon = 0, hora = horaActual};

                if (salonHorario.salon <= 0 && param.IgnoreMaxCapacity)
                    salonHorario = salonesHorario.OrderBy(sh => sh.salon).ThenBy(sh => sh.hora)
                        .Select(sh => new { sh.salon, sh.hora})
                        .FirstOrDefault() ?? new {salon = 0, hora = horaActual};
            }

            if (salonHorario.salon == 0)
            {
                notAssignedCourses.Add(new CursoConAdvertencia()
                {
                    CodigoCurso = c.CodigoCurso,
                    Nombre = cursos.Where(c2 => c2.CodigoCurso == c.CodigoCurso).Select(c2 => c2.Nombre)
                        .FirstOrDefault()!,
                    Advertencia = $"No existe salon que cumpla con la cantidad de alumnos asignados"
                });
                continue;
            }

            assignedCourses.Add(new CursoHorarioDTO()
            {
                CodigoHorario = lastScheduleId + 1,
                Curso = new CursoDTO()
                {
                    CodigoCurso = c.CodigoCurso,
                    Nombre = cursos.Find(c2 => c2.CodigoCurso == c.CodigoCurso)?.Nombre!,
                    Semestre = c.Semestre,
                    Carrera = carreras.Find(carrera => carrera.CodigoCarrera == c.CodigoCarrera)!
                },
                Salon = salones.Find(s => s.NoSalon == salonHorario.salon)!,
                HoraInicio = salonHorario.hora,
                HoraFin = salonHorario.hora.AddMinutes(param.DurationPeriod)
            });
        }
        
        Console.WriteLine("Cursos asignados");
        assignedCourses.ForEach(ac => Console.WriteLine(ac.ToString()));
        
        Console.WriteLine("Cursos no asignados");
        notAssignedCourses.ForEach(ac => Console.WriteLine(ac.ToString()));

        return horario;
    }
}