using System;
using System.Collections.Generic;

namespace Sammasati.App.Models;

public partial class Clase
{
    public int IdClase { get; set; }

    public int IdProfesor { get; set; }

    public int IdEspacio { get; set; }

    public int IdModalidad { get; set; }

    public string Dias { get; set; } = null!;

    public TimeOnly Horario { get; set; }

    public int? CupoMaximo { get; set; }

    public virtual Espacio IdEspacioNavigation { get; set; } = null!;

    public virtual Modalidade IdModalidadNavigation { get; set; } = null!;

    public virtual Profesore IdProfesorNavigation { get; set; } = null!;

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();
}
