using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Parameter.PagoInmovil;
using SETVIA.Util.Api.Model.Parameter.PagoRemolcado;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SETVIA.Intrane.ApiWeb.Controllers
{
    [Authorize]
    [RoutePrefix(EndpointHelper.PagosRemolcadoParqueoPrefix)]
    public class PagoRemolqueController : ApiController
    {
        private readonly DeudasRemolcadoBusinessDB _setViaBussDB = new DeudasRemolcadoBusinessDB();
        private readonly PagoRemolcadoBusinessBD _setViaBussDB1 = new PagoRemolcadoBusinessBD();
        [HttpPost]
        [Route(EndpointHelper.PagarDeudaRemolcado)]
        public async Task<ResponseApi<bool>> Crear(PagoRemolcadoParameter param)
        {
            var resp = await _setViaBussDB1.RegistrarRemolcado1(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }
        //public async Task<PagoResponse<bool>> UpdatePagoRemolcado(UpdatePagoInfracionUsoParqueoParameter param)
        //{
        //    var resp = await _setViaBussDB.UpdatePagodeudaremolcadoParqueo(param);

        //    //DataTable data = resp.Success;
        //    //string MensajeError = resp.Item2;
        //    //int Error = resp.Item3;
        //    if (!resp.Data)
        //    {
        //        //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
        //        return new PagoResponse<bool>
        //        {
        //            Data = false,
        //            Error = 1,
        //            MensajeError = "No se pudo realizar el pago. Verifique los datos e intente nuevamente."

        //        };
        //    }
        //    //return resp;
        //    return new PagoResponse<bool>
        //    {
        //        Data = true,
        //        Error = 2,
        //        MensajeError = "Pago realizado correctamente."
        //    };
        //}
    }
}
