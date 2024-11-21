using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.Inmovilizado
{
    public class CreateInmovilizadoParameter
    {
        //public string  Placa { get; set; }        
        public string Usuario_Modificacion   { get; set; }
        public int Id_Detalle { get; set; }
        public int Id_Infraccion_Inmovilizado { get; set; }
       // public int Id_Costo_Infraccion { get; set; }
      //  public string NroTransaccion { get; set; }
    }
}
