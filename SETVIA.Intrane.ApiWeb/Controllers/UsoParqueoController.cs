using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.DetalleParqueo;
using SETVIA.Util.Api.Model.Response.Tarifario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace SETVIA.Intrane.ApiWeb.Controllers
{
    [Authorize]
    [RoutePrefix(EndpointHelper.DParqueoPrefix)]
    
    public class UsoParqueoController : ApiController
    {
        //prueba
        private readonly DetalleParqueoBusinessDB _setViaBussDBw = new DetalleParqueoBusinessDB();

        [HttpPost]
        [Route(EndpointHelper.DPruebaParqueoCrear)]
        public async Task<ResponseApi<bool>> Crear(CreatePruebaUsoDetalleParameter param)
        {
            var resp = await _setViaBussDBw.RegistrarDetalleParqueoPrueba(param);
            if (!resp.Data)
            {
                // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
            }
            return resp;
        }
        //fin prueba

        private readonly DetalleParqueoBusinessDB _setViaBussDB = new DetalleParqueoBusinessDB();
        private readonly TarifarioBusinessDB _tarifarioBusinessDB = new TarifarioBusinessDB();
        [HttpPost]
        [Route(EndpointHelper.DParqueoCrear)]

        public async Task<ResponseApi<DetalleParqueoResponse>> Crear(CreateDParqueoParameter param)
        {

            var response = new ResponseApi<DetalleParqueoResponse>();
            var log = Method.global_Log();
            var ide = DateTime.Now.ToString("yyyyMMddhhmmssff");

           
            try
            {
                var resp = await _setViaBussDB.RegistrarDetalleParqueo(param);

                if (!resp.Data)
                {
                    response.Success = false;
                    response.Message = "No se pudo registrar el detalle de parqueo.";
                    return response;
                }

                //var listaTarifariosResponse = await _tarifarioBusinessDB.ObtieneListaTarifario(param);
                //var ListaDeudas = listaTarifariosResponse.Data.ToList();
                //var listaTarifarios = listaTarifariosResponse.Data
                //      .Where(t => t.TipoVehiculo == param.Tipo_Vehiculo) // Aplica el filtro
                //      .ToList();
                // Asignar la respuesta
                //response.Data = new DetalleParqueoResponse
                //{
                //    RegistroExitoso = resp.Data,
                //    ListaDeudas= ListaDeudas
                //};
                response.Success = true;
                response.Message = "Detalle de parqueo registrado correctamente y lista de tarifarios obtenida.";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = $"Error: {e.Message}";
            }
            return response;
            //return new ResponseApi<List<ListadoTarifario>> { Data = listaTarifarios, Success = true };
        }

        //para llenado de tiempo
        //[HttpPost]
        //[Route(EndpointHelper.DPruebacargaTiempoCrear)]
        //public async Task<ResponseApi<bool>> UpdateTiempo(UpdateTiempoParqueoParameter param)
        //{
        //    var resp = await _setViaBussDB.UpdateTiempoParqueo(param);
        //    if (!resp.Data)
        //    {
        //        //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
        //    }
        //    return resp;
        //}

        [HttpPost]
        [Route(EndpointHelper.DPruebacargaTiempoCrear)]
        public async Task<PagoResponse<bool>> Update(UpdateTiempoUsoParqueoParameter param)
        {
            var resp = await _setViaBussDB.UpdateTiempoUsodeParqueo(param);

            //DataTable data = resp.Success;
            //string MensajeError = resp.Item2;
            //int Error = resp.Item3;
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
                return new PagoResponse<bool>
                {
                    Data = false,
                    Error = 1,
                    MensajeError = "No se pudo realizar el pago. Verifique los datos e intente nuevamente."

                };
            }
            //return resp;
            return new PagoResponse<bool>
            {
                Data = true,
                Error = 2,
                MensajeError = "Tiempo Asignado correctamente."
            };
        }


        [HttpPost]
        [Route(EndpointHelper.DPParqueoCrear)]
        public async Task<PagoResponse<bool>> Update(UpdatePagoUsoParquepParameter param)
        {
            var resp = await _setViaBussDB.UpdatePagoUsoParqueo(param);

            //DataTable data = resp.Success;
            //string MensajeError = resp.Item2;
            //int Error = resp.Item3;
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
                return new PagoResponse<bool>
                {
                    Data = false,
                    Error = 1,
                    MensajeError = "No se pudo realizar el pago. Verifique los datos e intente nuevamente."
                    
                };
            }
            //return resp;
            return new PagoResponse<bool>
            {
                Data = true,
                Error = 2,
                MensajeError = "Pago realizado correctamente."
            };
        }
        //List Vigentes
        //lista de ACTIVOS
        [HttpPost]
        [Route(EndpointHelper.DPParqueoListVigente)]
        public async Task<ResponseApi<IEnumerable<ListadoTipoEstadoV>>> GetListSolicitud1()
        {
            var resp = await _setViaBussDB.ObtieneListaTipoEstadoVigente();
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
            }
            return resp;
        }




        [HttpPost]
        [Route(EndpointHelper.DPParqueoConcluidoCrear)]
        public async Task<ResponseApi<bool>> Update(UpdateModificarAConcluidoParquepParameter param)
        {
            var resp = await _setViaBussDB.UpdateConcluidoParqueo(param);
            if (!resp.Data)
            {
                //Logger.Debug($"No se pudo actualizar el id Marcas {param.Idm}, estado {param.Marca}");
            }
            return resp;
        }
        //listar por tipo de estado
        //lista de ACTIVOS
        [HttpPost]
        [Route(EndpointHelper.DPParqueoListConcluido)]
        public async Task<ResponseApi<IEnumerable<ListadoTipoEstadoV>>> GetListSolicitud()
        {
            var resp = await _setViaBussDB.ObtieneListaTipoEstado();
            if (resp.Data == null)
            {
                // Logger.Debug($"No se encontraron Datos del Listado de Marcas");
            }
            return resp;
        }

        [HttpPost]
        [Route(EndpointHelper.DPParqueoBuscarDeuda)]
        public async Task<ResponseApi<PlacaRead>> Read(ReadPlaca param)
        {
            var resp = await _setViaBussDB.ComparatorReadPlaca(param);
            if (resp.Data == null)
            {
                return new ResponseApi<PlacaRead>
                {
                    Data = null,
                    Success = false,
                    Message = "No se encontraron datos para la placa proporcionada.",
                   // TotalRows = 0
                };
            }
            //return resp;
            return new ResponseApi<PlacaRead>
            {
                Data = resp.Data,
                Success = true,
                Message = "Datos encontrados",
                //TotalRows = 1 // O el número de filas reales si tienes más
            };
        }
    }
}
