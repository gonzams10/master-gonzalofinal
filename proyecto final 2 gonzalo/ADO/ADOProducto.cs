using proyecto_final_2_gonzalo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_final_2_gonzalo.ADO
{
    internal class ADOProducto
    {

        public List<Producto> TraerProductoByUsuario(int usuarioId)
        {

            var listaProducto = new List<Producto>();

            Console.WriteLine("Ingrese el ID del USUARIO");
            usuarioId = int.Parse(Console.ReadLine());

            SqlConnectionStringBuilder conecctionbuilder = new SqlConnectionStringBuilder();
            conecctionbuilder.DataSource = "DESKTOP-NGLHGI6\\BBDDGON";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand conect = new SqlCommand("SELECT * FROM Poducto WHERE IdUsuario = @usuarioId;", connection))
                {
                    connection.Open();

                    SqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = "SELECT * FROM Producto WHERE IdUsuario = @usuarioId;";
                    cmd.Parameters.Add(new SqlParameter("@usuarioId", usuarioId));
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto producto = new Producto();

                        producto.id = Convert.ToInt32(reader.GetValue(0));
                        producto.descripciones = reader.GetValue(1).ToString();
                        producto.costo = Convert.ToInt32(reader.GetValue(2));
                        producto.precioVenta = Convert.ToInt32(reader.GetValue(3));
                        producto.stock = Convert.ToInt32(reader.GetValue(4));
                        producto.IdUsuario = Convert.ToInt32(reader.GetValue(5));




                        listaProducto.Add(producto);

                    }



                    foreach (var producto in listaProducto)
                    {
                        Console.WriteLine(":::PRODUCTOS:::");
                        if (producto.IdUsuario == usuarioId)
                        {
                            Console.WriteLine("id = " + producto.id);
                            Console.WriteLine("descripciones = " + producto.descripciones);
                            Console.WriteLine("IdUsuario = " + producto.IdUsuario);
                            Console.WriteLine("precioVenta = " + producto.precioVenta);
                            Console.WriteLine("stock = " + producto.stock);
                            Console.WriteLine("costo = " + producto.stock);
                        }
                        else
                        {
                            Console.WriteLine("NOMBRE DE USUARIO SIN DATOS");
                        }
                        reader.Close();
                        connection.Close();

                    }

                    return listaProducto;




                }




            }




        }


    }
}
