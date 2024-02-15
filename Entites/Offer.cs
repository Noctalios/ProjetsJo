namespace ProjetsJo.Entites
{
    public class Offer : Sale
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        
        public Offer() { }
        public Offer(int id, string name, int quantity, decimal price, int total) : base(total) 
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}
