using SETVIA.Business.DataBases;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.Empresa;
using SETVIA.Util.Api.Model.Parameter.Personal;
using SETVIA.Util.Api.Model.Response.Administrador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SETVIA.Intrane.ApiWeb.Controllers
{
   // [RoutePrefix(EndpointHelper.CrearLoginPrefix)]
    public class LoginController : ApiController
    {
        //private readonly LoginBusinessDB _setViaBussDB = new LoginBusinessDB();
        ////Admin
        //[HttpPost]
        //[Route(EndpointHelper.AdministradirLoginCrear)]
        //public async Task<ResponseApi<bool>> CrearA(CreateAdminLoginParameter param)
        //{
        //    var resp = await _setViaBussDB.RegistrarAdministradorLogin(param);
        //    if (!resp.Data)
        //    {
        //        // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
        //    }   
        //    return resp;
        //}
        ////Empresa
        //[HttpPost]
        //[Route(EndpointHelper.EmpresaLoginCrear)]
        //public async Task<ResponseApi<bool>> CrearE(CreateEmpresaLoginParameter param)
        //{
        //    var resp = await _setViaBussDB.RegistrarEmpresaLogin(param);
        //    if (!resp.Data)
        //    {
        //        // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
        //    }
        //    return resp;
        //}
        ////Personal
        //[HttpPost]
        //[Route(EndpointHelper.PersonalLoginCrear)]
        //public async Task<ResponseApi<bool>> CrearP(CreatePersonaLoginParameter param)
        //{
        //    var resp = await _setViaBussDB.RegistrarPersonalLogin(param);
        //    if (!resp.Data)
        //    {
        //        // Logger.Debug($"No se pudo registrar en Marca {param.Idm}");
        //    }
        //    return resp;
        //}
    }
}
