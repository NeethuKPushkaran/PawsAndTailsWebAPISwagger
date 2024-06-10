namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();

    }
}
