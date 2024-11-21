using Newtonsoft.Json;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Tarifario;
using SETVIA.Util.Api.Model.Response.Administrador;
using System.Data;
using SETVIA.Util.Api.Model.Response.Tarifario;
using SETVIA.Util.Api.Model.Parameter.Administrador;

namespace SETVIA.Core.Database.Repository.Tarifario
{
    public class TarifarioRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<bool>> RegistrarTarifario(CreateTarifarioParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarTarifarios]");
            var dataTable = await storeProcedure.ReturnRegistrarTarifario(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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


        public async Task<ResponseApi<IEnumerable<ListadoTarifario>>> ObtieneListaTarifario()
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarTarifario]");
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var response = new List<ListadoTarifario>();
            var apiResponse = new ResponseApi<IEnumerable<ListadoTarifario>>
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
                        response.Add(Listado_Vigente_Tarifario(reader));
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
        private ListadoTarifario Listado_Vigente_Tarifario(DataRow reader)
        {
            return new ListadoTarifario
            {

                Id_Tarifario = reader["Id_Tarifario"] != DBNull.Value ? (Int32)reader["Id_Tarifario"] : 0,
                Id_TipoVehiculo = reader["Id_TipoVehiculo"] != DBNull.Value ? (Int32)reader["Id_TipoVehiculo"] : 0,
                Id_TipoTarifario = reader["Id_TipoTarifario"] != DBNull.Value ? (Int32)reader["Id_TipoTarifario"] : 0,
                Tiempo = (TimeSpan)reader["Tiempo"],
                CostoTarifario = Convert.ToDecimal(reader["CostoTarifario"]),
                //Estado = reader["Estado"].ToString(),
                //Usuario_Creacion = reader["Usuario_Creacion"].ToString(),
                //Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                //Usuario_Modificacion = reader["Usuario_Modificacion"].ToString(),
                //Fecha_Modificacion = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,

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


        public async Task<ResponseApi<bool>> UpdateTarifario(UpdateTarifarioParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarTarifario]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateTarifario(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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
                        Message = "Datos Actualizados correctamente.",
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



        public async Task<ResponseApi<TarifarioReadID>> ComparatorReadIDTarifario(ReadIDTarifario param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarTarifarioPorID]");
            storeProcedure.AddParameterAsync("@Id_Tarifario", param.Id_Tarifario);
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new TarifarioReadID();
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                   
                    result.Id_Tarifario = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
                    result.Id_TipoVehiculo = Convert.ToInt32(dataTable.Rows[0].ItemArray[1]);
                    result.Id_TipoTarifario = Convert.ToInt32(dataTable.Rows[0].ItemArray[2]);
                    //result.Tiempo = TimeSpan dataTable.Rows[0].ItemArray[3].ToString();
                    result.Tiempo = TimeSpan.Parse(dataTable.Rows[0].ItemArray[3].ToString());
                    result.CostoTarifario = Convert.ToDecimal(dataTable.Rows[0].ItemArray[4]);
                    result.Estado = dataTable.Rows[0].ItemArray[5].ToString();
                    result.Usuario_Creacion = dataTable.Rows[0].ItemArray[6].ToString();
                    result.Fecha_Creacion = Convert.ToDateTime(dataTable.Rows[0].ItemArray[7].ToString());
                    result.Usuario_Modificacion = dataTable.Rows[0].ItemArray[8].ToString();
                    result.Fecha_Modificacion = dataTable.Rows[0].ItemArray[9] != DBNull.Value && !string.IsNullOrWhiteSpace(dataTable.Rows[0].ItemArray[24].ToString()) ? Convert.ToDateTime(dataTable.Rows[0].ItemArray[24].ToString()) : (DateTime?)null;

                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<TarifarioReadID>
            {
                Data = result,
                Message = "Datos Obtenidos correctamente",
                Time = DateTime.UtcNow,
                Success = true,
                //TotalRows = result.Id_Tarifario
            };
        }
    }
}
