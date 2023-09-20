using Microsoft.AspNetCore.Mvc;
using ScheduleCore.Models;

namespace ScheduleApi.Controllers;

[ApiController]
[Route("scheduleAPI/[controller]")]
public class ParametroController : ControllerBase
{
    private readonly ScheduleDB context;
    
    public ParametroController(ScheduleDB context)
    {
        this.context = context;
    }
    
    [HttpGet(Name = "GetParametros")]
    public IEnumerable<Parametro> Get()
    {
        var parametros = context.Parametro.ToList();
        
        return parametros;
    }
}