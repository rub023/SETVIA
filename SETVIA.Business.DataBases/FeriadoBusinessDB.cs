using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Feriado;
using SETVIA.Core.Database.Repository.Feriado;
using SETVIA.Util.Api.Model.Response.Feriado;

namespace SETVIA.Business.DataBases
{
    public class FeriadoBusinessDB
    {
        private readonly FeriadoRepository _setViaDB = new FeriadoRepository();
        public async Task<ResponseApi<bool>> RegistrarDiaFeriado(CreateFeriadoParameter param)
        {
            return await _setViaDB.RegistrarDiaFeriado(param);
        }
        public async Task<ResponseApi<IEnumerable<ListadoFeriado>>> ObtieneListaDiaFeriado()
        {
            return await _setViaDB.ObtieneListaDiaFeriado();
        }
        public async Task<ResponseApi<bool>> UpdateDiaFeriado(UpdateFeriadoParameter param)
        {
            return await _setViaDB.UpdateDiaFeriado(param);
        }
        public async Task<ResponseApi<FeriadoReadID>> ComparatorReadFeriado(ReadIdFeriado param)
        {
            return await _setViaDB.ComparatorReadIDFeriado(param);
        }
    }
}
