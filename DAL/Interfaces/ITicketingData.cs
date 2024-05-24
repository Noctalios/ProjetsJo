using ProjetsJo.Entities;

namespace ProjetsJo.DAL.Interfaces
{
    public interface ITicketingData
    {
        public List<Ticket> GetUserTickets(Guid accountKey);
        public void SaveTicketsForUser(Guid accountKey, Dictionary<Ticket, int> tickets);
    }
}
