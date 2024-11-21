using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Constant
{
    public class APIConstant
    {
        public const string UnhandledErrorMessage = "Ocurrió un error al procesar la información.";
        public const string NotFound = "Datos no encontrados.";
        public const string DatabaseError = "Error en la Base de Datos";
        public const string Success = "Success";
        //public const string CalculationError = "Ocurrió un problema al realizar el cálculo";
        public const string CalculationError = "Ocurrió un error";
        public const string ForbiddenError = "El usuario no está autorizado para el uso de esta aplicación";
        public const string HeaderNotFoundError = "No se encontraron las cabeceras de seguridad";

        public const string DuplicateValue = "El registro ya existe en la base de datos.";
        public const string PasswordMismatchMessage = "El usuario o la contraseña son incorrectos";
    }
}
