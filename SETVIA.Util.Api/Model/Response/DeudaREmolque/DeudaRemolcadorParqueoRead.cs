using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.DeudaREmolque
{
    public class DeudaRemolcadorParqueoRead
    {
        public string Direccion { get; set; }
        public string TipoVehiculo { get; set; }
        public string Placa { get; set; }

        public int Id_Detalle { get; set; }

        public List<ListadoRemolques> ListaDeudas { get; set; }
    }
}
