using System.Text.Json;

namespace ScheduleCore.Models.DTO;

public class CursoHorarioDTO
{
    public int IdRegistro { get; set; }
    public int CodigoHorario { get; set; }
    public CursoDTO Curso { get; set; }
    public Catedratico Catedratico { get; set; }
    public Salon Salon { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }

    // Sobreescribir ToString() para que se muestren todos los atributos como un Json
    public override string ToString()
    {
        return $@"{{
                    ""CodigoHorario"": {CodigoHorario},
                    ""Curso"": {Curso.CodigoCurso},
                    ""Carrera"": {Curso.Carrera.CodigoCarrera},
                    ""Catedratico"": {Catedratico.CodigoCatedratico},
                    ""Salon"": {Salon.NoSalon},
                    ""HoraInicio"": ""{HoraInicio}"",
                    ""HoraFin"": ""{HoraFin}""
                }}";
    }
}