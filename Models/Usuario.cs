using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sammasati.App.Models;

[Table("usuarios")]
public partial class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("email")]
    public string Email { get; set; } = null!;

    [Column("password")]
    public string Password { get; set; } = null!;

    [Column("rol")]
    public string Rol { get; set; } = null!;
}