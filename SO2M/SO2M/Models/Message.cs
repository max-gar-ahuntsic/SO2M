namespace SO2M.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ExpediteurId { get; set; }
        public int DestinataireId { get; set; }
        public string Contenu { get; set; }
        public DateTime DateEnvoye { get; set; }
        public bool Lu { get; set; }

    
    }
}
