using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repositorio;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaProductoDetalleController : ControllerBase
    {
        [HttpGet("TraerVentas")]
        public List<VentaProductoDetalle> Get()
        {
            return ADO_Venta.DevolverVentaProductos();
        }
    }
}
