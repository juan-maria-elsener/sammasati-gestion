using System;
using System.Collections.Generic;

namespace Sammasati.App.Models;

public partial class Espacio
{
    public int IdEspacio { get; set; }

    public string NombreDireccion { get; set; } = null!;

    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();
}
