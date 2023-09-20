using System.ComponentModel.DataAnnotations;

namespace ScheduleCore.Models;

public class CursoSalon
{
    [Key]
    public int NoSalon { get; set; }
    [Key]
    public int CodigoCurso { get; set; }
}