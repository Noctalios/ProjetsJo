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

        /// <summary>
        /// Provide the connection string from the Configuration.
        /// </summary>
        /// <returns>Return the value of DefaultConnection in appsettings.json.</returns>
        private string GetConnexionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Request the Database to execute the stored procedure GetUsertTickets 
        /// to retrieve all tickets of an User according his accountKey.
        /// </summary>
        /// <param name="accountKey">Account key of the user whose tickets must be retrieved.</param>
        /// <returns>Returns the list of tickets for the user who owns this account key.</returns>
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

        /// <summary>
        /// Request the Databasse to execute the Stored procedure CreateTicketsForUser. 
        /// The parameter of the procedure is a table containing all the new tickets.
        /// </summary>
        /// <param name="accountKey">Account key of the user who purchased the tickets.</param>
        /// <param name="tickets">List of tickets to save.</param>
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
        /// <summary>
        /// This methods permits to create the table parameter for the stored procedure CreateTicketForUser.
        /// </summary>
        /// <param name="tickets">List of tickets to insert in the table parameter.</param>
        /// <param name="accountKey">Account key needed to get the column referenced by the foreign key.</param>
        /// <returns>Return a DataTable parameter.</returns>
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
