using SETVIA.Util.Api.Model.Response.LogGlobal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SETVIA.Util.Api
{
    public static class Method
    {
        public static LogDetalle<string> global_Log()
        {
            HttpContext context = HttpContext.Current;

            HttpBrowserCapabilities browser = context.Request.Browser;

            var det = new LogDetalle<string>();
            //det.IDENTIFICADOR = identificador; //NOSONAR
            det.VER_BROWSER = browser.Browser + " - " + browser.Version;
            //det.IMEI = ""; //NOSONAR
            //det.SESSION_ID = identificador; //NOSONAR
            det.IP_ORIGEN = context.Request.UserHostAddress;
            if (browser.IsMobileDevice)
            {
                if (context.Request.UserAgent.Contains("Android"))
                {
                    det.SIS_OPERATIVO = "Android" + " - " + browser.MobileDeviceManufacturer + " : " + browser.MobileDeviceModel;
                }
                else if (context.Request.UserAgent.Contains("iPhone") || context.Request.UserAgent.Contains("iPad"))
                {
                    det.SIS_OPERATIVO = "iOS" + " - " + browser.MobileDeviceManufacturer + " : " + browser.MobileDeviceModel;
                }
            }
            else
            {
                det.SIS_OPERATIVO = "Escritorio : " + browser.Platform;
            }

            return det;
        }
    
    }
}
