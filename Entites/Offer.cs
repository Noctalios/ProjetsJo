namespace ProjetsJo.Entites
{
    public class Offer : Sale
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TicketNumber { get; set; }
        public decimal Price { get; set; }

        public Offer() { }
        public Offer(int id, string name, int ticketNumber, decimal price, int total) : base(total) 
        {
            Id = id;
            Name = name;
            TicketNumber = ticketNumber;
            Price = price;
        }

        protected decimal AmountOffer()
        {
            return Total * Price;
        }
    }
}
