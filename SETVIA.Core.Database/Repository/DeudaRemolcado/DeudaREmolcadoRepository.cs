using Newtonsoft.Json;
using SETVIA.Util.Api.Model.Parameter.DeudaInmovilizado;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Response.DeudaREmolque;
using SETVIA.Util.Api.Model.Parameter.DeudaRemolque;

namespace SETVIA.Core.Database.Repository.DeudaRemolcado
{
    public class DeudaREmolcadoRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<DeudaRemolqueParqueoRead>> ConsultaDeudaRemolcado(ReadDeudaRemolqueParqueo param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarDeudaRemolcadoParqueo]");

            storeProcedure.AddParameterAsync("@Placa", param.Placa);


            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new DeudaRemolqueParqueoRead();
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    result.Codigo_Parqueo = dataTable.Rows[0].ItemArray[0].ToString();
                    result.Direccion = dataTable.Rows[0].ItemArray[1].ToString();
                    result.TipoVehiculo = dataTable.Rows[0].ItemArray[2].ToString();
                    result.Placa = dataTable.Rows[0].ItemArray[3].ToString();
                    result.Monto_Infraccion = Convert.ToDecimal(dataTable.Rows[0].ItemArray[4]);
                    result.Servicio_Infraccion = Convert.ToDecimal(dataTable.Rows[0].ItemArray[5]);
                    result.Total_InfraccionRemolcado = Convert.ToDecimal(dataTable.Rows[0].ItemArray[6]);
                    result.NroTransaccion = dataTable.Rows[0].ItemArray[7].ToString();
                    result.Tiempo_InicioRemolcado = (TimeSpan)dataTable.Rows[0].ItemArray[8];

                    result.Estado = dataTable.Rows[0].ItemArray[9].ToString();

                }
            }
            catch (Exception e)
            {
                //Logger.Error("Exception: {0}", e.ToString());
                //Logger.Error("Message: {0}", e.Message);
            }
            return new ResponseApi<DeudaRemolqueParqueoRead>
            {
                Data = result,
                Message = "Datos Obtenidos correctamente",
                Time = DateTime.UtcNow,
                Success = true
                //TotalRows = result
            };
        }


        public async Task<ResponseApi<bool>> UpdatePagoDeudaRemolqueParqueo(UpdatePagoInfracionUsoParqueoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[PagoRemolqueParqueo]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);

            storeProcedure.AddParameterAsync("@Placa", param.Placa);
            storeProcedure.AddParameterAsync("@NroTransaccion", param.NroTransaccion);
            storeProcedure.AddParameterAsync("@Codigo_Parqueo", param.Codigo_Parqueo);
            var result = await storeProcedure.ReturnUpdatePagodeudaUsoPagoIR(ConectionString.GetParameterConnectionString(), TimeOutDB, param);

            DataTable dataTable = result.Item1; // Aquí obtienes el DataTable
            int estadoIni = result.Item2;

            try
            {
              
                if (!string.IsNullOrEmpty(storeProcedure.Error))
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }

                // Verificas el valor de estadoIni para determinar el resultado
                if (estadoIni != -1)
                {
                    return new ResponseApi<bool>
                    {
                        Data = true,
                        Message = "Pago realizado correctamente.",
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
                Message = "No se pudo hacer el pago",
                Time = DateTime.UtcNow,
                Success = false
            };

        }


        public async Task<ResponseApi<DeudaRemolcadorParqueoRead>> ConsultaDeudaRemolque(ReadinmovilizadoParqueo param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[BuscarDeudaPorRemolqueParqueo]");

            storeProcedure.AddParameterAsync("@Placa", param.Placa);

            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var result = new DeudaRemolcadorParqueoRead()
            {
                ListaDeudas = new List<ListadoRemolques>() // Inicializar la lista
            };
            var response = new ResponseApi<DeudaRemolcadorParqueoRead>
            {
                Data = result,
                Time = DateTime.UtcNow,
                Message = "Datos obtenidos correctamente.",
                Success = true,
                Error = 2
            };
            try
            {
                if (storeProcedure.Error != string.Empty)
                {
                    throw new Exception($"StoreProcedure: {JsonConvert.SerializeObject(storeProcedure)}");
                }
                if (dataTable.Rows.Count > 0)
                {
                    var firstRow = dataTable.Rows[0];
                    result.Direccion = firstRow["Direccion"].ToString();
                    result.TipoVehiculo = firstRow["TipoVehiculo"].ToString();
                    result.Placa = firstRow["Placa"].ToString();
                    result.Id_Detalle = Convert.ToInt32(firstRow["Id_Detalle"]);

                }
            }
            catch (Exception e)
            {
                
                response.Message = $"Error al obtener los datos: {e.Message}";
                response.Success = false;
                response.Error = 1;
            }
           
            return response;
        }
        public async Task<ResponseApi<IEnumerable<ListadoRemolques>>> ObtieneListaRemolcado()
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarInfraccionRemolcado]");
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);
            var response = new List<ListadoRemolques>();
            var apiResponse = new ResponseApi<IEnumerable<ListadoRemolques>>
            {
                Message = "DAtos Obtenidos correctamente",
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
                        response.Add(Listado_InfraccionRemolque(reader));
                    }
                   
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
           
        }

        private ListadoRemolques Listado_InfraccionRemolque(DataRow reader)
        {
            return new ListadoRemolques
            {

                Id_Infraccion_Remolque = reader["Id_Infraccion_Remolque"] != DBNull.Value ? (Int32)reader["Id_Infraccion_Remolque"] : 0,
                Id_Detalle = reader["Id_Detalle"] != DBNull.Value ? (Int32)reader["Id_Detalle"] : 0,
                Id_Costo_Infraccion = reader["Id_Costo_Infraccion"] != DBNull.Value ? (Int32)reader["Id_Costo_Infraccion"] : 0,
                Id_Parqueo = reader["Id_Parqueo"] != DBNull.Value ? (Int32)reader["Id_Parqueo"] : 0,
                Id_TipoPago = reader["Id_TipoPago"] != DBNull.Value ? (Int32)reader["Id_TipoPago"] : 0,
                Placa = reader["Placa"].ToString(),
                Monto_Infraccion = Convert.ToDecimal(reader["Monto_Infraccion"]),
                Servicio_Infraccion = Convert.ToDecimal(reader["Servicio_Infraccion"]),
                Total_InfraccionRemolcado = Convert.ToDecimal(reader["Total_InfraccionRemolcado"]),
                Tiempo_InicioRemolcado = (TimeSpan)reader["Tiempo_InicioRemolcado"],
                NroTransaccion = reader["NroTransaccion"].ToString(),
                Estado = reader["Estado"].ToString(),
                Usuario_Creacion = reader["Usuario_Creacion"].ToString(),
                Fecha_Creacion = Convert.ToDateTime(reader["Fecha_Creacion"]),
                Usuario_Modificacion = reader["Usuario_Modificacion"].ToString(),
                Fecha_Modificacion = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,

               
            };
        }
    }
}
