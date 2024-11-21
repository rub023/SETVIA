using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.DetalleParqueo
{
    public class CreatePruebaUsoDetalleParameter
    {
        
            public int Id_Detalle { get; set; }
            public int Id_Parqueo { get; set; }
            public int Id_TipoVehiculo { get; set; }
            // public int Id_TipoPago { get; set; }
            public int Id_Tarifario { get; set; }
            public string Placa { get; set; }
            //public TimeSpan Tiempo_Ocupar { get; set; }
            //public TimeSpan Hora_Inicio { get; set; }
            //public TimeSpan Hora_Final { get; set; }
            //public decimal Costo_Parqueo { get; set; }
            public string Usuario_Creacion { get; set; }
            // public string NroTransaccion { get; set; }
        }
   
}
