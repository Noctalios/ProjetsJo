using ProjetsJo.BLL.Interfaces;
using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entities;

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

    }
}
