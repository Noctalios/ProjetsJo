using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entities;

namespace ProjetsJo.BLL.Interfaces
{
    public interface ITicketingService
    {
        public Task<bool> MockPaymentApi();
        public Task<List<Ticket>> GetUserTickets(Guid accountKey);
        public void CreateTickets(Guid accountKey, Dictionary<Offer, int> cart);
    }
}
