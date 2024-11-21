using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SETVIA.Core.Database
{
    public  class DBHelper
    {
        public string GetParameterConnectionString()
        {
            string server = ConfigurationManager.AppSettings["TCServer"];
            string nameDB = ConfigurationManager.AppSettings["TCDBName"];
            string userDB = ConfigurationManager.AppSettings["TCDBUser"];
            string passDB = ConfigurationManager.AppSettings["TCDBPass"];
            string timeDB = ConfigurationManager.AppSettings["TimeOutDB"];
            //   string applicationNameDB = ConfigurationManager.AppSettings["TCDBApli"];
            //   try
            //   {
            //       //userDB = SegCrypt.EncryptDecrypt(false, userDB);
            //       //passDB = SegCrypt.EncryptDecrypt(false, passDB);
            //       //userDB = SegCrypt.EncryptDecrypt(true, userDB);
            //       //passDB = SegCrypt.EncryptDecrypt(true, passDB);
            //       userDB = userDB;
            //       passDB = passDB;
            //   }
            //   catch (Exception e)
            //   {
            ///*****/      // Logger.Error($"Error: { e }");
            //       throw new Exception($"Error en SegCrypt, verificar la existencia de la semilla de 64bits SegCryptMotorEnvioMensajes en el servidor.");
            //   }+
            string externalConnectionString =
                      "Persist Security Info = True; " +
                      "Data Source = " + server + ";" +
                      "User Id = " + userDB + ";" +
                      "Password = " + passDB + ";" +
                      "Initial Catalog = " + nameDB + ";"
                      ;
            return externalConnectionString;
            //string externalConnectionString =
            //            "Persist Security Info=False;" +
            //            "Data Source=" + server + ",1433;" +
            //            "User Id=" + userDB + ";" +
            //            "Password=\"" + passDB + "\";" +   // Contraseña entre comillas dobles
            //            "Initial Catalog=" + nameDB + ";" +
            //            "MultipleActiveResultSets=False;" +
            //            "Encrypt=True;" +
            //            "TrustServerCertificate=False;" +
            //            "Connection Timeout=" + timeDB + ";";
            //return externalConnectionString;
        }
        //public static SqlConnection CreateConnection()
        //{
        //    string connectionString = ConfigurationManager.AppSettings[DatabaseConstant.ConnectionString].ToString();
        //    SqlConnection connection = new SqlConnection();
        //    try
        //    {
        //        connection = new SqlConnection(connectionString);
        //    }
        //    catch (Exception e)
        //    {
        //        //LogHelper.WriteErrorLog("Credifondo.Core.Database.DbHelper.CreateConnection", e.Message);
        //    }
        //    return connection;
        //}
    }
}
