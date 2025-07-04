using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPSProject.Database;

namespace PPSProject.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const int PageSize = 15;

        public ActivitiesController(ApplicationDbContext db) => _db = db;

        // GET /Activities?search=foo&page=2
        public async Task<IActionResult> Index(string? search, int page = 1,
                                       string sort = "date", string dir = "desc")
        {
            const int PageSize = 15;
            var query = _db.Activities.AsQueryable();

           
            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.ToLowerInvariant();
                query = query.Where(a => a.Title.ToLower().Contains(term)
                                      || a.Description.ToLower().Contains(term));
            }

            // sorting functionality
            query = (sort, dir) switch
            {
                ("type", "asc") => query.OrderBy(a => a.Type),
                ("type", "desc") => query.OrderByDescending(a => a.Type),
                ("date", "asc") => query.OrderBy(a => a.DateUtc),
                _ => query.OrderByDescending(a => a.DateUtc) // default date‑desc
            };

            // PAGINATION
            var total = await query.CountAsync();
            var pages = (int)Math.Ceiling(total / (double)PageSize);
            var items = await query.Skip((page - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToListAsync();

            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.Pages = pages;
            ViewBag.Sort = sort;
            ViewBag.Dir = dir;

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavourite(int id)
        {
            var act = await _db.Activities.FindAsync(id);
            if (act == null) return NotFound();

            act.IsFavourite = !act.IsFavourite;
            await _db.SaveChangesAsync();

            return Ok(new { isFavourite = act.IsFavourite });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleReadLater(int id)
        {
            var act = await _db.Activities.FindAsync(id);
            if (act == null) return NotFound();

            act.IsReadLater = !act.IsReadLater;
            await _db.SaveChangesAsync();

            return Ok(new { isReadLater = act.IsReadLater });
        }

        public async Task<IActionResult> Favourites()
        {
            var favs = await _db.Activities
                .Where(a => a.IsFavourite)
                .OrderByDescending(a => a.DateUtc)
                .ToListAsync();

            ViewBag.Title = "Favourites";
            return View("Filtered", favs);
        }

        public async Task<IActionResult> ReadLater()
        {
            var read = await _db.Activities
                .Where(a => a.IsReadLater)
                .OrderByDescending(a => a.DateUtc)
                .ToListAsync();

            ViewBag.Title = "Read Later";
            return View("Filtered", read);
        }

    }
}
