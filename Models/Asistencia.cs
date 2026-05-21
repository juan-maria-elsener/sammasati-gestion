using System;
using System.Text.Json.Serialization;

namespace Sammasati.App.Models;

public partial class Asistencia
{
    public int IdAsistencia { get; set; }
    public int IdAlumno { get; set; }
    public int IdClase { get; set; }
    public DateOnly Fecha { get; set; }
    public string Estado { get; set; } = null!;

    [JsonIgnore]
    public virtual Alumno? IdAlumnoNavigation { get; set; }
    [JsonIgnore]
    public virtual Clase? IdClaseNavigation { get; set; }
}