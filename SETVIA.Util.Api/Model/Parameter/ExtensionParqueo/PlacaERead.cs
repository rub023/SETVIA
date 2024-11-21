using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.ExtensionParqueo
{
    public class PlacaERead
    {
        public int Id_Extension { get; set; }
        public int Id_Detalle { get; set; }
        public int Id_Tarifario { get; set; }
        public TimeSpan Tiempo_Extension { get; set; }
        public decimal Costo_Extension { get; set; }
        public string Codigo_Pago { get; set; }
        public string Estado { get; set; }
        public string Usuario_Creacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public int Id_TipoPago { get; set; }
    }
}
