using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SO2M.Models
{
    public partial class Utilisateur
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Genre { get; set; }
        public string? NiveauAcademique { get; set; }
        public int? Age { get; set; }
        public string? OrientationS { get; set; }
        public string? Courriel { get; set; }
        public string? Username { get; set; }
        public string? MotDePasse { get; set; }
        public bool? Est_Active { get; set; }
        public DateOnly? DateCreationProfil { get; set; }
        public int? CritereRechercheId { get; set; }
        public int? ModelPsycologiqueId { get; set; }
        
        //----------- added 24-06-22-------------
        public int? Modele1Axe1 { get; set; }
        public int? Modele1Axe2 { get; set; }
        public int? Modele1Axe3 { get; set; }

     
        public virtual CritereRecherche? CritereRecherche { get; set; }
        public virtual ModelPsycologique? ModelPsycologique { get; set; }
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public virtual ICollection<Utilisateur> Favoris { get; set; } = new List<Utilisateur>();
        public virtual ICollection<Utilisateur> Matches { get; set; } = new List<Utilisateur>();
        public virtual ICollection<Photo> PhotosNavigation { get; set; } = new List<Photo>();
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
        public virtual ICollection<Utilisateur> UtilisateursNavigation { get; set; } = new List<Utilisateur>();
        public virtual ICollection<Message> MessagesEnvoyés { get; set; } = new List<Message>(); // Ajout des messages envoyés
        public virtual ICollection<Message> MessagesReçus { get; set; } = new List<Message>(); // Ajout des messages reçus
    }
}
