using Newtonsoft.Json;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Inmovilizado;
using SETVIA.Util.Api.Model.Parameter.ExtensionParqueo;
using SETVIA.Util.Api.Model.Parameter.Remolcado;
using SETVIA.Util.Api.Model.Response.Tarifario;
using System.Data;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;

namespace SETVIA.Core.Database.Repository.Inmovilizado
{
    public class InmovilizadoRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<bool>> RegistrarInmovilizado(CreateInmovilizadoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarEstadoVigenteAInmovilizado]");
            var dataTable = await storeProcedure.ReturnRegistrarInmovilizado(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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

        public async Task<ResponseApi<bool>> UpdatePagoInmovilizadoParqueo(UpdatePagInmovilizadoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarPagoInmovilizado]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdatePagoInmovilizado(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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

        public async Task<ResponseApi<bool>> UpdateEstadoLiberadoConluido(UpdateModificarLieradoAConcluidoParquepParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarEstadoLiberadoAConcluido]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateliberadoAConluido(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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

        public async Task<ResponseApi<IEnumerable<ListadoInmovilizado>>> ObtieneListaInmovilizado()
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarInfraccionInmovilizado]");
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var response = new List<ListadoInmovilizado>();
            var apiResponse = new ResponseApi<IEnumerable<ListadoInmovilizado>>
            {
                Message = "Datos Obtenidos correctamente",
                Time = DateTime.UtcNow,
                Success = true,
                Data = response
            };
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
                        response.Add(Listado_InfraccionInmovilizado(reader));
                    }
                    //apiResponse.TotalRows = response.Count;
                    //return new ResponseApi<IEnumerable<ListadoTarifario>>
                    //{
                    //    Data = response,
                    //    Message = "Datos Obtenidos correctamente",
                    //    Time = DateTime.UtcNow,
                    //    Success = true,
                    //    TotalRows = response.Count
                    //};
                }
                else
                {
                    apiResponse.Message = "No hay datos que mostrar";
                    apiResponse.Success = false;
                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
                apiResponse.Message = "Error al obtener los datos";
                apiResponse.Success = false;
            }
            return apiResponse;
            //return new ResponseApi<IEnumerable<ListadoTarifario>>
            //{
            //    Message = "No hay datos que mostrar",
            //    Time = DateTime.UtcNow,
            //    Success = false
            //};
        }
        //
        private ListadoInmovilizado Listado_InfraccionInmovilizado(DataRow reader)
        {
            return new ListadoInmovilizado
            {

                Id_Infraccion_Inmovilizado = reader["Id_Infraccion_Inmovilizado"] != DBNull.Value ? (Int32)reader["Id_Infraccion_Inmovilizado"] : 0,
                Id_Detalle = reader["Id_Detalle"] != DBNull.Value ? (Int32)reader["Id_Detalle"] : 0,
                Id_Costo_Infraccion = reader["Id_Costo_Infraccion"] != DBNull.Value ? (Int32)reader["Id_Costo_Infraccion"] : 0,
                Id_Parqueo = reader["Id_Parqueo"] != DBNull.Value ? (Int32)reader["Id_Parqueo"] : 0,
                Id_TipoPago = reader["Id_TipoPago"] != DBNull.Value ? (Int32)reader["Id_TipoPago"] : 0,
                Placa = reader["Placa"].ToString(),
                Monto_Infraccion = Convert.ToDecimal(reader["Monto_Infraccion"]),
                Servicio_Infraccion = Convert.ToDecimal(reader["Servicio_Infraccion"]),
                Total_InfraccionInmovilizado = Convert.ToDecimal(reader["Total_InfraccionInmovilizado"]),
                Tiempo_InicioInmovilizado = (TimeSpan)reader["Tiempo_InicioInmovilizado"],
                NroTransaccion = reader["NroTransaccion"].ToString(),
                Estado = reader["Estado"].ToString(),
                Usuario_Creacion = reader["Usuario_Creacion"].ToString(),
                Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                Usuario_Modificacion = reader["Usuario_Modificacion"].ToString(),
                Fecha_Modificacion = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,

                //Id_Tarifario = reader["Id_Tarifario"] != DBNull.Value ? (Int32)reader["Id_Tarifario"] : 0,
                //Id_TipoVehiculo = reader["Id_TipoVehiculo"] != DBNull.Value ? (Int32)reader["Id_TipoVehiculo"] : 0,
                //Id_TipoTarifario = reader["Id_TipoTarifario"] != DBNull.Value ? (Int32)reader["Id_TipoTarifario"] : 0,
                //Tiempo = (TimeSpan)reader["Tiempo"],
                //CostoTarifario = Convert.ToDecimal(reader["CostoTarifario"]),
                //Estado = reader["Estado"].ToString(),
                //Usuario_Creacion = reader["Usuario_Creacion"].ToString(),
                //Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),              
                //Usuario_Modificacion = reader["Usuario_Modificacion"].ToString(),
                //Fecha_Modificacion = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,

            };
        }
    }
}
