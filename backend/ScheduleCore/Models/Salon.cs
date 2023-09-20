using System.ComponentModel.DataAnnotations;

namespace ScheduleCore.Models;

public class Salon
{
    [Key]
    public int NoSalon { get; set; }
    public string Nombre { get; set; }
    public int CapacidadMaxima { get; set; }
    public int CapacidadRecomendada { get; set; }
    public int? CodigoCarreraPreferida { get; set; }
    public string Ubicacion { get; set; }
}