using System.ComponentModel.DataAnnotations;

namespace ScheduleCore.Models;

public class CursoCatedratico
{
    [Key]
    public int CodigoCatedratico { get; set; }
    [Key]
    public int CodigoCurso { get; set; }
    public int Prioridad { get; set; }
}