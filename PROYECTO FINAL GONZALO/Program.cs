using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PROYECTO_FINAL_GONZALO.Producto;

namespace PROYECTO_FINAL_GONZALO
{
    class Program
    {
        static void Main(string[] args)
        {
            Venta venta = new Venta();
            venta.TraerVentaPorUsuario();



            ProductoVendido gonzalo3 = new ProductoVendido();
            gonzalo3.ProdutoVendidoByUsuario();

            Console.ReadKey();

           




             Usuario gonzalo1 = new Usuario();
            Producto gonzalo2 = new Producto();

            gonzalo1.IniciarSesion();

            Console.WriteLine("Ingrese el nombre del  usuario a traer datos");
            string usuarioATraer = Console.ReadLine();

            gonzalo2.TraerProductoByUsuario(1);
            
            

           
            gonzalo1.TraerUsuario(usuarioATraer);
            Console.ReadKey();

            


            
        }
    }

    class Usuario
    {
        public double Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Mail { get; set; }

        
        public  double CrearUsuario(Usuario usuario)
        {
            double IdUsuario = 0;

            string connectionString = @"Server=DESKTOP-NGLHGI6\BBDDGON;Database=SistemaGestion;Trusted_Connection=True;";

            using (SqlConnection conection = new SqlConnection(connectionString))
            {
                var query = @"Insert into Usuario (Nombre,Apellido,NombreUsuario,Contraseña,Mail)
                             Values(@Nombre,@Apellido,@NombreUsuario,@Contraseña,@Mail) ; select @@IDENTITY";


                conection.Open();

                using (SqlCommand comando = new SqlCommand (query,conection))
                {
                    comando.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                    comando.Parameters.Add(new SqlParameter("Apellido",SqlDbType.VarChar) { Value = usuario.Apellido });
                    comando.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    comando.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña });
                    comando.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.Contraseña });
                    IdUsuario = Convert.ToDouble(comando.ExecuteScalar());
                }

                conection.Close();


            }
            return IdUsuario;

        }

        public  List<Usuario> TraerUsuario(string usuarioATraer) 
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
                        if (user.NombreUsuario ==usuarioATraer)
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


        public void IniciarSesion() 
        {

            var Usuario = new Usuario();

            Console.WriteLine("Ingrese nombre de Usuario");
            string nombreIn1 = Console.ReadLine();
            Console.WriteLine("Ingrese Contraseña");
            string usuContra = Console.ReadLine();


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
                    cmd.Parameters.Add(new SqlParameter("@user", nombreIn1));
                    cmd.Parameters.Add(new SqlParameter("@cont", usuContra));
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

                        if (user.NombreUsuario == nombreIn1 && user.Contraseña == usuContra)
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

        }

  
    }
    class Producto
    {
     

        public double id { get; set; }
        public string descripciones { get; set; }
        public double costo { get; set; }
        public double precioVenta { get; set; }
        public int stock { get; set; }
        public int IdUsuario { get; set; }

        public Producto()
        {
            id = 0;
            descripciones = String.Empty;
            costo = 0;
            precioVenta = 0;
            stock = 0;
            IdUsuario = 0;
        }





        public  List<Producto> TraerProductoByUsuario(int usuarioId)
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


    class ProductoVendido
    {

        private int Id { get; set; }
        private int IdProducto { get; set; }
        private int Stock { get; set; }
        private int IdVenta { get; set; }

        

        
        public void  ProdutoVendidoByUsuario()
        {
            
            {

          
                var listaProducto = new List<Producto>();    

                Console.WriteLine("NOMBRE DE USUARIO");
                string usuarioATraer = Console.ReadLine();
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

                        Console.WriteLine("::: PRODUCTOS VENDIDOS POR EL USUARIO {0} :::",usuarioATraer);
                        
                        
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


    class Venta
    {

        private int Id { get; set; }
        private string Comentarios { get; set; }
        private int IdUsuario { get; set; }

        public List<Venta> TraerVentaPorUsuario()
        {
            Console.WriteLine("INGRESE EL ID Del usuario a buscar ventas");
            int usuarioId = int.Parse(Console.ReadLine());

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
                        Console.WriteLine("ID de venta: " +venta.Id);
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















