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

        /// <summary>
        /// This method permits to retrieve all the offers of the database.
        /// </summary>
        /// <returns></returns>
        public List<Offer> GetOffers() => _offerData.GetOffers();

        /// <summary>
        /// This method requires the DAL to add this new the offer.
        /// </summary>
        /// <param name="newOffer">Offer to add</param>
        public void AddOffer(Offer newOffer) 
        {
            _offerData.AddOffer(newOffer);
        }

        /// <summary>
        /// This method requires the DAL to update the offer.
        /// </summary>
        /// <param name="offerToUpdate">Offer to update</param>
        public void UpdateOffer(Offer offerToUpdate)
        {
            _offerData.UpdateOffer(offerToUpdate);
        }

        /// <summary>
        /// This method requires the DAL to disable according his Id.
        /// </summary>
        /// <param name="offerId">Id of the offer to disable</param>
        public void DisableOffer(int offerId)
        {
            _offerData.DisableOffer(offerId);
        }
    }
}
