using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.DeudasPagos;
using SETVIA.Core.Database.Repository.DeudaPago;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;

namespace SETVIA.Business.DataBases
{
    public class DeudasPagosBusinessDB
    {
        private readonly DeudaPagoRepository _setViaDB = new DeudaPagoRepository();
        public async Task<ResponseApi<DeudaParqueoRead>> ConsultaDeuda(ReadDeudaParqueo param)
        {
            return await _setViaDB.ComparatorReadDeudaPago(param);
        }
        public async Task<ResponseApi<bool>> UpdatePagodeudaUsoParqueo(UpdatePagoDeudaUsoParqueoParameter param)
        {
             return await _setViaDB.RegistradoPagoDeudaUsoParqueo(param);    
        }

        public async Task<ResponseApi<bool>> RegistrarDetalleParqueoPrueba(CreateDParqueoParameter param)
        {
            return await _setViaDB.RegistrarDetalleParqueo(param);
        }
    }


}
