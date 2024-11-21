using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace SETVIA.Intrane.ApiWeb.Filters
{
    public class AddAuthorizationHeaderOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            // Verifica si ya se han añadido parámetros. Si no, crea una nueva lista.
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();

            // Añade el parámetro de autorización para que se incluya en el encabezado
            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                description = "JWT Token para la autorización",
                required = false, // No es obligatorio que el cliente pase un token en cada solicitud
                type = "string"
            });
        }
    }
}