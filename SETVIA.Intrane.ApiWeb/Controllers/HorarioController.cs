using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.Horario;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.Horario;
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
    [RoutePrefix(EndpointHelper.HorarioPrefix)]
    public class HorarioController : ApiController
    {
        private readonly HorarioBusinessDB _setViaBussDB = new HorarioBusinessDB();

        [HttpPost]
        [Route(EndpointHelper.HorarioCrear)]
        public async Task<ResponseApi<bool>> Crear(CreateHorarioParameter param)
        {
            var resp = await _setViaBussDB.RegistrarHorario(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }

        [HttpPost]
        [Route(EndpointHelper.HorarioLista)]
        public async Task<ResponseApi<IEnumerable<ListadoHorario>>> GetListSolicitud()
        {
            var resp = await _setViaBussDB.ObtieneListaHorario();
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
            }
            return resp;
        }

        [HttpPost]
        [Route(EndpointHelper.HorarioUpdate)]
        public async Task<ResponseApi<bool>> Update(UpdateHorarioParameter param)
        {
            var resp = await _setViaBussDB.UpdateHorario(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }

        [HttpPost]
        [Route(EndpointHelper.ComparatorHorarioId)]
        public async Task<ResponseApi<HorarioReadID>> Read(ReadIDHorario param)
        {
            var resp = await _setViaBussDB.ComparatorReadHorario(param);
            if (resp.Data == null)
            {
                //Logger.Debug($"No se encontraron Datos Read Marcas ID {param.Idm}");
            }
            return resp;

        }
    }
}
