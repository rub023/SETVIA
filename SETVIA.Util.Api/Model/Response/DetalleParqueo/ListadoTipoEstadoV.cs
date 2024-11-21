using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.DetalleParqueo
{
    public class ListadoTipoEstadoV
    {
        public int Id_Detalle {get; set;}
        public int Id_Parqueo { get; set;} 
        public int Id_TipoVehiculo {get; set;}  
        public string Placa { get; set; }
        public TimeSpan Tiempo_Ocupar { get; set; }
        public TimeSpan Hora_Inicio { get; set; }
        public TimeSpan Hora_Final { get; set; }
        public decimal Costo_Parqueo { get; set; }
        public string Estado_Vehiculo { get; set; }
        public string Usuario_Creacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        virtual public string Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int Id_TipoPago { get; set; }
        public int Id_Tarifario { get; set; }
        public string NroTransaccion { get; set; }
        public string Estado_Pago { get; set; }
    }
}
