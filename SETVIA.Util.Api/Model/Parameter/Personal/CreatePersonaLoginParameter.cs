using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.Personal
{
    public class CreatePersonaLoginParameter
    {
        public int Id_Login { get; set; }
        public int Id_Personal { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Us_Creacion { get; set; }
    }
}
