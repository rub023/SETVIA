using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Tarifario;
using SETVIA.Core.Database.Repository.Tarifario;
using SETVIA.Util.Api.Model.Response.Tarifario;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;

namespace SETVIA.Business.DataBases
{
    public class TarifarioBusinessDB
    {
        private readonly TarifarioRepository _setViaDB = new TarifarioRepository();
        private readonly DeudasPagosBusinessDB _setViaBussDB = new DeudasPagosBusinessDB();
        public async Task<ResponseApi<bool>> RegistrarTarifario(CreateTarifarioParameter param)
        {
            return await _setViaDB.RegistrarTarifario(param);
        }
        public async Task<ResponseApi<IEnumerable<ListadoTarifario>>> ObtieneListaTarifario(ReadDeudaParqueo param)
        {

            return await _setViaDB.ObtieneListaTarifario();


           // var response = new ResponseApi<IEnumerable<ListadoTarifario>>();
            // Consulta la deuda 
           // var respDeuda = await _setViaBussDB.ConsultaDeuda(param);
            //if (respDeuda.Data == null)
            //{
            //    response.Success = false;
            //    response.Message = "No se encontraron datos de la deuda.";
            //    return response;
            //} // Consulta la lista de tarifarios 
           //// var respTarifarios = await _setViaDB.ObtieneListaTarifario();
            //if (respTarifarios.Data == null)
            //{
            //    response.Success = false;
            //    response.Message = "No se encontraron datos del listado de tarifarios.";
            //    return response;
            //} // Combina los resultados 
            //var listaDeudas = respTarifarios.Data.ToList();
            //response.Data = listaDeudas;
            //response.Message = "Datos obtenidos correctamente.";
            //response.Success = true; // Agrega la deuda a la respuesta 
            //response.Deuda = respDeuda.Data;
            //return response;
        }
        public async Task<ResponseApi<bool>> UpdateTarifario(UpdateTarifarioParameter param)
        {
            return await _setViaDB.UpdateTarifario(param);
        }
        public async Task<ResponseApi<TarifarioReadID>> ComparatorReadTarifario(ReadIDTarifario param)
        {
            return await _setViaDB.ComparatorReadIDTarifario(param);
        }
    }
}
