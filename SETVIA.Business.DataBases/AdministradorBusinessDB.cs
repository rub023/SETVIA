using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Response.Administrador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Business.DataBases
{
    public class AdministradorBusinessDB
    {
        private readonly AdministradorRepository _setViaDB = new AdministradorRepository();
        public async Task<ResponseApi<bool>> RegistrarAdministrador(CreateAdministradorParameter param)
        {
            return await _setViaDB.RegistrarAdministrador(param);
        }
        public async Task<ResponseApi<IEnumerable<ListadoAdministrador>>> ObtieneListaAdministrador()
        {
            return await _setViaDB.ObtieneListaAdministrador();
        }
        public async Task<ResponseApi<bool>> UpdateAdministrador(UpdateAdministradorParameter param)
        {
            return await _setViaDB.UpdateAdministrador(param);
        }
        public async Task<ResponseApi<AdministradorReadID>> ComparatorReadAdministrador(ReadIDAdministrador param)
        {
            return await _setViaDB.ComparatorReadIDAdministrador(param);
        }
    }
}
