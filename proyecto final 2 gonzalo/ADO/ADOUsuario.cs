using Microsoft.Win32;
using proyecto_final_2_gonzalo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_final_2_gonzalo.ADO
{
    internal class ADOUsuario
    {

        public List<Usuario> TraerUsuario(string usuarioATraer)
        {

            var listaUsuario = new List<Usuario>();



            SqlConnectionStringBuilder conecctionbuilder = new SqlConnectionStringBuilder();
            conecctionbuilder.DataSource = "DESKTOP-NGLHGI6\\BBDDGON";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand conect = new SqlCommand("Select * from  Usuario where NombreUsuario = user;", connection))
                {
                    connection.Open();

                    SqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = "Select  * from Usuario where NombreUsuario = @user;";
                    cmd.Parameters.Add(new SqlParameter("@user", usuarioATraer));
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Usuario user = new Usuario();
                        user.Id = Convert.ToInt32(reader.GetValue(0));
                        user.Nombre = reader.GetValue(1).ToString();
                        user.Apellido = reader.GetValue(2).ToString();
                        user.NombreUsuario = reader.GetValue(3).ToString();
                        user.Contraseña = reader.GetValue(4).ToString();
                        user.Mail = reader.GetValue(5).ToString();

                        listaUsuario.Add(user);

                    }

                    Console.WriteLine(":::USUARIO:::");
                    foreach (var user in listaUsuario)
                    {
                        if (user.NombreUsuario == usuarioATraer)
                        {
                            Console.WriteLine("NombreUsuario = " + user.NombreUsuario);
                            Console.WriteLine("Id = " + user.Id);
                            Console.WriteLine("nombre = " + user.Nombre);
                            Console.WriteLine("Mail = " + user.Mail);
                            Console.WriteLine("Apellido = " + user.Apellido);
                            Console.WriteLine("Contraseña = " + user.Contraseña);
                        }
                        else
                        {
                            Console.WriteLine("NOMBRE DE USUARIO SIN DATOS");
                        }
                        reader.Close();
                        connection.Close();

                    }

                    return listaUsuario;




                }




            }


        }

        public Usuario IniciarSesion(string nombreUsuario, string contraseña)
        {

           
            Usuario user = new Usuario();

            SqlConnectionStringBuilder conecctionbuilder = new SqlConnectionStringBuilder();
            conecctionbuilder.DataSource = "DESKTOP-NGLHGI6\\BBDDGON";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand conect = new SqlCommand("Select * from  Usuario where NombreUsuario = user AND Contraseña = cont;", connection))
                {
                    connection.Open();

                    SqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = "Select  * from Usuario where NombreUsuario = @user;";
                    cmd.CommandText = "Select  * from Usuario where Contraseña = @cont;";
                    cmd.Parameters.Add(new SqlParameter("@user", nombreUsuario));
                    cmd.Parameters.Add(new SqlParameter("@cont", contraseña));
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                       
                        user.Id = Convert.ToInt32(reader.GetValue(0));
                        user.Nombre = reader.GetValue(1).ToString();
                        user.Apellido = reader.GetValue(2).ToString();
                        user.NombreUsuario = reader.GetValue(3).ToString();
                        user.Contraseña = reader.GetValue(4).ToString();
                        user.Mail = reader.GetValue(5).ToString();

                        if (user.NombreUsuario == nombreUsuario && user.Contraseña == contraseña)
                        {
                            Console.WriteLine(":::ACCESO EXITOSO:::");
                            Console.WriteLine("NombreUsuario = " + user.NombreUsuario);
                            Console.WriteLine("Id = " + user.Id);
                            Console.WriteLine("nombre = " + user.Nombre);
                            Console.WriteLine("Mail = " + user.Mail);
                            Console.WriteLine("Apellido = " + user.Apellido);
                            Console.WriteLine("Contraseña = " + user.Contraseña);
                        }




                        break;
                    }
                    Console.WriteLine("NOMBRE DE USUARIO O CONTRASEÑA NO COINCIDEN O NO EXISTEN");

                    

                    reader.Close();
                    connection.Close();

                }
                
            }
            return user;

        }


    }
}
