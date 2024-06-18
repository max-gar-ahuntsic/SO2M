namespace SO2M.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ExpéditeurId { get; set; }
        public int DestinataireId { get; set; }
        public string Contenu { get; set; }
        public DateTime DateEnvoyé { get; set; }
        public bool Lu { get; set; }

        public virtual Utilisateur Expéditeur { get; set; }
        public virtual Utilisateur Destinataire { get; set; }
    }
}
