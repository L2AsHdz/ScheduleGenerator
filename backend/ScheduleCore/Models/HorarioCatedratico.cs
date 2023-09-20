using System.ComponentModel.DataAnnotations;

namespace ScheduleCore.Models;

public class HorarioCatedratico
{
    [Key]
    public int CodigoHorarioCatedratico { get; set; }
    public int CodigoCatedratico { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
}