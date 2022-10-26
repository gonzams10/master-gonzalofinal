using System.Data.SqlClient;
using WebApplication2.Model;

namespace WebApplication2.Repositorio
{
    public class ADO_ProductoVendido
    {
        public static List<ProductoVendido> DevolverProductosVendidos()
        {
            var listaProductos = new List<ProductoVendido>();
            string connectionString = Connection.traerConnection();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "SELECT Id,Stock,IdProducto,IdVenta FROM dbo.ProductoVendido";
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    var producto = new ProductoVendido();
                    producto.Id = Convert.ToInt32(reader2.GetValue(0));
                    producto.Stock = Convert.ToInt32(reader2.GetValue(1).ToString());
                    producto.IdProducto = Convert.ToInt32(reader2.GetValue(2));
                    producto.IdVenta = Convert.ToInt32(reader2.GetValue(3));

                    listaProductos.Add(producto);

                }
                reader2.Close();
                connection.Close();

            }
            return listaProductos;
        }
    }
}
