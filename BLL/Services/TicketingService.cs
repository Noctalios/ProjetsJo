using ProjetsJo.BLL.Interfaces;
using ProjetsJo.DAL.Interfaces;

namespace ProjetsJo.BLL.Services
{
    public class TicketingService : ITicketingService
    {
        public async Task<bool> MockPaymentApi()
        {
            await Task.Delay(8000);
            return true;
        }
    }
}
