using System;
using System.Collections.Generic;

namespace Sammasati.App.Models;

public partial class PagosCuota
{
    public int IdPago { get; set; }

    public int IdAlumno { get; set; }

    public int Mes { get; set; }

    public int Anio { get; set; }

    public string? Estado { get; set; }

    public DateOnly? FechaPago { get; set; }

    public decimal? Monto { get; set; }

    public virtual Alumno IdAlumnoNavigation { get; set; } = null!;
}
