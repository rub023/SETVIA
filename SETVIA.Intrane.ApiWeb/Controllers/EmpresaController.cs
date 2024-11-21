using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Response.Administrador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SETVIA.Util.Api.Model.Parameter.Empresa;
using SETVIA.Util.Api.Model.Response.Empresa;

namespace SETVIA.Intrane.ApiWeb.Controllers
{
    [Authorize]
    [RoutePrefix(EndpointHelper.EmpresaPrefix)]
    public class EmpresaController : ApiController
    {
        private readonly EmpresaBusinessDB _setViaBussDB = new EmpresaBusinessDB();

        [HttpPost]
        [Route(EndpointHelper.AdministradirCrear)]
        public async Task<ResponseApi<bool>> Crear(CreateEmpresaParameter param)
        {
            var resp = await _setViaBussDB.RegistrarEmpresa(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }
        //lista de Empresas ACTIVOS
        [HttpPost]
        [Route(EndpointHelper.AdministradorLista)]
        public async Task<ResponseApi<IEnumerable<ListadoEmpresa>>> GetListSolicitud()
        {
            var resp = await _setViaBussDB.ObtieneListaEmpresa();
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
            }
            return resp;
        }
        //ACTUALIZAR DATOS DE EMPRESA CON ESTADO ACTIVO
        [HttpPost]
        [Route(EndpointHelper.AdministradorUpdate)]
        public async Task<ResponseApi<bool>> Update(UpdateEmpresaParameter param)
        {
            var resp = await _setViaBussDB.UpdateEmpresaActivo(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }
        ////ACTUALIZARc ESTADO DE EMPRESA 
        //[HttpPost]
        //[Route(EndpointHelper.EmpresaUpdateEstado)]
        //public async Task<ResponseApi<bool>> UpdateEstado(UpdateEmpresaEstadoParameter param)
        //{
        //    var resp = await _setViaBussDB.UpdateEmpresaEstado(param);
        //    if (!resp.Data)
        //    {
        //        //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
        //    }
        //    return resp;
        //}
        [HttpPost]
        [Route(EndpointHelper.ComparatorEmpresaId)]
        public async Task<ResponseApi<EmpresaReadID>> Read(ReadIDEmpresa param)
        {
            var resp = await _setViaBussDB.ComparatorReadEmpresa(param);
            if (resp.Data == null)
            {
                //Logger.Debug($"No se encontraron Datos Read Marcas ID {param.Idm}");
            }
            return resp;

        }
    }
}
