using SETVIA.Util.Api.Model.Response.Tarifario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.DetalleParqueo
{
    public class DetalleParqueoResponse
    {
        public bool RegistroExitoso { get; set; }               // Indica si el registro fue exitoso
        public List<ListadoTarifario> ListaDeudas { get; set; }
    }
}
