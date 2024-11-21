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
using System.Data;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api.Model.Response.Tarifario;
using SETVIA.Util.Api.Model.Parameter.PagoExtension;

namespace SETVIA.Core.Database.Repository.DeudaExtension
{
    public class DeudaExtensionRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<DeudaExtensionRead>> ComparatorReadDeudaPago(ReadDeudaParqueo param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarDeudaExtensionUsoParqueo]");
            storeProcedure.AddParameterAsync("@Codigo_Parqueo", param.Codigo_Parqueo);
            storeProcedure.AddParameterAsync("@TipoVehiculo", param.TipoVehiculo);
            storeProcedure.AddParameterAsync("@Placa", param.Placa);


            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new DeudaExtensionRead()            
            {
                ListaDeudas = new List<ListadoTarifario>() // Inicializar la lista
            };
            var response = new ResponseApi<DeudaExtensionRead>
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
                    result.Id_TipoVehiculo = Convert.ToInt32(firstRow["Id_TipoVehiculo"]);

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
        public async Task<ResponseApi<bool>> RegistrarPagoDeudaextensionUsoParqueo(UpdatePagoExtensionUsoParqueoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[PagarDeudaExtensionDUsoParqueo01]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnRegistrarPagoExtensionUsoParqueo(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Item2 != 0)
                {
                    return new ResponseApi<bool>
                    {
                        Data = true,
                        Message = "Solicitud registrada correctamente.",
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
                Message = "No se pudo hacer el registro",
                Time = DateTime.UtcNow,
                Success = false
            };
            //storeProcedure.AddParameterAsync("@Placa", param.Placa);
            //storeProcedure.AddParameterAsync("@NroTransaccion", param.NroTransaccion);
            //var result = await storeProcedure.ReturnUpdatePagodeudaextensionUsoPago(ConectionString.GetParameterConnectionString(), TimeOutDB, param);

            //DataTable dataTable = result.Item1; // Aquí obtienes el DataTable
            //int estadoIni = result.Item2;

            //try
            //{
            //    //if (storeProcedure.Error != string.Empty)
            //    //{
            //    //    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
            //    //}
            //    //if (dataTable.Item2 != 0)
            //    //{
            //    //    return new ResponseApi<bool>
            //    //    {
            //    //        Data = true,
            //    //        Message = "Pago realizado correctamente.",
            //    //        Time = DateTime.UtcNow,
            //    //        Success = true
            //    //    };
            //    //}
            //    if (!string.IsNullOrEmpty(storeProcedure.Error))
            //    {
            //        throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
            //    }

            //    // Verificas el valor de estadoIni para determinar el resultado
            //    if (estadoIni != -1)
            //    {
            //        return new ResponseApi<bool>
            //        {
            //            Data = true,
            //            Message = "Pago realizado correctamente.",
            //            Time = DateTime.UtcNow,
            //            Success = true
            //        };
            //    }
            //}
            //catch (Exception e)
            //{
            //    //Logger.Error("Exception: {0}", e.ToString());
            //    //Logger.Error("Message: {0}", e.Message);
            //}
            //return new ResponseApi<bool>
            //{
            //    Message = "No se pudo hacer el pago",
            //    Time = DateTime.UtcNow,
            //    Success = false
            //};

        }


        //public async Task<ResponseApi<bool>> UpdatePagoDeudaextensionUsoParqueo(UpdatePagoDeudaUsoParqueoParameter param)
        //{
        //    StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[PagarDeudaExtensionDUsoParqueo]");
        //    //storeProcedure.AddParameterAsync("@Id", param.Id);

        //    storeProcedure.AddParameterAsync("@Placa", param.Placa);
        //    storeProcedure.AddParameterAsync("@NroTransaccion", param.NroTransaccion);
        //    var result = await storeProcedure.ReturnUpdatePagodeudaextensionUsoPago(ConectionString.GetParameterConnectionString(), TimeOutDB, param);

        //    DataTable dataTable = result.Item1; // Aquí obtienes el DataTable
        //    int estadoIni = result.Item2;

        //    try
        //    {
        //        //if (storeProcedure.Error != string.Empty)
        //        //{
        //        //    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
        //        //}
        //        //if (dataTable.Item2 != 0)
        //        //{
        //        //    return new ResponseApi<bool>
        //        //    {
        //        //        Data = true,
        //        //        Message = "Pago realizado correctamente.",
        //        //        Time = DateTime.UtcNow,
        //        //        Success = true
        //        //    };
        //        //}
        //        if (!string.IsNullOrEmpty(storeProcedure.Error))
        //        {
        //            throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
        //        }

        //        // Verificas el valor de estadoIni para determinar el resultado
        //        if (estadoIni != -1)
        //        {
        //            return new ResponseApi<bool>
        //            {
        //                Data = true,
        //                Message = "Pago realizado correctamente.",
        //                Time = DateTime.UtcNow,
        //                Success = true
        //            };
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //Logger.Error("Exception: {0}", e.ToString());
        //        //Logger.Error("Message: {0}", e.Message);
        //    }
        //    return new ResponseApi<bool>
        //    {
        //        Message = "No se pudo hacer el pago",
        //        Time = DateTime.UtcNow,
        //        Success = false
        //    };

        //}


        public async Task<ResponseApi<bool>> RegistrarNuevaExtension(CreateDParqueoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarNuevoDetalleUsoParqueo]");
            var dataTable = await storeProcedure.ReturnRegistrarDetalleUsoParqueo(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Item2 != 0)
                {
                    return new ResponseApi<bool>
                    {
                        Data = true,
                        Message = "Solicitud registrada correctamente.",
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
                Message = "No se pudo hacer el registro",
                Time = DateTime.UtcNow,
                Success = false
            };
        }
    }
}
