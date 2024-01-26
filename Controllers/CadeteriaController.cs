using Microsoft.AspNetCore.Mvc;
using Entidades;
using Microsoft.AspNetCore.Http.HttpResults;

namespace cadeteriaApi.Controllers;

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

    
    [HttpGet (Name="cadeteria")]
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

    // ● [Get] GetPedidos() => Retorna una lista de Pedidos
    [HttpGet]
    [Route("pedidos")]
    public ActionResult<IEnumerable<Pedido>> GetPedidos(){
        return Ok(cadeteria.ListadoPedidos);
    }
    // ● [Get] GetCadetes() => Retorna una lista de Cadetes
    [HttpGet]
    [Route("cadetes")]
    public ActionResult<IEnumerable<Cadete>> GetCadetes(){
        return Ok(cadeteria.Cadetes);
    }

// ● [Get] GetInforme() => Retorna un objeto Informe
    [HttpGet("GetInforme", Name = "GetInforme")]
    public ActionResult<string> GetInforme(){
        // var cadeteria = Cadeteria.GetInstancia();
        var informe = cadeteria.GetInforme();
        return Ok(informe);
    }

// ● [Post] AgregarPedido(Pedido pedido)

    [HttpPost]
    [Route("AddPedido")]
    public ActionResult<string> AgregarPedido(Pedido pedido){
        bool agregado = cadeteria.AgregarPedido(pedido);
        if (agregado)
        {
            return Ok("Pedido agregado con exito");        
        }
        else
        {
            return BadRequest("No se pudo agregar el pedido deseado");      
        }   
    }
// ● [Put] AsignarPedido(int idPedido, int idCadete)
    [HttpPut]
    [Route("AsignarPedido")]
    public ActionResult<bool> AsignarPedido(int nroPedido, int idCadete){
        if (cadeteria.BuscarPedido(nroPedido) != null)
        {
            if (cadeteria.BuscarCadetePorId(idCadete) != null)
            {
                cadeteria.AsignarCadeteAPedido(nroPedido, idCadete);
                return Ok("Pedido asignado con exito");
            }        
            else
            {
                return BadRequest("Cadete no encontrado");
            }
        }
        else
        {
            return BadRequest("Numero de pedido invalido");      
        }   
    }

// ● [Put] CambiarEstadoPedido(int idPedido,int NuevoEstado)

    [HttpPut]
    [Route("CambiarEstadoPedido")]
    public ActionResult<bool> CambiarEstadoPedido(int nroPedido, int nuevoEstado){
        // Pedido pedido = cadeteria.BuscarPedido(nroPedido);
        if (cadeteria.BuscarPedido(nroPedido) != null)
        {
            cadeteria.CambiarEstado(nuevoEstado,nroPedido);
            return Ok("Estado cambiado");
        }
        else
        {
            return BadRequest("Numero de pedido invalido");      
        }   
    }
// ● [Put] CambiarCadetePedido(int idPedido,int idNuevoCadete)
    [HttpPut]
    [Route ("CambiarCadetePedido")] 
    public ActionResult<Pedido> CambiarCadetePedido(int nroPedido, int idNuevoCadete){
        // Pedido pedido = cadeteria.BuscarPedido(nroPedido);
       if (cadeteria.BuscarPedido(nroPedido) != null)
        {
            if (cadeteria.BuscarCadetePorId(idNuevoCadete) != null)
            {
                cadeteria.ReasignarPedido(nroPedido, idNuevoCadete);
                return Ok("Pedido re-asignado con exito");
            }        
            else
            {
                return BadRequest("Cadete no encontrado");
            }
        }
        else
        {
            return BadRequest("Numero de pedido invalido");      
        }  
    }

}
