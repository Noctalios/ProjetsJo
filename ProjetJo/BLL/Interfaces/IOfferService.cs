using ProjetsJo.Entities;

namespace ProjetsJo.BLL.Interfaces
{
    public interface IOfferService
    {
        List<Offer> GetOffers();
        void AddOffer(Offer newOffer);
        void UpdateOffer(Offer offerToUpdate);
        void DisableOffer(int offerId);
    }
}
