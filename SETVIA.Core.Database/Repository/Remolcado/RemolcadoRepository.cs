using Newtonsoft.Json;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Remolcado;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
using SETVIA.Util.Api.Model.Response.DeudaREmolque;
using System.Data;

namespace SETVIA.Core.Database.Repository.Remolcado
{
    public class RemolcadoRepository
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<bool>> RegistrarRemolcado(CreateRemolcadoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarEstadoInmovilizadoARemolcado]");
            var dataTable = await storeProcedure.ReturnRegistrarRemolcado(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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


        public async Task<ResponseApi<bool>> UpdatePagoRemolcado(UpdatePagoRemolcadoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarPagoRemolcado]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateRemolcado(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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


        public async Task<ResponseApi<bool>> UpdateEstadoLiberado(UpdateModificarRemolcadoALiberadoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarEstadoRemolcadoAInmobilizado]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateRemolcadoLiberado(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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


        public async Task<ResponseApi<bool>> UpdaterRemLibConcluido(UpdateModificarRLiberadoAConcluidoParameter param)
        {
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ModificarEstadoRemolcadoLiberadoInmAConcluido]");
            //storeProcedure.AddParameterAsync("@Id", param.Id);
            var dataTable = await storeProcedure.ReturnUpdateRemolcadoLiberadoAConcluido(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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


        


       
    }
}
