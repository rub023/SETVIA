using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Response.Horario
{
    public class ListadoHorario
    {
        public int Id_Horario {  get; set; }    
        public int Id_Parqueo { get; set; }
        public string NombreParqueo { get; set; }
        public string DiaSemana { get; set; }
        public TimeSpan Horario_Inicio { get; set; }
        public TimeSpan Horario_Fin { get; set; }
        public string Estado { get; set; }
        public string Usuario_Creacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public string Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }

        //nombre empresa
        //public string DiaSemana { get; set; }     
        //public TimeSpan Horario_Inicio { get; set; }
        //public TimeSpan Horario_Fin { get; set; }
        //public string EstadoHorario { get; set; }
        //public DateTime Fecha_Feriado { get; set; }
        //public string Detalle_Feriado { get; set; }
        //public string EstadoFeriado { get; set; }
        //public string Usuario_Creacion { get; set; }
        //public DateTime Fecha_Creacion { get; set; }
        //public string Usuario_Modificacion { get; set; }
        //public DateTime? Fecha_Modificacion { get; set; }



    }
}
