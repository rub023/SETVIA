using SETVIA.Core.Database.Repository.CostoInfraccion;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Response.Administrador;
using SETVIA.Util.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETVIA.Util.Api.Model.Parameter.Empresa;
using SETVIA.Core.Database.Repository.Empresa;
using SETVIA.Util.Api.Model.Response.Empresa;

namespace SETVIA.Business.DataBases
{
    public class EmpresaBusinessDB
    {
        private readonly EmpresaRepository _setViaDB = new EmpresaRepository();
        public async Task<ResponseApi<bool>> RegistrarEmpresa(CreateEmpresaParameter param)
        {
            return await _setViaDB.RegistrarEmpresa(param);
        }
        public async Task<ResponseApi<IEnumerable<ListadoEmpresa>>> ObtieneListaEmpresa()
        {
            return await _setViaDB.ObtieneListaEmpresa();
        }
        public async Task<ResponseApi<bool>> UpdateEmpresaActivo(UpdateEmpresaParameter param)
        {
            return await _setViaDB.UpdateEmpresa(param);
        }
        
        //public async Task<ResponseApi<bool>> UpdateEmpresaEstado(UpdateEmpresaEstadoParameter param)
        //{
        //    return await _setViaDB.UpdateEmpresaEstadoo(param);
        //}
        public async Task<ResponseApi<EmpresaReadID>> ComparatorReadEmpresa(ReadIDEmpresa param)
        {
            return await _setViaDB.ComparatorReadIDEmpresa(param);
        }
    }
}
