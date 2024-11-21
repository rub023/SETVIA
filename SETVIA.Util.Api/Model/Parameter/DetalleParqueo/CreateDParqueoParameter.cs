using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.DetalleParqueo
{
    public class CreateDParqueoParameter
    {
        public int Id_Detalle { get; set; }
        public string Codigo_Parqueo { get; set; }
        public string Tipo_Vehiculo { get; set; }

        public string Placa { get; set; }
    }
}
