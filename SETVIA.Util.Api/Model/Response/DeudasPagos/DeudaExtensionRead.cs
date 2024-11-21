using SETVIA.Util.Api.Model.Response.Tarifario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.DeudasPagos
{
    public class DeudaExtensionRead
    {
        public string Direccion { get; set; }
        public string TipoVehiculo { get; set; }
        public string Placa { get; set; }

        public int Id_TipoVehiculo { get; set; }
        public List<ListadoTarifario> ListaDeudas { get; set; }
        //public TimeSpan Tiempo_Extension { get; set; }
        //public decimal Costo_Extension { get; set; }
        //public string NroTransaccion { get; set; }
        //public string Estado { get; set; }
    }
}
