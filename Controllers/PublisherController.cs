using Library_System.Data;
using Library_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class PublisherController : Controller
{
    private readonly LibraryCtx _context;

    public PublisherController()
    {
        _context = new LibraryCtx();
    }

    public async Task<IActionResult> Index()
    {
        var publishers = await _context.Publishers.ToListAsync();
        return View(publishers);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePublisher(Publisher publisher)
    {
        if (publisher.Name != null)
        {
            _context.Add(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Create", publisher);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var publisher = await _context.Publishers.FindAsync(id);
        if (publisher == null) return NotFound();

        return View(publisher);
    }

    [HttpPost]
    public async Task<IActionResult> EditPublisher(Publisher publisher)
    {
        if (publisher.Name != null)
        {
            _context.Update(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Edit", publisher.PublisherID);
    }

    public async Task<IActionResult> DeletePublisher(int id)
    {
        var publisher = await _context.Publishers.FindAsync(id);
        if (publisher == null) return NotFound();

        _context.Publishers.Remove(publisher);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}