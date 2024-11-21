using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SETVIA.Util.Api
{
    public class Response<T>
    {
        // public string StatusCode { get; set; }
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Content { get; set; }


        public T Data { get; set; }
        //public DateTime Time { get; set; }
      //  public string Message { get; set; }
        //public bool Success { get; set; }
       // public long TotalRows { get; set; }
       // public object Deuda { get; set; }
        // public string TotalRows { get; set; }

        //public static Response<T> Succes(T content, string message)
        //{
        //    return new Response<T>()
        //    {
        //        Content = content,
        //        Message= message,
        //        StatusCode = GCNotificationStatus.OK
        //        // StatusCode = "200"
        //    };
        //}
        public static Response<T> Success(T content, string message)
        {
            return new Response<T>()
            {
                Content = content,
                Message = message,
                StatusCode = StatusCode.OK
            };
        }
        public static Response<T> Error(StatusCode code, string message)
        {
            return new Response<T>()
            {
                StatusCode = code,
                Message = message
            };
        }
        //public static Response<T> Error(string code, string message)
        //{
        //    return new Response<T>()
        //    {
        //        StatusCode = code.ToString(),
        //        Message = message
        //    };
        //}
        public static Response<T> UnhandledError(string message = "")
        {
            return new Response<T>()
            {
                Message = "Ocurrió un error al procesar la información, si el error es frecuente por favor comuníquese con el administrador. " + message,
                StatusCode = StatusCode.UnhandledError,
                Content = default(T)
            };
        }
        //public static Response<T> UnhandledError(string message = "")
        //{
        //    return new Response<T>()
        //    {
        //        Message = "Ocurrió un error al procesar la información, si el error es frecuente por favor comuníquese con el administrador. " + message,
        //        StatusCode = "600",
        //        Content = default(T)
        //    };
        //}
    }

    public enum StatusCode
    {
        OK=200,
        BadRequest=400,
        NotFound=404,
        UnhandledError = 500,
        DataBaseError
    }
}
