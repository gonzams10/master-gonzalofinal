using proyecto_final_2_gonzalo.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_final_2_gonzalo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ADOUsuario user = new ADO.ADOUsuario();
            user.TraerUsuario("tcasazza");
            Console.ReadKey();
            Console.Clear();

            ADOProducto producto = new ADOProducto();
            producto.TraerProductoByUsuario(1);
            Console.ReadKey();
            Console.Clear();

            ADOProductoVendido productoVendido = new ADOProductoVendido();
            productoVendido.ProdutoVendidoByUsuario("tcasazza");
            Console.ReadKey();
            Console.Clear();

            ADOVenta venta = new ADOVenta();
            venta.TraerVentaPorUsuario(1);
            Console.ReadKey();


        }
    }
}
