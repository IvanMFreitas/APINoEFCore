namespace APINoEFCore.Entities.ViewModels{
    public class OrderViewModel{
        public string Id { get; set; }
        public int Qty { get; set; }
        public decimal Total { get; set; }
        public string PersonId { get; set; }
        public string ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}