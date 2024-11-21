using Newtonsoft.Json;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.DeudasPagos;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using System.Data;
using SETVIA.Util.Api.Model.Response.Tarifario;

namespace SETVIA.Core.Database.Repository.DeudaPago
{
    public class DeudaPagoRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<DeudaParqueoRead>> ComparatorReadDeudaPago(ReadDeudaParqueo param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarTipoVehiculoYParqueo]");
            storeProcedure.AddParameterAsync("@Codigo_Parqueo", param.Codigo_Parqueo);
            storeProcedure.AddParameterAsync("@TipoVehiculo", param.TipoVehiculo);
            //storeProcedure.AddParameterAsync("@Placa", param.Placa);

            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new DeudaParqueoRead
            {
                ListaDeudas = new List<ListadoTarifario>() // Inicializar la lista
            };
            var response = new ResponseApi<DeudaParqueoRead>
            {
                Data = result,
                Time = DateTime.UtcNow,
                Message = "Datos obtenidos correctamente",
                Success = true,
                Error=2
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
                   // result.Placa = firstRow["Placa"].ToString();
                   result.Placa=param.Placa;
                    result.Id_TipoVehiculo = Convert.ToInt32(firstRow["Id_TipoVehiculo"]);

                    //var tarifarioResponse = await _setViaBussDB.ObtieneListaTarifario();

                    //if (tarifarioResponse.Success && tarifarioResponse.Data != null)
                    //{
                    //    // Filtrar tarifarios según el tipo de vehículo
                    //    var tarifariosFiltrados = tarifarioResponse.Data
                    //        .Where(t => t.TipoVehiculo == param.TipoVehiculo) // Asegúrate de que exista esta propiedad en `ListadoTarifario`
                    //        .ToList();

                    //    result.ListaDeudas.AddRange(tarifariosFiltrados);
                    //}
                    //else
                    //{
                    //    response.Message = "No se encontraron tarifarios";
                    //    response.Success = false;
                    //}
                    //foreach (DataRow row in dataTable.Rows)
                    //{


                    //    //if (row["TipoVehiculo"].ToString() == param.TipoVehiculo && row["CodigoParqueo"].ToString() == param.Codigo_Parqueo)
                    //    if (row["Id_TipoVehiculo"].ToString() == uoo.ToString())
                    //    {
                    //        result.ListaDeudas.Add(new ListadoTarifario
                    //        {
                    //            // Id_TipoTarifario = row["Id_TipoTarifario"].ToString(),
                    //            Tiempo = (TimeSpan)(row["Tiempo"]),
                    //            CostoTarifario = Convert.ToDecimal(row["CostoTarifario"])
                    //        });
                    //    }
                    //}
                }
                else 
                {
                    response.Message = "No se encontraron datos registrados";
                    response.Success = false;
                    response.Error = 1;
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
            return response;
            //return new ResponseApi<DeudaParqueoRead>
            //{
            //    Data = result,
            //    Message = "Datos Obtenidos correctamente",
            //    Time = DateTime.UtcNow,
            //    Success = true
            //    //TotalRows = result
            //};
        }

        public async Task<ResponseApi<bool>> UpdatePagoDeudaUsoParqueo(UpdatePagoDeudaUsoParqueoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[PagarDeudaUsoParqueo1]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
          //  storeProcedure.AddParameterAsync("@Id_Detalle", param.Id_Detalle);
            storeProcedure.AddParameterAsync("@Placa", param.Placa);
            storeProcedure.AddParameterAsync("@Codigo_Parqueo ", param.Codigo_Parqueo);
            storeProcedure.AddParameterAsync("@Tipo_Vehiculo", param.Tipo_Vehiculo);
            //storeProcedure.AddParameterAsync("@Tiempo_Ocupar", param.Tiempo_Ocupar);
            //storeProcedure.AddParameterAsync("@Hora_Inicio", param.Hora_Inicio);
            //storeProcedure.AddParameterAsync("@Hora_Final", param.Hora_Final);
           // storeProcedure.AddParameterAsync("@Costo_Parqueo", param.Costo_Parqueo);
          //  storeProcedure.AddParameterAsync("@Id_Tarifario", param.Id_Tarifario);
            //storeProcedure.AddParameterAsync("@NroTransaccionPago", param.NroTransaccionPago);



            //storeProcedure.AddParameterAsync("@Codigo_Parqueo", param.Codigo_Parqueo);
            var result = await storeProcedure.ReturnUpdatePagodeudaUsoPago(ConectionString.GetParameterConnectionString(), TimeOutDB, param);

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

        //public async Task<PagoResponse<bool>> UpdatePagoDeudaUsoParqueo(UpdatePagoDeudaUsoParqueoParameter param)
        //{

        //    //StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[PagarDeudaUsoParqueo]");
        //    ////storeProcedure.AddParameterAsync("@Id", param.Id);

        //    //storeProcedure.AddParameterAsync("@Placa", param.Placa);
        //    //storeProcedure.AddParameterAsync("@NroTransaccion", param.NroTransaccion);

        //    ////storeProcedure.AddParameterAsync("@Codigo_Parqueo", param.Codigo_Parqueo);
        //    //var result = await storeProcedure.ReturnUpdatePagodeudaUsoPago(ConectionString.GetParameterConnectionString(), TimeOutDB, param);

        //    //DataTable dataTable = result.Item1; // Aquí obtienes el DataTable
        //    //int estadoIni = result.Item2;

        //    //try
        //    //{

        //    //    if (!string.IsNullOrEmpty(storeProcedure.Error))
        //    //    {
        //    //        throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
        //    //    }

        //    //    // Verificas el valor de estadoIni para determinar el resultado
        //    //    if (estadoIni != -1)
        //    //    {
        //    //        return new PagoResponse<bool>
        //    //        {
        //    //            Data = true,
        //    //            MensajeError = "Pago realizado correctamente."
        //    //            //Time = DateTime.UtcNow,
        //    //            //Success = true
        //    //        };
        //    //    }
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    //Logger.Error("Exception: {0}", e.ToString());
        //    //    //Logger.Error("Message: {0}", e.Message);
        //    //}
        //    //return new PagoResponse<bool>
        //    //{
        //    //    //Message = "No se pudo hacer el pago",
        //    //    //Time = DateTime.UtcNow,
        //    //    //Success = false
        //    //};
        //    ///////////////////////////////
        //    /// StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[PagarDeudaUsoParqueo]");
        //     //StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[PagarDeudaUsoParqueo]");
        //    ////storeProcedure.AddParameterAsync("@Id", param.Id);

        //    //storeProcedure.AddParameterAsync("@Placa", param.Placa);
        //    //storeProcedure.AddParameterAsync("@NroTransaccion", param.NroTransaccion);
        //    StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[PagarDeudaUsoParqueo]");
        //    storeProcedure.AddParameterAsync("@Placa", param.Placa);
        //    storeProcedure.AddParameterAsync("@NroTransaccion", param.NroTransaccion);

        //    var result = await storeProcedure.ReturnUpdatePagodeudaUsoPago(ConectionString.GetParameterConnectionString(), TimeOutDB, param);

        //    DataTable dataTable = result.Item1; // Aquí obtienes el DataTable
        //    int estadoIni = result.Item2;

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(storeProcedure.Error))
        //        {
        //            throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
        //        }

        //        // Si el estadoIni es positivo, significa que se procesó correctamente
        //        if (estadoIni != -1)
        //        {
        //            return new PagoResponse<bool>
        //            {
        //                Data = true,
        //                MensajeError = "Pago realizado correctamente."
        //            };
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        // Manejar el error aquí
        //    }

        //    return new PagoResponse<bool>
        //    {
        //        Data = false,
        //        MensajeError = "Error al procesar el pago."
        //    };

        //}
        public async Task<ResponseApi<bool>> RegistradoPagoDeudaUsoParqueo(UpdatePagoDeudaUsoParqueoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[PagarDeudaUsoParqueo1]");

            var dataTable = await storeProcedure.ReturnRegistrarPAGODetalleUsoParqueo(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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



        public async Task<ResponseApi<bool>> RegistrarDetalleParqueo(CreateDParqueoParameter param)
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
