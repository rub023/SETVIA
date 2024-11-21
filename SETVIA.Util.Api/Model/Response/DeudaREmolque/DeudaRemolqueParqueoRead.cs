using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.DeudaREmolque
{
    public class DeudaRemolqueParqueoRead
    {
        public string Codigo_Parqueo { get; set; }
        public string Direccion { get; set; }
        public string TipoVehiculo { get; set; }
        public string Placa { get; set; }
        public decimal Monto_Infraccion { get; set; }
        public decimal Servicio_Infraccion { get; set; }
        public decimal Total_InfraccionRemolcado { get; set; }
        public string NroTransaccion { get; set; }
        public TimeSpan Tiempo_InicioRemolcado { get; set; }
        
        public string Estado { get; set; }
    }
}
