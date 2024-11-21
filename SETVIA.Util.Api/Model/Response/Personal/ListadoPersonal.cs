using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.Personal
{
    public class ListadoPersonal
    {
        public int Id_Personal { get; set; }
        public int Id_Empresa { get; set; }
        public int Id_Usuario { get; set; }
       // public int Id_Tipo_Personal { get; set; }
        public string Nombres { get; set; }
        public string Ap_Paterno { get; set; }
        public string Ap_Materno { get; set; }
        public int CI { get; set; }
        public string Extension { get; set; }
        public string Complemento { get; set; }
        public string Genero { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public int Celular { get; set; }
        //public DateTime?  Fecha_Alta { get; set; }
        //public DateTime? Fecha_Baja { get; set; }
        public string Estado { get; set; }
        public string Usuario_Creacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public string Usuario_Modificacion { get; set; }
         public DateTime? Fecha_Modificacion { get; set; }

       // public int Id_Usuario { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string EstadoUsuario { get; set; }
        public int Id_Perfil { get; set; }
        public string Usuario_CreacionUsuario { get; set; }
        public DateTime Fecha_CreacionUsuario { get; set; }
        public string Usuario_ModificacionUsuario { get; set; }
        public DateTime? Fecha_ModificacionUsuario { get; set; }
    }
}
