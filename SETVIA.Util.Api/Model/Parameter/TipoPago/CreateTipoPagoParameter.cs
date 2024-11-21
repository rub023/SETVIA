using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.TipoPago
{
    public class CreateTipoPagoParameter
    {
        public int Id_TipoPago { get; set; }
        public string Detalle { get; set; }
        public string Usuario_Creacion { get; set; }
    }
}
