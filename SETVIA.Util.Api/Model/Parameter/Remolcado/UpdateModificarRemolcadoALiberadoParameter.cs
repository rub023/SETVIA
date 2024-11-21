using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.Remolcado
{
    public class UpdateModificarRemolcadoALiberadoParameter
    {
        public int Id_Detalle { get; set; }
        public string Placa { get; set; }
        public string Usuario_Modificacion { get; set; }
    }
}
