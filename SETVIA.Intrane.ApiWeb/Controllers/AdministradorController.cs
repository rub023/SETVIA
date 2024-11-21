using SETVIA.Business.DataBases;
using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Response.Administrador;
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
    [RoutePrefix(EndpointHelper.AdministradorPrefix)]
    public class AdministradorController : ApiController
    {
        private readonly AdministradorBusinessDB _setViaBussDB = new AdministradorBusinessDB();

        [HttpPost]
        [Route(EndpointHelper.AdministradirCrear)]
        public async Task<ResponseApi<bool>> Crear(CreateAdministradorParameter param)
        {
            var resp = await _setViaBussDB.RegistrarAdministrador(param);
            if (!resp.Data)
            {
               // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }
        //lista de ACTIVOS
        [HttpPost]
        [Route(EndpointHelper.AdministradorLista)]
        public async Task<ResponseApi<IEnumerable<ListadoAdministrador>>> GetListSolicitud()
        {
            var resp = await _setViaBussDB.ObtieneListaAdministrador();
            if (resp.Data == null)
            {
               // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
            }
            return resp;
        }
        //ACTUALIZAR DATOS DEL ADMINSITARDOR SEA O NO ACTIVO
        [HttpPost]
        [Route(EndpointHelper.AdministradorUpdate)]
        public async Task<ResponseApi<bool>> Update(UpdateAdministradorParameter param)
        {
            var resp = await _setViaBussDB.UpdateAdministrador(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }
        [HttpPost]
        [Route(EndpointHelper.ComparatorAdministradorId)]
        public async Task<ResponseApi<AdministradorReadID>> Read(ReadIDAdministrador param)
        {
            var resp = await _setViaBussDB.ComparatorReadAdministrador(param);
            if (resp.Data == null)
            {
                //Logger.Debug($"No se encontraron Datos Read Marcas ID {param.Idm}");
            }
            return resp;

        }
    }
        
}
