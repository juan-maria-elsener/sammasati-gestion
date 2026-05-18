using System;
using System.Collections.Generic;

namespace Sammasati.App.Models;

public partial class Alumno
{
    public int IdAlumno { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? Observaciones { get; set; }

    public DateOnly? FechaAlta { get; set; }

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    public virtual ICollection<PagosCuota> PagosCuota { get; set; } = new List<PagosCuota>();
}
