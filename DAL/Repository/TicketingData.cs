using Microsoft.Data.SqlClient;
using ProjetsJo.DAL.Interfaces;
using System.Data;
using ProjetsJo.Entities;
using System.Globalization;

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

        public void SaveTicketsForUser(Guid accountKey, Dictionary<Ticket, int> tickets)
        {
            string sql = "EXEC [CreateTicketsForUser] @Tickets";

            using (SqlConnection connection = new SqlConnection(GetConnexionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                // Parameter @Tickets
                SqlParameter parameter = command.Parameters.AddWithValue("@Tickets", CreateTicketsDataTable(tickets, accountKey));
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.TypeName = "dbo.TicketTableType";

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        #region DataTable

        private DataTable CreateTicketsDataTable(Dictionary<Ticket, int> tickets, Guid accountKey)
        {
            DataTable ticketTable = new DataTable();
            ticketTable.Columns.Add("QrCode", typeof(string));
            ticketTable.Columns.Add("Date", typeof(DateTime));
            ticketTable.Columns.Add("OfferId", typeof(int));
            ticketTable.Columns.Add("accountKey", typeof(string));

            if (tickets != null)
            {
                foreach (var ticket in tickets.Keys.ToList())
                {
                    ticketTable.LoadDataRow(new object[]
                    {
                        ticket.Qrcode,
                        ticket.Date,
                        tickets[ticket],
                        accountKey.ToString()
                    },
                    true);
                }
            }

            return ticketTable;
        }

        #endregion
    }
}
