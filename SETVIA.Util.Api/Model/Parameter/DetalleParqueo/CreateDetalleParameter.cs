using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.DetalleParqueo
{
    public class CreateDetalleParameter
    {
        public string  Codigo_Via{ get; set; }
        public string TipoVehiculo { get; set; }
        // public int Id_TipoPago { get; set; }
        
        public string Placa { get; set; }
    }
}
