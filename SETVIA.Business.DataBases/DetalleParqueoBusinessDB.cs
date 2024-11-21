using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Parqueo;
using SETVIA.Core.Database.Repository.DetalleParqueo;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.DetalleParqueo;
using SETVIA.Util.Api.Model.Response.Tarifario;
using SETVIA.Core.Database.Repository.Tarifario;

namespace SETVIA.Business.DataBases
{
    public class DetalleParqueoBusinessDB
    {
        private readonly DetalleParqueoRepository _setViaDB = new DetalleParqueoRepository();

        //preueba
        public async Task<ResponseApi<bool>> RegistrarDetalleParqueoPrueba(CreatePruebaUsoDetalleParameter param)
        {
            return await _setViaDB.RegistrarDetalleParqueoPrueba(param);
        }
        // private readonly TarifarioRepository _setViaDB1 = new TarifarioRepository();

        ///tiempo de uso 
        public async Task<ResponseApi<bool>> UpdateTiempoUsodeParqueo(UpdateTiempoUsoParqueoParameter param)
        {
            return await _setViaDB.UpdateTiempoUsoParqueo(param);
        }



        public async Task<ResponseApi<bool>> RegistrarDetalleParqueo(CreateDParqueoParameter param)
        {
            return await _setViaDB.RegistrarDetalleParqueo(param);
        }
        public async Task<ResponseApi<bool>> UpdatePagoUsoParqueo(UpdatePagoUsoParquepParameter param)
        {
            return await _setViaDB.UpdatePagoUsoParqueo(param);
        }
        //public async Task<ResponseApi<List<ListadoTarifario>>> ObtieneListaTarifario()
        //{
        //    return await _setViaDB1.ObtieneListaTarifario();
        //}
        //LISTADO POR ESTDO vigente
        public async Task<ResponseApi<IEnumerable<ListadoTipoEstadoV>>> ObtieneListaTipoEstadoVigente()
        {
            return await _setViaDB.ObtieneListaTipoEstadoVigente();
        }
        public async Task<ResponseApi<bool>> UpdateConcluidoParqueo(UpdateModificarAConcluidoParquepParameter param)
        {
            return await _setViaDB.UpdateEstadoConluido(param);
        }

        //LISTADO POR ESTDO
        public async Task<ResponseApi<IEnumerable<ListadoTipoEstadoV>>> ObtieneListaTipoEstado()
        {
            return await _setViaDB.ObtieneListaTipoEstado();
        }

        public async Task<ResponseApi<PlacaRead>> ComparatorReadPlaca(ReadPlaca param)
        {
            return await _setViaDB.ComparatorReadPlaca(param);
        }
    }
}
