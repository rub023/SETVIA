using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.Extorno
{
    public class ExtornoParameter
    {
        public string NroTransaccionPago { get; set; }   //lo que me envia el banco
        public string NroTransaccionExtorno { get; set; }
    }   
}
