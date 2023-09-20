using System.ComponentModel.DataAnnotations;

namespace ScheduleCore.Models;

public class Curso
{
    [Key]
    public int CodigoCurso { get; set; }
    public string Nombre { get; set; }
}