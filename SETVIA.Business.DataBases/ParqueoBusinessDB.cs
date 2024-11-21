using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Parqueo;
using SETVIA.Core.Database.Repository.Parqueo;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.Parqueo;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.Feriado;

namespace SETVIA.Business.DataBases
{
    public class ParqueoBusinessDB
    {
        private readonly ParqueoRepository _setViaDB = new ParqueoRepository();
        public async Task<ResponseApi<bool>> RegistrarParqueo(CreateParqueoParameter param)
        {
            return await _setViaDB.RegistrarParqueo(param);
        }
        public async Task<ResponseApi<IEnumerable<ListadoParqueo>>> ObtieneListaParqueo()
        {
            return await _setViaDB.ObtieneListaParqueo();
        }
        public async Task<ResponseApi<bool>> UpdateParqueo(UpdateParqueoParameter param)
        {
            return await _setViaDB.UpdateParqueo(param);
        }
        public async Task<ResponseApi<ParqueoReadID>> ComparatorReadParqueo(ReadIDParqueo param)
        {
            return await _setViaDB.ComparatorReadIDParqueo(param);
        }
    }

    //lista parqueo vigentes
}
