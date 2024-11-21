using Newtonsoft.Json;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Parqueo;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Response.Administrador;
using System.Data;
using SETVIA.Util.Api.Model.Response.DetalleParqueo;
using SETVIA.Util.Api.Model.Response.Empresa;

namespace SETVIA.Core.Database.Repository.DetalleParqueo
{
    public class DetalleParqueoRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();






        //
        public async Task<ResponseApi<bool>> RegistrarDetalleParqueoPrueba(CreatePruebaUsoDetalleParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarNuevoDetalleUsotiempoParqueo]");
            var dataTable = await storeProcedure.ReturnRegistrarusotiempo(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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


        //
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
        //update teiempo
        public async Task<ResponseApi<bool>> UpdateTiempoUsoParqueo(UpdateTiempoUsoParqueoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarNuevoDetalleUsotiempoParqueo]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateTiempoPsrqueo(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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
                        Message = "DAtos Actualizados correctamente.",
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
                Message = "No se pudo hacer la actualizacion",
                Time = DateTime.UtcNow,
                Success = false
            };

        }




        public async Task<ResponseApi<bool>> UpdatePagoUsoParqueo(UpdatePagoUsoParquepParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarPagoDetalleDEUsoParqueo]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdatePagoUsoPago(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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
                        Message = "DAtos Actualizados correctamente.",
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
                Message = "No se pudo hacer la actualizacion",
                Time = DateTime.UtcNow,
                Success = false
            };

        }


        //listVigentes
        public async Task<ResponseApi<IEnumerable<ListadoTipoEstadoV>>> ObtieneListaTipoEstadoVigente()
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarPorEstadoVehiculoVigente]");
            // storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var response = new List<ListadoTipoEstadoV>();
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {Newtonsoft.Json.JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow reader in dataTable.Rows)
                    {
                        response.Add(Listado_Estado_Vehiculo(reader));
                    }
                    return new ResponseApi<IEnumerable<ListadoTipoEstadoV>>
                    {
                        Data = response,
                        Message = "Datos Obtenidos correctamente",
                        Time = DateTime.UtcNow,
                        Success = true,
                        //TotalRows = response.Count
                    };
                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<IEnumerable<ListadoTipoEstadoV>>
            {
                Message = "No hay datos que mostrar",
                Time = DateTime.UtcNow,
                Success = false
            };
        }



        public async Task<ResponseApi<bool>> UpdateEstadoConluido(UpdateModificarAConcluidoParquepParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarEstadoVigenteAConcluido]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateVigenteAConluido(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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
                        Message = "DAtos Actualizados correctamente.",
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
                Message = "No se pudo hacer la actualizacion",
                Time = DateTime.UtcNow,
                Success = false
            };

        }

        public async Task<ResponseApi<IEnumerable<ListadoTipoEstadoV>>> ObtieneListaTipoEstado()
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarPorEstadoVehiculoConcluido]");
            // storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var response = new List<ListadoTipoEstadoV>();
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {Newtonsoft.Json.JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow reader in dataTable.Rows)
                    {
                        response.Add(Listado_Estado_Vehiculo(reader));
                    }
                    return new ResponseApi<IEnumerable<ListadoTipoEstadoV>>
                    {
                        Data = response,
                        Message = "Datos Obtenidos correctamente",
                        Time = DateTime.UtcNow,
                        Success = true,
                       // TotalRows = response.Count
                    };
                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<IEnumerable<ListadoTipoEstadoV>>
            {
                Message = "No hay datos que mostrar",
                Time = DateTime.UtcNow,
                Success = false
            };
        }


        private ListadoTipoEstadoV Listado_Estado_Vehiculo(DataRow reader)
        {
            return new ListadoTipoEstadoV
            {
                Id_Detalle = reader["Id_Detalle"] != DBNull.Value ? (Int32)reader["Id_Detalle"] : 0,
                Id_Parqueo = reader["Id_Parqueo"] != DBNull.Value ? (Int32)reader["Id_Parqueo"] : 0,
                Id_TipoVehiculo = reader["Id_TipoVehiculo"] != DBNull.Value ? (Int32)reader["Id_TipoVehiculo"] : 0,
                Placa = reader["Placa"].ToString(),
                Tiempo_Ocupar = (TimeSpan)reader["Tiempo_Ocupar"],
                Hora_Inicio = (TimeSpan)reader["Hora_Inicio"],
                Hora_Final = (TimeSpan)reader["Hora_Final"],
                Costo_Parqueo = Convert.ToDecimal(reader["Costo_Parqueo"]),
                Estado_Vehiculo = reader["Estado_Vehiculo"].ToString(),
                Usuario_Creacion = reader["Usuario_Creacion"].ToString(),
                Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]), // Asumiendo que Fecha_Creacion siempre tiene un valor
                Usuario_Modificacion = reader["Usuario_Modificacion"].ToString(),
                Fecha_Modificacion = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,
                Id_TipoPago = reader["Id_TipoPago"] != DBNull.Value ? (Int32)reader["Id_TipoPago"] : 0,
                Id_Tarifario = reader["Id_Tarifario"] != DBNull.Value ? (Int32)reader["Id_Tarifario"] : 0,

                NroTransaccion = reader["NroTransaccion"].ToString(),
                Estado_Pago = reader["Estado_Pago"].ToString(),

            };
        }

        public async Task<ResponseApi<PlacaRead>> ComparatorReadPlaca(ReadPlaca param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarPendientesUsoParqueo]");
            storeProcedure.AddParameterAsync("@Placa", param.Placa);
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new PlacaRead();
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    result.Id_Detalle = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
                    result.Id_Parqueo = Convert.ToInt32(dataTable.Rows[0].ItemArray[1]);
                    result.Id_TipoVehiculo = Convert.ToInt32(dataTable.Rows[0].ItemArray[2]);
                    result.Placa = dataTable.Rows[0].ItemArray[3].ToString();
                    result.Tiempo_Ocupar = dataTable.Rows[0].ItemArray[4].ToString();
                    result.Costo_Parqueo = Convert.ToDecimal(dataTable.Rows[0].ItemArray[5]);
                    result.Id_TipoPago = Convert.ToInt32(dataTable.Rows[0].ItemArray[6]);
                    result.Id_Tarifario = Convert.ToInt32(dataTable.Rows[0].ItemArray[7]);
                    result.Codigo_Pago = dataTable.Rows[0].ItemArray[8].ToString();
                    result.Estado_Pago = dataTable.Rows[0].ItemArray[9].ToString();
                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<PlacaRead>
            {
                Data = result,
                Message = "Datos Obtenidos correctamente",
                Time = DateTime.UtcNow,
                Success = true,
                //TotalRows = result.Id_Detalle
            };
        }
    }
}
