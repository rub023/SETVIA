using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.DeudaInmovilizado
{
    public class InmovilizadoBuscaResponse
    {
        public string state { get; set; }
        public ListadoInmovilizado InmovilizadoData {get; set;}

    }
}
