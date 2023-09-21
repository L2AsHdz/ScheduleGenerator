using Microsoft.AspNetCore.Mvc;
using ScheduleCore.Models;
using ScheduleCore.Models.DTO;
using ScheduleCore.Services.Core;

namespace ScheduleApi.Controllers;

[ApiController]
[Route("scheduleAPI/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly ScheduleDB context;
    private readonly CoreService service;
    
    public ScheduleController(ScheduleDB context, CoreService service)
    {
        this.context = context;
        this.service = service;
    }

    [HttpGet]
    public ActionResult<ResponseData> Init()
    {
        var horario = service.Execute();

        // if (!horario.Any()) return NoContent();
        
        return Ok(horario);
    }
    
    [HttpPost]
    public ActionResult<Horario> Save([FromBody] RequestData data)
    {
        var horario = new Horario()
        {
            PorcentajeEfectividad = data.PorcentajeEfectividad,
            FechaCreacion = DateTime.Now,
            Comentario = data.Comentario,
            HoraFin = TimeOnly.Parse(data.HoraFin),
            HoraInicio = TimeOnly.Parse(data.HoraInicio),
            DuracionPeriodo = data.DuracionPeriodo
        };
        
        context.Horario.Add(horario);
        context.SaveChanges();
        
        data.Horario.ForEach(h => context.CursoHorario.Add(new CursoHorario()
        {
            CodigoHorario = horario.CodigoHorario,
            CodigoCurso = h.CodigoCurso,
            CodigoCatedratico = h.CodigoCatedratico,
            CodigoCarrera = h.CodigoCarrera,
            NoSalon = h.NoSalon,
            HoraInicio = TimeOnly.Parse(h.HoraInicio),
            HoraFin = TimeOnly.Parse(h.HoraFin)
        }));
        
        data.Advertencias.ForEach(a => new CursoAdvertencia()
        {
            CodigoHorario = horario.CodigoHorario,
            CodigoCurso = a.CodigoCurso,
            Nombre = a.Nombre,
            Advertencia = a.Advertencia
        });
        context.SaveChanges();
        
        if (data is null)
        {
            Console.WriteLine(("BadRequest"));
            return BadRequest("Los datos no son válidos.");
        }
        
        return Ok(horario);
    }
    
    [HttpGet("{id}")]
    public ActionResult<ResponseData> Get(int id)
    {
        var advertencias = context.CursoAdvertencia.Where(c => c.CodigoHorario == id).ToList();
        var carreras = context.Carrera.ToList();
        var cursos = context.Curso.ToList();
        var cursosCarrera = context.CursoCarrera.ToList();
        var catedraticos = context.Catedratico.ToList();
        var salones = context.Salon.ToList();
        var horario = context.Horario.FirstOrDefault(h => h.CodigoHorario == id);
        var cursosHorario = context.CursoHorario.Where(ch => ch.CodigoHorario == id).ToList();

        var cursosHorarioDto = cursosHorario.Select(h => new CursoHorarioDTO()
        {
            CodigoHorario = h.CodigoHorario,
            Curso = new CursoDTO()
            {
                CodigoCurso = h.CodigoCurso,
                Nombre = cursos.Find(c2 => h.CodigoCurso == c2.CodigoCurso).Nombre,
                Semestre = cursosCarrera
                    .Find(cc => cc.CodigoCarrera == h.CodigoCarrera && cc.CodigoCurso == h.CodigoCurso).Semestre!,
                Carrera = carreras.Find(carrera => carrera.CodigoCarrera == h.CodigoCarrera)!
            },
            Catedratico = catedraticos.Find(cat => cat.CodigoCatedratico == h.CodigoCatedratico)!,
            Salon = salones.Find(s => s.NoSalon == h.NoSalon)!,
            HoraInicio = h.HoraInicio,
            HoraFin = h.HoraFin
        }).ToList();

        if (cursosHorario.Count <= 0)
        {
            Console.WriteLine(("NotFound"));
            return NotFound($"No se encontró el horario con el código {id}.");
        }

        var response = new ResponseData()
        {
            Horario = cursosHorarioDto,
            Advertencias = advertencias,
            Carreras = carreras.Select(c => new CarreraColor()
            {
                CodigoCarrera = c.CodigoCarrera,
                Nombre = c.Nombre,
                Color = c.Color
            }).ToList(),
            Salones = salones,
            HoraInicio = horario.HoraInicio,
            HoraFin = horario.HoraFin,
            DuracionPeriodo = horario.DuracionPeriodo,
            PorcentajeEfectividad = horario.PorcentajeEfectividad
        };
        
        return Ok(response);
    }
    
}