using System.ComponentModel.DataAnnotations;

namespace ScheduleCore.Models;

public class Curso
{
    [Key]
    public int CodigoCurso { get; set; }
    public string Nombre { get; set; }
}

public class CursoAdvertencia
{
    [Key]
    public int IdRegistro { get; set; }
    public int CodigoHorario { get; set; }
    public int CodigoCurso { get; set; }
    public string Nombre { get; set; }
    public string Advertencia { get; set; }

    public override string ToString()
    {
        return $"{{ Codigo: {CodigoCurso}, Nombre: {Nombre}, Advertencia: {Advertencia} }}";
    }
}