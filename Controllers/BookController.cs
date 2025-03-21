using Library_System.Data;
using Library_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class BookController : Controller
{
    private readonly LibraryCtx _context;

    public BookController()
    {
        _context = new LibraryCtx();
    }

    public async Task<IActionResult> Index()
    {
        var books = await _context.Books.Include(b => b.Author).Include(b => b.Publisher).ToListAsync();
        return View(books);
    }

    public IActionResult Create(Book book)
    {
        ViewData["Authors"] = new SelectList(_context.Authors, "AuthorID", "Name");
        ViewData["Publishers"] = new SelectList(_context.Publishers, "PublisherID", "Name");
        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(Book book)
    {
        if (book.Title != null)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Create", book);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();

        ViewData["Authors"] = new SelectList(_context.Authors, "AuthorID", "Name", book.AuthorID);
        ViewData["Publishers"] = new SelectList(_context.Publishers, "PublisherID", "Name", book.PublisherID);
        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> EditBook(Book book)
    {
        if (book.Title != null)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Edit", book.BookID);
    }

    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}