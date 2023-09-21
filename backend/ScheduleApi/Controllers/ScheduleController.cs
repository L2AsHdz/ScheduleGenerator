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
    public ActionResult<RequestData> Save([FromBody] RequestData data)
    {
        data.Horario.ForEach(h => context.CursoHorario.Add(new CursoHorario()
        {
            CodigoHorario = h.CodigoHorario,
            CodigoCurso = h.CodigoCurso,
            CodigoCatedratico = h.CodigoCatedratico,
            CodigoCarrera = h.CodigoCarrera,
            NoSalon = h.NoSalon,
            HoraInicio = TimeOnly.Parse(h.HoraInicio),
            HoraFin = TimeOnly.Parse(h.HoraFin)
            
        }));
        
        data.Advertencias.ForEach(a => context.CursoAdvertencia.Add(a));
        context.SaveChanges();
        
        if (data is null)
        {
            Console.WriteLine(("BadRequest"));
            return BadRequest("Los datos no son válidos.");
        }
        
        
        Console.WriteLine("Save");
        
        return Ok(data);
    }
    
}