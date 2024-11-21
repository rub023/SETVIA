using SETVIA.Core.Database.Repository.DetalleParqueo;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Inmovilizado;
using SETVIA.Core.Database.Repository.Inmovilizado;
using SETVIA.Util.Api.Model.Parameter.ExtensionParqueo;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.Tarifario;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;

namespace SETVIA.Business.DataBases
{
    public class InmovilizadoBusinessDB
    {
        private readonly InmovilizadoRepository _setViaDB = new InmovilizadoRepository();
        public async Task<ResponseApi<bool>> RegistrarInmovilizado(CreateInmovilizadoParameter param)
        {
            return await _setViaDB.RegistrarInmovilizado(param);
        }
        public async Task<ResponseApi<bool>> UpdatePagoInmovilizadoParqueo(UpdatePagInmovilizadoParameter param)
        {
            return await _setViaDB.UpdatePagoInmovilizadoParqueo(param);
        }
        public async Task<ResponseApi<bool>> UpdateLiberadoAConcluido(UpdateModificarLieradoAConcluidoParquepParameter param)
        {
            return await _setViaDB.UpdateEstadoLiberadoConluido(param);
        }
        public async Task<ResponseApi<IEnumerable<ListadoInmovilizado>>> ObtieneListaInmovilizado(ReadDeudaParqueo param)
        {

            return await _setViaDB.ObtieneListaInmovilizado();


            
        }
        public async Task<ResponseApi<IEnumerable<ListadoInmovilizado>>> ObtieneListaInmovilizado(ReadinmovilizadoParqueo param)
        {

            return await _setViaDB.ObtieneListaInmovilizado();



        }
    }
}
