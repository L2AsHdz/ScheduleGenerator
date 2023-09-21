namespace ScheduleCore.Models.DTO;

public class CursoDTO
{
    public int CodigoCurso { get; set; }
    public string Nombre { get; set; }
    public int Semestre { get; set; }
    public Carrera Carrera { get; set; }
}