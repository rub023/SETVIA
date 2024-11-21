using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.DetalleParqueo
{
    public class PlacaRead
    {
        public int Id_Detalle { get; set; }
         public int Id_Parqueo { get; set; }
        public int Id_TipoVehiculo { get; set; }
        public string Placa { get; set; }
        public string Tiempo_Ocupar { get; set; }
        public decimal Costo_Parqueo { get; set; }
        public int Id_TipoPago { get; set; }
        public int Id_Tarifario { get; set; }
        public string Codigo_Pago { get; set; }
        public string Estado_Pago { get; set; }
    }
}
