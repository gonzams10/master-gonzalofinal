using static WebApplication2.Controllers.UsuarioController;
using System.Data.SqlClient;
using WebApplication2.Model;
using System.Data;
using WebApplication2.Repositorio;

namespace WebApplication2.Repositorio
{

    public class ADO_Usuario
    {
        public static List<Usuario> DevolverUsuarios()
        {
            var listaUsuarios = new List<Usuario>();
            string connectionString = Connection.traerConnection();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "SELECT * FROM usuario";
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    var usuario = new Usuario();

                    usuario.Id = Convert.ToInt32(reader2.GetValue(0));
                    usuario.Nombre = reader2.GetValue(1).ToString();
                    usuario.Apellido = reader2.GetValue(2).ToString();
                    usuario.NombreUsuario = reader2.GetValue(3).ToString();
                    usuario.Contraseña = reader2.GetValue(4).ToString();
                    usuario.Mail = reader2.GetValue(5).ToString();

                    listaUsuarios.Add(usuario);

                }
                reader2.Close();
                connection.Close();

            }
            return listaUsuarios;


        }


        public static Usuario TraerUsuario(string nombreUsuario)
        {
            Usuario usuario = new Usuario();
            string connectionString = Connection.traerConnection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id ,Nombre ,Apellido ,NombreUsuario ,Contraseña ,Mail FROM Usuario WHERE NombreUsuario like @NombreUsuario", conn);
                adapter.SelectCommand.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar)).Value = nombreUsuario;
                conn.Open();
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    DataRow dr = tabla.Rows[0];
                    usuario.Id = Convert.ToInt32(dr["Id"]);
                    usuario.Apellido = dr["Apellido"].ToString();
                    usuario.Nombre = dr["Nombre"].ToString();
                    usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                    usuario.Contraseña = dr["Contraseña"].ToString();
                    usuario.Mail = dr["Mail"].ToString();
                }

                conn.Close();
            }
            return usuario;
        }
        public static long CrearUsuario(Usuario usu)

        {
            long id;
            string connectionString = Connection.traerConnection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Usuario(Nombre,Apellido,NombreUsuario,Contraseña,Mail) VALUES (@Nombre,@Apellido,@NombreUsuario,@Contraseña,@Mail); Select scope_identity()", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.NVarChar)).Value = usu.Nombre;
                cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.NVarChar)).Value = usu.Apellido;
                cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.NVarChar)).Value = usu.NombreUsuario;
                cmd.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.NVarChar)).Value = usu.Contraseña;
                cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.NVarChar)).Value = usu.Mail;
                id = Convert.ToInt64(cmd.ExecuteScalar());
                conn.Close();
            }
            return id;

        }
        public static int ModificarUsuario(Usuario usu)

        {
            if (usu.Nombre != string.Empty && usu.Apellido != string.Empty && usu.NombreUsuario != string.Empty && usu.Contraseña != string.Empty && usu.Mail != string.Empty)
            {
                int filas_modificadas;
                string connectionString = Connection.traerConnection();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Usuario SET  Nombre = @Nombre, Apellido = @Apellido , NombreUsuario = @NombreUsuario , Contraseña = @Contraseña , Mail = @Mail WHERE id = @id", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.NVarChar)).Value = usu.Id;
                    cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.NVarChar)).Value = usu.Nombre;
                    cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.NVarChar)).Value = usu.Apellido;
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.NVarChar)).Value = usu.NombreUsuario;
                    cmd.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.NVarChar)).Value = usu.Contraseña;
                    cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.NVarChar)).Value = usu.Mail;
                    filas_modificadas = Convert.ToInt32(cmd.ExecuteNonQuery());
                    conn.Close();
                }
                return filas_modificadas;
            }
            else
            {
                return 0; // Si alguno de los datos esta vacio
            }
        }
        public static int EliminarUsuario(long idUsuario)

        {
            int filas_eliminadas;
            string connectionString = Connection.traerConnection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Delete FROM Usuario WHERE Id = @IdUsuario", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt)).Value = idUsuario;
                filas_eliminadas = Convert.ToInt32(cmd.ExecuteNonQuery());
                conn.Close();
            }
            return filas_eliminadas;
        }
        public static Usuario InicioSesion(string nomUsuario, string pass)
        {
            Usuario usuario = new Usuario();
            string connectionString = Connection.traerConnection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id ,Nombre ,Apellido ,NombreUsuario ,Contraseña ,Mail FROM Usuario WHERE NombreUsuario like @NombreUsuario and  Contraseña like @password", conn);
                adapter.SelectCommand.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar)).Value = nomUsuario;
                adapter.SelectCommand.Parameters.Add(new SqlParameter("password", SqlDbType.VarChar)).Value = pass;
                conn.Open();
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    DataRow dr = tabla.Rows[0];
                    usuario.Id = Convert.ToInt32(dr["Id"]);
                    usuario.Apellido = dr["Apellido"].ToString();
                    usuario.Nombre = dr["Nombre"].ToString();
                    usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                    usuario.Contraseña = dr["Contraseña"].ToString();
                    usuario.Mail = dr["Mail"].ToString();
                }

                conn.Close();
            }
            return usuario;

        }
    }

}
