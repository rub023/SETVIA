using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.DeudaInmovilizado
{
    public class ListadoInmovilizado
    {
        public int Id_Infraccion_Inmovilizado { get; set; }
        public int Id_Detalle { get; set; }
        public int Id_Costo_Infraccion { get; set; }
        public int Id_Parqueo { get; set; }
        public int Id_TipoPago { get; set; }
        public  string Placa { get; set; }  
        public decimal Monto_Infraccion { get; set; }   
        public decimal Servicio_Infraccion { get;set; }
        public decimal Total_InfraccionInmovilizado { get; set; }
        public TimeSpan Tiempo_InicioInmovilizado { get; set; }
        public string NroTransaccion { get; set; }
        public string Estado { get; set; }
        public string Usuario_Creacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public string Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
