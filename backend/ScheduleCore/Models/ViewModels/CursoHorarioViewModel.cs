namespace ScheduleCore.Models.ViewModels;

public class CursoHorarioViewModel
{
    public int IdRegistro { get; set; }
    public int CodigoHorario { get; set; }
    public CursoViewModel Curso { get; set; }
    public Catedratico Catedratico { get; set; }
    public Salon Salon { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public int Estado { get; set; }
}