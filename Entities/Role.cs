namespace ProjetsJo.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public Role(int id, string label) 
        {
            Id = id;
            Label = label;
        }
    }
}
