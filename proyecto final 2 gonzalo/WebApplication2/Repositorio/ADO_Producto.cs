using System.Data.SqlClient;
using System.Data;
using WebApplication2.Model;

namespace WebApplication2.Repositorio
{

    public class ADO_Producto
    {
        public static List<Producto> DevolverProductos()
        {
            var listaProductos = new List<Producto>();
            string connectionString = Connection.traerConnection();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "SELECT * FROM producto";
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    var producto = new Producto();

                    producto.Id = Convert.ToInt64(reader2.GetValue(0));
                    producto.Descripciones = reader2.GetValue(1).ToString();
                    producto.Costo = Convert.ToDouble(reader2.GetValue(2).ToString());
                    producto.PrecioVenta = Convert.ToDouble(reader2.GetValue(3).ToString());
                    producto.Stock = Convert.ToInt32(reader2.GetValue(4).ToString());
                    producto.IdUsuario = Convert.ToInt32(reader2.GetValue(5).ToString());

                    listaProductos.Add(producto);

                }
                reader2.Close();
                connection.Close();

            }
            return listaProductos;
        }

        public static  List<Producto> TraerProducto(int idUsuario)
        {
            List<Producto> productos = new List<Producto>();

            string connectionString = Connection.traerConnection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, Descripciones, Costo, PrecioVenta, Stock, IdUsuario FROM Producto WHERE IdUsuario = @IdUsuario", conn);
                adapter.SelectCommand.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt)).Value = idUsuario;
                conn.Open();
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);

                foreach (DataRow dr in tabla.Rows)
                {
                    Producto producto = new Producto();
                    producto.Id = Convert.ToInt64(dr["Id"]);
                    producto.Descripciones = dr["Descripciones"].ToString();
                    producto.Costo = Convert.ToDouble(dr["Costo"].ToString());
                    producto.PrecioVenta = Convert.ToDouble(dr["PrecioVenta"].ToString());
                    producto.Stock = Convert.ToInt32(dr["Stock"].ToString());
                    producto.IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString());
                    productos.Add(producto);
                }

                conn.Close();
            }

            return productos;
        }


        public static long CrearProducto(Producto prod)
        {
            long id;
            string connectionString = Connection.traerConnection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Producto (Descripciones,Costo,PrecioVenta,Stock,IdUsuario) VALUES (@Descripciones,@Costo,@PrecioVenta,@Stock,@IdUsuario); Select scope_identity()", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("Descripciones", SqlDbType.NVarChar)).Value = prod.Descripciones;
                cmd.Parameters.Add(new SqlParameter("Costo", SqlDbType.Float)).Value = prod.Costo;
                cmd.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Float)).Value = prod.PrecioVenta;
                cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = prod.Stock;
                cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt)).Value = prod.IdUsuario;
                id = Convert.ToInt64(cmd.ExecuteScalar());
                conn.Close();
            }
            return id;

        }
        public static Int32 ModificarProducto(Producto prod)
        {
            int filas_modificadas;
            string connectionString = Connection.traerConnection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE dbo.Producto  SET Descripciones = @Descripciones ,Costo = @Costo ,PrecioVenta = @PrecioVenta, Stock = @Stock, IdUsuario = @idUsuario WHERE id = @id ", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt)).Value = prod.Id;
                cmd.Parameters.Add(new SqlParameter("Descripciones", SqlDbType.NVarChar)).Value = prod.Descripciones;
                cmd.Parameters.Add(new SqlParameter("Costo", SqlDbType.Float)).Value = prod.Costo;
                cmd.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Float)).Value = prod.PrecioVenta;
                cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = prod.Stock;
                cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt)).Value = prod.IdUsuario;
                filas_modificadas = Convert.ToInt32(cmd.ExecuteNonQuery());
                conn.Close();
            }
            return filas_modificadas;
        }
        public static Int32 EliminarProducto(long idProducto)

        {
            int filas_eliminadas;
            string connectionString = Connection.traerConnection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //Eliminar productos vendidos
                SqlCommand cmd = new SqlCommand("Delete FROM ProductoVendido WHERE Id = @idProducto", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.BigInt)).Value = idProducto;
                cmd.ExecuteNonQuery();

                //Eliminar Producto
                cmd = new SqlCommand("Delete FROM Producto WHERE Id = @idProducto", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.BigInt)).Value = idProducto;
                filas_eliminadas = Convert.ToInt32(cmd.ExecuteNonQuery());
                conn.Close();
            }
            return filas_eliminadas;
        }
    }




}
