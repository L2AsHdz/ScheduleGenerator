using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ScheduleCore.Models;

public class Parametro
{
    [Key]
    public int CodigoParametro { get; set; }
    public string Nombre { get; set; }
    public string Valor { get; set; }
    [Column(TypeName = "ENUM('CONFIGURACION', 'PRIORIDAD', 'COMPORTAMIENTO')")]
    public TipoParametro Tipo { get; set; }
    public string Descripcion { get; set; }
}

public enum TipoParametro {
    CONFIGURACION,
    PRIORIDAD,
    COMPORTAMIENTO
}