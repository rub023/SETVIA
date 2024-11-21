using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.DEudaPAgos
{
    public class UpdatePagoDeudaUsoParqueoParameter
    {
        //public int Id_Detalle { get; set; }
        public string  Codigo_Parqueo { get; set; }
        public string Tipo_Vehiculo { get; set; }
        public string Placa { get; set; }
        public TimeSpan Tiempo { get; set; }
        public decimal CostoTarifario { get; set; }
      //  public string NroTransaccionPago { get; set; }
        public string NIT { get; set; }
        public string Razon_Social { get; set; }
        public string Correo { get; set; }
        public string CUF { get; set; }
   
    }

    //public class Deuda
    //{
    //    public int Id { get; set; }
    //    public decimal Monto { get; set; }
    //    //public string Moneda { get; set; }  
    //    //public int Id_Detalle { get; set; }
    //    //public int Id_Parqueo { get; set; }
    //    //public int Id_TipoVehiculo { get; set; }
    //    //public int Id_TipoPago { get; set; }
    //    //public int Id_Tarifario { get; set; }
    //    //public string Placa { get; set; }
    //    //public TimeSpan Tiempo_Ocupar { get; set; }

    //    //public TimeSpan Hora_Inicio { get; set; }
    //    //public TimeSpan Hora_Final { get; set; }
    //    //public decimal Costo_Parqueo { get; set; } // Otros campos relevantes } 
    //    //public string Estado_Vehiculo { get; set; }
    //    //public string NroTransaccion { get; set; }
    //    //public string Estado_Pago { get; set; }

    //}

    //public class Facturacion
    //{
    //    public int NIT { get; set; }
    //    public string  RazonSocial { get; set; } // Otros campos relevantes }
    //    public string Correo { get; set; }
    //}
}