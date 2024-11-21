using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.DeudasPagos;
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
    [RoutePrefix(EndpointHelper.DeudaExtensionParqueoPrefix)]
    public class DeudaExtensionController : ApiController
    {
        private readonly DeudaExtensionBusinessDB _setViaBussDB = new DeudaExtensionBusinessDB();
        private readonly TarifarioBusinessDB _tarifarioBusinessDB = new TarifarioBusinessDB();
        [HttpPost]
        [Route(EndpointHelper.ConusltarDeudaextension)]
        public async Task<ResponseApi<DeudaExtensionRead>> ConusltarDeudaExtension(ReadDeudaParqueo param)
        {
            // Consultar deuda
            var resp = await _setViaBussDB.ConsultaDeudaExtens(param);
            var tarifarioResponse = await _tarifarioBusinessDB.ObtieneListaTarifario(param);
            // Validar si no hay datos
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos para ID {param.Idm}");
                return resp;
            }

            // Inicializar el objeto resultado
            var result = new DeudaExtensionRead
            {
                ListaDeudas = new List<ListadoTarifario>()
            };
            result.Direccion = resp.Data.Direccion;
            result.TipoVehiculo = resp.Data.TipoVehiculo; // Esto será igual a lo que se recibe en el modelo DeudaParqueoRead
            result.Placa = resp.Data.Placa;
            result.Id_TipoVehiculo = resp.Data.Id_TipoVehiculo;

            // Si obtenemos una lista de tarifarios válida
            if (tarifarioResponse.Success && tarifarioResponse.Data != null)
            {
                // Filtrar los tarifarios según el Id_TipoVehiculo (que es un entero)
                var tarifariosFiltrados = tarifarioResponse.Data
                    .Where(t => t.Id_TipoVehiculo == resp.Data.Id_TipoVehiculo && t.Id_TipoTarifario == 1)
                    .ToList();

                // Asignar los tarifarios filtrados a la respuesta
                result.ListaDeudas.AddRange(tarifariosFiltrados);
            }
            else
            {
                // Si no se encuentran tarifarios, devolvemos un mensaje de error
                return new ResponseApi<DeudaExtensionRead>
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
        //public async Task<ResponseApi<DeudaExtensionRead>> ConusltarDeudaExtension(ReadDeudaParqueo param)
        //{
        //    var resp = await _setViaBussDB.ConsultaDeudaExtens(param);
        //    if (resp.Data == null)
        //    {
        //        //Logger.Debug($"No se encontraron Datos Read Marcas ID {param.Idm}");
        //    }
        //    return resp;
        //}
    }
}
