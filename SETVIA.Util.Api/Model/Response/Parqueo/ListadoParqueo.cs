using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.Parqueo
{
    public class ListadoParqueo
    {
        public int Id_Parqueo { get; set; }
        public int Id_Empresa { get; set; }
        public string Codigo_Parqueo { get; set; }
        public string Ciudad { get; set; }
        public string NombreParqueo { get; set; }
        public string Direccion { get; set; }
        public string Ubicacion { get; set; }
        //public TimeSpan Horario_Inicio { get; set; }
        //public TimeSpan Horario_Fin { get; set; }
        public string Estado { get; set; }
        public string Usuario_Creacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public string Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
