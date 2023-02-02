namespace TheMovingCompanyAPI.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public DateTime Date { get; set; }
        public string FromAddress { get; set; } = null!;
        public string ToAddress { get; set; } = null!;
    }
}
