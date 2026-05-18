using System;
using System.Collections.Generic;

namespace Sammasati.App.Models;

public partial class Profesore
{
    public int IdProfesor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();
}
