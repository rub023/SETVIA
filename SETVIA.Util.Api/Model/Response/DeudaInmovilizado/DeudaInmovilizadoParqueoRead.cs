using SETVIA.Util.Api.Model.Response.Tarifario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.DeudaInmovilizado
{
    public class DeudaInmovilizadoParqueoRead
    {
        public string Direccion { get; set; }
        public string TipoVehiculo { get; set; }
        public string Placa { get; set; }

        public int Id_Detalle { get; set; }

        public List<ListadoInmovilizado> ListaDeudas { get; set; }
        //public string Codigo_Parqueo { get; set; }
        //public string Direccion { get; set; }
        //public string TipoVehiculo { get; set; }
        //public string Placa { get; set; }
        //public decimal Monto_Infraccion { get; set; }
        //public decimal Servicio_Infraccion { get; set; }
        //public decimal Total_InfraccionInmovilizado { get; set; }      
        //public string NroTransaccion { get; set; }
        //public TimeSpan Tiempo_InicioInmovilizado { get; set; }
        //public string Estado { get; set; }
    }
}
