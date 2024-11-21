using Newtonsoft.Json;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Feriado;
using SETVIA.Util.Api.Model.Response.Administrador;
using System.Data;
using SETVIA.Util.Api.Model.Response.Feriado;
using SETVIA.Util.Api.Model.Parameter.Administrador;

namespace SETVIA.Core.Database.Repository.Feriado
{
    public class FeriadoRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();

        public async Task<ResponseApi<bool>> RegistrarDiaFeriado(CreateFeriadoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarDiasFeriado]");
            var dataTable = await storeProcedure.ReturnRegistrarDFeriado(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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

        public async Task<ResponseApi<IEnumerable<ListadoFeriado>>> ObtieneListaDiaFeriado()
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarDiasFeriado]");
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var response = new List<ListadoFeriado>();
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
                        response.Add(Listado_Dias_Feriado(reader));
                    }
                    return new ResponseApi<IEnumerable<ListadoFeriado>>
                    {
                        Data = response,
                        Message = "Datos Obtenidos correctamente",
                        Time = DateTime.UtcNow,
                        Success = true,
                        ///TotalRows = response.Count
                    };
                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<IEnumerable<ListadoFeriado>>
            {
                Message = "No hay datos que mostrar",
                Time = DateTime.UtcNow,
                Success = false
            };
        }

        private ListadoFeriado Listado_Dias_Feriado(DataRow reader)
        {
            return new ListadoFeriado
            {
                Id_Feriado = reader["Id_Feriado"] != DBNull.Value ? (Int32)reader["Id_Feriado"] : 0,
                Dias = Convert.ToDateTime(reader["Dias"]),
                Detalle = reader["Detalle"].ToString(),
                Estado = reader["Estado"].ToString(),
                Usuario_Creacion = reader["Usuario_Creacion"].ToString(),
                Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                Usuario_Modificacion = reader["Usuario_Modificacion"].ToString(),
                Fecha_Modificacion = reader["Usuario_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,

            };
        }


        public async Task<ResponseApi<bool>> UpdateDiaFeriado(UpdateFeriadoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarFeriado]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateDiaFeriado(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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

        public async Task<ResponseApi<FeriadoReadID>> ComparatorReadIDFeriado(ReadIdFeriado param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarDiaFeriadoPorID]");
            storeProcedure.AddParameterAsync("@Id_Feriado", param.Id_Feriado);
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new FeriadoReadID();
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    result.Id_Feriado = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
                    result.Dias = Convert.ToDateTime(dataTable.Rows[0].ItemArray[1].ToString());
                    result.Detalle = dataTable.Rows[0].ItemArray[2].ToString();
                    result.Estado = dataTable.Rows[0].ItemArray[3].ToString();
                    result.Usuario_Creacion = dataTable.Rows[0].ItemArray[4].ToString();
                    result.Fecha_Creacion = Convert.ToDateTime(dataTable.Rows[0].ItemArray[5].ToString());
                    result.Usuario_Modificacion = dataTable.Rows[0].ItemArray[6].ToString();
                    result.Fecha_Modificacion = Convert.ToDateTime(dataTable.Rows[0].ItemArray[7].ToString());

                  
                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<FeriadoReadID>
            {
                Data = result,
                Message = "Datos Obtenidos correctamente",
                Time = DateTime.UtcNow,
                Success = true,
                //TotalRows = result.Id_Feriado
            };
        }

    }
}
