using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api
{
    public class PagoResponse<T>
    {
        
        public T Data { get; set; }
        public string parametro { get; set; }
        public int Error { get; set; }
        public string MensajeError { get; set; }

        //public int Error { get; set; }
        //public DataResponse Data1 { get; set; }
        //public List<Facturacion> FacturacionData { get; set; }
        // public string NroTransaccion { get; set; }
        //public List<Facturacion> FacturacionData { get; set; }
    }

    //public class DataResponse
    //{
    //    public string Direccion { get; set; }
    //    public string TipoVehiculo { get; set; }
    //    public string Placa { get; set; }
    //    public List<ListaPagoResponse> ListaPago { get; set; }
    //    public List<FacturacionResponse> Facturacion { get; set; }
    //}

   

    //public class ListaPagoResponse
    //{
    //    public int Id_Detalle { get; set; }
    //    public string Placa { get; set; }
    //    public decimal Monto_Infraccion { get; set; }
    //    public decimal Servicio_Infraccion { get; set; }
    //    public decimal Total_InfraccionInmovilizado { get; set; }
    //    public string Tiempo_InicioInmovilizado { get; set; }
    //}

    //public class FacturacionResponse
    //{
    //    public int NIT { get; set; }
    //    public string RAZONSOCIAL { get; set; }
    //    public string Correo { get; set; }
    //}
    //public class FacturacionData
    //{
    //    public int NIT { get; set; }
    //    public string RazonSocial { get; set; } // Otros campos relevantes }
    //    public string Correo { get; set; }
    //}

    ////public class FacturacionData
    ////{
    ////    public int NIT { get; set; }
    ////    public string RazonSocial { get; set; } // Otros campos relevantes }
    ////    public string Correo { get; set; }
    ////}

}
