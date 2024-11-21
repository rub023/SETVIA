using SETVIA.Core.Database.Repository.DeudaPago;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.DeudasPagos;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Core.Database.Repository.DeudaExtension;
using SETVIA.Util.Api.Model.Parameter.ExtensionParqueo;
using SETVIA.Util.Api.Model.Parameter.PagoExtension;

namespace SETVIA.Business.DataBases
{
   
    public class DeudaExtensionBusinessDB
    {
        private readonly DeudaExtensionRepository _setViaDB = new DeudaExtensionRepository();
        public async Task<ResponseApi<DeudaExtensionRead>> ConsultaDeudaExtens(ReadDeudaParqueo param)
        {
            return await _setViaDB.ComparatorReadDeudaPago(param);
        }
        //public async Task<ResponseApi<bool>> UpdatePagodeudaExtensionUsoParqueo(UpdatePagoDeudaUsoParqueoParameter param)
        //{
        //    return await _setViaDB.UpdatePagoDeudaextensionUsoParqueo(param);
        //}

        public async Task<ResponseApi<bool>> UpdatePagodeudaExtensionUsoParqueo(UpdatePagoExtensionUsoParqueoParameter param)
        {
            return await _setViaDB.RegistrarPagoDeudaextensionUsoParqueo(param);
        }        //public async Task<ResponseApi<bool>> RegistrarNuevaExtension(CreateExtensionParqueoParameter param)
        //{
        //    return await _setViaDB.RegistrarNuevaExtension(param);

        //}
    }
}
