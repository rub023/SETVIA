using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.Feriado
{
    public class ListadoFeriado
    {
        public int Id_Feriado { get; set; }
        public DateTime Dias { get; set; }
        public string Detalle { get; set; }
        public string Estado { get; set; }
        public string Usuario_Creacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public string Usuario_Modificacion{ get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
