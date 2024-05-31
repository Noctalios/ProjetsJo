using ProjetsJo.BLL.Interfaces;
using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entities;
using QRCoder;
using System.Collections.Generic;

namespace ProjetsJo.BLL.Services
{
    public class TicketingService : ITicketingService
    {
        private ITicketingData _ticketingData;

        public TicketingService(ITicketingData ticketingData) 
        {
            _ticketingData = ticketingData;
        }

        /// <summary>
        /// This method permits to Mock a request to payment API.
        /// </summary>
        /// <returns>Return true if the payment succeded.</returns>
        public async Task<bool> MockPaymentApi()
        {
            await Task.Delay(8000);
            return true;
        }

        /// <summary>
        /// This method requires the DAL to get all the ticket of an account.
        /// </summary>
        /// <param name="accountKey">Key account of an user.</param>
        /// <returns> Returns the list of tickets for a user's account based on their account key.</returns>
        public async Task<List<Ticket>> GetUserTickets(Guid accountKey)
        {
            return await Task.FromResult(_ticketingData.GetUserTickets(accountKey));
        }

        /// <summary>
        /// This methods call the method GenerateQrCode providing a Dictionary of Offer int in order to reate QrCodes.
        /// Then requires The DAL to register these newly created QrCodes.
        /// </summary>
        /// <param name="accountKey">Account key of the user who purchased the tickets.</param>
        /// <param name="cart">Dictionary with all the Offers (keys) and the quantity bought by Offers (value).</param>
        public void CreateTickets(Guid accountKey, Dictionary<Offer, int> cart)
        {
            Dictionary<Ticket, int> newTickets = GenerateQrCode(accountKey, cart);
            _ticketingData.SaveTicketsForUser(accountKey, newTickets);
        }

        /// <summary>
        /// This method permit to generate an amount of Tickets by offers.
        /// </summary>
        /// <param name="accountKey">Account key of the user who purchased the tickets.</param>
        /// <param name="cart">Dictionary with all the Offers (keys) and the quantity bought by Offers (value).</param>
        /// <returns>Returns a dictionary of all the tickets with the offers from which they come.</returns>
        public Dictionary<Ticket, int> GenerateQrCode(Guid accountKey, Dictionary<Offer, int> cart) 
        {
            Dictionary<Ticket, int > tickets =  new();
            DateTime dateTime = DateTime.Now;
            using QRCodeGenerator qrGenerator = new();
            foreach (Offer offer in cart.Keys.ToList()) 
            {
                for (int j = 0; j < offer.TicketNumber * cart[offer]; j++) 
                {
                    DateTime dateTicket= dateTime.AddMilliseconds(j);

                    using QRCodeData qrCodeData = qrGenerator.CreateQrCode($"Le billet est celui de {accountKey} et date du {dateTicket:0:MM/dd/yyy HH:mm:ss.fff}", QRCodeGenerator.ECCLevel.Q);
                    Base64QRCode qrCode = new (qrCodeData);
                    string qrCodeImageAsBase64 = qrCode.GetGraphic(20);
                    Ticket ticket = new (tickets.Keys.ToList().Count, qrCodeImageAsBase64, dateTicket);
                    tickets.Add(ticket, offer.Id);
                }
            }
            return tickets;
        }

    }
}
