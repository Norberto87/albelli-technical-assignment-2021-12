namespace TechnicalAssignment.Services.Models
{
    public class OperationResult
    {
        public OperationResult()
        {

        }

        public OperationResult(OperationStatusCode statusCode)
            : this(statusCode, "")
        {

        }

        public OperationResult(OperationStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public OperationStatusCode StatusCode { get; set; }

        public string Message { get; set; }
    }
}
