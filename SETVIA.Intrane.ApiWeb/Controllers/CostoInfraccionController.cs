using SETVIA.Business.DataBases;
using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.CostoInfraccion;
using SETVIA.Util.Api.Model.Response;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.CostoInfraccion;
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
    [RoutePrefix(EndpointHelper.CostoInfraccionPrefix)]
    public class CostoInfraccionController : ApiController
    {
       
        private readonly CostoInfraccionBusinessDB _setViaBussDB = new CostoInfraccionBusinessDB();
        //Crear Costo por infraccion por parte de la empresa
        [HttpPost]
        [Route(EndpointHelper.CostoInfraccionCrear)]
        public async Task<ResponseApi<bool>> Crear(CreateCostoInfraccionParameter param)
        {
            var resp = await _setViaBussDB.RegistrarCostoInfraccion(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }

        //listado
        [HttpPost]
        [Route(EndpointHelper.CostoInfraccionLista)]
        public async Task<ResponseApi<IEnumerable<ListCostoInfraccion>>> GetListSolicitud()
        {
            var resp = await _setViaBussDB.ObtieneLista();
            if (resp.Data == null)
            {
                //Logger.Debug($"No se encontraron Datos.");
            }
            return resp;
        }

        //ACTUALIZAR DATOS
        [HttpPost]
        [Route(EndpointHelper.CostoInfraccionUpdate)]
        public async Task<ResponseApi<bool>> Update(UpdateCostoInfraccionParameter param)
        {
            var resp = await _setViaBussDB.UpdateCostoInfraccion(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }


        //buscar por Id
        [HttpPost]
        [Route(EndpointHelper.ComparatorostoInfraccionId)]
        public async Task<ResponseApi<CostoInfraccionReadID>> Read(ReadIDCostoInfraccion param)
        {
            var resp = await _setViaBussDB.ComparatorostoInfraccionId(param);
            if (resp.Data == null)
            {
                //Logger.Debug($"No se encontraron Datos Read Marcas ID {param.Idm}");
            }
            return resp;

        }
    }
}
