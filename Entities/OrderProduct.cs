namespace TheMovingCompanyAPI.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public string FromAddress { get; set; } = null!;
        public string ToAddress { get; set; } = null!;


        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
