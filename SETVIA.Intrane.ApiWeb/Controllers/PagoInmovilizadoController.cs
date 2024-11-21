using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Parameter.PagoInmovil;
using SETVIA.Util.Api.Model.Parameter.Tarifario;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
using SETVIA.Util.Api.Model.Response.DeudasPagos;
using SETVIA.Util.Api.Model.Response.Tarifario;
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
    [RoutePrefix(EndpointHelper.PagosInmovilizadoParqueoPrefix)]
    public class PagoInmovilizadoController : ApiController
    {
        private readonly DeudasInmovilizadoBusinessDB _setViaBussDB = new DeudasInmovilizadoBusinessDB();
        private readonly PagoInmovilizadoBusinessDB _setViaBussDB1 = new PagoInmovilizadoBusinessDB();
        [HttpPost]
        [Route(EndpointHelper.PagarDeudaInmovilizado)]
        public async Task<ResponseApi<bool>> Crear(PagoInmovilizadoParameter param)
        {
            var resp = await _setViaBussDB1.RegistrarInmovilizado1(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }
      
        public async Task<PagoResponse<bool>> UpdatePsgoInm(UpdatePagoInfracionUsoParqueoParameter param)
        {
            var resp = await _setViaBussDB.UpdatePagodeudaInmovilizadoParqueo(param);

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
            return new PagoResponse<bool>
            {
                Data = true,
                Error = 2,
                MensajeError = "Pago realizado correctamente."
            };
        }
    }
}
