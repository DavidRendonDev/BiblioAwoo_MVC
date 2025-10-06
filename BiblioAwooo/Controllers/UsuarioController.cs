using BibliotecaMVC.Infrastructure;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BibliotecaMVC.Controllers;

public class UsuarioController : Controller
{
     private readonly DbAppContext _context;
    
    public UsuarioController(DbAppContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var  usuarios = _context.Usuarios.AsNoTracking().ToList();
        return View(usuarios);
    }
    
    //create
    public IActionResult Create()
    {
        return View();
    }
    //POST:create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Usuario usuarios)
    {
        try
        {
            if (_context.Usuarios.Any(u => u.Documento == usuarios.Documento))
            {
                ModelState.AddModelError(nameof(usuarios.Documento), "Documento ya existe");
                return View(usuarios);
            }

            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuarios);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error al guardar usuario: " + ex.Message);
        }
        return View(usuarios);
    }
    
    //GET: Details
    public IActionResult Details(int? id)
    {
        if (id == null) return NotFound();
        
        var usuario = _context.Usuarios
            .Include(u => u.Prestamos)
            .ThenInclude(p => p.Libro)
            .AsNoTracking()
            .FirstOrDefault(U => U.Id == id);
        
        if (usuario == null) return NotFound();
        
        return View(usuario);
    }
    
    //GET: Edit
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        
        var usuario = _context.Usuarios.Find(id);
        if (usuario == null) return NotFound();
            
        return View(usuario);
    }
    
    //POST: Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Usuario usuarios)
    {
        if (id != usuarios.Id) return BadRequest();

        try
        {
            if (_context.Usuarios.Any(u => u.Documento == usuarios.Documento && u.Id != usuarios.Id))
            {
                ModelState.AddModelError(nameof(usuarios.Documento), "Documento ya existe");
                return View(usuarios);
            }

            if (ModelState.IsValid)
            {
                _context.Update(usuarios);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
           ModelState.AddModelError(string.Empty, "Error al actualizar: " + ex.Message);
        }
        return View(usuarios);
    }
    
    //GET: Delete 
    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();

        var usuario = _context.Usuarios.Find(id);
        if (usuario == null) return NotFound();
        return View(usuario);
    }
    
    //POST: Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return NotFound();
            
            var tienePrestamosActivos = _context.Prestamos.Any(p => p.UsuarioId == id );
            if (tienePrestamosActivos)
            {
                ModelState.AddModelError(string.Empty, "el usuario tiene prestamos activos no se puede eliminar ");
                return View(usuario);
            }
            
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty,"Error al eliminar: " + ex.Message);
            var usuario = _context.Usuarios.Find(id);
            
            return View(usuario);
        }
    }
}