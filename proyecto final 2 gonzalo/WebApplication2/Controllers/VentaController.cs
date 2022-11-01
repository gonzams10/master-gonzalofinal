using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Repositorio;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpPost("CargarVenta")]
        public void CargarVenta([FromBody] VentaProducto vtas)
        {
            ADO_Venta.CargarVenta(vtas);
        }
        [HttpGet("GetVentas")]
        public List<Venta> Get()
        {
            return ADO_Venta.DevolverVenta();
        }


        [HttpDelete("EliminarVenta")]
        public void EliminarVenta([FromBody] long idVenta)
        {
            ADO_Venta.EliminarVenta(idVenta);
        }

    }

}
