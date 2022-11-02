using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Repositorio;

namespace WebApiCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet("GetProductos")]
        public List<Producto> Get()
        {
            return ADO_Producto.DevolverProductos();
        }
        [HttpGet("GetProductosId")]
        public Producto Get([FromBody]int id)
        {
            return ADO_Producto.TraerProducto(id);
        }

        [HttpPost]
        public void Crear([FromBody] Producto prod)
        {
            ADO_Producto.CrearProducto(prod);
        }
        [HttpPut]
        public void Modificar([FromBody] Producto prod)
        {
            ADO_Producto.ModificarProducto(prod);
        }
        [HttpDelete]
        public void Eliminar([FromBody] long idProducto)
        {
            ADO_Producto.EliminarProducto(idProducto);
        }
    }
}