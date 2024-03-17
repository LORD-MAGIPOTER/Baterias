using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baterias.NetMaui.Modelo
{
    public class Producto
    {
        public int PilaId { get; set; }
        public string Nombre { get; set; }
        public string Presentacion { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public double Precio { get; set; }
        public string ImagenPath { get; set; }
    }
}
