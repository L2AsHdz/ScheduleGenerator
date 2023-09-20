using Microsoft.AspNetCore.Mvc;
using ScheduleCore.Models;
using ScheduleCore.Models.ViewModels;
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
    public ActionResult<IEnumerable<CursoHorarioDTO>> Init()
    {
        var horario = service.Execute();

        // if (!horario.Any()) return NoContent();
        
        return Ok(horario);
    }
    
}