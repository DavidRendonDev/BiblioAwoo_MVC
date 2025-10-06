using System.ComponentModel.DataAnnotations;

namespace BibliotecaMVC.Models;

public class Usuario
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }
    [Required]
    [StringLength(50)]
    public string Documento { get; set; }
    [EmailAddress]
    public string Correo { get; set; }
    
    public string Telefono { get; set; }
    public List<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}