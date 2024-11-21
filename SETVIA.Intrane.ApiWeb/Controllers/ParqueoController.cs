using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.Feriado;
using SETVIA.Util.Api.Model.Parameter.Parqueo;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.Parqueo;
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
    [RoutePrefix(EndpointHelper.ParqueoPrefix)]
    public class ParqueoController : ApiController
    {
        private readonly ParqueoBusinessDB _setViaBussDB = new ParqueoBusinessDB();

        [HttpPost]
        [Route(EndpointHelper.ParqueoCrear)]
        public async Task<ResponseApi<bool>> Crear(CreateParqueoParameter param)
        {
            var resp = await _setViaBussDB.RegistrarParqueo(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }
        //lista de Parqueos
        [HttpPost]
        [Route(EndpointHelper.ParqueoLista)]
        public async Task<ResponseApi<IEnumerable<ListadoParqueo>>> GetListSolicitud()
        {
            var resp = await _setViaBussDB.ObtieneListaParqueo();
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
            }
            return resp;
        }
        //ACTUALIZAR DATOS DEL  PARQUEO
        [HttpPost]
        [Route(EndpointHelper.ParqueoUpdate)]
        public async Task<ResponseApi<bool>> Update(UpdateParqueoParameter param)
        {
            var resp = await _setViaBussDB.UpdateParqueo(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }
        //Buscar por Codigo_Parqueo
        [HttpPost]
        [Route(EndpointHelper.ComparatorParqueoId)]
        public async Task<ResponseApi<ParqueoReadID>> Read(ReadIDParqueo param)
        {
            var resp = await _setViaBussDB.ComparatorReadParqueo(param);
            if (resp.Data == null)
            {
                //Logger.Debug($"No se encontraron Datos Read Marcas ID {param.Idm}");
            }
            return resp;

        }
    }
}
