using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.CostoInfraccion;
using SETVIA.Util.Api.Model.Response;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.CostoInfraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Business.DataBases
{
    public class CostoInfraccionBusinessDB
    {
        private readonly CostoInfraccionRepository _setViaDB = new CostoInfraccionRepository();
        public async Task<ResponseApi<bool>> RegistrarCostoInfraccion(CreateCostoInfraccionParameter param)
        {
            return await _setViaDB.RegistrarCostoInfracciones(param);
        }
        public async Task<ResponseApi<IEnumerable<ListCostoInfraccion>>> ObtieneLista()
        {
            return await _setViaDB.ObtieneLista();
        }
        public async Task<ResponseApi<bool>> UpdateCostoInfraccion(UpdateCostoInfraccionParameter param)
        {
            return await _setViaDB.UpdateCostoInfraccion(param);
        }
        public async Task<ResponseApi<CostoInfraccionReadID>> ComparatorostoInfraccionId(ReadIDCostoInfraccion param)
        {
            return await _setViaDB.ComparatorReadIDCostoInfraccion(param);
        }
    }
}
