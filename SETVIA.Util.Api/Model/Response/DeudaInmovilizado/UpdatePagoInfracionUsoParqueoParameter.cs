using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.DeudaInmovilizado
{
    public class UpdatePagoInfracionUsoParqueoParameter
    {
        public string Placa { get; set; }
        public string NroTransaccion { get; set; }
        public string Codigo_Parqueo { get; set; }
        public List<DeudaInfraccion> ListaDeudas { get; set; }
    }

    public class DeudaInfraccion
    {
        public int Id { get; set; }
        public int Id_Detalle { get; set; }
        public int Id_CostoInfraccion { get; set; }
        public decimal Montoinfraccion { get; set; } // Otros campos relevantes }
        public decimal ServicioInfraccion { get; set; }
        public decimal TotalInfraccion { get; set; }
        public string Estado { get; set; }
        public TimeSpan TiempoInicioInfraccion { get; set; }
        
    }
}

