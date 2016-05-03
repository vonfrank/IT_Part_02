using System.Linq;
using Microsoft.AspNet.Mvc;
using IT_Part_02.Models;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNet.Http;

namespace IT_Part_02.Controllers
{
    public class ImagesController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUser _currentUser;

        public ImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Images
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                _currentUser = GetUser(User.GetUserId());

                return View(_context.Image.Where(i => i.Author.Id == _currentUser.Id).ToList());
            }
            else
            {
                ViewData["Message"] = "You must login, in order to view this page.";

                return View("Error");
            }
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

                _currentUser = GetUser(User.GetUserId());
                if (image.Author.Id == _currentUser.Id)
                {
                    return View(image);
                }
                else
                {
                    ViewData["Message"] = "You are not the owner of this image.";

                    return View("Error");
                }
            }
            else
            {
                ViewData["Message"] = "You must login, to view details.";

                return View("Error");
            }
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                ViewData["Message"] = "You must login, to upload new images.";

                return View("Error");
            }
        }

        // POST: Images/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Image image, IFormFile file)
        {
            if (User.Identity.IsAuthenticated)
            {
                _currentUser = GetUser(User.GetUserId());

                if (ModelState.IsValid)
                {
                    image.Author = _currentUser;
                    //image.MimeType = file.ContentType;

                    if (file != null)
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            byte[] buffer = new byte[16 * 1024];
                            using (MemoryStream ms = new MemoryStream())
                            {
                                int read;
                                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    ms.Write(buffer, 0, read);
                                }
                                image.Data = ms.ToArray();
                            }
                        }
                    }
                    else
                    {
                        ViewData["Message"] = "No file was selected";
                        return View("Error");
                    }

                    image.Likes = 0;

                    _context.Image.Add(image);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(image);
            }
            else
            {
                ViewData["Message"] = "You must login, to upload new images.";

                return View("Error");
            }
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

                _currentUser = GetUser(User.GetUserId());
                if (image.Author.Id == _currentUser.Id)
                {
                    return View(image);
                }
                else
                {
                    ViewData["Message"] = "You are not the owner of this image.";

                    return View("Error");
                }
            }
            else
            {
                ViewData["Message"] = "You must login, to edit images";

                return View("Error");
            }
        }

        // POST: Images/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Image image, IFormFile file)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            byte[] buffer = new byte[16 * 1024];
                            using (MemoryStream ms = new MemoryStream())
                            {
                                int read;
                                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    ms.Write(buffer, 0, read);
                                }
                                image.Data = ms.ToArray();
                            }
                        }
                    }
                    else
                    {
                        ViewData["Message"] = "No file was selected";
                        return View("Error");
                    }
                    _context.Update(image);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(image);
            }
            else
            {
                ViewData["Message"] = "You must login, to edit images.";

                return View("Error");
            }
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

                _currentUser = GetUser(User.GetUserId());
                if (image.Author.Id == _currentUser.Id)
                {
                    return View(image);
                }
                else
                {
                    ViewData["Message"] = "You are not the owner of this image.";

                    return View("Error");
                }
            }
            else
            {
                ViewData["Message"] = "You must login, to delete images.";

                return View("Error");
            }
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
            else
            {
                ViewData["Message"] = "You must login, to delete images.";

                return View("Error");
            }
        }

        public ApplicationUser GetUser(string id)
        {
            var users = from u in _context.Users select u;

            foreach(ApplicationUser u in users)
            {
                if(u.Id == id)
                {
                    return u;
                }
            }

            return null;
        }

        public IActionResult GetImage(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var image = from i in _context.Image select i;

                foreach (Image i in image)
                {
                    if (i.ImageID == id)
                    {
                        byte[] imageData = i.Data;
                        return File(imageData, "image/png");
                    }
                }
            }
            else
            {
                return View("Error");
            }

            return null;
        }

        [HttpPost]
        public int LikeImage(int id)
        {
            Image image = _context.Image.Single(m => m.ImageID == id);
            image.Likes = image.Likes + 1;
            _context.SaveChanges();

            return image.Likes;
        }
    }
}
