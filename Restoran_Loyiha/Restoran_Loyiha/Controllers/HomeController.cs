using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restoran_Loyiha.Models;

namespace Restoran_Loyiha.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db;
        public HomeController(AppDbContext _db)
        {
            db = _db;

        }
        public IActionResult Index()
        {
            var model = db.AsosiySahifalar.FirstOrDefault();
            return View(model); 
        }
       


        public IActionResult Menu()
        {
            var menyular = db.Taomlar
                         .Include(t => t.Oshpaz)
                         .OrderBy(t => t.Nomi)
                         .ToList();

            return View(menyular);
        }

        
        public IActionResult BizningJamoa()
        {
            var oshpazlar = db.Oshpazlar.ToList();
            return View(oshpazlar);
        }
        public IActionResult BizBilanAloqa()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Aloqa()
        {
            return View(new Aloqa());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendAloqa(Aloqa model)
        {
           
            model.Sana = DateTime.Now;
            db.Aloqalar.Add(model);
            db.SaveChanges();

            TempData["Success"] = "Xabaringiz yuborildi!";
            return RedirectToAction("Aloqa");
        }

        public IActionResult Stollar()
        {
            var stollar = db.Stollar.ToList();
            return View(stollar);
        }

        [HttpGet]
        public IActionResult JoyBandQilish()
        {
            ViewBag.Stollar = db.Stollar.ToList();
            return View(new Bron());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult JoyBandQilish(Bron model)
        {
           

            bool exists = db.Bronlar.Any(b =>
                b.StolId == model.StolId &&
                b.Sana == model.Sana &&
                b.Vaqt == model.Vaqt &&
                b.Holat != "Bekor qilindi");

            if (exists)
            {
                ModelState.AddModelError("", "Ushbu stol shu sana va vaqtda allaqachon band.");
                ViewBag.Stollar = db.Stollar.ToList();
                return View(model);
            }

            db.Bronlar.Add(model);
            db.SaveChanges();

            TempData["Success"] = "Bron saqlandi!";
            return RedirectToAction("JoyBandQilish");
        }



        public IActionResult BizHaqimizda()
        {
            var model = db.BizHaqimizda.FirstOrDefault();
            return View(model);
        }
    }
    }


