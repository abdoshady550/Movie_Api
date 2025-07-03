using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Movie_Api.Handler
{
    public class APIRespone<T>
    {
        public bool Success { get; set; }
        public string Massage { get; set; }
        public T Data { get; set; }
        public object Error { get; set; }
        public APIRespone(T data, string massage, bool success)
        {
            Success = success;
            Massage = massage;
            Data = data;

        }
        public APIRespone(T data, string massage, object error, bool success)
        {
            Success = success;
            Massage = massage;
            Data = data;
            Error = error;
        }
        public static APIRespone<T> CreateSuccess(T data, string massage = "Completed Successfully")
        {
            return new APIRespone<T>(data, massage, true);

        }
        public static APIRespone<T> CreateError(T data, string massage = "Operation Failed", object error = null)
        {
            return new APIRespone<T>(data, massage, error, false);

        }




    }
}
