namespace TechnicalAssignment.Services.Models
{
    /// <summary>
    /// Implements an operation result class, which is used to return the details of operations running in services.
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        public OperationResult()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        /// <param name="statusCode">Operation status code.</param>
        public OperationResult(OperationStatusCode statusCode)
            : this(statusCode, "")
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        /// <param name="statusCode">Operation status code.</param>
        /// <param name="message">Operation status message.</param>
        public OperationResult(OperationStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        /// <summary>
        /// Gets or sets the operation status code.
        /// </summary>
        public OperationStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the operation status message.
        /// </summary>
        public string Message { get; set; }
    }
}
