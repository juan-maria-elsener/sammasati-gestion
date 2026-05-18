using System;
using System.Collections.Generic;

namespace Sammasati.App.Models;

public partial class Inscripcione
{
    public int IdInscripcion { get; set; }

    public int IdAlumno { get; set; }

    public int IdClase { get; set; }

    public string? Estado { get; set; }

    public virtual Alumno IdAlumnoNavigation { get; set; } = null!;

    public virtual Clase IdClaseNavigation { get; set; } = null!;
}
