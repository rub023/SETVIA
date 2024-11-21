using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api.Model.Parameter.ExtensionParqueo;
using SETVIA.Util.Api.Model.Response.DetalleParqueo;
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
    [RoutePrefix(EndpointHelper.EParqueoPrefix)]
    public class EstensionParqueoController : ApiController
    {
        private readonly ExtensionPBusinessDB _setViaBussDB = new ExtensionPBusinessDB();

        [HttpPost]
        [Route(EndpointHelper.EParqueoCrear)]
        public async Task<ResponseApi<bool>> Crear(CreateExtensionParqueoParameter param)
        {
            var resp = await _setViaBussDB.RegistrarNuevaExtension(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }


        //aumento de tiempo

        [HttpPost]
        [Route(EndpointHelper.DpTiempoExtensionCrear)]
        public async Task<PagoResponse<bool>> UpdateEx(UpdateExtensiondeTiempoParqueoParameter param)
        {
            var resp = await _setViaBussDB.UpdateTiempoExtensiondeParqueo(param);

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

        [HttpPost]
        [Route(EndpointHelper.EPagoParqueoCrear)]
        public async Task<ResponseApi<bool>> Update(UpdatePagoExtensionParquepParameter param)
        {
            var resp = await _setViaBussDB.UpdatePagoExtensionParqueo(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }



        //pasar a consulidos
        [HttpPost]
        [Route(EndpointHelper.DExParqueoConcluidoCrear)]
        public async Task<ResponseApi<bool>> Update(UpdateModificarExtenAConcluidoParquepParameter param)
        {
            var resp = await _setViaBussDB.UpdateExtenConcluidoParqueo(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }


        [HttpPost]
        [Route(EndpointHelper.ExtensionBuscarDeuda)]
        public async Task<ResponseApi<PlacaERead>> Read(ReadPlaca param)
        {
            var resp = await _setViaBussDB.ComparatorReadEPlaca(param);
            if (resp.Data == null)
            {
                //Logger.Debug($"No se encontraron Datos Read Marcas ID {param.Idm}");
            }
            return resp;

        }
    }
}
