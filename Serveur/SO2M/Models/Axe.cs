using System;
using System.Collections.Generic;

namespace SO2M.Models;

public partial class Axe
{
    public int Id { get; set; }

    public string? TitreAxe { get; set; }

    public int? Score { get; set; }

    public int? ModelPsycologiqueId { get; set; }

    public virtual ModelPsycologique? ModelPsycologique { get; set; }
}
