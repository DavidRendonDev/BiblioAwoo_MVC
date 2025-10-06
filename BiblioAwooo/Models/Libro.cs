using System.ComponentModel.DataAnnotations;

namespace BibliotecaMVC.Models;

public class Libro
{
    public int Id {get; set;}
    
    
    [Required]
    public string Titulo {get; set;}
    
    public string Autor {get; set;}
    
    [Required]
    public string Codigo {get; set;}
    public int EjemplaresDisponibles {get; set;}

    public List<Prestamo> Prestamos { get; set; } = new List<Prestamo>(); 
}