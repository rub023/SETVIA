using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Parameter.Tarifario;
using SETVIA.Util.Api.Model.Response.Administrador;
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
    [RoutePrefix(EndpointHelper.TarifarioPrefix)]
    public class TarifarioController : ApiController
    {
        private readonly TarifarioBusinessDB _setViaBussDB = new TarifarioBusinessDB();

        [HttpPost]
        [Route(EndpointHelper.TarifarioCrear)]
        public async Task<ResponseApi<bool>> Crear(CreateTarifarioParameter param)
        {
            var resp = await _setViaBussDB.RegistrarTarifario(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }
        //lista de ACTIVOS
        [HttpPost]
        [Route(EndpointHelper.TarifarioLista)]
        public async Task<ResponseApi<IEnumerable<ListadoTarifario>>> GetListSolicitud(ReadDeudaParqueo param)
        {
            var resp = await _setViaBussDB.ObtieneListaTarifario(param);
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
            }
            return resp;
        }


        [HttpPost]
        [Route(EndpointHelper.TarifarioUpdate)]
        public async Task<ResponseApi<bool>> Update(UpdateTarifarioParameter param)
        {
            var resp = await _setViaBussDB.UpdateTarifario(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }

        [HttpPost]
        [Route(EndpointHelper.ComparatorAdministradorId)]
        public async Task<ResponseApi<TarifarioReadID>> Read(ReadIDTarifario param)
        {
            var resp = await _setViaBussDB.ComparatorReadTarifario(param);
            if (resp.Data == null)
            {
                //Logger.Debug($"No se encontraron Datos Read Marcas ID {param.Idm}");
            }
            return resp;

        }

    }
}
