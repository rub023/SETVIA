using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.Empresa
{
    public class CreateEmpresaParameter
    {
        public int Id_Perfil { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Usuario_Creacion { get; set; }

        public int Id_Empresa { get; set; }
        public int Id_Admin { get; set; }
        //public int Id_Usuario { get; set; }
        public string Nombre_Empresa { get; set; }
        public string Representante { get; set; }
        public int Nit { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public int Telefono { get; set; }
        //public string Usuario { get; set; }
        //public string Contrasena { get; set; }
        //public string Us_Creacion { get; set; }
    }
}
