using ProjetsJo.BLL.Interfaces;
using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entities;
using QRCoder;

namespace ProjetsJo.BLL.Services
{
    public class TicketingService : ITicketingService
    {
        private ITicketingData _ticketingData;

        public TicketingService(ITicketingData ticketingData) 
        {
            _ticketingData = ticketingData;
        }

        public async Task<bool> MockPaymentApi()
        {
            await Task.Delay(8000);
            return true;
        }

        public async Task<List<Ticket>> GetUserTickets(Guid accountKey)
        {
            return await Task.FromResult(_ticketingData.GetUserTickets(accountKey));
        }

        public void CreateTickets(Guid accountKey, Dictionary<Offer, int> cart)
        {
            Dictionary<Ticket, int> newTickets = GenerateQrCode(accountKey, cart);
            _ticketingData.SaveTicketsForUser(accountKey, newTickets);
        }
        private Dictionary<Ticket, int> GenerateQrCode(Guid accountKey, Dictionary<Offer, int> cart) 
        {
            Dictionary<Ticket, int > tickets =  new();
            DateTime dateTime = DateTime.Now;
            int i = 0;
            using QRCodeGenerator qrGenerator = new();
            foreach (Offer offer in cart.Keys.ToList()) 
            {
                DateTime dateTicket= dateTime.AddMilliseconds(i);

                using QRCodeData qrCodeData = qrGenerator.CreateQrCode($"Le billet est celui de {accountKey} et a été acheté le {dateTicket.ToString("0:MM/dd/yyy HH:mm:ss.fff")}", QRCodeGenerator.ECCLevel.Q);
                Base64QRCode qrCode = new (qrCodeData);
                string qrCodeImageAsBase64 = qrCode.GetGraphic(20);
                Ticket ticket = new (0, qrCodeImageAsBase64, dateTicket);
                tickets.Add(ticket, offer.Id);
                i++;
            }
            return tickets;
        }

    }
}
