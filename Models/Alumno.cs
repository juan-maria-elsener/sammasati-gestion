using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sammasati.App.Models;

public partial class Alumno
{
    public int IdAlumno { get; set; }

    [Required(ErrorMessage = "El nombre del alumno es completamente obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El apellido del alumno es obligatorio.")]
    [StringLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres.")]
    public string Apellido { get; set; } = null!;

    [RegularExpression(@"^[0-9]+$", ErrorMessage = "El número de teléfono solo puede contener números, sin letras ni espacios.")]
    [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El formato del correo electrónico ingresado no es válido.")]
    public string? Email { get; set; }

    // Con la librería Schema activa, el [Column] ya se conecta perfecto a MySQL
    [Column("observaciones")]
    [StringLength(500, ErrorMessage = "Las observaciones no pueden superar los 500 caracteres.")]
    public string? Observaciones { get; set; }

    [Column("fecha_alta")]
    public DateOnly? FechaAlta { get; set; }

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    // Si tu modelo no usa PagosCuotas, podés borrar esta última línea sin problema
    public virtual ICollection<PagosCuota> PagosCuotas { get; set; } = new List<PagosCuota>();
}