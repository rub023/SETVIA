using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api.Model.Parameter.Remolcado;
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
    [RoutePrefix(EndpointHelper.RemolcadoPrefix)]
    public class RemolcadoController : ApiController
    {
        private readonly RemolcadoBusinessDB _setViaBussDB = new RemolcadoBusinessDB();

        [HttpPost]
        [Route(EndpointHelper.DParqueoCrear)]
        public async Task<ResponseApi<bool>> Crear(CreateRemolcadoParameter param)
        {
            var resp = await _setViaBussDB.RegistrarRemolcado(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }
        [HttpPost]
        [Route(EndpointHelper.RemolcadoPagoCrear)]
        public async Task<ResponseApi<bool>> Update(UpdatePagoRemolcadoParameter param)
        {
            var resp = await _setViaBussDB.UpdatePagoRemolcado(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }

        [HttpPost]
        [Route(EndpointHelper.RemolcadoLiberadoCrear)]
        public async Task<ResponseApi<bool>> Update(UpdateModificarRemolcadoALiberadoParameter param)
        {
            var resp = await _setViaBussDB.UpdateLiberadoInm(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }

        [HttpPost]
        [Route(EndpointHelper.InmovilizadoInmConcluidoCrear)]
        public async Task<ResponseApi<bool>> Update(UpdateModificarRLiberadoAConcluidoParameter param)
        {
            var resp = await _setViaBussDB.UpdateLibConcluido(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }
    }
}
