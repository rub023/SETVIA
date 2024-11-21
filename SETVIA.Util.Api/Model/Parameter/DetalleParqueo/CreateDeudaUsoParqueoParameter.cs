using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.DetalleParqueo
{
    public  class CreateDeudaUsoParqueoParameter
    {
        public int Id_Detalle { get; set; }
        public int Id_Parqueo { get; set; }
        public int Id_TipoVehiculo { get; set; }
       // public int Id_Tarifario { get; set; }
        public string Placa { get; set; }
        public string @Usuario_Creacion { get; set; }
        public string Codigo_Pago { get; set; }
    }
}
