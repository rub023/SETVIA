using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Personal;
using SETVIA.Core.Database.Repository.Personal;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.Empresa;
using SETVIA.Util.Api.Model.Response.Personal;
using SETVIA.Util.Api.Model.Parameter.Administrador;

namespace SETVIA.Business.DataBases
{
    public class PersonalBusinessDB
    {
        private readonly PersonalRepository _setViaDB = new PersonalRepository();
        public async Task<ResponseApi<bool>> RegistrarPersonal(CreatePersonalParameter param)
        {
            return await _setViaDB.RegistrarPersonal(param);
        }
        public async Task<ResponseApi<IEnumerable<ListadoPersonal>>> ObtieneListaPersonal()
        {
            return await _setViaDB.ObtieneListaPersonal();
        }
        public async Task<ResponseApi<bool>> UpdatePersonalActivo(UpdatePersonalParameter param)
        {
            return await _setViaDB.UpdatePersonalActivo(param);
        }
        public async Task<ResponseApi<PersonalReadID>> ComparatorReadPersonal(ReadIDPersonal param)
        {
            return await _setViaDB.ComparatorReadIDPersonal(param);
        }
    }
}
