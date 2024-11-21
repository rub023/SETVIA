using SETVIA.Core.Database.Repository.DetalleParqueo;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Core.Database.Repository.Remolcado;
using SETVIA.Util.Api.Model.Parameter.Remolcado;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
using SETVIA.Util.Api.Model.Response.DeudaREmolque;
using SETVIA.Core.Database.Repository.DeudaRemolcado;

namespace SETVIA.Business.DataBases
{
    public class RemolcadoBusinessDB
    {
        private readonly RemolcadoRepository _setViaDB = new RemolcadoRepository();
        private readonly DeudaREmolcadoRepository _setViaDB1 = new DeudaREmolcadoRepository();
        public async Task<ResponseApi<bool>> RegistrarRemolcado(CreateRemolcadoParameter param)
        {
            return await _setViaDB.RegistrarRemolcado(param);
        }
        public async Task<ResponseApi<bool>> UpdatePagoRemolcado(UpdatePagoRemolcadoParameter param)
        {
            return await _setViaDB.UpdatePagoRemolcado(param);
        }
        public async Task<ResponseApi<bool>> UpdateLiberadoInm(UpdateModificarRemolcadoALiberadoParameter param)
        {
            return await _setViaDB.UpdateEstadoLiberado(param);
        }
        public async Task<ResponseApi<bool>> UpdateLibConcluido(UpdateModificarRLiberadoAConcluidoParameter param)
        {
            return await _setViaDB.UpdaterRemLibConcluido(param);
        }

        public async Task<ResponseApi<DeudaRemolcadorParqueoRead>> ConsultaDeudaRemolque(ReadinmovilizadoParqueo param)
        {
            return await _setViaDB1.ConsultaDeudaRemolque(param);
        }
       
    }
}
