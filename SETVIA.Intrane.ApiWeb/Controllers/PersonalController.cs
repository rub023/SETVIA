using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.Personal;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.Personal;
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
    [RoutePrefix(EndpointHelper.PersonalPrefix)]
    public class PersonalController : ApiController
    {
        private readonly PersonalBusinessDB _setViaBussDB = new PersonalBusinessDB();

        [HttpPost]
        [Route(EndpointHelper.PersonalCrear)]
        public async Task<ResponseApi<bool>> Crear(CreatePersonalParameter param)
        {
            var resp = await _setViaBussDB.RegistrarPersonal(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }
        //lista de personal ACTIVOS
        [HttpPost]
        [Route(EndpointHelper.PersonalLista)]
        public async Task<ResponseApi<IEnumerable<ListadoPersonal>>> GetListSolicitud()
        {
            var resp = await _setViaBussDB.ObtieneListaPersonal();
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
            }
            return resp;
        }

        //ACTUALIZAR DATOS DEL PERSONAL
        [HttpPost]
        [Route(EndpointHelper.PersonalUpdate)]
        public async Task<ResponseApi<bool>> UpdatePersonaActivo(UpdatePersonalParameter param)
        {
            var resp = await _setViaBussDB.UpdatePersonalActivo(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }

        [HttpPost]
        [Route(EndpointHelper.ComparatorPersonalId)]
        public async Task<ResponseApi<PersonalReadID>> Read(ReadIDPersonal param)
        {
            var resp = await _setViaBussDB.ComparatorReadPersonal(param);
            if (resp.Data == null)
            {
                //Logger.Debug($"No se encontraron Datos Read Marcas ID {param.Idm}");
            }
            return resp;

        }

    }
}
