using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sammasati.App.Models;

[Table("asistencias")]
public partial class Asistencia
{
    [Key]
    [Column("id_asistencia")]
    public int IdAsistencia { get; set; }

    [Column("id_alumno")]
    public int IdAlumno { get; set; }

    [Column("id_clase")]
    public int IdClase { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("estado")]
    public string Estado { get; set; } = null!;

    [JsonIgnore]
    [ForeignKey("IdAlumno")]
    public virtual Alumno? IdAlumnoNavigation { get; set; }

    [JsonIgnore]
    [ForeignKey("IdClase")]
    public virtual Clase? IdClaseNavigation { get; set; }
}