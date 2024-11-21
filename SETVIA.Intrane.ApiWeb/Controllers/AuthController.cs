using SETVIA.Util.Api.Model.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

//using System;
//using System.Net;
//using System.Threading;
//using System.Web.Http;
//using WebApiSegura.Models;

namespace SETVIA.Intrane.ApiWeb.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class AuthController : ApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

      
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate()
        {
            // Obtener las credenciales desde los encabezados
            IEnumerable<string> usernameHeader, passwordHeader;
            if (!Request.Headers.TryGetValues("Username", out usernameHeader) ||
                !Request.Headers.TryGetValues("Password", out passwordHeader))
            {
                return BadRequest("Username or Password headers are missing.");
            }

            string username = usernameHeader.FirstOrDefault();
            string password = passwordHeader.FirstOrDefault();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Invalid Username or Password headers.");
            }

            // Validar credenciales (ejemplo básico)
            bool isCredentialValid = (username== "Cor3P4rk1ng" && password == "m8P$9wQ@zX#1L!f3bV");  // Ajustar esta validación según tu lógica de negocio real
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(username);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
