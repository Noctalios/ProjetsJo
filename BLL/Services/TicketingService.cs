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

        public void CreateTickets(Guid accountKey, int number)
        {
            List<Ticket> newTickets = GenerateQrCode(accountKey, number);
        }
        private List<Ticket> GenerateQrCode(Guid accountKey, int number) 
        {
            List<Ticket> tickets =  new();
            DateTime dateTime = DateTime.Now;

            using QRCodeGenerator qrGenerator = new();
            for ( int i = 0; i < number; i++) 
            {
                using QRCodeData qrCodeData = qrGenerator.CreateQrCode($"Le billet est celui de {accountKey} et a été acheté le {dateTime.AddMilliseconds(i)}", QRCodeGenerator.ECCLevel.Q);
                Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                string qrCodeImageAsBase64 = qrCode.GetGraphic(20);
                Ticket ticket = new Ticket(0-i, qrCodeImageAsBase64, dateTime.AddMilliseconds(i));
                tickets.Add(ticket);
            }
            return tickets;
        }

    }
}
