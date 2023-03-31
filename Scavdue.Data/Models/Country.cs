using System;
using System.Collections.Generic;

namespace Scavdue.Data.Models;

public partial class Country : Entity
{
    public string Name { get; set; } = null!;

    public string Iso3166 { get; set; } = null!;

    public virtual ICollection<AdministrativeUnit> AdministrativeUnits { get; } = new List<AdministrativeUnit>();
}
