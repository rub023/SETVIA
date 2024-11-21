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
using SETVIA.Util.Api.Model.Response.Administrador;
using System.Data;
using SETVIA.Util.Api.Model.Response.Parqueo;
using SETVIA.Util.Api.Model.Parameter.Administrador;

namespace SETVIA.Core.Database.Repository.Parqueo
{
    public class ParqueoRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<bool>> RegistrarParqueo(CreateParqueoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarParqueo]");
            var dataTable = await storeProcedure.ReturnRegistrarParqueo(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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

        public async Task<ResponseApi<IEnumerable<ListadoParqueo>>> ObtieneListaParqueo()
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarParqueosActivos]");
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var response = new List<ListadoParqueo>();
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
                        response.Add(Listado_Vigentes_Parqueos(reader));
                    }
                    return new ResponseApi<IEnumerable<ListadoParqueo>>
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
            return new ResponseApi<IEnumerable<ListadoParqueo>>
            {
                Message = "No hay datos que mostrar",
                Time = DateTime.UtcNow,
                Success = false
            };
        }

        private ListadoParqueo Listado_Vigentes_Parqueos(DataRow reader)
        {
            return new ListadoParqueo
            {
                Id_Parqueo = reader["Id_Parqueo"] != DBNull.Value ? (Int32)reader["Id_Parqueo"] : 0,
                Id_Empresa = reader["Id_Empresa"] != DBNull.Value ? (Int32)reader["Id_Empresa"] : 0,
                Codigo_Parqueo = reader["Codigo_Parqueo"].ToString(),
                Ciudad = reader["Ciudad"].ToString(),
                //Nombre_Empresa = reader["Nombre_Empresa"].ToString(),
                NombreParqueo = reader["NombreParqueo"].ToString(),
                Direccion = reader["Direccion"].ToString(),
                Ubicacion = reader["Ubicacion"].ToString(),
                //Horario_Inicio = (TimeSpan)reader["Horario_Inicio"],
                //Horario_Fin = (TimeSpan)reader["Horario_Fin"],
                Estado = reader["Estado"].ToString(),
                Usuario_Creacion = reader["Usuario_Creacion"].ToString(),
                Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                Usuario_Modificacion = reader["Usuario_Modificacion"].ToString(),
                Fecha_Modificacion = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,


            };
        }
        public async Task<ResponseApi<bool>> UpdateParqueo(UpdateParqueoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarParqueo]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateParqueo(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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


        public async Task<ResponseApi<ParqueoReadID>> ComparatorReadIDParqueo(ReadIDParqueo param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarParqueoPorID]");
            storeProcedure.AddParameterAsync("@Id_Parqueo", param.Id_Parqueo);
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new ParqueoReadID();
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    result.Id_Parqueo = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
                    result.Id_Empresa = Convert.ToInt32(dataTable.Rows[0].ItemArray[1]);
                    result.Codigo_Parqueo = dataTable.Rows[0].ItemArray[2].ToString();
                    result.Ciudad = dataTable.Rows[0].ItemArray[3].ToString();
                    result.Nombre_Empresa = dataTable.Rows[0].ItemArray[4].ToString();
                    result.NombreParqueo = dataTable.Rows[0].ItemArray[5].ToString();
                    result.Direccion = dataTable.Rows[0].ItemArray[6].ToString();
                    result.Ubicacion = dataTable.Rows[0].ItemArray[7].ToString();
                    result.Horario_Inicio = (TimeSpan)dataTable.Rows[0].ItemArray[8];
                    result.Horario_Fin = (TimeSpan)dataTable.Rows[0].ItemArray[9];
                    result.Estado = dataTable.Rows[0].ItemArray[10].ToString();
                    result.Usuario_Creacion = dataTable.Rows[0].ItemArray[11].ToString();
                    //result.Usuario = dataTable.Rows[0].ItemArray[11].ToString();
                    //result.Contrasena = dataTable.Rows[0].ItemArray[12].ToString(); //reader.IsDBNull(10) ? null : (byte[])reader[10];

                    result.Fecha_Creacion = Convert.ToDateTime(dataTable.Rows[0].ItemArray[12].ToString());
                    result.Usuario_Modificacion = dataTable.Rows[0].ItemArray[13].ToString();
                    result.Fecha_Modificacion = Convert.ToDateTime(dataTable.Rows[0].ItemArray[14].ToString());


                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<ParqueoReadID>
            {
                Data = result,
                Message = "Datos Obtenidos correctamente",
                Time = DateTime.UtcNow,
                Success = true,
             //   TotalRows = result.Id_Parqueo
            };
        }
    }
}
