using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Movie_Api.Handler
{
    public class APIRespone<T>
    {
        public bool Success { get; set; }
        public string Massage { get; set; }
        public string? Version { get; set; }

        public T Data { get; set; }
        public object Error { get; set; }
        public APIRespone(T data, string massage, string version, bool success)
        {
            Success = success;
            Massage = massage;
            Data = data;
            Version = version;

        }
        public APIRespone(T data, string massage, object error, bool success)
        {
            Success = success;
            Massage = massage;
            Data = data;
            Error = error;
        }
        public static APIRespone<T> CreateSuccess(T data, string massage = "Completed Successfully", string version = "ApiVersion:1.0")
        {
            return new APIRespone<T>(data, massage, version, true);

        }
        public static APIRespone<T> CreateError(T data, string massage = "Operation Failed", object error = null)
        {
            return new APIRespone<T>(data, massage, error, false);

        }




    }
}
