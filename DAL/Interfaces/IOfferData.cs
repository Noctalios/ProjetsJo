using ProjetsJo.Entites;

namespace ProjetsJo.DAL.Interfaces
{
    public interface IOfferData
    {
        List<Offer> GetOffers();
        List<Offer> newOffer(List<Offer> offers, Offer newOffer);
    }
}
