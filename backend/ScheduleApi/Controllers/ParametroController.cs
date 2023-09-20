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
    
    [HttpGet]
    public ActionResult<IEnumerable<Parametro>> Get()
    {
        var parametros = context.Parametro.ToList();
        
        return Ok(parametros);
    }

    [HttpPut("{codigo:int}")]
    public ActionResult<Parametro> Put(int codigo, [FromBody] Parametro parametro)
    {
        var param = context.Parametro.FirstOrDefault(p => p.CodigoParametro == codigo);

        if (param == null) return NotFound();
        
        param.Valor = parametro.Valor;
        
        context.SaveChanges();

        return Ok(param);
    }
}