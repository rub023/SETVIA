using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.Feriado;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.Feriado;
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
    [RoutePrefix(EndpointHelper.FeriadoPrefix)]
    public class FeriadoController : ApiController
    {
        private readonly FeriadoBusinessDB _setViaBussDB = new FeriadoBusinessDB();

        [HttpPost]
        [Route(EndpointHelper.FeriadoCrear)]
        public async Task<ResponseApi<bool>> Crear(CreateFeriadoParameter param)
        {
            var resp = await _setViaBussDB.RegistrarDiaFeriado(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }

        //lista de Feriados
        [HttpPost]
        [Route(EndpointHelper.FeriadoLista)]
        public async Task<ResponseApi<IEnumerable<ListadoFeriado>>> GetListSolicitud()
        {
            var resp = await _setViaBussDB.ObtieneListaDiaFeriado();
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
            }
            return resp;
        }

        //ACTUALIZAR DATOS DEL Feriado
        [HttpPost]
        [Route(EndpointHelper.FeriadoUpdate)]
        public async Task<ResponseApi<bool>> Update(UpdateFeriadoParameter param)
        {
            var resp = await _setViaBussDB.UpdateDiaFeriado(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }

        [HttpPost]
        [Route(EndpointHelper.ComparatorAdministradorId)]
        public async Task<ResponseApi<FeriadoReadID>> Read(ReadIdFeriado param)
        {
            var resp = await _setViaBussDB.ComparatorReadFeriado(param);
            if (resp.Data == null)
            {
                //Logger.Debug($"No se encontraron Datos Read Marcas ID {param.Idm}");
            }
            return resp;

        }
    }
}
