using System;
using System.Collections.Generic;

namespace SO2M.Models;

public partial class CritereRecherche
{
    public int Id { get; set; }

    public int? AgeMin { get; set; }

    public int? AgeMax { get; set; }

    public string? NiveauAcademique { get; set; }

    public string? Genre { get; set; }

    public string? OrientationS { get; set; }

    public string? Ville { get; set; }

    public int? RayonRecherche { get; set; }

 
}
