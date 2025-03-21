using Library_System.Data;
using Library_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class AuthorController : Controller
{
    private readonly LibraryCtx _context;

    public AuthorController()
    {
        _context = new LibraryCtx();
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _context.Authors.ToListAsync();
        return View(authors);
    }

    public IActionResult Create(Author author)
    {
        return View(author);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(Author author)
    {
        if (author.Name != null)
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Create", author);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return NotFound();

        return View(author);
    }

    [HttpPost]
    public async Task<IActionResult> EditAuthor(Author author)
    {
        if (author.Name != null)
        {
            _context.Update(author);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Edit", author.AuthorID);
    }

    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return NotFound();

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}