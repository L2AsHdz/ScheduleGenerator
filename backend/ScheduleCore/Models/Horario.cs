using System.ComponentModel.DataAnnotations;

namespace ScheduleCore.Models;

public class Horario
{
    [Key]
    public int CodigoHorario { get; set; }
    public decimal PorcentajeEfectividad { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Comentario { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public int DuracionPeriodo { get; set; }
}