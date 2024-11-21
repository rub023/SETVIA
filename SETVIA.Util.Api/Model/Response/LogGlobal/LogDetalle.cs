using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.LogGlobal
{
    public class LogDetalle<T>
    {
        public string IDENTIFICADOR { get; set; }
        public string VER_BROWSER { get; set; }
        public string IMEI { get; set; }
        public string SESSION_ID { get; set; }
        public string IP_ORIGEN { get; set; }
        public string SIS_OPERATIVO { get; set; }
        public T RequestAnterior { get; set; }
        public T Request { get; set; }
        public T Response { get; set; }
    }
}
