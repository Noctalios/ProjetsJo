using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entites;

namespace ProjetsJo.DAL.Repository
{
    public class OfferData : IOfferData
    {
        private IConfiguration Configuration;

        public OfferData(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private string GetConnectionString()
        {
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<Offer> GetOffers()
        {
            List<Offer> offers = new List<Offer>();
            
            for (int i = 0; i < 3; i++)
                offers.Add(new Offer(i, $"Offer number {i}", i + 1, (decimal)(i + 1) * 10, (i + 1) * 45 ));
            
            return offers;
        }

        public List<Offer> newOffer(List<Offer> offers, Offer newOffer)
        {
            Offer no = new Offer(offers.Count(), newOffer.Name, newOffer.Quantity, newOffer.Price, newOffer.Total);
            offers.Add(no);
            return offers;
        }
    }
}
