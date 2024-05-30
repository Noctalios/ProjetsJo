using Microsoft.Data.SqlClient;

using System.Data;
using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entities;

namespace ProjetsJo.DAL.Repository
{
    public class OfferData : IOfferData
    {
        private IConfiguration _configuration;

        public OfferData(IConfiguration configuration)
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
        /// Request the Database to retrieve all the offers saved in.
        /// </summary>
        /// <returns>Returns all the offers.</returns>
        public List<Offer> GetOffers()
        {
            string sql = "EXEC [GetOffers]";

            List<Offer> offers = new List<Offer>();
            using (SqlConnection connection = new SqlConnection(GetConnexionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Offer offer = new Offer
                        (
                            reader.GetInt32("Id"),
                            reader.GetString("Label"),
                            reader.GetInt32("TicketNumber"),
                            reader.GetDecimal("Price"),
                            reader[reader.GetOrdinal("Total")] is not DBNull ? reader.GetInt32("Total") : 0,
                            reader.GetBoolean("State") 
                        );
                        offers.Add(offer);
                    }

                };
                connection.Close();
            };
            return offers;
        }

        /// <summary>
        /// Request the Database to execute the stored procedure CreateOffer.
        /// The parameters of the stored procedure are the Label, TicketNumber and the price of the newOffer.
        /// </summary>
        /// <param name="newOffer">Offer to save in database.</param>
        public void AddOffer(Offer newOffer)
        {
            string sql = "EXEC [CreateOffer] @Label, @TicketNumber, @Price";
            using (SqlConnection connection = new SqlConnection(GetConnexionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Label", newOffer.Name);
                command.Parameters.AddWithValue("@TicketNumber", newOffer.TicketNumber);
                command.Parameters.AddWithValue("@Price", newOffer.Price);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Request the Database to execute the stored procedure DisableOffer according Id.
        /// This procedure update the state of the offer to false.
        /// </summary>
        /// <param name="id">Id of the offer to disable.</param>
        public void DisableOffer(int id)
        {
            string sql = "EXEC [DisableOffer] @Id";
            using (SqlConnection connection = new SqlConnection(GetConnexionString()))
            {
                SqlCommand command = new SqlCommand(sql,connection);

                command.Parameters.AddWithValue("@Id", id);
                
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Request the Database to execute the stored procedure UdpdateOffer.
        /// This procedure can update the Label and the TicketNumber of the offer according Id.
        /// </summary>
        /// <param name="OfferToUpdate">Offer to update in database.</param>
        public void UpdateOffer(Offer OfferToUpdate)
        {
            string sql = "EXEC [UpdateOffer] @OfferId, @Label, @TicketNumber";
            using(SqlConnection connection = new SqlConnection(GetConnexionString()))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@OfferId", OfferToUpdate.Id);
                command.Parameters.AddWithValue("@Label", OfferToUpdate.Name);
                command.Parameters.AddWithValue("@TicketNumber", OfferToUpdate.TicketNumber);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
