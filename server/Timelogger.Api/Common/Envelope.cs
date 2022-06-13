using System;
namespace Timelogger.Api.Common
{
    public class Envelope<T>
    {
        public T Result { get; }
        public string ErrorMessage { get; set; }
        public string ApiVersion { get; set; }
        public DateTime TimeGenerated { get; set; }

        protected internal Envelope(T result, string errorMessage)
        {
            Result = result;
            ErrorMessage = errorMessage;
            TimeGenerated = DateTime.UtcNow;
            ApiVersion = "1.0";
        }
    }

    public class Envelope : Envelope<string>
    {
        protected Envelope(string errorMessage) : base(null, errorMessage)
        {

        }
        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(result, null);
        }

        public static Envelope Ok()
        {
            return new Envelope(null);
        }

        public static Envelope Error(string errorMessage)
        {
            return new Envelope(errorMessage);
        }
    }
}
