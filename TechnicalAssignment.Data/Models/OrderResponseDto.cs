namespace TechnicalAssignment.Data.Models
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }

        public int Status { get; set; }

        public float RequiredBinWidth { get; set; }
    }
}
