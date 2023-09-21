namespace ScheduleCore.Models.ViewModels;

public class ResponseData
{
    public List<CursoHorarioDTO> Horario { get; set; }
    public List<CursoConAdvertencia> Advertencias { get; set; }
    public List<CarreraColor> Carreras { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public int DuracionPeriodo { get; set; }
}

public class CarreraColor
{
    public int CodigoCarrera { get; set; }
    public string Nombre { get; set; }
    public string Color { get; set; }
}