using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Core.Database.Repository.Extension;
using SETVIA.Util.Api.Model.Parameter.ExtensionParqueo;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api.Model.Response.DetalleParqueo;

namespace SETVIA.Business.DataBases
{
    
    public class ExtensionPBusinessDB
    {
        private readonly ExtensionRepository _setViaDB = new ExtensionRepository();
        public async Task<ResponseApi<bool>> RegistrarNuevaExtension(CreateExtensionParqueoParameter param)
        {
            return await _setViaDB.RegistrarNuevoExtension(param);

        }

        public async Task<ResponseApi<bool>> UpdateTiempoExtensiondeParqueo(UpdateExtensiondeTiempoParqueoParameter param)
        {
            return await _setViaDB.UpdateTiempoExtensionParqueo(param);
        }

        public async Task<ResponseApi<bool>> UpdatePagoExtensionParqueo(UpdatePagoExtensionParquepParameter param)
        {
            return await _setViaDB.UpdatePagoExtensionUsoParqueo(param);
        }
        public async Task<ResponseApi<bool>> UpdateExtenConcluidoParqueo(UpdateModificarExtenAConcluidoParquepParameter param)
        {
            return await _setViaDB.UpdateEstadoExtAConcl(param);
        }
        public async Task<ResponseApi<PlacaERead>> ComparatorReadEPlaca(ReadPlaca param)
        {
            return await _setViaDB.ComparatorReadEPlaca(param);
        }
    }
}
