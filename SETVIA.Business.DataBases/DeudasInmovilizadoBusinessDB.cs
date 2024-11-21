using SETVIA.Core.Database.Repository.DeudaPago;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.DeudasPagos;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Core.Database.Repository.DeudaInmovilizado;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
using SETVIA.Util.Api.Model.Parameter.DeudaInmovilizado;

namespace SETVIA.Business.DataBases
{
    public class DeudasInmovilizadoBusinessDB
    {
        private readonly DeudaInmovilizadoRepository _setViaDB = new DeudaInmovilizadoRepository();
        //public async Task<Response<DeudaInmovilizadoParqueoRead>> ConsultaDeudaInmovilizado(ReadDeudaInmovilizadoParqueo param)
        //{
        //    return await _setViaDB.ConsultaDeudaInmovilizado(param);
        //}
        public async Task<ResponseApi<DeudaInmovilizadoParqueoRead>> ConsultaDeudaInmovilizado(ReadinmovilizadoParqueo param)
        {
            return await _setViaDB.ConsultaDeudaInmovilizado(param);
        }
        public async Task<ResponseApi<bool>> UpdatePagodeudaInmovilizadoParqueo(UpdatePagoInfracionUsoParqueoParameter param)
        {
            return await _setViaDB.UpdatePagoDeudaInmovilParqueo(param);
        }
    }
}
