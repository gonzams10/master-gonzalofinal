using System.Data;
using System.Data.SqlClient;
using WebApplication2.Model;
using WebApplication2.Repositorio;
using WebApplication2.Controllers;

namespace WebApplication2.Repositorio
{
    public class ADO_ProductoVendido
    {


        public static List<ProductoVendido> TraerProductoVendidos(int idUsuario)
        {

            string connectionString = Connection.traerConnection();
            
            List<Producto> productos = ADO_Producto.TraerProducto(idUsuario);
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
            foreach (Producto prod in productos)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT pv.id as IdProductoVendido, pv.IdProducto, pv.idVenta, pv.Stock as cantidad FROM ProductoVendido pv WHERE pv.IdProducto = @idProducto", conn);
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("idProducto", SqlDbType.BigInt)).Value = prod.Id;
                    conn.Open();
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);
                    foreach (DataRow dr in tabla.Rows)
                    {
                        ProductoVendido prdVendido = new ProductoVendido();
                        prdVendido.Id = Convert.ToInt32(dr["IdProductoVendido"]);
                        prdVendido.IdProducto = Convert.ToInt32(dr["IdProducto"]);
                        prdVendido.IdVenta = Convert.ToInt32(dr["IdVenta"]);
                        prdVendido.Stock = Convert.ToInt32(dr["cantidad"].ToString());
                        productosVendidos.Add(prdVendido);

                    }
                }
            }
            return productosVendidos;
        }
    }
}
