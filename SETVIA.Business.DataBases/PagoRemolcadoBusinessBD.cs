using SETVIA.Util.Api.Model.Parameter.PagoInmovil;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Core.Database.Repository.PagoInmovilizado;
using SETVIA.Core.Database.Repository.PagoRemolcado;
using SETVIA.Util.Api.Model.Parameter.PagoRemolcado;

namespace SETVIA.Business.DataBases
{
    public class PagoRemolcadoBusinessBD
    {
        private readonly PagoRemolcadoRepository _setViaDB = new PagoRemolcadoRepository();
        public async Task<ResponseApi<bool>> RegistrarRemolcado1(PagoRemolcadoParameter param)
        {
            return await _setViaDB.RegistrarRemolcado1(param);
        }
    }
}
