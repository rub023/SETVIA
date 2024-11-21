using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter.Extorno;
using SETVIA.Util.Api.Model.Parameter.Tarifario;
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
    [RoutePrefix(EndpointHelper.PagosExtornoPrefix)]
    public class ExtornoController : ApiController
    {

        private readonly ExtornoBusinesDB _setViaBussDB = new ExtornoBusinesDB();

        [HttpPost]
        [Route(EndpointHelper.TarifarioCrear)]
        public async Task<ResponseApi<bool>> Crear(ExtornoParameter param)
        {
            var resp = await _setViaBussDB.RegistrarExtorno(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }
    }
}
