namespace TechnicalAssignment.Data.Models.Enums
{
    /// <summary>
    /// Order status types.
    /// </summary>
    public enum OrderStatusType
    {
        /// <summary>
        /// Order is received.
        /// </summary>
        Received = 1,

        /// <summary>
        /// Order is being processed.
        /// </summary>
        Processing = 2,

        /// <summary>
        /// Order is shipped.
        /// </summary>
        Shipped = 3,

        /// <summary>
        /// Order is closed.
        /// </summary>
        Closed = 4
    }
}
