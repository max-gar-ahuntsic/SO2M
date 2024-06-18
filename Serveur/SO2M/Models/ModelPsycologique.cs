using System;
using System.Collections.Generic;

namespace SO2M.Models;

public partial class ModelPsycologique
{
    public int Id { get; set; }

    public string? TitreModel { get; set; }

    public string? AuteurModel { get; set; }

    public virtual ICollection<Axe> Axes { get; set; } = new List<Axe>();

    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
