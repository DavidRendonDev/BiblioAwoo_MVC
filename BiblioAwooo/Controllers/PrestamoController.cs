using BibliotecaMVC.Infrastructure;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiblioAwoo.Controllers;

public class PrestamoController : Controller 
{
    private readonly DbAppContext _context;

    public PrestamoController(DbAppContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var prestamos = await _context.Prestamos
            .Include(p => p.Usuario)
            .Include(p => p.Libro)
            .ToListAsync();

        ViewBag.Usuarios = await _context.Usuarios.ToListAsync();
        ViewBag.Libros = await _context.Libros.ToListAsync();

        return View(prestamos);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(Prestamo prestamo)
    {
        try
        {
            var libro = await _context.Libros.FirstOrDefaultAsync(l => l.Id == prestamo.LibroId);
            if (libro == null)
            {
                ViewBag.Error = "El libro seleccionado no existe.";
            }
            else if (libro.EjemplaresDisponibles <= 0)
            {
                ViewBag.Error = "No hay ejemplares disponibles para este libro.";
            }
            else
            {
                libro.EjemplaresDisponibles -= 1;
                await _context.Prestamos.AddAsync(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Libros = await _context.Libros.ToListAsync();
            ViewBag.Usuarios = await _context.Usuarios.ToListAsync();
            var prestamos = await _context.Prestamos.ToListAsync();
            return View("Index", prestamos);
        }
        catch (Exception err)
        {
            ViewBag.Error = $"Error al registrar prÃ©stamo: {err.Message}";
            ViewBag.Libros = await _context.Libros.ToListAsync();
            ViewBag.Usuarios = await _context.Usuarios.ToListAsync();
            var prestamos = await _context.Prestamos.ToListAsync();
            return View("Index", prestamos);
        }
    }


    public async Task<IActionResult> Devolver(int id)
    {
        var prestamo = await _context.Prestamos
            .Include(p => p.Libro)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (prestamo != null)
        {
            prestamo.Libro.EjemplaresDisponibles += 1;
            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> HistorialUsuario(int id)
    {
        var prestamos = await _context.Prestamos
            .Include(p => p.Libro)
            .Where(p => p.UsuarioId == id)
            .ToListAsync();

        ViewBag.Usuario = await _context.Usuarios.FindAsync(id);
        return View("HistorialUsuario", prestamos);
    }
    
    public async Task<IActionResult> PrestamosPorLibro(int id)
    {
        var prestamos = await _context.Prestamos
            .Include(p => p.Usuario)
            .Where(p => p.LibroId == id)
            .ToListAsync();

        ViewBag.Libro = await _context.Libros.FindAsync(id);
        return View("PrestamosPorLibro", prestamos);
    }

}