using Newtonsoft.Json;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.Empresa;
using SETVIA.Util.Api.Model.Parameter.Personal;

namespace SETVIA.Core.Database.Repository.Login
{
    public class LoginRepository
    {
        //public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        //public static DBHelper ConectionString = new DBHelper();
        //public async Task<ResponseApi<bool>> RegistrarAdministradorLogin(CreateAdminLoginParameter param)
        //{
        //    StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarLoginAdmin]");
        //    var dataTable = await storeProcedure.ReturnRegistrarAdminLogin(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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
        //                Message = "Solicitud registrada correctamente.",
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
        //        Message = "No se pudo hacer el registro",
        //        Time = DateTime.UtcNow,
        //        Success = false
        //    };
        //}
        //public async Task<ResponseApi<bool>> RegistrarEmpresaLogin(CreateEmpresaLoginParameter param)
        //{
        //    StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarLoginEmpresa]");
        //    var dataTable = await storeProcedure.ReturnRegistrarEmpresLogin(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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
        //                Message = "Solicitud registrada correctamente.",
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
        //        Message = "No se pudo hacer el registro",
        //        Time = DateTime.UtcNow,
        //        Success = false
        //    };
        //}
        //public async Task<ResponseApi<bool>> RegistrarPersonalLogin(CreatePersonaLoginParameter param)
        //{
        //    StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[InsertarLoginPersonal]");
        //    var dataTable = await storeProcedure.ReturnRegistrarPersonalLogin(ConectionString.GetParameterConnectionString(), TimeOutDB, param);
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
        //                Message = "Solicitud registrada correctamente.",
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
        //        Message = "No se pudo hacer el registro",
        //        Time = DateTime.UtcNow,
        //        Success = false
        //    };
        //}
    }
}
