using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.PagoInmovil
{
    public class PagoInmovilizadoParameter
    {
     
      //  public int Id_Infraccion_Inmovilizado { get; set; }
        public string Placa { get; set; }
        public string NroTransaccionPago { get; set; }

        public decimal Monto_Infraccion { get; set; }

        public decimal Servicio_Infraccion { get; set; }

        public decimal Total_InfraccionInmovilizado { get; set; }
        public string NIT { get; set; }
        public string Razon_Social { get; set; }
        public string Correo { get; set; }
        public string CUF { get; set; }
    }
}
