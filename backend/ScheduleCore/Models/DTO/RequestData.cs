namespace ScheduleCore.Models.DTO;

public class RequestData
{
    public List<CursoHorarioRequest> Horario { get; set; }
    public List<CursoAdvertencia> Advertencias { get; set; }
    public decimal PorcentajeEfectividad { get; set; }
    public string Comentario { get; set; }
    public string HoraInicio { get; set; }
    public string HoraFin { get; set; }
    public int DuracionPeriodo { get; set; }
}

public class CursoHorarioRequest
{
    public int CodigoHorario { get; set; }
    public int CodigoCurso { get; set; }
    public int CodigoCarrera { get; set; }
    public int CodigoCatedratico { get; set; }
    public int NoSalon { get; set; }
    public string HoraInicio { get; set; }
    public string HoraFin { get; set; }
}