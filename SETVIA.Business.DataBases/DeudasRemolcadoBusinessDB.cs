using SETVIA.Core.Database.Repository.DeudaInmovilizado;
using SETVIA.Util.Api.Model.Parameter.DeudaInmovilizado;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Core.Database.Repository.DeudaRemolcado;
using SETVIA.Util.Api.Model.Response.DeudaREmolque;
using SETVIA.Util.Api.Model.Parameter.DeudaRemolque;

namespace SETVIA.Business.DataBases
{
    public class DeudasRemolcadoBusinessDB
    {
        private readonly DeudaREmolcadoRepository _setViaDB = new DeudaREmolcadoRepository();
        public async Task<ResponseApi<DeudaRemolqueParqueoRead>> ConsultaDeudaRemolcado(ReadDeudaRemolqueParqueo param)
        {
            return await _setViaDB.ConsultaDeudaRemolcado(param);
        }
        public async Task<ResponseApi<bool>> UpdatePagodeudaremolcadoParqueo(UpdatePagoInfracionUsoParqueoParameter param)
        {
            return await _setViaDB.UpdatePagoDeudaRemolqueParqueo(param);
        }
        public async Task<ResponseApi<IEnumerable<ListadoRemolques>>> ObtieneListaRemolcador(ReadinmovilizadoParqueo param)
        {

            return await _setViaDB.ObtieneListaRemolcado();
        }
    }
}
