namespace BpInterface.Core.Exceptions
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; }
        public string Msg { get; set; }
    }
}
