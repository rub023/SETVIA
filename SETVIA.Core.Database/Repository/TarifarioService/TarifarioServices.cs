using SETVIA.Util.Api.Model.Response.Tarifario;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace SETVIA.Core.Database.Repository.TarifarioService
{
    public class TarifarioServices
    {
        public static int TimeOutDB = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutDB"]);
        public static DBHelper ConectionString = new DBHelper();
        public async Task<ResponseApi<IEnumerable<ListadoTarifario>>> ObtieneListaTarifarioAsync()
        {
            // Lógica actual del método
            StoreProcedureAsync storeProcedure = new StoreProcedureAsync("[bdsv].[ListarTarifario]");
            var dataTable = await storeProcedure.ReturnData(ConectionString.GetParameterConnectionString(), TimeOutDB);

            var response = new List<ListadoTarifario>();
            foreach (DataRow row in dataTable.Rows)
            {
                response.Add(new ListadoTarifario
                {
                    // Lógica para mapear filas
                });
            }

            return new ResponseApi<IEnumerable<ListadoTarifario>>
            {
                Data = response,
                Success = true,
                Message = "Datos obtenidos correctamente"
            };
        }
    }
}
