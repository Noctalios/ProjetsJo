namespace ProjetsJo.Entities
{
    public class Offer : Sale
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TicketNumber { get; set; }
        public decimal Price { get; set; }
        public bool State { get; set; }

        public Offer() { }
        public Offer(int id, string name, int ticketNumber, decimal price, int total, bool state) : base(total)
        {
            Id = id;
            Name = name;
            TicketNumber = ticketNumber;
            Price = price;
            State = state;
        }

        protected decimal AmountOffer()
        {
            return Total * Price;
        }
    }
}
