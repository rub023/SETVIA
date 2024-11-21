using SETVIA.Core.Database.Repository.Tarifario;
using SETVIA.Util.Api.Model.Parameter.Tarifario;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Extorno;
using SETVIA.Core.Database.Repository.Extorno;

namespace SETVIA.Business.DataBases
{
    public class ExtornoBusinesDB
    {
        private readonly ExtornoRepository _setViaDB = new ExtornoRepository();
        //private readonly DeudasPagosBusinessDB _setViaBussDB = new DeudasPagosBusinessDB();
        public async Task<ResponseApi<bool>> RegistrarExtorno(ExtornoParameter param)
        {
            return await _setViaDB.RegistrarExtorno(param);
        }
    }


}
