namespace TechnicalAssignment.Services.Models
{
    public class OperationResultWithData<T> : OperationResult
    {
        public OperationResultWithData()
        {

        }

        public OperationResultWithData(OperationResult operationResult, T data)
            : this(operationResult.StatusCode, operationResult.Message, data)
        {
        }

        public OperationResultWithData(OperationStatusCode statusCode, T data)
            : this(statusCode, "", data)
        {
        }

        public OperationResultWithData(OperationStatusCode statusCode, string message, T data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public T Data { get; set; }
    }
}
