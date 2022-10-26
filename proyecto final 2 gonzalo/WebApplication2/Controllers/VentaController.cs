using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Repositorio;

namespace WebApplication2.Controllers
{
    
        public class VentaController : ControllerBase
        {
            [HttpPost]
            public void CargarVenta([FromBody] VentaProducto vtas)
            {
                ADO_Venta.CargarVenta(vtas);
            }
            [HttpGet("GetVentas")]
            public List<Venta> Get()
            {
                return ADO_Venta.DevolverVenta();
            }

        }

    
}
