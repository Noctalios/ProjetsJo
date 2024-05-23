using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entities;

namespace ProjetsJo.BLL.Interfaces
{
    public interface ITicketingService
    {
        public Task<bool> MockPaymentApi();

        public Task<List<Ticket>> GetUserTickets(Guid accountKey);
    }
}
