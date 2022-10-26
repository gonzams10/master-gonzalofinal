using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Repositorio;

namespace WebApplication2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        [HttpGet("GetUsuarios")]
        public List<Usuario> Get()
        {
            return ADO_Usuario.DevolverUsuarios();
        }

        [HttpGet("GetUsuariosId")]
        public Usuario Get(Int32 id)
        {
            return ADO_Usuario.TraerUsuarioId(id);
        }

        [HttpGet("GetUsuariosNombre")]
        public Usuario Get(String nombre)
        {
            return ADO_Usuario.TraerUsuarioNombre(nombre);
        }

        [HttpGet("GetInicioSesion")]
        public Usuario Get(String nombre, String contraseña)
        {
            return ADO_Usuario.InicioSesion(nombre, contraseña);
        }

        [HttpPost]
        public long Crear([FromBody] Usuario usu)
        {
            return ADO_Usuario.CrearUsuario(usu);

        }

        [HttpPut]
        public long Actualizar([FromBody] Usuario usu)
        {
            return ADO_Usuario.ModificarUsuario(usu);
        }

        [HttpDelete]
        public long Eliminar([FromBody] long idUsuario)
        {
            return ADO_Usuario.EliminarUsuario(idUsuario);
        }



    }
}


