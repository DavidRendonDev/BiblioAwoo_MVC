using BibliotecaMVC.Infrastructure;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Controllers;

public class LibroController : Controller
{
    private readonly DbAppContext _context;
    
    public LibroController(DbAppContext context)
    {
        _context = context;
    }
    
    //Listar Libros

    public async Task<IActionResult> Index()
    {
        var libros = await _context.Libros.ToListAsync();
        return View(libros);
    }
    
    //Formulario de Creacion

    public IActionResult Create()
    {
        return View();
    }
    
    //Guardar Libro Nuevo
    [HttpPost]
    public async Task<IActionResult> Create(Libro libro)
    {
        //Validar si tiene el mismo codigo para un libro 
        var existeCodigo = await _context.Libros.AnyAsync(l => l.Codigo == libro.Codigo);
        if (existeCodigo)
        {
            ModelState.AddModelError("codigo", "El codigo ya existe, Ingrese uno diferente por favor ");
            return View(libro);
        }

        if (ModelState.IsValid)
        {
            _context.Add(libro);
            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Index));
        }
        return View(libro);
    }
    
    //Editar Libro

    public async Task<IActionResult> Edit(int id)
    {
        var libro = await _context.Libros.FindAsync(id);
        if (libro == null)
            return NotFound();
           
        return View(libro);
                
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Libro libro)
    {
        if (id != libro.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            { 
                _context.Update(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }
        return View(libro);
    }
    
    //Eliminar libro 

    public async Task<IActionResult> Delete(int id)
    {
        var libro = await _context.Libros.FindAsync(id);
        if (libro == null)
            return NotFound();
        
        return View(libro);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var libro = await _context.Libros.FindAsync(id);
        if (libro != null)
        {
            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}