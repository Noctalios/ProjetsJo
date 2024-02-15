namespace ProjetsJo.Entites
{
    public class Sale
    {
        public int Total { get; set; }

        public Sale() { }
        public Sale(int total) 
        {
            Total = total;
        }

        protected decimal AmountOffer (int total, decimal price)
        {
            return total * price;
        }
    }
}
