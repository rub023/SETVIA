using Newtonsoft.Json;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Horario;
using SETVIA.Util.Api.Model.Response.Administrador;
using System.Data;
using SETVIA.Util.Api.Model.Response.Horario;
using SETVIA.Util.Api.Model.Parameter.Administrador;

namespace SETVIA.Core.Database.Repository.Horario
{
    public class HorarioRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<bool>> RegistrarHorario(CreateHorarioParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarHorario]");
            var dataTable = await storeProcedure.ReturnRegistrarHorario(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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




        public async Task<ResponseApi<IEnumerable<ListadoHorario>>> ObtieneListaHorario()
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarHorario]");
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var response = new List<ListadoHorario>();
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
                        response.Add(Listado_Vigente_Horario(reader));
                    }
                    return new ResponseApi<IEnumerable<ListadoHorario>>
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
            return new ResponseApi<IEnumerable<ListadoHorario>>
            {
                Message = "No hay datos que mostrar",
                Time = DateTime.UtcNow,
                Success = false
            };
        }

        private ListadoHorario Listado_Vigente_Horario(DataRow reader)
        {
            return new ListadoHorario
            {
                Id_Horario = reader["Id_Horario"] != DBNull.Value ? (Int32)reader["Id_Horario"] : 0,
                Id_Parqueo = reader["Id_Parqueo"] != DBNull.Value ? (Int32)reader["Id_Parqueo"] : 0,
                DiaSemana = reader["DiaSemana"].ToString(),
                Horario_Inicio = (TimeSpan)reader["Horario_Inicio"],
                Horario_Fin = (TimeSpan)reader["Horario_Fin"],
                Estado = reader["Estado"].ToString(),
                Usuario_Creacion = reader["Usuario_Creacion"].ToString(),
                Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                Usuario_Modificacion = reader["Usuario_Modificacion"].ToString(),
                Fecha_Modificacion = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,
                
               
               
               
            };
        }


        public async Task<ResponseApi<bool>> UpdateHorario(UpdateHorarioParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarHorario]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateHorario(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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


        public async Task<ResponseApi<HorarioReadID>> ComparatorReadHorario(ReadIDHorario param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarHorarioPorID]");
            storeProcedure.AddParameterAsync("@Id_Horario", param.Id_Horario);
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new HorarioReadID();
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    result.Id_Horario = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
                    result.Id_Parqueo = Convert.ToInt32(dataTable.Rows[0].ItemArray[1]);
                    result.NombreParqueo = dataTable.Rows[0].ItemArray[2].ToString();
                    result.DiaSemana = dataTable.Rows[0].ItemArray[3].ToString();
                    result.Horario_Inicio = (TimeSpan)(dataTable.Rows[0].ItemArray[4]);
                    result.Horario_Fin = (TimeSpan)dataTable.Rows[0].ItemArray[5];
                    result.Estado = dataTable.Rows[0].ItemArray[6].ToString();
                    result.Usuario_Creacion = dataTable.Rows[0].ItemArray[7].ToString();
                    result.Fecha_Creacion = Convert.ToDateTime(dataTable.Rows[0].ItemArray[8].ToString());
                    result.Usuario_Modificacion = dataTable.Rows[0].ItemArray[9].ToString();
                    result.Fecha_Modificacion = Convert.ToDateTime(dataTable.Rows[0].ItemArray[10].ToString());


                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<HorarioReadID>
            {
                Data = result,
                Message = "Datos Obtenidos correctamente",
                Time = DateTime.UtcNow,
                Success = true,
                //TotalRows = result.Id_Horario
            };
        }

    }
}
