using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter.DeudaInmovilizado;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
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
    [RoutePrefix(EndpointHelper.DeudaInmovilizadoParqueoPrefix)]
    public class DeudaInmovilizadoController : ApiController
    {
        private readonly InmovilizadoBusinessDB _setViaBussDB1 = new InmovilizadoBusinessDB();
        private readonly DeudasInmovilizadoBusinessDB _setViaBussDB = new DeudasInmovilizadoBusinessDB();
       // private readonly CostoInfraccionBusinessDB _setViaBussDB1 = new CostoInfraccionBusinessDB();
        [HttpPost]
        [Route(EndpointHelper.ConusltarDeudaInmovilizado)]
        public async Task<ResponseApi<DeudaInmovilizadoParqueoRead>> ConusltarDeudaInm(ReadinmovilizadoParqueo param)
        {

            var resp = await _setViaBussDB.ConsultaDeudaInmovilizado(param);
            var tarifarioResponse = await _setViaBussDB1.ObtieneListaInmovilizado(param);
            // Validar si no hay datos
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos para ID {param.Idm}");
                return resp;
            }
            var result = new DeudaInmovilizadoParqueoRead
            {
                ListaDeudas = new List<ListadoInmovilizado>()
            };
            result.Direccion = resp.Data.Direccion;
            result.TipoVehiculo = resp.Data.TipoVehiculo; // Esto será igual a lo que se recibe en el modelo DeudaParqueoRead
            result.Placa = resp.Data.Placa;
            result.Id_Detalle=resp.Data.Id_Detalle;
            // Si obtenemos una lista de tarifarios válida
            if (tarifarioResponse.Success && tarifarioResponse.Data != null)
            {
                // Filtrar los tarifarios según el Id_TipoVehiculo (que es un entero)
                var tarifariosFiltrados = tarifarioResponse.Data
                    .Where(t => t.Id_Detalle== resp.Data.Id_Detalle)
                    .ToList();

                // Asignar los tarifarios filtrados a la respuesta
                result.ListaDeudas.AddRange(tarifariosFiltrados);
            }
            else
            {
                // Si no se encuentran tarifarios, devolvemos un mensaje de error
                return new ResponseApi<DeudaInmovilizadoParqueoRead>
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

        //public async Task<ResponseApi<IEnumerable<ListCostoInfraccion>>> ConusltarDeudaInm(ReadDeudaInmovilizadoParqueo param)
        //{
        //    var resp = await _setViaBussDB1.ObtieneLista();
        //    if (resp.Data == null)
        //    {
        //        //Logger.Debug($"No se encontraron Datos.");
        //    }
        //    return resp;
        //}

    }
}
