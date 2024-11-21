using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.Administrador
{
    public class ListadoAdministrador
    {
        public int Id_Admin { get; set; }
        public string Nombres { get; set; }
        public string Ap_Paterno { get; set; }
        public string Ap_Materno { get; set; }
        public int CI { get; set; }
        public string Extension { get; set; }
        public string Complemento { get; set; }
        public string Genero { get; set; }
        public string Direccion { get; set; }
        public int Celular { get; set; }
        public string Correo { get; set; }
        //public string Contrasena { get; set; }
        public string EstadoAdmin { get; set; }
        public string Usuario_CreacionAdmin { get; set; }
        public DateTime Fecha_CreacionAdmin { get; set; }
        public string Usuario_ModificacionAdmin { get; set; }
        public DateTime? Fecha_ModificacionAdmin { get; set; }
       
        public int Id_Usuario { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string EstadoUsuario { get; set; }
        public int Id_Perfil { get; set; }
        public string Usuario_CreacionUsuario { get; set; }
        public DateTime Fecha_CreacionUsuario { get; set; }
        public string Usuario_ModificacionUsuario { get; set; }
        public DateTime? Fecha_ModificacionUsuario { get; set; }
        //public int Id_Admin { get; set; }
        //public string Nombres { get; set; }
        //public string Ap_PAterno { get; set; }
        //public string Ap_Materno { get; set; }
        //public int CI { get; set; }
        //public string Extension { get; set; }
        //public string Complemento { get; set; }
        //public string Genero { get; set; }
        //public string Direccion { get; set; }
        //public int Celular { get; set; }
        //public string Correo { get; set; }
        //public string Usuario { get; set; }
        //public string Contrasena { get; set; }
        //public string Estado { get; set; }
        //public DateTime Fecha_Creacion { get; set; }
        //public DateTime? Fecha_Modificacion { get; set; }
    }
}
