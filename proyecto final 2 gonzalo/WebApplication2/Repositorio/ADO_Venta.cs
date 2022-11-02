using System.Data.SqlClient;
using System.Data;
using WebApplication2.Model;

namespace WebApplication2.Repositorio
{
    public class ADO_Venta
    {
        public static void CargarVenta(VentaProducto vtaProductos)
        {
            long idVenta;
            string connectionString = Connection.traerConnection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //INSERT en tabla venta y obtener el id de la venta

                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Venta] (Comentarios, IdUsuario) VALUES (@Comentarios, @IdUsuario); Select scope_identity();", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.NVarChar)).Value = vtaProductos.Comentarios;
                cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt)).Value = vtaProductos.IdUsuario;
                idVenta = Convert.ToInt64(cmd.ExecuteScalar());

                //INSERT en tabla producto vendido con lista de productos enviados

                foreach (ProductoVendido producto in vtaProductos.Producto)
                {
                    //Agregar Venta
                    cmd = new SqlCommand("INSERT INTO ProductoVendido (Stock,IdProducto,IdVenta)  VALUES   (@Stock,@IdProducto,@IdVenta) ", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = producto.Stock;
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt)).Value = producto.IdProducto;
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt)).Value = idVenta;
                    cmd.ExecuteNonQuery();

                    //Actualizar Stock en Productos

                    cmd = new SqlCommand("UPDATE Producto SET Stock = Stock - @Stock WHERE idProducto = @IdProducto", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = producto.Stock;
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt)).Value = producto.IdProducto;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

        }
        public static List<Venta> TraerVentas(long idUsuario)
        {

            List<Venta> ventas = new List<Venta>();

            string connectionString = Connection.traerConnection();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, comentarios, IdUsuario From venta where IdUsuario = @IdUsuario", conn);
                adapter.SelectCommand.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt)).Value = idUsuario;
                conn.Open();
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                foreach (DataRow dr in tabla.Rows)
                {
                    Venta vta = new Venta();
                    vta.Id = Convert.ToInt32(dr["Id"]);
                    vta.Comentarios = dr["Comentarios"].ToString();
                    vta.IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString());
                    ventas.Add(vta);
                }

                conn.Close();
            }

            return ventas;
        }
        public static void EliminarVenta(long idVenta)
        {
            //Buscar Productos vendidos para 
            string connectionString = Connection.traerConnection();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "SELECT Id,Stock,IdProducto,IdVenta FROM ProductoVendido WHERE IdVenta = @idVenta";
                cmd2.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt)).Value = idVenta;
                var reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    //Actualizar Stock en Productos
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "UPDATE Producto SET Stock = Stock + @Stock WHERE id = @Id";
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = Convert.ToInt32(reader2.GetValue(1).ToString());
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt)).Value = Convert.ToInt64(reader2.GetValue(2));
                    cmd.ExecuteNonQuery();
                }
                reader2.Close();
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Borrar Productos Vendidos del idventa
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM ProductoVendido WHERE IdVenta = @idVenta";
                cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt)).Value = idVenta;
                cmd.ExecuteNonQuery();

                //borrar venta
                SqlCommand cmdVta = connection.CreateCommand();
                cmdVta.CommandText = "DELETE FROM Venta WHERE IdVenta = @idVenta";
                cmdVta.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt)).Value = idVenta;
                cmdVta.ExecuteNonQuery();
                connection.Close();
            }

        }

        public static List<VentaProductoDetalle> DevolverVentaProductos()
        {
            var ventas = new List<VentaProductoDetalle>();
            string connectionString = Connection.traerConnection();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "SELECT pv.IdVenta, pv.IdProducto, p.Descripciones,p.Costo, p.PrecioVenta as unitario, pv.Stock as cantidad, p.PrecioVenta * pv.Stock as total FROM ProductoVendido pv INNER JOIN Producto p on pv.IdProducto = p.Id";
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    var venta = new VentaProductoDetalle();
                    venta.IdVenta = Convert.ToInt32(reader2.GetValue("IdVenta"));
                    venta.IdProducto = Convert.ToInt32(reader2.GetValue("IdProducto"));
                    venta.Descripciones = reader2.GetValue("Descripciones").ToString();
                    venta.Costo = Convert.ToDouble(reader2.GetValue("Costo"));
                    venta.PrecioVenta = Convert.ToDouble(reader2.GetValue("unitario"));
                    venta.Stock = Convert.ToInt32(reader2.GetValue("cantidad"));
                    venta.Total = Convert.ToDouble(reader2.GetValue("total"));
                    ventas.Add(venta);

                }
                reader2.Close();
                connection.Close();

            }
            return ventas;
        }
    }
}
