using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_final_2_gonzalo.Models
{
    internal class Producto
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



    }
}
