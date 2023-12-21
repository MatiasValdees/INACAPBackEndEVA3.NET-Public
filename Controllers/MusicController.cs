using EVA3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EVA3.Controllers
{
    [Authorize(Roles = "SuperAdministrador")]
    public class MusicController : Controller
    {
        private readonly AppDbContext _context;

        public MusicController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.TblMusic.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Music m)
        {

            if (m!=null)
            {
                _context.TblMusic.Add(m);
                _context.SaveChanges();
                return RedirectToAction("Index","Music");
            }
            else
            {
                ModelState.AddModelError("", "Error");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var existe = _context.TblMusic.Find(Id);
            if (existe!=null)
            {
                return View(existe);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(Music m)
        {
            if (m!=null) 
            {
                _context.Update(m);
                _context.SaveChanges(true);
                return RedirectToAction("Index", "Music");
            }
            else
            {
                ModelState.AddModelError("", "Ocurrio un Error");
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            Music m = _context.TblMusic.Find(id);
            if (m != null)
            {
                _context.TblMusic.Remove(m);
                _context.SaveChanges();
                return RedirectToAction("Index", "Music");
            }
            return NotFound();

        }
    }
}
