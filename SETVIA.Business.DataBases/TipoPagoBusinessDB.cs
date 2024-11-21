using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Core.Database.Repository.TipoPago;
using SETVIA.Util.Api.Model.Parameter.TipoPago;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.TipoPago;

namespace SETVIA.Business.DataBases
{
    public class TipoPagoBusinessDB
    {
        private readonly TipoPagoRepository _setViaDB = new TipoPagoRepository();
        public async Task<ResponseApi<bool>> RegistrarTipoPago(CreateTipoPagoParameter param)
        {
            return await _setViaDB.RegistrarTipoPago(param);
        }
        public async Task<ResponseApi<IEnumerable<ListadoTipoPago>>> ObtieneListaTipoPago()
        {
            return await _setViaDB.ObtieneListaTipoPago();
        }
    }
}
