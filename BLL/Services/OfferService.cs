using ProjetsJo.Entities;
using ProjetsJo.DAL.Interfaces;
using ProjetsJo.BLL.Interfaces;

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

        public void AddOffer(Offer newOffer) 
        {
            _offerData.AddOffer(newOffer);
        }

        public void UpdateOffer(Offer offerToUpdate)
        {
            _offerData.UpdateOffer(offerToUpdate);
        }

        public void DisableOffer(int offerId)
        {
            _offerData.DisableOffer(offerId);
        }
    }
}
