using SETVIA.Core.Database.Repository.Tarifario;
using SETVIA.Util.Api.Model.Parameter.Tarifario;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Core.Database.Repository.PagoInmovilizado;
using SETVIA.Util.Api.Model.Parameter.PagoInmovil;

namespace SETVIA.Business.DataBases
{
    public class PagoInmovilizadoBusinessDB
    {
        private readonly PagoInmovilizadoRepository _setViaDB = new PagoInmovilizadoRepository();
       
        public async Task<ResponseApi<bool>> RegistrarInmovilizado1(PagoInmovilizadoParameter param)
        {
            return await _setViaDB.RegistrarInmovili(param);
        }

    }
}
