using Newtonsoft.Json;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.DeudasPagos;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.DeudaInmovilizado;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
using System.Data;
using SETVIA.Util.Api.Model.Response.DetalleParqueo;
using SETVIA.Util.Api.Model.Response.Tarifario;

namespace SETVIA.Core.Database.Repository.DeudaInmovilizado
{
    public class DeudaInmovilizadoRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<DeudaInmovilizadoParqueoRead>> ConsultaDeudaInmovilizado(ReadinmovilizadoParqueo param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarDeudaPorInmovilizacionParqueo]");
           
            storeProcedure.AddParameterAsync("@Placa", param.Placa);

            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new DeudaInmovilizadoParqueoRead()
            {
                ListaDeudas = new List<ListadoInmovilizado>() // Inicializar la lista
            };
            var response = new ResponseApi<DeudaInmovilizadoParqueoRead>
            {
                Data = result,
                Time = DateTime.UtcNow,
                Message = "Datos obtenidos correctamente",
                Success = true,
                Error = 2
            };
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    var firstRow = dataTable.Rows[0];
                    result.Direccion = firstRow["Direccion"].ToString();
                    result.TipoVehiculo = firstRow["TipoVehiculo"].ToString();
                    result.Placa = firstRow["Placa"].ToString();
                    result.Id_Detalle = Convert.ToInt32(firstRow["Id_Detalle"]);

                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
                response.Message = $"Error al obtener los datos: {e.Message}";
                response.Success = false;
                response.Error = 1;
            }
            //return new ResponseApi<DeudaExtensionRead>
            //{
            //    Data = result,
            //    Message = "Datos Obtenidos correctamente",
            //    Time = DateTime.UtcNow,
            //    Success = true
            //    //TotalRows = result
            //};
            return response;
        }


        public async Task<ResponseApi<bool>> UpdatePagoDeudaInmovilParqueo(UpdatePagoInfracionUsoParqueoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[PagoInmovilizadoParqueo]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);

            storeProcedure.AddParameterAsync("@Placa", param.Placa);
            storeProcedure.AddParameterAsync("@NroTransaccion", param.NroTransaccion);
            var result = await storeProcedure.ReturnUpdatePagodeudaUsoPagoIR(ConectionString.GetParameterConnectionString(), TimeOutDB, param);

            DataTable dataTable = result.Item1; // Aquí obtienes el DataTable
            int estadoIni = result.Item2;

            try
            {
                //if (storeProcedure.Error != string.Empty)
                //{
                //    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                //}
                //if (dataTable.Item2 != 0)
                //{
                //    return new ResponseApi<bool>
                //    {
                //        Data = true,
                //        Message = "Pago realizado correctamente.",
                //        Time = DateTime.UtcNow,
                //        Success = true
                //    };
                //}
                if (!string.IsNullOrEmpty(storeProcedure.Error))
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }

                // Verificas el valor de estadoIni para determinar el resultado
                if (estadoIni != -1)
                {
                    return new ResponseApi<bool>
                    {
                        Data = true,
                        Message = "Pago realizado correctamente.",
                        Time = DateTime.UtcNow,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<bool>
            {
                Message = "No se pudo hacer el pago",
                Time = DateTime.UtcNow,
                Success = false
            };

        }

    }
}
