using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.PagoExtension
{
    public class UpdatePagoExtensionUsoParqueoParameter
    {
       // public int Id_Extension { get; set;   
        public string Codigo_Parqueo { get; set; }
        public string Tipo_Vehiculo { get; set; }
        public string Placa { get; set; }
        public TimeSpan Tiempo_Extension { get; set; }
        public decimal Costo_Extension { get; set; }
        public string NroTransaccionPago { get; set; }
        public string NIT { get; set; }
        public string Razon_Social { get; set; }
        public string Correo { get; set; }
        public string CUF { get; set; }
    }
}
