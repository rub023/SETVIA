using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.Tarifario
{
    public class TarifarioReadID
    {
        public int Id_Tarifario { get; set; } 
        public int Id_TipoVehiculo { get;  set; }
        public int Id_TipoTarifario { get; set; }
        public TimeSpan Tiempo { get; set; }
        public decimal CostoTarifario { get; set; }
        public string Estado { get; set; }
        public string Usuario_Creacion { get; set; }    
        public DateTime Fecha_Creacion { get; set; }
        public string Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
