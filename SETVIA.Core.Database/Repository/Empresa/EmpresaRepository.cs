using Newtonsoft.Json;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Empresa;
using SETVIA.Util.Api.Model.Response.Administrador;
using System.Data;
using SETVIA.Util.Api.Model.Response.Empresa;
using SETVIA.Util.Api.Model.Parameter.Administrador;

namespace SETVIA.Core.Database.Repository.Empresa
{
    public class EmpresaRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<bool>> RegistrarEmpresa(CreateEmpresaParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarUsuarioEmpresa]");
            var dataTable = await storeProcedure.ReturnRegistrarEmpresa(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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

        public async Task<ResponseApi<IEnumerable<ListadoEmpresa>>> ObtieneListaEmpresa()
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarEmpresa]");
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var response = new List<ListadoEmpresa>();
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
                        response.Add(Listado_Vigente_Empresa(reader));
                    }
                    return new ResponseApi<IEnumerable<ListadoEmpresa>>
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
            return new ResponseApi<IEnumerable<ListadoEmpresa>>
            {
                Message = "No hay datos que mostrar",
                Time = DateTime.UtcNow,
                Success = false
            };
        }
        private ListadoEmpresa Listado_Vigente_Empresa(DataRow reader)
        {
            return new ListadoEmpresa
            {
                Id_Empresa = reader["Id_Empresa"] != DBNull.Value ? (Int32)reader["Id_Empresa"] : 0,
                Id_Admin = reader["Id_Admin"] != DBNull.Value ? (Int32)reader["Id_Admin"] : 0,
                Id_Usuario = reader["Id_Usuario"] != DBNull.Value ? (Int32)reader["Id_Usuario"] : 0,
                Nombre_Empresa = reader["Nombre_Empresa"].ToString(),
                Representante = reader["Representante"].ToString(),
                Nit = reader["Nit"] != DBNull.Value ? (Int32)reader["Nit"] : 0,
                Direccion = reader["Direccion"].ToString(),
                Correo = reader["Correo"].ToString(),                
                Telefono = reader["Telefono"] != DBNull.Value ? (Int32)reader["Telefono"] : 0,
                Estado = reader["Estado"].ToString(),
                Usuario_Creacion = reader["Usuario_Creacion"].ToString(),
                Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]), // Asumiendo que Fecha_Creacion siempre tiene un valor
                Usuario_Modificacion = reader["Usuario_Modificacion"].ToString(),
                Fecha_Modificacion = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,

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


        public async Task<ResponseApi<bool>> UpdateEmpresa(UpdateEmpresaParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarEmpresa]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateEmpresa(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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

        //public async Task<ResponseApi<bool>> UpdateEmpresaEstadoo(UpdateEmpresaEstadoParameter param)
        //{
        //    StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarEstadoEmpresa]");
        //    //storeProcedure.AddParameterAsync("@Id", param.Id);
        //    var dataTable = await storeProcedure.ReturnUpdateEmpresaEstado(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
        //    try
        //    {
        //        if (storeProcedure.Error != string.Empty)
        //        {
        //            throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
        //        }
        //        if (dataTable.Item2 != 0)
        //        {
        //            return new ResponseApi<bool>
        //            {
        //                Data = true,
        //                Message = "DAtos Actualizados correctamente.",
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
        //        Message = "No se pudo hacer la actualizacion",
        //        Time = DateTime.UtcNow,
        //        Success = false
        //    };

        //}


        public async Task<ResponseApi<EmpresaReadID>> ComparatorReadIDEmpresa(ReadIDEmpresa param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarEmpresaPorID]");
            storeProcedure.AddParameterAsync("@Id_Empresa", param.Id_Empresa);
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new EmpresaReadID();
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    result.Id_Empresa = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
                    result.Id_Admin = Convert.ToInt32(dataTable.Rows[0].ItemArray[1]);
                    result.Nombre_Empresa = dataTable.Rows[0].ItemArray[2].ToString();
                    result.Representante = dataTable.Rows[0].ItemArray[3].ToString();
                    result.Nit = Convert.ToInt32(dataTable.Rows[0].ItemArray[4]);
                    result.Direccion = dataTable.Rows[0].ItemArray[5].ToString();
                    result.Correo = dataTable.Rows[0].ItemArray[6].ToString();
                    result.Telefono = Convert.ToInt32(dataTable.Rows[0].ItemArray[7]);                
                    result.Estado = dataTable.Rows[0].ItemArray[8].ToString();
                    result.Usuario_Creacion = dataTable.Rows[0].ItemArray[9].ToString();
                    result.Fecha_Creacion = Convert.ToDateTime(dataTable.Rows[0].ItemArray[10].ToString());
                    result.Usuario_Modificacion = dataTable.Rows[0].ItemArray[11].ToString();
                    result.Fecha_Modificacion = dataTable.Rows[0].ItemArray[12] != DBNull.Value && !string.IsNullOrWhiteSpace(dataTable.Rows[0].ItemArray[12].ToString()) ? Convert.ToDateTime(dataTable.Rows[0].ItemArray[12].ToString()) : (DateTime?)null;


                    result.Id_Usuario = Convert.ToInt32(dataTable.Rows[0].ItemArray[13]);
                    result.Usuario = dataTable.Rows[0].ItemArray[14].ToString();
                    result.Contrasena = dataTable.Rows[0].ItemArray[15].ToString();
                    result.EstadoUsuario = dataTable.Rows[0].ItemArray[16].ToString();
                    result.Id_Perfil = Convert.ToInt32(dataTable.Rows[0].ItemArray[17]);
                    result.Usuario_CreacionUsuario = dataTable.Rows[0].ItemArray[18].ToString();
                    result.Fecha_CreacionUsuario = Convert.ToDateTime(dataTable.Rows[0].ItemArray[19].ToString());
                    result.Usuario_ModificacionUsuario = dataTable.Rows[0].ItemArray[20].ToString();
                    result.Fecha_ModificacionUsuario = dataTable.Rows[0].ItemArray[21] != DBNull.Value && !string.IsNullOrWhiteSpace(dataTable.Rows[0].ItemArray[21].ToString()) ? Convert.ToDateTime(dataTable.Rows[0].ItemArray[21].ToString()) : (DateTime?)null;
                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<EmpresaReadID>
            {
                Data = result,
                Message = "Datos Obtenidos correctamente",
                Time = DateTime.UtcNow,
                Success = true,
                //TotalRows = result.Id_Empresa
            };
        }
    }
}
