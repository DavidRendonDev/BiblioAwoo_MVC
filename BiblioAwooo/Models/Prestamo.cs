using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMVC.Models;

public class Prestamo
{
    
    [Key]
    public int Id { get; set; }
    
    public int UsuarioId { get; set; }
    public int LibroId { get; set; }
    
    public DateTime FechaPrestamo { get; set; } = DateTime.Now;
    
    public DateTime FechaDevolucion { get; set; }
    
    [ForeignKey("LibroId")]
    public Libro Libro { get; set; }
    
   [ForeignKey("UsuarioId")]
    public Usuario Usuario { get; set; }
}
