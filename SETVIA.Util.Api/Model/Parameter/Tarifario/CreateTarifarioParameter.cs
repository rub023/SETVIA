using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.Tarifario
{
    public class CreateTarifarioParameter
    {

        public int Id_Tarifario { get; set; }
        public int Id_TipoVehiculo { get; set; }
        public int Id_TipoTarifario { get; set; }
        public TimeSpan Tiempo { get; set; }
        public decimal CostoTarifario { get; set; }
        public string Usuario_Creacion { get; set; }
    }
}
