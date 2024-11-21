using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.TipoPago;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.TipoPago;
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
    [RoutePrefix(EndpointHelper.TipoPagoServicioPrefix)]
    public class TipoPagoController : ApiController
    {
        private readonly TipoPagoBusinessDB _setViaBussDB = new TipoPagoBusinessDB();

        [HttpPost]
        [Route(EndpointHelper.TipoPagoCrear)]
        public async Task<ResponseApi<bool>> Crear(CreateTipoPagoParameter param)
        {
            var resp = await _setViaBussDB.RegistrarTipoPago(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }

        [HttpPost]
        [Route(EndpointHelper.TipoPagoLista)]
        public async Task<ResponseApi<IEnumerable<ListadoTipoPago>>> GetListSolicitud()
        {
            var resp = await _setViaBussDB.ObtieneListaTipoPago();
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
            }
            return resp;
        }

    }
}
