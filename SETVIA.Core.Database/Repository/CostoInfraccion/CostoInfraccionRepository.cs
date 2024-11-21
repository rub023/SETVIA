using Newtonsoft.Json;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.CostoInfraccion;
using SETVIA.Util.Api.Model.Response;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.CostoInfraccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Core.Database.Repository.CostoInfraccion
{
    public class CostoInfraccionRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();

        public async Task<ResponseApi<bool>> RegistrarCostoInfracciones(CreateCostoInfraccionParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarCostoPorInfraccion]");
            var dataTable = await storeProcedure.ReturnRegistrarCostoInfraccion(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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


        public async Task<ResponseApi<IEnumerable<ListCostoInfraccion>>> ObtieneLista()
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarCostoPorInfraccion]");
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var response = new List<ListCostoInfraccion>();
            if (storeProcedure.Error != string.Empty)
            {
                throw new Exception($"StoreProcedure: {Newtonsoft.Json.JsonConvert.SerializeObject(storeProcedure)}");
            }
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow reader in dataTable.Rows)
                {
                    response.Add(Listado_Registro_Vigentes(reader));
                }
                return new ResponseApi<IEnumerable<ListCostoInfraccion>>
                {
                    Data = response,
                    Message = "Datos Obtenidos correctamente",
                    Time = DateTime.UtcNow,
                    Success = true,
                   // TotalRows = response.Count
                };
            }
            return new ResponseApi<IEnumerable<ListCostoInfraccion>>
            {
                Message = "No hay datos que mostrar",
                Time = DateTime.UtcNow,
                Success = false
            };
        }

        private ListCostoInfraccion Listado_Registro_Vigentes(DataRow reader)
        {
            return new ListCostoInfraccion
            {
                Id_Costo_Infraccion = reader["Id_Costo_Infraccion"] != DBNull.Value ? (Int32)reader["Id_Costo_Infraccion"] : 0,
                Id_Empresa = reader["Id_Empresa"] != DBNull.Value ? (Int32)reader["Id_Empresa"] : 0,
                Id_Tipo_Infraccion = reader["Id_Tipo_Infraccion"] != DBNull.Value ? (Int32)reader["Id_Tipo_Infraccion"] : 0,
                Costo_Multa = Convert.ToDecimal(reader["Costo_Multa"]),
                Costo_Servicio = Convert.ToDecimal(reader["Costo_Servicio"]),
                Estado = reader["Estado"].ToString(),
                Usuario_Creacion = reader["Usuario_Creacion"].ToString(),
                Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                Usuario_Modificacion = reader["Usuario_Modificacion"].ToString(),
                Fecha_Modificacion = reader["Usuario_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,
            };
        }



        public async Task<ResponseApi<bool>> UpdateCostoInfraccion(UpdateCostoInfraccionParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarCostoPorInfraccion]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateCostoInfraccion(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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


        public async Task<ResponseApi<CostoInfraccionReadID>> ComparatorReadIDCostoInfraccion(ReadIDCostoInfraccion param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarCosto_Por_InfraccionPorID]");
            storeProcedure.AddParameterAsync("@Id_Costo_Infraccion", param.Id_Costo_Infraccion);
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new CostoInfraccionReadID();
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    result.Id_Costo_Infraccion = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
                    result.Id_Empresa = Convert.ToInt32(dataTable.Rows[0].ItemArray[1]);
                    result.Id_Tipo_Infraccion = Convert.ToInt32(dataTable.Rows[0].ItemArray[2]);
                    result.Costo_Multa = Convert.ToDecimal(dataTable.Rows[0].ItemArray[3]);
                    result.Costo_Servicio = Convert.ToDecimal(dataTable.Rows[0].ItemArray[4].ToString());
                    result.Estado = dataTable.Rows[0].ItemArray[5].ToString();
                    result.Usuario_Creacion = dataTable.Rows[0].ItemArray[6].ToString();
                    result.Fecha_Creacion = Convert.ToDateTime(dataTable.Rows[0].ItemArray[7].ToString());
                    result.Usuario_Modificacion = dataTable.Rows[0].ItemArray[8].ToString();
                    result.Fecha_Modificacion = Convert.ToDateTime(dataTable.Rows[0].ItemArray[9].ToString());
                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<CostoInfraccionReadID>
            {
                Data = result,
                Message = "Datos Obtenidos correctamente",
                Time = DateTime.UtcNow,
                Success = true,
                //TotalRows = result.Id_Costo_Infraccion
            };
        }
    }
}
