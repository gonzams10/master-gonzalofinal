using proyecto_final_2_gonzalo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace proyecto_final_2_gonzalo.ADO
{
    internal class  ADOVenta
    {

        public List<Venta> TraerVentaPorUsuario(int usuarioId)
        {
         
            var listaVenta = new List<Venta>();



            SqlConnectionStringBuilder conecctionbuilder = new SqlConnectionStringBuilder();
            conecctionbuilder.DataSource = "DESKTOP-NGLHGI6\\BBDDGON";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand conect = new SqlCommand("select * from Venta where IdUsuario = user;", connection))
                {
                    connection.Open();

                    SqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = "Select  * from Venta where IdUsuario = @user;";
                    cmd.Parameters.Add(new SqlParameter("@user", usuarioId));
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Venta user = new Venta();
                        user.Id = Convert.ToInt32(reader.GetValue(0));
                        user.Comentarios = reader.GetValue(1).ToString();
                        user.IdUsuario = Convert.ToInt32(reader.GetValue(2).ToString());



                        listaVenta.Add(user);

                    }

                    Console.WriteLine("Ventas realizadas por el ID: {0}", usuarioId);
                    foreach (var venta in listaVenta)
                    {
                        Console.WriteLine("ID de venta: " + venta.Id);
                        Console.WriteLine("Comentarios:  " + venta.Comentarios);
                        Console.WriteLine("Id Del usuario: " + venta.IdUsuario);
                        Console.WriteLine("-------------------------------------");
                        break;

                    }





                    return listaVenta;




                }




            }


        }

    }
}
