using System;
using System.Collections.Generic;

namespace Sammasati.App.Models;

public partial class Modalidade
{
    public int IdModalidad { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();
}
