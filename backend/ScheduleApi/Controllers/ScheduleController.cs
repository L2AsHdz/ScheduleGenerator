using Microsoft.AspNetCore.Mvc;
using ScheduleCore.Models;
using ScheduleCore.Models.ViewModels;
using ScheduleCore.Services;

namespace ScheduleApi.Controllers;

[ApiController]
[Route("scheduleAPI/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly ScheduleDB context;
    private readonly ICoreService service;
    
    public ScheduleController(ScheduleDB context, ICoreService service)
    {
        this.context = context;
        this.service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CursoHorarioViewModel>> Init()
    {
        var horario = service.Execute();

        // if (!horario.Any()) return NoContent();
        
        return Ok(horario);
    }
    
}