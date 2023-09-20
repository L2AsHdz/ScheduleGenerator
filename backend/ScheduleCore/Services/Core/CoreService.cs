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
        
        Console.WriteLine(lastScheduleId);
        
        // Obtener cursos a aperturar
        var queryCursos = (from cc in context.CursoCarrera select cc);
        var cursos = queryCursos.ToList();
        
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
        
        filteredCourses = param.EvenSemester ? 
            cursos.Where(c => c.Semestre % 2 == 0).ToList() : 
            cursos.Where(c => c.Semestre % 2 != 0).ToList();
        
        // Filtrar por minimo asignaciones
        if (!param.IgnoreMinAssignments)
            filteredCourses = 
                filteredCourses.Where(c => c.CantidadAsignaciones >= param.MinAssignments).ToList();
        
        // Ordenar dependiendo del valor del parametro LastSemesterPriority
        
        filteredCourses = param.LastSemesterPriority ? 
            filteredCourses.OrderByDescending(c => c.Semestre).ToList() :
            filteredCourses.OrderBy(c => c.Semestre).ToList();
        
        //---------------------------------------------Asignaciones---------------------------------------------
        
        filteredCourses.ForEach(c =>
        {
            
        });
        
        
        
        
        return horario;
    }
}