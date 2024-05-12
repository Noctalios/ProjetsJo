namespace ProjetsJo.Entites
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Qrcode { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Ticket() { }
        public Ticket(int id, string qrcode, DateTime date)
        {
            Id = id;
            Qrcode = qrcode;
            Date = date;
        }
    }
}