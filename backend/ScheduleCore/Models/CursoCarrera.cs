using System.ComponentModel.DataAnnotations;

namespace ScheduleCore.Models;

public class CursoCarrera
{
    [Key]
    public int CodigoCarrera { get; set; }
    [Key]
    public int CodigoCurso { get; set; }
    public int CantidadAsignaciones { get; set; }
    public int Semestre { get; set; }
    public int Creditos { get; set; }
    public int Costo { get; set; }
    public bool EsObligatorio { get; set; }
}