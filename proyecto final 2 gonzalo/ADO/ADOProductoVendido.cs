using proyecto_final_2_gonzalo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_final_2_gonzalo.ADO
{
    internal class ADOProductoVendido
    {
        public void ProdutoVendidoByUsuario(string usuarioATraer)
        {

            {


                var listaProducto = new List<Producto>();

    
                SqlConnectionStringBuilder conecctionbuilder = new SqlConnectionStringBuilder();
                conecctionbuilder.DataSource = "DESKTOP-NGLHGI6\\BBDDGON";
                conecctionbuilder.InitialCatalog = "SistemaGestion";
                conecctionbuilder.IntegratedSecurity = true;
                var cs = conecctionbuilder.ConnectionString;

                using (SqlConnection connection = new SqlConnection(cs))
                {
                    using (SqlCommand conect = new SqlCommand("SELECT pr.* FROM Producto pr INNER JOIN ProductoVendido pv ON pr.Id = pv.IdProducto INNER JOIN Venta vnt ON vnt.Id = pv.IdVenta INNER JOIN Usuario us ON Us.Id = vnt.IdUsuario WHERE Us.NombreUsuario = @user;", connection))
                    {
                        connection.Open();

                        SqlCommand cmd = connection.CreateCommand();

                        cmd.CommandText = "SELECT pr.* FROM Producto pr INNER JOIN ProductoVendido pv ON pr.Id = pv.IdProducto INNER JOIN Venta vnt ON vnt.Id = pv.IdVenta INNER JOIN Usuario us ON Us.Id = vnt.IdUsuario WHERE Us.NombreUsuario = @user;";
                        cmd.Parameters.Add(new SqlParameter("@user", usuarioATraer));
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {

                            Producto pr = new Producto();


                            pr.id = Convert.ToInt32(reader.GetValue(0));
                            pr.descripciones = reader.GetValue(1).ToString();
                            pr.costo = Convert.ToDouble(reader.GetValue(2));
                            pr.precioVenta = Convert.ToDouble(reader.GetValue(3));
                            pr.stock = Convert.ToInt32(reader.GetValue(4));
                            pr.IdUsuario = Convert.ToInt32(reader.GetValue(5));




                            listaProducto.Add(pr);

                        }

                        Console.WriteLine("::: PRODUCTOS VENDIDOS POR EL USUARIO {0} :::", usuarioATraer);


                        foreach (var producto in listaProducto)
                        {

                            Console.WriteLine("Id Del producto = " + producto.id + " PRODUCTO: " + producto.descripciones + " COSTO: " + producto.costo + " Precio De Venta: " + producto.precioVenta);
                            reader.Close();
                            connection.Close();
                        }

                    }

                }



            }











        }


    }
}
