using Newtonsoft.Json;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Personal;
using SETVIA.Util.Api.Model.Response.Administrador;
using System.Data;
using SETVIA.Util.Api.Model.Response.Personal;
using SETVIA.Util.Api.Model.Parameter.Administrador;

namespace SETVIA.Core.Database.Repository.Personal
{
    public class PersonalRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<bool>> RegistrarPersonal(CreatePersonalParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarUsuarioPersonal]");
            var dataTable = await storeProcedure.ReturnRegistrarPersonal(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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

        public async Task<ResponseApi<IEnumerable<ListadoPersonal>>> ObtieneListaPersonal()
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarPersonal]");
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var response = new List<ListadoPersonal>();
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
                        response.Add(Listado_Vigente_Personal(reader));
                    }
                    return new ResponseApi<IEnumerable<ListadoPersonal>>
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
            return new ResponseApi<IEnumerable<ListadoPersonal>>
            {
                Message = "No hay datos que mostrar",
                Time = DateTime.UtcNow,
                Success = false
            };
        }

        private ListadoPersonal Listado_Vigente_Personal(DataRow reader)
        {
            return new ListadoPersonal
            {
                Id_Personal = reader["Id_Personal"] != DBNull.Value ? (Int32)reader["Id_Personal"] : 0,
                Id_Empresa = reader["Id_Empresa"] != DBNull.Value ? (Int32)reader["Id_Empresa"] : 0,
                Id_Usuario = reader["Id_Usuario"] != DBNull.Value ? (Int32)reader["Id_Usuario"] : 0,
                //Id_Tipo_Personal = reader["Id_Tipo_Personal"] != DBNull.Value ? (Int32)reader["Id_Tipo_Personal"] : 0,
                Nombres = reader["Nombres"].ToString(),
                Ap_Paterno = reader["Ap_Paterno"].ToString(),
                Ap_Materno = reader["Ap_Materno"].ToString(),
                CI = reader["CI"] != DBNull.Value ? (Int32)reader["CI"] : 0,
                Extension = reader["Extension"].ToString(),
                Complemento = reader["Complemento"].ToString(),
                Genero = reader["Genero"].ToString(),
                Direccion = reader["Direccion"].ToString(),
                Correo = reader["Correo"].ToString(),
                Celular = reader["Celular"] != DBNull.Value ? (Int32)reader["Celular"] : 0,
                //Fecha_Alta = Convert.ToDateTime(reader["Fecha_Alta"]),
                //Fecha_Baja = reader["Fecha_Baja"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Baja"]) : (DateTime?)null,
                Estado = reader["Estado"].ToString(),
                Usuario_Creacion = reader["Usuario_Creacion"].ToString(),
                Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]), // Asumiendo que Fecha_Creacion siempre tiene un valor
                Usuario_Modificacion = reader["Usuario_Modificacion"].ToString(),
                // Para Fecha_Modificacion, verificar si es nulo antes de convertir
                Fecha_Modificacion = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fec_Modificacion"]) : (DateTime?)null,
                //Usuario = reader["Usuario"].ToString(),

                Usuario = reader["Usuario"].ToString(),
                Contrasena = reader["Contrasena"].ToString(),
                EstadoUsuario = reader["EstadoUsuario"].ToString(),
                Id_Perfil = reader["Id_Perfil"] != DBNull.Value ? (Int32)reader["Id_Perfil"] : 0,
                Usuario_CreacionUsuario = reader["Usuario_Creacion"].ToString(),
                Fecha_CreacionUsuario = Convert.ToDateTime(reader["Fecha_Creacion"]),
                Usuario_ModificacionUsuario = reader["Usuario_Modificacion"].ToString(),
                Fecha_ModificacionUsuario = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,
            };
        }

        public async Task<ResponseApi<bool>> UpdatePersonalActivo(UpdatePersonalParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarPersonal]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdatePersonalActivo(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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


        public async Task<ResponseApi<PersonalReadID>> ComparatorReadIDPersonal(ReadIDPersonal param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarPersonalPorID]");
            storeProcedure.AddParameterAsync("@Id_Personal", param.Id_Personal);
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new PersonalReadID();
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    result.Id_Personal = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
                    result.Id_Empresa = Convert.ToInt32(dataTable.Rows[0].ItemArray[1]);
                    result.Id_Usuario = Convert.ToInt32(dataTable.Rows[0].ItemArray[2]);
                  //  result.Id_Tipo_Personal = Convert.ToInt32(dataTable.Rows[0].ItemArray[3]);
                    result.Nombres = dataTable.Rows[0].ItemArray[4].ToString();
                    result.Ap_Paterno = dataTable.Rows[0].ItemArray[5].ToString();
                    result.Ap_Materno = dataTable.Rows[0].ItemArray[6].ToString();
                    result.CI = Convert.ToInt32(dataTable.Rows[0].ItemArray[7]);
                    result.Extension = dataTable.Rows[0].ItemArray[8].ToString();
                    result.Complemento = dataTable.Rows[0].ItemArray[9].ToString();
                    result.Genero = dataTable.Rows[0].ItemArray[10].ToString();
                    result.Direccion = dataTable.Rows[0].ItemArray[11].ToString();
                    result.Correo = dataTable.Rows[0].ItemArray[12].ToString();
                    result.Celular = Convert.ToInt32(dataTable.Rows[0].ItemArray[13]);
                    //result.Usuario = dataTable.Rows[0].ItemArray[11].ToString();
                    //result.Contrasena = dataTable.Rows[0].ItemArray[12].ToString(); //reader.IsDBNull(10) ? null : (byte[])reader[10];
                    result.Estado = dataTable.Rows[0].ItemArray[14].ToString();
                    result.Usuario_Creacion = dataTable.Rows[0].ItemArray[15].ToString();
                    result.Fecha_Creacion = Convert.ToDateTime(dataTable.Rows[0].ItemArray[16].ToString());
                    result.Usuario_Modificacion = dataTable.Rows[0].ItemArray[17].ToString();
                    result.Fecha_Modificacion = dataTable.Rows[0].ItemArray[18] != DBNull.Value && !string.IsNullOrWhiteSpace(dataTable.Rows[0].ItemArray[18].ToString()) ? Convert.ToDateTime(dataTable.Rows[0].ItemArray[18].ToString()) : (DateTime?)null;
                    //result.Fecha_ModificacionAdmin = dataTable.Rows[0].ItemArray[15] != DBNull.Value && !string.IsNullOrWhiteSpace(dataTable.Rows[0].ItemArray[15].ToString())? Convert.ToDateTime(dataTable.Rows[0].ItemArray[15].ToString()): (DateTime?)null;

                   // result.Id_Usuario = Convert.ToInt32(dataTable.Rows[0].ItemArray[19]);
                    result.Usuario = dataTable.Rows[0].ItemArray[19].ToString();
                    result.Contrasena = dataTable.Rows[0].ItemArray[20].ToString();
                    result.EstadoUsuario = dataTable.Rows[0].ItemArray[21].ToString();
                    result.Id_Perfil = Convert.ToInt32(dataTable.Rows[0].ItemArray[22]);
                    result.Usuario_CreacionUsuario = dataTable.Rows[0].ItemArray[23].ToString();
                    result.Fecha_CreacionUsuario = Convert.ToDateTime(dataTable.Rows[0].ItemArray[24].ToString());
                    result.Usuario_ModificacionUsuario = dataTable.Rows[0].ItemArray[25].ToString();
                    result.Fecha_ModificacionUsuario = dataTable.Rows[0].ItemArray[26] != DBNull.Value && !string.IsNullOrWhiteSpace(dataTable.Rows[0].ItemArray[26].ToString()) ? Convert.ToDateTime(dataTable.Rows[0].ItemArray[26].ToString()) : (DateTime?)null;

                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<PersonalReadID>
            {
                Data = result,
                Message = "Datos Obtenidos correctamente",
                Time = DateTime.UtcNow,
                Success = true,
                //TotalRows = result.Id_Personal
            };
        }


    }
}
