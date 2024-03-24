using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TechBlogApp.Models;

namespace TechBlogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogDbContext _blogDbContext;

        public HomeController(ILogger<HomeController> logger, BlogDbContext blogDbContext)
        {
            _logger = logger;
            _blogDbContext = blogDbContext;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var blogPosts = await _blogDbContext.BlogPosts.ToListAsync();
                return View(blogPosts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
            
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Create([Bind("Title,Author,Summary")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost.Publication_date = DateTime.Now;
                _blogDbContext.BlogPosts.Add(blogPost);
                _blogDbContext.SaveChangesAsync();

                
                return RedirectToAction("Index", "Home");
            }

            
            return View("Error");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _blogDbContext.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        [HttpPost]
       
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content")] BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogDbContext.Update(blogPost);
                    await _blogDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(EditConfirmation));
            }
            return View(blogPost);
        }

        private bool BlogPostExists(int id)
        {
            return _blogDbContext.BlogPosts.Any(e => e.Id == id);
        }

        public IActionResult EditConfirmation()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _blogDbContext.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _blogDbContext.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            _blogDbContext.BlogPosts.Remove(blogPost);
            await _blogDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
