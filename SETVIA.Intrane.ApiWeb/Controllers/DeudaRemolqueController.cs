using SETVIA.Business.DataBases;
using SETVIA.Util.Api.Model.Parameter.DeudaInmovilizado;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SETVIA.Util.Api.Model.Parameter.DeudaRemolque;
using SETVIA.Util.Api.Model.Response.DeudaREmolque;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.Tarifario;
using SETVIA.Util.Api.Model.Response;

namespace SETVIA.Intrane.ApiWeb.Controllers
{
    [Authorize]
    [RoutePrefix(EndpointHelper.DeudaRemolcadoParqueoPrefix)]
    public class DeudaRemolqueController : ApiController
    {
        //private readonly DeudasRemolcadoBusinessDB _setViaBussDB = new DeudasRemolcadoBusinessDB();
        private readonly RemolcadoBusinessDB _setViaBussDB = new RemolcadoBusinessDB();
        private readonly DeudasRemolcadoBusinessDB _setViaBussDB1 = new DeudasRemolcadoBusinessDB();
        [HttpPost]
        [Route(EndpointHelper.ConusltarDeudaRemolcado)]

        //public async Task<ResponseApi<IEnumerable<ListadoTarifario>>> ConusltarDeudaInm(ReadDeudaRemolqueParqueo param)
        //{
        //    var resp = await _tarifarioBusinessDB.ObtieneListaTarifario();
        //    if (resp.Data == null)
        //    {
        //        // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
        //    }
        //    return resp;
        //}
        public async Task<ResponseApi<DeudaRemolcadorParqueoRead>> ConusltarDeudaRemol(ReadinmovilizadoParqueo param)
        {
            var resp = await _setViaBussDB.ConsultaDeudaRemolque(param);
            var tarifarioResponse = await _setViaBussDB1.ObtieneListaRemolcador(param);
            // Validar si no hay datos
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos para ID {param.Idm}");
                return resp;
            }
            var result = new DeudaRemolcadorParqueoRead
            {
                ListaDeudas = new List<ListadoRemolques>()
            };
            result.Direccion = resp.Data.Direccion;
            result.TipoVehiculo = resp.Data.TipoVehiculo; // Esto será igual a lo que se recibe en el modelo DeudaParqueoRead
            result.Placa = resp.Data.Placa;
            result.Id_Detalle = resp.Data.Id_Detalle;
            // Si obtenemos una lista de tarifarios válida
            if (tarifarioResponse.Success && tarifarioResponse.Data != null)
            {
                // Filtrar los tarifarios según el Id_TipoVehiculo (que es un entero)
                var tarifariosFiltrados = tarifarioResponse.Data
                    .Where(t => t.Id_Detalle == resp.Data.Id_Detalle)
                    .ToList();

                // Asignar los tarifarios filtrados a la respuesta
                result.ListaDeudas.AddRange(tarifariosFiltrados);
            }
            else
            {
                // Si no se encuentran tarifarios, devolvemos un mensaje de error
                return new ResponseApi<DeudaRemolcadorParqueoRead>
                {
                    Message = "No se encontraron tarifarios para el tipo de vehículo.",
                    Success = false,
                    Time = DateTime.UtcNow,
                    Error = 1
                };
            }

            // Devolvemos la respuesta final que incluye la deuda y los tarifarios correspondientes
            resp.Data = result;
            return resp;
        }

    }
}
