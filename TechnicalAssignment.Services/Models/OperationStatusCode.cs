namespace TechnicalAssignment.Services.Models
{
    /// <summary>
    /// Operation status codes.
    /// </summary>
    public enum OperationStatusCode
    {
        /// <summary>
        /// Operation successful.
        /// </summary>
        Ok,

        /// <summary>
        /// Data not found.
        /// </summary>
        NotFound,

        /// <summary>
        /// Invalid data.
        /// </summary>
        InvalidData,

        /// <summary>
        /// Data already exists.
        /// </summary>
        AlreadyExists
    }
}
