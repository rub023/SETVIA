using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Parameter.ExtensionParqueo;
using SETVIA.Util.Api.Model.Parameter.Inmovilizado;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
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
    [RoutePrefix(EndpointHelper.InmovilizadoPrefix)]
    public class InmovilizadoController : ApiController
    {
        private readonly InmovilizadoBusinessDB _setViaBussDB = new InmovilizadoBusinessDB();
        //private readonly InmovilizadoBusinessDB _setViaBussDB = new InmovilizadoBusinessDB();
        private readonly TarifarioBusinessDB _tarifarioBusinessDB = new TarifarioBusinessDB();
        [HttpPost]
        [Route(EndpointHelper.DParqueoCrear)]
        public async Task<ResponseApi<bool>> Crear(CreateInmovilizadoParameter param)
        {
            var resp = await _setViaBussDB.RegistrarInmovilizado(param);
            if (!resp.Data)
            { // Logger.Debug($"No se pudo registrar en Marca {param.Idm}"); return resp; }
            }

            //var resp1 = await _tarifarioBusinessDB.ObtieneListaTarifario(param);
            //if (resp1.Data == null)
            //{ // Logger.Debug("No se pudo obtener la lista de tarifarios");
            //    return new ResponseApi<bool> { Data = false, Message = "Error al obtener la lista de tarifarios" };
            //}
          //  var listaTarifarios = resp1.Data.ToList();
            //return resp;
            ///pagos de multa por inmovilizacion
            return resp;
        } 
        [HttpPost]
        [Route(EndpointHelper.InmovilizadoPagoCrear)]
        public async Task<ResponseApi<bool>> Update(UpdatePagInmovilizadoParameter param)
        {
            var resp = await _setViaBussDB.UpdatePagoInmovilizadoParqueo(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }

        [HttpPost]
        [Route(EndpointHelper.InmovilizadoConcluidoCrear)]
        public async Task<ResponseApi<bool>> Update(UpdateModificarLieradoAConcluidoParquepParameter param)
        {
            var resp = await _setViaBussDB.UpdateLiberadoAConcluido(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }


        [HttpPost]
        [Route(EndpointHelper.TarifarioLista)]
        public async Task<ResponseApi<IEnumerable<ListadoInmovilizado>>> GetListSolicitud(ReadDeudaParqueo param)
        {
            var resp = await _setViaBussDB.ObtieneListaInmovilizado(param);
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
            }
            return resp;
        }
    }
}
