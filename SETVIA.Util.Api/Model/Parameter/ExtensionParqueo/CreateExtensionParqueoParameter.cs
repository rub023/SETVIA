using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.ExtensionParqueo
{
    public class CreateExtensionParqueoParameter
    {

        public int Id_Extension { get; set; }
        public string Codigo_Parqueo { get; set; }
        public string Tipo_Vehiculo { get; set; }
        public string Placa { get; set; }
        //public int Id_Extension { get; set; }
        //public int Id_Detalle { get; set; }
        //public int Id_Tarifario { get; set; }
        //public string NroTransaccion { get; set; }
        //public string Usuario_Creacion { get; set; }

    }
}
