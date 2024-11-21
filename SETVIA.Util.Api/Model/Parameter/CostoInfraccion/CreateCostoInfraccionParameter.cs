using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response
{
    public class CreateCostoInfraccionParameter
    {
        public int Id_Costo_Infraccion { get; set; }
        public int Id_Empresa { get; set; }
        public int Id_Tipo_Infraccion { get; set; }
        public decimal Costo_Multa { get; set; }
        public decimal Costo_Servicio { get; set; }
        public string Usuario_Creacion { get; set; }
    }

}