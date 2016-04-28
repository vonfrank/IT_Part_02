using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using IT_Part_02.Models;

namespace IT_Part_02.Controllers
{
    public class ImagesController : Controller
    {
        private ApplicationDbContext _context;

        public ImagesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Images
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(_context.Image.ToList());
            }

            ViewData["Message"] = "You must login, in order to view this page.";

            return View("Error");
        }

        // GET: Images/Details/5
        public IActionResult Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return HttpNotFound();
                }

                Image image = _context.Image.Single(m => m.ImageID == id);
                if (image == null)
                {
                    return HttpNotFound();
                }

                return View(image);
            }

            ViewData["Message"] = "You must login, to view details.";

            return View("Error");
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            ViewData["Message"] = "You must login, to upload new images.";

            return View("Error");
        }

        // POST: Images/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Image image)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Image.Add(image);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(image);
            }

            ViewData["Message"] = "You must login, to upload new images.";

            return View("Error");
        }

        // GET: Images/Edit/5
        public IActionResult Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return HttpNotFound();
                }

                Image image = _context.Image.Single(m => m.ImageID == id);
                if (image == null)
                {
                    return HttpNotFound();
                }
                return View(image);
            }

            ViewData["Message"] = "You must login, to edit images";

            return View("Error");
        }

        // POST: Images/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Image image)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Update(image);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(image);
            }

            ViewData["Message"] = "You must login, to edit images.";

            return View("Error");
        }

        // GET: Images/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return HttpNotFound();
                }

                Image image = _context.Image.Single(m => m.ImageID == id);
                if (image == null)
                {
                    return HttpNotFound();
                }

                return View(image);
            }

            ViewData["Message"] = "You must login, to delete images.";

            return View("Error");
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Image image = _context.Image.Single(m => m.ImageID == id);
                _context.Image.Remove(image);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["Message"] = "You must login, to delete images.";

            return View("Error");
        }
    }
}
