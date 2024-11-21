using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api
{
    public class PagoDeudasResponse
    {
      //  public DataResponse2 Data { get; set; }
        public string Time { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public int Error { get; set; }
    }
    //public class DataResponse2
    //{
    //    public string Direccion { get; set; }
    //    public string TipoVehiculo { get; set; }
    //    public string Placa { get; set; }
    //    public List<ListaPagoResponse> ListaPago { get; set; }
    //    public List<FacturacionResponse> Facturacion { get; set; }
    //}

    //public class ListaPagoResponse2
    //{
    //    public int Id_Detalle { get; set; }
    //    public string Placa { get; set; }
    //    public decimal Monto_Infraccion { get; set; }
    //    public decimal Servicio_Infraccion { get; set; }
    //    public decimal Total_InfraccionInmovilizado { get; set; }
    //    public string Tiempo_InicioInmovilizado { get; set; }
    //}

    //public class FacturacionResponse2
    //{
    //    public int NIT { get; set; }
    //    public string RAZONSOCIAL { get; set; }
    //    public string Correo { get; set; }
    //}
}
