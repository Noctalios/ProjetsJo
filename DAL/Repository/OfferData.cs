using Microsoft.Data.SqlClient;

using System.Data;
using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entites;

namespace ProjetsJo.DAL.Repository
{
    public class OfferData : IOfferData
    {
        private IConfiguration _configuration;

        public OfferData(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GetConnexionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }
        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

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
                            reader.GetString("Name"),
                            reader.GetInt32("TicketNumber"),
                            reader.GetDecimal("Price"),
                            reader.GetInt32("Total")
                        );
                        offers.Add(offer);
                    }

                };
                connection.Close();
            };
            return offers;
        }

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

        public void DisableOffer(int id)
        {
            string sql = "EXEC [DisableOffer] @Id";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql,connection);

                command.Parameters.AddWithValue("@Id", id);
                
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateOffer(Offer OfferToUpdate)
        {
            string sql = "EXEC [UpdateOffer] @OfferId, @Label, @TicketNumber";
            using(SqlConnection connection = new SqlConnection(GetConnectionString()))
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
