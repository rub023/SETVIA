using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SETVIA.Business.DataBases;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using System.Data;

namespace SETVIA.Intrane.ApiWeb.Controllers
{
   //[Authorize]
    [RoutePrefix(EndpointHelper.PagosParqueoPrefix)]
    public class PagoDeudasController : ApiController
    {

        private readonly DeudasPagosBusinessDB _setViaBussDB = new DeudasPagosBusinessDB();
        [HttpPost]
        [Route(EndpointHelper.PagarDeuda)]
        public async Task<PagoResponse<bool>> Update(UpdatePagoDeudaUsoParqueoParameter param)
        {
            var resp = await _setViaBussDB.UpdatePagodeudaUsoParqueo(param);

            if (!resp.Data)
            {

                return new PagoResponse<bool>
                {
                    Data = false,
                    Error = 1,
                    MensajeError = "No se pudo realizar el pago. Verifique los datos e intente nuevamente."

                };
            }
           
            return new PagoResponse<bool>
            {
                Data = true,
                Error = 2,
                MensajeError = "Pago Uso de Parqueo realizado correctamente."
            };
        }
        //public async Task<PagoResponse<UpdatePagoDeudaUsoParqueoParameter>> Update(UpdatePagoDeudaUsoParqueoParameter param)
        //{

        //    var resp = await _setViaBussDB.UpdatePagodeudaUsoParqueo(param);

        //    if (!resp.Data)
        //    {

        //        return new PagoResponse<UpdatePagoDeudaUsoParqueoParameter>
        //        {
        //            // Data = false,
        //            Error = true,
        //            MensajeError = "No se pudo realizar el pago. Verifique los datos e intente nuevamente."

        //        };
        //        //  return resp;
        //    }
        //    var result = new PagoResponse<bool>
        //    {
        //        ListaPagos = new List<Deuda>()
        //    };
        //    result.Placa = param.Placa;
        //    result.NroTransaccion = param.NroTransaccion; // Esto será igual a lo que se recibe en el modelo DeudaParqueoRead

        //    //result.Placa = resp.Data.Placa;
        //    //result.Id_TipoVehiculo = resp.Data.Id_TipoVehiculo;
        //    foreach (var deuda in param.ListaDeudas)
        //    {
        //        var id = deuda.Id; var monto = deuda.Monto;
        //    }
        //    return new PagoResponse<UpdatePagoDeudaUsoParqueoParameter>
        //    {
        //        Placa = param.Placa,
        //        NroTransaccion = param.NroTransaccion,

        //        //Data = true,
        //        Error = false,
        //        MensajeError = "Pago Uso de Parqueo realizado correctamente."
        //    };
        //    ////
        //    //var resp = await _setViaBussDB.UpdatePagodeudaUsoParqueo(param);

        //    //if (!resp.Data)
        //    //{

        //    //    return new PagoResponse<bool>
        //    //    {
        //    //        Data = false,
        //    //        Error = true,
        //    //        MensajeError = "No se pudo realizar el pago. Verifique los datos e intente nuevamente."

        //    //    };
        //    //}

        //    //foreach (var deuda in param.ListaDeudas)
        //    //{
        //    //    var id = deuda.Id; var monto = deuda.Monto;
        //    //}
        //    //return new PagoResponse<bool>
        //    //{
        //    //    Data = true,
        //    //    Error = false,
        //    //    MensajeError = "Pago Uso de Parqueo realizado correctamente."
        //    //};
        //}

    }
}
