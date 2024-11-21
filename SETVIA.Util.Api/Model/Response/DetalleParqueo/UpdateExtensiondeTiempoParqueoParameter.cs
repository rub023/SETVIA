using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.DetalleParqueo
{
    public class UpdateExtensiondeTiempoParqueoParameter
    {
        public int Id_Detalle { get; set; }
        public int Id_Tarifario { get; set; }
        public string Usuario_Modificacion { get; set; }
    }
}
