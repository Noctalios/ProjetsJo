using ProjetsJo.Entities;

namespace ProjetsJo.DAL.Interfaces
{
    public interface IOfferData
    {
        List<Offer> GetOffers();
        void AddOffer(Offer newOffer);
        void DisableOffer(int id);
        void UpdateOffer(Offer newOffer);
    }
}
