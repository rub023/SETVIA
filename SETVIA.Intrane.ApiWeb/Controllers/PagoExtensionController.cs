using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Parameter.PagoExtension;
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
    [RoutePrefix(EndpointHelper.PagosextensionParqueoPrefix)]
    public class PagoExtensionController : ApiController
    {
        private readonly DeudaExtensionBusinessDB _setViaBussDB = new DeudaExtensionBusinessDB();
        [HttpPost]
        [Route(EndpointHelper.PagarDeudaExtension)]
        public async Task<PagoResponse<bool>> Update(UpdatePagoExtensionUsoParqueoParameter param)
        {
            var resp = await _setViaBussDB.UpdatePagodeudaExtensionUsoParqueo(param);

            //DataTable data = resp.Success;
            //string MensajeError = resp.Item2;
            //int Error = resp.Item3;
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
                return new PagoResponse<bool>
                {
                    Data = false,
                    Error = 1,
                    MensajeError = "No se pudo realizar el pago. Verifique los datos e intente nuevamente."

                };
            }
            //return resp;
            //foreach (var deuda in param.ListaDeudas)
            //{
            //    var id = deuda.Id; var monto = deuda.Monto;
            //}
            return new PagoResponse<bool>
            {
                Data = true,
                Error = 2,
                MensajeError = "Pago extension de parqueo realizado correctamente."
            };
        }
    }
}
