using Microsoft.IdentityModel.Tokens;
using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.DetalleParqueo;
using SETVIA.Util.Api.Model.Response.DeudasPagos;
using SETVIA.Util.Api.Model.Response.Tarifario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SETVIA.Intrane.ApiWeb.Controllers
{
   [Authorize]
    [RoutePrefix(EndpointHelper.DeudasPagosParqueoPrefix)]
    public class DeudasPagosController : ApiController
    {
        private readonly TarifarioBusinessDB _tarifarioBusinessDB = new TarifarioBusinessDB();
        private readonly DeudasPagosBusinessDB _setViaBussDB = new DeudasPagosBusinessDB();
        
        [HttpPost]
        [Route(EndpointHelper.ConusltarDeuda)]
        public async Task<ResponseApi<DeudaParqueoRead>> ConusltarDeuda(ReadDeudaParqueo param)
        {
              // Consultar deuda
            var resp = await _setViaBussDB.ConsultaDeuda(param);
            var tarifarioResponse = await _tarifarioBusinessDB.ObtieneListaTarifario(param);
            // Validar si no hay datos
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos para ID {param.Idm}");
                return resp;
            }

            // Inicializar el objeto resultado
            var result = new DeudaParqueoRead
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
                return new ResponseApi<DeudaParqueoRead>
                {
                    Message = "No se encontraron tarifarios para el tipo de vehículo.",
                    Success = false,
                    Time = DateTime.UtcNow,
                    Error=1
                };
            }

            // Devolvemos la respuesta final que incluye la deuda y los tarifarios correspondientes
            resp.Data = result;
            return resp;
        }

        //public async Task<ResponseApi<IEnumerable<ListadoTarifario>>> GetListSolicitud(ReadDeudaParqueo param)
        //{
        //    var response = new ResponseApi<IEnumerable<ListadoTarifario>>();
        //    // Consulta la deuda 
        //    var respDeuda = await _setViaBussDB.ConsultaDeuda(param);
        //    if (respDeuda.Data == null)
        //    {
        //        response.Success = false;
        //        response.Message = "No se encontraron datos de la deuda.";
        //        return response;
        //    } // Consulta la lista de tarifarios 
        //    var respTarifarios = await _tarifarioBusinessDB.ObtieneListaTarifario(param);
        //    if (respTarifarios.Data == null)
        //    {
        //        response.Success = false;
        //        response.Message = "No se encontraron datos del listado de tarifarios.";
        //        return response;
        //    } // Combina los resultados 
        //    var listaDeudas = respTarifarios.Data.ToList();
        //    response.Data = listaDeudas;
        //    response.Message = "Datos obtenidos correctamente.";
        //    response.Success = true; // Agrega la deuda a la respuesta 
        //    response.Deuda = respDeuda.Data;
        //    return response;
        //    //var resp = await _setViaBussDB.ConsultaDeuda(param);
        //    //if (resp.Data == null)
        //    //{
        //    //    // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
        //    //}
        //    //var resp1 = await _tarifarioBusinessDB.ObtieneListaTarifario(param);
        //    //if (resp1.Data == null)
        //    //{
        //    //    // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
        //    //}
        //    //resp1.Data.ListaTarifarios = listaDeudas;
        //    //return resp;
        //}
        //private readonly DetalleParqueoBusinessDB _setViaBussDBw = new DetalleParqueoBusinessDB();

        //[HttpPost]
        //[Route(EndpointHelper.DPruebaParqueoCrear)]
        //public async Task<ResponseApi<bool>> Crear(CreatePruebaUsoDetalleParameter param)
        //{
        //    var resp = await _setViaBussDBw.RegistrarDetalleParqueoPrueba(param);
        //    if (!resp.Data)
        //    {
        //        // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
        //    }
        //    return resp;
        //}
        //fin prueba

        //private readonly DetalleParqueoBusinessDB _setViaBussDB = new DetalleParqueoBusinessDB();
        //private readonly TarifarioBusinessDB _tarifarioBusinessDB = new TarifarioBusinessDB();
        //[HttpPost]
        //[Route(EndpointHelper.DParqueoCrear)]

        //public async Task<ResponseApi<DetalleParqueoResponse>> Crear(CreateDParqueoParameter param)
        //{

        //    var response = new ResponseApi<DetalleParqueoResponse>();
        //    var log = Method.global_Log();
        //    var ide = DateTime.Now.ToString("yyyyMMddhhmmssff");


        //    try
        //    {
        //        var resp = await _setViaBussDB.RegistrarDetalleParqueo(param);

        //        if (!resp.Data)
        //        {
        //            response.Success = false;
        //            response.Message = "No se pudo registrar el detalle de parqueo.";
        //            return response;
        //        }

        //        var listaTarifariosResponse = await _tarifarioBusinessDB.ObtieneListaTarifario();
        //        var ListaDeudas = listaTarifariosResponse.Data.ToList();
        //        //var listaTarifarios = listaTarifariosResponse.Data
        //        //      .Where(t => t.TipoVehiculo == param.Tipo_Vehiculo) // Aplica el filtro
        //        //      .ToList();
        //        // Asignar la respuesta
        //        response.Data = new DetalleParqueoResponse
        //        {
        //            RegistroExitoso = resp.Data,
        //            ListaDeudas = ListaDeudas
        //        };
        //        response.Success = true;
        //        response.Message = "Detalle de parqueo registrado correctamente y lista de tarifarios obtenida.";
        //    }
        //    catch (Exception e)
        //    {
        //        response.Success = false;
        //        response.Message = $"Error: {e.Message}";
        //    }
        //    return response;
        //    //return new ResponseApi<List<ListadoTarifario>> { Data = listaTarifarios, Success = true };
        //}
    }
}
