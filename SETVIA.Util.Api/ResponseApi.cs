using SETVIA.Util.Api.Model.Response.Tarifario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api
{
    public class ResponseApi<T>
    {
        public T Data { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        //public long TotalRows { get; set; }
        public int Error { get; set; }

        //public List<Facturacion> FacturacionData { get; set; }

    }
    //public class Facturacion
    //{
    //    public int NIT { get; set; }
    //    public string RazonSocial { get; set; } // Otros campos relevantes }
    //    public string Correo { get; set; }
    //}
}
