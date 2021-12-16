namespace TechnicalAssignment.Services.Models
{
    /// <summary>
    /// Implements an operation result class that can contain a data object, which is used to return the details of operations running in services.
    /// </summary>
    public class OperationResultWithData<T> : OperationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResultWithData{T}"/> class.
        /// </summary>
        public OperationResultWithData()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResultWithData{T}"/> class.
        /// </summary>
        /// <param name="operationResult">Operation result.</param>
        public OperationResultWithData(OperationResult operationResult)
            : this(operationResult.StatusCode, operationResult.Message, default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResultWithData{T}"/> class.
        /// </summary>
        /// <param name="statusCode">Operation status code.</param>
        /// <param name="data">Operation data.</param>
        public OperationResultWithData(OperationStatusCode statusCode, T data)
            : this(statusCode, "", data)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResultWithData{T}"/> class.
        /// </summary>
        /// <param name="statusCode">Operation status code.</param>
        /// <param name="message">Operation status message.</param>
        /// <param name="data">Operation data.</param>
        public OperationResultWithData(OperationStatusCode statusCode, string message, T data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Gets or sets the operation data.
        /// </summary>
        public T Data { get; set; }
    }
}
