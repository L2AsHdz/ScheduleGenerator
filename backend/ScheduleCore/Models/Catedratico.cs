using System.ComponentModel.DataAnnotations;

namespace ScheduleCore.Models;

public class Catedratico
{
    [Key]
    public int CodigoCatedratico { get; set; }
    public string Nombre { get; set; }
    public int NoColegiado { get; set; }
    public TimeOnly HoraEntrada { get; set; }
    public TimeOnly HoraSalida { get; set; }
}