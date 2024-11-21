using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Horario;
using SETVIA.Core.Database.Repository.Horario;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api.Model.Response.Horario;
using SETVIA.Util.Api.Model.Parameter.Administrador;

namespace SETVIA.Business.DataBases
{
    public class HorarioBusinessDB
    {

        private readonly HorarioRepository _setViaDB = new HorarioRepository();
        public async Task<ResponseApi<bool>> RegistrarHorario(CreateHorarioParameter param)
        {
            return await _setViaDB.RegistrarHorario(param);
        }
        public async Task<ResponseApi<IEnumerable<ListadoHorario>>> ObtieneListaHorario()
        {
            return await _setViaDB.ObtieneListaHorario();
        }
        public async Task<ResponseApi<bool>> UpdateHorario(UpdateHorarioParameter param)
        {
            return await _setViaDB.UpdateHorario(param);
        }
        public async Task<ResponseApi<HorarioReadID>> ComparatorReadHorario(ReadIDHorario param)
        {
            return await _setViaDB.ComparatorReadHorario(param);
        }
    }
}
