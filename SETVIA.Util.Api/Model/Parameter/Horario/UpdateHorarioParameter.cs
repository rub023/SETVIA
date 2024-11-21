using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api.Model.Parameter.Horario
{
    public class UpdateHorarioParameter
    {
        public int Id_Horario { get; set; }
        public int Id_Parqueo { get; set; }
        //nombre empresa
        public string DiaSemana { get; set; }
        public TimeSpan Horario_Inicio { get; set; }
        public TimeSpan Horario_Fin { get; set; }
        public string Estado { get; set; }
        public string Usuario_Modificacion { get; set; }
    }
}
