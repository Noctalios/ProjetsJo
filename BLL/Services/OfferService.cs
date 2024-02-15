using ProjetsJo.Entites;
using ProjetsJo.DAL.Interfaces;
using ProjetsJo.BLL.Interfaces;
using ProjetsJo.DAL.Repository;

namespace ProjetsJo.BLL.Services
{
    public class OfferService : IOfferService
    {
        private IOfferData _offerData;

        public OfferService(IOfferData offerData)
        {
            _offerData = offerData;
        }

        public List<Offer> GetOffers() => _offerData.GetOffers();

        public List<Offer> newOffer(List<Offer> offers, Offer newOffer) => _offerData.newOffer(offers, newOffer);
    }
}
