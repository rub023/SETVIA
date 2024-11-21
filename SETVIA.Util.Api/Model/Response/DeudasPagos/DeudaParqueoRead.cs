using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.Tarifario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.DeudasPagos
{
        public class DeudaParqueoRead
        {
            public string Direccion { get; set; }
            public string TipoVehiculo { get; set; }
            public string Placa { get; set; }
            public int Id_TipoVehiculo { get; set; }
           //public int Id_Parqueo { get; set; }
        //public TimeSpan Tiempo_Ocupar { get; set; }
        //public decimal Costo_Parqueo { get; set; }
        //public string NroTransaccion { get; set; }
        //public string Estado_Pago { get; set; }
        public List<ListadoTarifario> ListaDeudas { get; set; }
       // public List<TarifarioDeuda> Tarifarios { get; set; } = new List<TarifarioDeuda>();
    }
    //public class TarifarioDeuda
    //{
    //    public string Id_ { get; set; }
    //    public TimeSpan Tiempo { get; set; }
    //    public decimal CostoTarifario { get; set; }

    //}
}
