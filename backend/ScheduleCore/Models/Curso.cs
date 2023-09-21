using System.ComponentModel.DataAnnotations;

namespace ScheduleCore.Models;

public class Curso
{
    [Key]
    public int CodigoCurso { get; set; }
    public string Nombre { get; set; }
}

public class CursoConAdvertencia: Curso
{
    public string Advertencia { get; set; }

    public override string ToString()
    {
        return $"{{ Codigo: {CodigoCurso}, Nombre: {Nombre}, Advertencia: {Advertencia} }}";
    }
}