namespace TechnicalAssignment.Services.Models
{
    public class OperationResultWithData<T> : OperationResult
    {
        public T Data { get; set; }
    }
}
