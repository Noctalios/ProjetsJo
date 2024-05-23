using Microsoft.Data.SqlClient;
using ProjetsJo.DAL.Interfaces;
using System.Data;
using ProjetsJo.Entities;

namespace ProjetsJo.DAL.Repository
{
    public class TicketingData : ITicketingData
    {
        private IConfiguration _configuration;

        public TicketingData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConnexionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public List<Ticket> GetUserTickets(Guid accountKey)
        {
            string sql = "EXEC [GetUserTickets] @accountKey";

            List<Ticket> tickets = new();

            using (SqlConnection connection = new SqlConnection(GetConnexionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@accountKey", accountKey.ToString());
                
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ticket ticket = new(reader.GetInt32("Id"), reader.GetString("Qrcode"), reader.GetDateTime("Date"));
                        tickets.Add(ticket);
                    }

                };
                connection.Close();
            };
            return tickets;
        }
    }
}
