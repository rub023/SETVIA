using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.Administrador
{
    public class CreateAdminLoginParameter
    {
        public int Id_Login { get; set; }
        public int Id_Administrador { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Us_Creacion { get; set; }
        //public string Estado { get; set; }
    }
}
