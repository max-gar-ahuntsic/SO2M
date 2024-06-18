using System;
using System.Collections.Generic;

namespace SO2M.Models;

public partial class Photo
{
    public int Id { get; set; }

    public string? Url { get; set; }

    public DateOnly? DateAjout { get; set; }

    public bool? PhotoProfil { get; set; }

    public int? UtilisateurId { get; set; }

    public virtual Utilisateur? Utilisateur { get; set; }

    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
