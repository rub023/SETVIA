using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.Empresa
{
    public class UpdateEmpresaEstadoParameter
    {
        public string Estado  { get; set; }
        public string Us_Modificacion { get; set; }	
        public int Id_Empresa { get; set; }
    }
}
