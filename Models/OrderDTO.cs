namespace TheMovingCompanyAPI.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerEmail { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public List<ProductDTO> Products { get; set; } = null!;
    }
}
