using Microsoft.AspNetCore.Mvc;
using Entidades;

namespace APICadeteria.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private readonly ILogger<CadeteriaController> _logger;
    private Cadeteria cadeteria;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.GetInstancia();
    }

    // ● [Get] GetCadetes() => Retorna una lista de Cadetes

    [HttpGet]
    
    public ActionResult<string> CadeteriaNombre(){
        if (cadeteria != null)
        {
            return Ok(cadeteria.Nombre);
            
        }
        else
        {
            return NotFound();
        }
    } 

// ● [Get] GetInforme() => Retorna un objeto Informe
// ● [Post] AgregarPedido(Pedido pedido)
// ● [Put] AsignarPedido(int idPedido, int idCadete)
// ● [Put] CambiarEstadoPedido(int idPedido,int NuevoEstado)
// ● [Put] CambiarCadetePedido(int idPedido,int idNuevoCadete)

}
