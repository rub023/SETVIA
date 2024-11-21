using SETVIA.Util.Api.Model.Response;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter;
using Newtonsoft.Json;
using System.Data;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Parameter.Administrador;

namespace SETVIA.Core.Database.Repository.CostoInfraccion
{
    public class AdministradorRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<bool>> RegistrarAdministrador(CreateAdministradorParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarUsuarioAdministrador]");
            var dataTable = await storeProcedure.ReturnRegistrarAdmin(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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
        
        //Listado_Vigente_Administrador
        public async Task<ResponseApi<IEnumerable<ListadoAdministrador>>> ObtieneListaAdministrador()
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarAdministrador]");
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var response = new List<ListadoAdministrador>();
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
                        response.Add(Listado_Vigente_Administrador(reader));
                    }
                    return new ResponseApi<IEnumerable<ListadoAdministrador>>
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
            return new ResponseApi<IEnumerable<ListadoAdministrador>>
            {
                Message = "No hay datos que mostrar",
                Time = DateTime.UtcNow,
                Success = false
            };
        }
        private ListadoAdministrador Listado_Vigente_Administrador(DataRow reader)
        {
            return new ListadoAdministrador
            {
                Id_Admin = reader["Id_Admin"] != DBNull.Value ? (Int32)reader["Id_Admin"] : 0,
                Nombres = reader["Nombres"].ToString(),
                Ap_Paterno = reader["Ap_PAterno"].ToString(),
                Ap_Materno = reader["Ap_Materno"].ToString(),
                CI = reader["CI"] != DBNull.Value ? (Int32)reader["CI"] : 0,
                Extension = reader["Extension"].ToString(),
                Complemento = reader["Complemento"].ToString(),
                Genero = reader["Genero"].ToString(),
                Direccion = reader["Direccion"].ToString(),
                Celular = reader["Celular"] != DBNull.Value ? (Int32)reader["Celular"] : 0,
                Correo = reader["Correo"].ToString(),
                EstadoAdmin = reader["Estado"].ToString(),               
                Usuario_CreacionAdmin = reader["Usuario_Creacion"].ToString(),
                Fecha_CreacionAdmin = Convert.ToDateTime(reader["Fecha_Creacion"]),
                Usuario_ModificacionAdmin = reader["Usuario_Modificacion"].ToString(),
                Fecha_ModificacionAdmin = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,
                
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

        public async Task<ResponseApi<bool>> UpdateAdministrador(UpdateAdministradorParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarAdministrador]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateAdministrador(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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


        public async Task<ResponseApi<AdministradorReadID>> ComparatorReadIDAdministrador(ReadIDAdministrador param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarAdministradorPorID]");
            storeProcedure.AddParameterAsync("@Id_Admin", param.Id_Admin);
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new AdministradorReadID();
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    result.Id_Admin = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
                    result.Nombres = dataTable.Rows[0].ItemArray[1].ToString();
                    result.Ap_Paterno = dataTable.Rows[0].ItemArray[2].ToString();                    
                    result.Ap_Materno = dataTable.Rows[0].ItemArray[3].ToString();
                    result.CI = Convert.ToInt32(dataTable.Rows[0].ItemArray[4]);
                    result.Extension = dataTable.Rows[0].ItemArray[5].ToString();
                    result.Complemento = dataTable.Rows[0].ItemArray[6].ToString();
                    result.Genero = dataTable.Rows[0].ItemArray[7].ToString();
                    result.Direccion = dataTable.Rows[0].ItemArray[8].ToString();
                    result.Correo = dataTable.Rows[0].ItemArray[9].ToString();
                    result.Celular = Convert.ToInt32(dataTable.Rows[0].ItemArray[10]);                   
                    //result.Usuario = dataTable.Rows[0].ItemArray[11].ToString();
                    //result.Contrasena = dataTable.Rows[0].ItemArray[12].ToString(); //reader.IsDBNull(10) ? null : (byte[])reader[10];
                    result.EstadoAdmin = dataTable.Rows[0].ItemArray[11].ToString();
                    result.Usuario_CreacionAdmin = dataTable.Rows[0].ItemArray[12].ToString();
                    result.Fecha_CreacionAdmin = Convert.ToDateTime(dataTable.Rows[0].ItemArray[13].ToString());
                    result.Usuario_ModificacionAdmin = dataTable.Rows[0].ItemArray[14].ToString();
                    result.Fecha_ModificacionAdmin = dataTable.Rows[0].ItemArray[15] != DBNull.Value && !string.IsNullOrWhiteSpace(dataTable.Rows[0].ItemArray[15].ToString()) ? Convert.ToDateTime(dataTable.Rows[0].ItemArray[15].ToString()) : (DateTime?)null;
                    //result.Fecha_ModificacionAdmin = dataTable.Rows[0].ItemArray[15] != DBNull.Value && !string.IsNullOrWhiteSpace(dataTable.Rows[0].ItemArray[15].ToString())? Convert.ToDateTime(dataTable.Rows[0].ItemArray[15].ToString()): (DateTime?)null;

                    result.Id_Usuario = Convert.ToInt32(dataTable.Rows[0].ItemArray[16]);
                    result.Usuario = dataTable.Rows[0].ItemArray[17].ToString();
                    result.Contrasena = dataTable.Rows[0].ItemArray[18].ToString();
                    result.EstadoUsuario = dataTable.Rows[0].ItemArray[19].ToString();
                    result.Id_Perfil = Convert.ToInt32(dataTable.Rows[0].ItemArray[20]);
                    result.Usuario_CreacionUsuario = dataTable.Rows[0].ItemArray[21].ToString();
                    result.Fecha_CreacionUsuario = Convert.ToDateTime(dataTable.Rows[0].ItemArray[22].ToString());
                    result.Usuario_ModificacionUsuario = dataTable.Rows[0].ItemArray[23].ToString();
                    result.Fecha_ModificacionUsuario = dataTable.Rows[0].ItemArray[24] != DBNull.Value && !string.IsNullOrWhiteSpace(dataTable.Rows[0].ItemArray[24].ToString()) ? Convert.ToDateTime(dataTable.Rows[0].ItemArray[24].ToString()) : (DateTime?)null;

                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<AdministradorReadID>
            {
                Data = result,
                Message = "Datos Obtenidos correctamente",
                Time = DateTime.UtcNow,
                Success = true,
                //TotalRows = result.Id_Admin
            };
        }
    }
}
