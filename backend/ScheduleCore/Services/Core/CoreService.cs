using System.Text.Json;
using ScheduleCore.Models;
using ScheduleCore.Models.DTO;
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

    public ResponseData Execute()
    {
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

        var notAssignedCourses = new List<CursoAdvertencia>();

        var horaActual = param.StartHour;

        //Listado de horarios disponibles
        var horarios = Enumerable.Range(0, (int)((param.EndHour - horaActual).TotalMinutes / param.DurationPeriod))
            .Select(i => horaActual.Add(TimeSpan.FromMinutes(i * param.DurationPeriod)))
            .ToList();


        foreach (var c in filteredCourses)
        {
            var cursoHorario = new CursoHorario();
            var catedraticosCursos = (from cat in catedraticos
                join curcat in cursosCatedraticos on cat.CodigoCatedratico equals curcat.CodigoCatedratico
                where curcat.CodigoCurso == c.CodigoCurso
                select new
                {
                    cat.CodigoCatedratico,
                    cat.HoraEntrada,
                    cat.HoraSalida,
                    curcat.Prioridad
                }).ToList();


            var salonHorario = new { salon = 0, hora = horaActual };

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

            var salonesHorarioTemp = salonesHorario;

            if (param.CapacityPriority)
            {
                salonesHorarioTemp = salonesHorario.Where(sh => sh.ocupacionRecomendada <= 1)
                    .OrderByDescending(sh => sh.ocupacionRecomendada)
                    .ThenBy(sh => sh.salon)
                    .ThenBy(sh => sh.hora).ToList();

                salonHorario = salonesHorarioTemp
                    .Select(sh => new { sh.salon, sh.hora })
                    .FirstOrDefault() ?? new { salon = 0, hora = horaActual };

                if (salonHorario.salon <= 0)
                {
                    salonesHorarioTemp = salonesHorario.Where(sh => sh.ocupacionMaxima <= 1)
                        .OrderByDescending(sh => sh.ocupacionMaxima)
                        .ThenBy(sh => sh.salon)
                        .ThenBy(sh => sh.hora).ToList();

                    salonHorario = salonesHorarioTemp
                        .Select(sh => new { sh.salon, sh.hora })
                        .FirstOrDefault() ?? new { salon = 0, hora = horaActual };
                }

                if (salonHorario.salon <= 0 && param.IgnoreMaxCapacity)
                {
                    salonesHorarioTemp = salonesHorario.OrderBy(sh => sh.salon).ThenBy(sh => sh.hora).ToList();

                    salonHorario = salonesHorarioTemp
                        .Select(sh => new { sh.salon, sh.hora })
                        .FirstOrDefault() ?? new { salon = 0, hora = horaActual };
                }
            }
            else if (param.CareerPriority)
            {
                salonesHorarioTemp = salonesHorario.Where(sh => sh.ocupacionRecomendada <= 1).ToList();

                var filtro =
                    salonesHorarioTemp.Any(sh => sh.carreraPreferida > 0 && sh.carreraPreferida == c.CodigoCarrera);

                salonesHorarioTemp = filtro
                        ? salonesHorarioTemp.Where(sh =>
                            sh.carreraPreferida > 0 && sh.carreraPreferida == c.CodigoCarrera).ToList()
                        : salonesHorarioTemp
                    ;

                salonHorario = salonesHorarioTemp.OrderBy(sh => sh.salon).ThenBy(sh => sh.hora)
                    .Select(sh => new { sh.salon, sh.hora })
                    .FirstOrDefault() ?? new { salon = 0, hora = horaActual };

                if (salonHorario.salon <= 0)
                {
                    salonesHorarioTemp = salonesHorario.Where(sh => sh.ocupacionMaxima <= 1).ToList();

                    salonesHorarioTemp = filtro
                            ? salonesHorarioTemp.Where(sh =>
                                sh.carreraPreferida > 0 && sh.carreraPreferida == c.CodigoCarrera).ToList()
                            : salonesHorarioTemp
                        ;

                    salonHorario = salonesHorarioTemp.OrderBy(sh => sh.salon).ThenBy(sh => sh.hora)
                        .Select(sh => new { sh.salon, sh.hora })
                        .FirstOrDefault() ?? new { salon = 0, hora = horaActual };
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
                        .Select(sh => new { sh.salon, sh.hora })
                        .FirstOrDefault() ?? new { salon = 0, hora = horaActual };
                }
            }
            else
            {
                salonesHorarioTemp = salonesHorario.Where(sh => sh.ocupacionRecomendada <= 1)
                    .OrderBy(sh => sh.salon)
                    .ThenBy(sh => sh.hora).ToList();

                salonHorario = salonesHorarioTemp
                    .Select(sh => new { sh.salon, sh.hora })
                    .FirstOrDefault() ?? new { salon = 0, hora = horaActual };

                if (salonHorario.salon <= 0)
                {
                    salonesHorarioTemp = salonesHorario.Where(sh => sh.ocupacionMaxima <= 1)
                        .OrderBy(sh => sh.salon)
                        .ThenBy(sh => sh.hora).ToList();

                    salonHorario = salonesHorarioTemp
                        .Select(sh => new { sh.salon, sh.hora })
                        .FirstOrDefault() ?? new { salon = 0, hora = horaActual };
                }

                if (salonHorario.salon <= 0 && param.IgnoreMaxCapacity)
                {
                    salonesHorarioTemp = salonesHorario;

                    salonHorario = salonesHorarioTemp.OrderBy(sh => sh.salon).ThenBy(sh => sh.hora)
                        .Select(sh => new { sh.salon, sh.hora })
                        .FirstOrDefault() ?? new { salon = 0, hora = horaActual };
                }
            }

            if (salonHorario.salon == 0)
            {
                notAssignedCourses.Add(new CursoAdvertencia()
                {
                    CodigoHorario = lastScheduleId + 1,
                    CodigoCurso = c.CodigoCurso,
                    Nombre = cursos.Where(c2 => c2.CodigoCurso == c.CodigoCurso).Select(c2 => c2.Nombre)
                        .FirstOrDefault()!,
                    Advertencia = $"No existe salon que cumpla con la cantidad de alumnos asignados"
                });
                continue;
            }

            salonesHorarioTemp.RemoveAt(0);

            var catedraticoHorario = 0;
            if (param.TeacherPriority)
                catedraticosCursos = catedraticosCursos.OrderByDescending(cat => cat.Prioridad).ToList();

            var salonHorarioTemp = salonHorario;
            foreach (var catedratico in catedraticosCursos)
            {
                if (catedratico.HoraEntrada > salonHorario.hora ||
                    catedratico.HoraSalida < salonHorario.hora.AddMinutes(param.DurationPeriod) ||
                    (assignedCourses.Any(ac =>
                        ac.HoraInicio == salonHorario.hora &&
                        ac.Catedratico.CodigoCatedratico == catedratico.CodigoCatedratico)))
                {
                    foreach (var salonH in salonesHorarioTemp)
                    {
                        salonHorarioTemp = new { salon = salonH.salon, hora = salonH.hora };
                        if (catedratico.HoraEntrada > salonH.hora ||
                            catedratico.HoraSalida < salonH.hora.AddMinutes(param.DurationPeriod) ||
                            assignedCourses.Any(ac =>
                                ac.HoraInicio == salonH.hora &&
                                ac.Catedratico.CodigoCatedratico == catedratico.CodigoCatedratico)) continue;
                        catedraticoHorario = catedratico.CodigoCatedratico;
                        break;
                    }

                    if (catedraticoHorario > 0) break;
                    continue;
                }

                catedraticoHorario = catedratico.CodigoCatedratico;
            }

            if (catedraticoHorario == 0)
            {
                notAssignedCourses.Add(new CursoAdvertencia()
                {
                    CodigoHorario = lastScheduleId + 1,
                    CodigoCurso = c.CodigoCurso,
                    Nombre = cursos.Where(c2 => c2.CodigoCurso == c.CodigoCurso).Select(c2 => c2.Nombre)
                        .FirstOrDefault()!,
                    Advertencia = $"No existe catedratico disponible en el horario {salonHorario.hora}"
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
                Catedratico = catedraticos.Find(cat => cat.CodigoCatedratico == catedraticoHorario)!,
                Salon = salones.Find(s => s.NoSalon == salonHorarioTemp.salon)!,
                HoraInicio = salonHorarioTemp.hora,
                HoraFin = salonHorarioTemp.hora.AddMinutes(param.DurationPeriod)
            });
        }

        var data = new ResponseData()
        {
            Horario = assignedCourses,
            Advertencias = notAssignedCourses,
            Carreras = carreras.Select(c => new CarreraColor()
            {
                CodigoCarrera = c.CodigoCarrera,
                Nombre = c.Nombre,
                Color = c.Color
            }).ToList(),
            Salones = salones,
            HoraInicio = param.StartHour,
            HoraFin = param.EndHour,
            DuracionPeriodo = param.DurationPeriod,
            PorcentajeEfectividad = assignedCourses.Count / (decimal)filteredCourses.Count
        };
        
        return data;
    }
}