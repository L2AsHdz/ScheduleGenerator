using System.ComponentModel.DataAnnotations;

namespace ScheduleCore.Models;

public class CursoHorario
{
    [Key]
    public int IdRegistro { get; set; }
    public int CodigoHorario { get; set; }
    public int CodigoCurso { get; set; }
    public int CodigoCatedratico { get; set; }
    public int NoSalon { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public int Estado { get; set; }
}