namespace luxclusif.order.application.Models
{
    public class ErrorModel
    {
        public ErrorModel(string code, string message)
        {
            Code = code;
            Message = message;
        }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
