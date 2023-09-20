using System.ComponentModel.DataAnnotations;

namespace ScheduleCore.Models;

public class Carrera
{
    [Key]
    public int CodigoCarrera { get; set; }
    public string Nombre { get; set; }
    public int CantidadSemestres { get; set; }
    public decimal Presupuesto { get; set; }
}