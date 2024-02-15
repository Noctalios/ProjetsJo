using ProjetsJo.Entites;

namespace ProjetsJo.BLL.Interfaces
{
    public interface IOfferService
    {
        List<Offer> GetOffers();
        List<Offer> newOffer(List<Offer> offers, Offer newOffer);
    }
}
