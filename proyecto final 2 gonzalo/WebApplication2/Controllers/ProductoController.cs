using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Repositorio;

namespace WebApplication2.Controllers
{
    public class ProductoController : ControllerBase
    {

   

        [HttpGet(Name = "GetUsuarios")]
        public List<Producto> Get()
        {
            return ADO_Producto.DevolverProductos();

        }

        [HttpDelete]
        public void Eliminar([FromBody] int id)
        {
            ADO_Producto.EliminarProducto(id);

        }

        [HttpPut]
        public void Modificar([FromBody] Producto prod)
        {

            ADO_Producto.ModificarProducto(prod);

        }

        [HttpPost]
        public void CrearProducto([FromBody]Producto prod)
        {
            ADO_Producto.CrearProducto(prod);


        }

    }


}

