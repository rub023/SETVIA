using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SETVIA.Intranet.Api.Controllers
{
    [RoutePrefix(EndpointHelper.CostoInfraccionPrefix)]
    public class CostoInfraccionController : ApiController
    {
        private readonly CostoInfraccionRepository _firDigBussDB = new CostoInfraccionRepository();

        //[HttpPost]
        //[Route(EndpointHelper.FirDigCrear)]
        //public async Task<ResponseApi<bool>> Crear(CreateParameter param)
        //{
        //    var resp = await _firDigBussDB.RegistrarVigentes(param);
        //    if (!resp.Data)
        //    {
        //        //Logger.Debug($"No se pudo registrar ");
        //    }
        //    return resp;
        //}

        [HttpPost]
        [Route(EndpointHelper.CostoInfraccionLista)]
        public async Task<ResponseApi<IEnumerable<ListCostoInfraccion>>> GetListSolicitud()
        {
            var resp = await _firDigBussDB.ObtieneLista();
            if (resp.Data == null)
            {
                //Logger.Debug($"No se encontraron Datos.");
            }
            return resp;
        }
    }
}
