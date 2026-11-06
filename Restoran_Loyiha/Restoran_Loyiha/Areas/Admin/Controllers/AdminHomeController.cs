using Microsoft.AspNetCore.Hosting;  
using Microsoft.AspNetCore.Http;     
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Restoran_Loyiha.Migrations;
using Restoran_Loyiha.Models;
using Restoran_Loyiha.Models.VieModels;
using Restoran_Loyiha.Models.ViewModels;
using System.IO;
using System.Security.Claims;
using static Restoran_Loyiha.Models.Buyurtma;


namespace Restoran_Loyiha.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment env;

        public AdminHomeController(AppDbContext _db, IWebHostEnvironment _env)
        {
            db = _db;
            env = _env;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Buyurtma()
        {
            var data = db.Buyurtmalar
            .Include(b => b.Elementlar)
            .ThenInclude(e => e.Taom)
            .Select(b => new BuyurtmaListVM
            {
                Id = b.Id,
                Taom = b.Elementlar.FirstOrDefault().Taom.Nomi,
                TaomRasm = b.Elementlar.FirstOrDefault().Taom.RasmUrl,
                Holat = b.Holat,
                Summa = b.Summa
            })
            .ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult AddBuyurtma()
        {
            ViewBag.Stollar = db.Stollar.ToList();
            ViewBag.Taomlar = db.Taomlar.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBuyurtma(CreateBuyurtmaVM model)
        {
            ViewBag.Stollar = db.Stollar.ToList();
            ViewBag.Taomlar = db.Taomlar.ToList();

            var buyurtma = new Buyurtma
            {
                StolId = model.StolId,
                Holat = model.Holat,
                Summa = 0,
                Elementlar = new List<BuyurtmaElementlari>()
            };

            foreach (var item in model.Elementlar)
            {
                var taom = db.Taomlar.Find(item.TaomId);
                if (taom != null)
                {
                    var narx = (decimal)taom.Narx;

                    var element = new BuyurtmaElementlari
                    {
                        TaomId = taom.Id,
                        Miqdor = item.Miqdor,
                        UmumiyNarx = narx * item.Miqdor
                    };

                    buyurtma.Elementlar.Add(element);
                    buyurtma.Summa += element.UmumiyNarx;
                }
            }

            db.Buyurtmalar.Add(buyurtma);
            db.SaveChanges();

            return RedirectToAction("Buyurtma", "AdminHome");
        }


        public IActionResult EditBuyurtma(int id)
        {
            var buyurtma = db.Buyurtmalar
                             .Include(b => b.Elementlar)
                             .FirstOrDefault(b => b.Id == id);

            if (buyurtma == null)
                return NotFound();

            var model = new CreateBuyurtmaVM
            {
                Id = buyurtma.Id,
                StolId = buyurtma.StolId,
                Summa = buyurtma.Summa,
                Holat = buyurtma.Holat,
                Elementlar = buyurtma.Elementlar?.Select(e => new CreateBuyurtmaItemVM
                {
                    TaomId = e.TaomId,
                    Miqdor = e.Miqdor,
                    UmumiyNarx = e.UmumiyNarx
                }).ToList() ?? new List<CreateBuyurtmaItemVM>()
            };


            ViewBag.Stollar = db.Stollar.ToList() ?? new List<Stol>();
            ViewBag.Taomlar = db.Taomlar.ToList() ?? new List<Taom>();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBuyurtma(CreateBuyurtmaVM model)
        {
            if (!ModelState.IsValid || model.StolId == 0)
            {
                if (model.StolId == 0)
                    ModelState.AddModelError("StolId", "Stolni tanlang");

                ViewBag.Stollar = db.Stollar.ToList() ?? new List<Stol>();
                ViewBag.Taomlar = db.Taomlar.ToList() ?? new List<Taom>();
                return View(model);
            }

            var buyurtma = db.Buyurtmalar
                             .Include(b => b.Elementlar)
                             .FirstOrDefault(b => b.Id == model.Id);

            if (buyurtma == null)
                return NotFound();

            buyurtma.StolId = model.StolId;
            buyurtma.Summa = model.Summa;
            buyurtma.Holat = model.Holat;

            if (buyurtma.Elementlar == null)
                buyurtma.Elementlar = new List<BuyurtmaElementlari>();

            buyurtma.Elementlar.Clear();
            foreach (var e in model.Elementlar)
            {
                buyurtma.Elementlar.Add(new BuyurtmaElementlari
                {
                    TaomId = e.TaomId,
                    Miqdor = e.Miqdor,
                    UmumiyNarx = e.UmumiyNarx
                });
            }

            db.SaveChanges();
            TempData["Success"] = "Buyurtma muvaffaqiyatli yangilandi";

            return RedirectToAction("Buyurtma", "AdminHome");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBuyurtma(int id)
        {
            var buyurtma = db.Buyurtmalar
                .Include(b => b.Elementlar)
                .FirstOrDefault(b => b.Id == id);

            if (buyurtma != null)
            {
                db.BuyurtmaElementlari.RemoveRange(buyurtma.Elementlar);
                db.Buyurtmalar.Remove(buyurtma);
                db.SaveChanges();
            }

            return RedirectToAction("Buyurtma", "AdminHome");
        }


        //Stol tayyor
       
        public IActionResult Stollar()
        {
            var stollar = db.Stollar.ToList();
            return View(stollar);
        }


        [HttpGet]
        public IActionResult AddStol()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStol(Stol stol, IFormFile RasmFile)
        {

            if (RasmFile != null && RasmFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(RasmFile.FileName);

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/stollar");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await RasmFile.CopyToAsync(stream);
                }

                stol.RasmUrl = "/images/stollar/" + fileName;
            }

            db.Stollar.Add(stol);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Stollar));
        }


        public IActionResult EditStol(int id)
        {
            var stol = db.Stollar.FirstOrDefault(s => s.Id == id);
            if (stol == null) return NotFound();
            return View(stol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStol(Stol model, IFormFile RasmFile)
        {
            var stol = db.Stollar.FirstOrDefault(s => s.Id == model.Id);
            if (stol != null)
            {
                stol.StolRaqami = model.StolRaqami;
                stol.Sigimi = model.Sigimi;

                if (RasmFile != null && RasmFile.Length > 0)
                {
                    var fileName = Path.GetFileName(RasmFile.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/stollar", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        RasmFile.CopyTo(stream);
                    }

                    stol.RasmUrl = "/img/stollar/" + fileName;
                }
                db.SaveChanges();
                TempData["Success"] = "Stol yangilandi!";
            }

            return RedirectToAction("Stollar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStol(int id)
        {
            var stol = db.Stollar.Find(id);
            if (stol != null)
            {
                db.Stollar.Remove(stol);
                db.SaveChanges();
                TempData["Success"] = "Stol muvaffaqiyatli o'chirildi.";
            }
            else
            {
                TempData["Error"] = "Stol topilmadi!";
            }

            return RedirectToAction(nameof(Stollar));
        }





        //bron to'liq tayyor

        
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
            ViewBag.Stollar = db.Stollar.ToList();

            if (!ModelState.IsValid)
                return View(model);

            bool exists = db.Bronlar.Any(b =>
                b.StolId == model.StolId &&
                b.Sana == model.Sana &&
                b.Vaqt == model.Vaqt &&
                b.Holat != "Bekor qilindi");

            if (exists)
            {
                ModelState.AddModelError("", "Ushbu stol shu sana va vaqtda allaqachon band. Iltimos boshqa stol yoki vaqt tanlang.");
                return View(model);
            }

            db.Bronlar.Add(model);
            db.SaveChanges();

            TempData["Success"] = "Stol muvaffaqiyatli band qilindi!";
            return RedirectToAction("JoyBandQilish");
        }



        public IActionResult Bronlar()
        {
            
            var bronlar = db.Bronlar
                            .Include(b => b.Stol)
                            .OrderByDescending(b => b.Sana)
                            .ThenByDescending(b => b.Vaqt)
                            .ToList();

            return View(bronlar);
        }

      
        [HttpGet]
        public IActionResult AddBron()
        {
            ViewBag.Stollar = db.Stollar.ToList();
            return View(new Bron());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBron(Bron model)
        {
           
            bool exists = db.Bronlar.Any(b =>
                b.StolId == model.StolId &&
                b.Sana == model.Sana &&
                b.Vaqt == model.Vaqt &&
                b.Holat != "Bekor qilindi");

            if (exists)
            {
                ModelState.AddModelError("", "Ushbu stol shu sana va vaqtda allaqachon band. Iltimos boshqa stol yoki vaqt tanlang.");
                ViewBag.Stollar = db.Stollar.ToList();
                return View(model);
            }

            db.Bronlar.Add(model);
            db.SaveChanges();

            TempData["Success"] = "Yangi bron qo‘shildi!";
            return RedirectToAction("Bronlar", "AdminHome", new { area = "Admin" });

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBron(int id)
        {
            var bron = db.Bronlar.Find(id);
            if (bron != null)
            {
                db.Bronlar.Remove(bron);
                db.SaveChanges();
            }

            return RedirectToAction("Bronlar");
        }



        //Menyu tayyor 

        public IActionResult Menyu()
        {
            var menyu = db.Taomlar.Include(t => t.Oshpaz).ToList();
            return View(menyu);
        }

        [HttpGet]
        public IActionResult AddMenyu()
        {
            ViewBag.Oshpazlar = new SelectList(db.Oshpazlar.ToList(), "Id", "Ism");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMenyu(CreateMenyuVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Oshpazlar = new SelectList(db.Oshpazlar.ToList(), "Id", "Ism");
                return View(model);
            }

            if (!db.Oshpazlar.Any(o => o.Id == model.OshpazId))
            {
                ModelState.AddModelError("OshpazId", "Tanlangan oshpaz mavjud emas");
                ViewBag.Oshpazlar = new SelectList(db.Oshpazlar.ToList(), "Id", "Ism");
                return View(model);
            }

            var taom = new Taom
            {
                Nomi = model.Nomi,
                Tavsif = string.IsNullOrEmpty(model.Tavsif) ? "Tavsif mavjud emas" : model.Tavsif,
                Narx = model.Narx,  
                OshpazId = model.OshpazId
            };

          
            if (model.RasmUrl != null && model.RasmUrl.Length > 0)
            {
                string folder = Path.Combine(env.WebRootPath, "images/taomlar");
                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(model.RasmUrl.FileName);
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.RasmUrl.CopyTo(stream);
                }

                taom.RasmUrl = "/images/taomlar/" + fileName;
            }
            else
            {
                taom.RasmUrl = "/images/taomlar/default.png";
            }

            db.Taomlar.Add(taom);
            db.SaveChanges();

            return RedirectToAction("Menyu", "AdminHome", new { area = "Admin" });
        }

        public IActionResult EditMenyu(int id)
        {
            var taom = db.Taomlar.Find(id);
            if (taom == null) return NotFound();

            var taomVM = new EditMenyuVM
            {
                Id = taom.Id,
                Nomi = taom.Nomi,
                Narx = taom.Narx,
                Tavsif = taom.Tavsif,
                OshpazId = taom.OshpazId,
                EskiRasmUrl = string.IsNullOrEmpty(taom.RasmUrl) ? "/images/no-image.png" : taom.RasmUrl
            };

            var oshpazlar = db.Oshpazlar.Select(x => new
            {
                x.Id,
                FullName = x.Ism + " " + x.Familiya
            }).ToList();
            ViewBag.Oshpazlar = new SelectList(oshpazlar, "Id", "FullName");

            return View(taomVM);
        }

        [HttpPost]
        public IActionResult EditMenyu(EditMenyuVM model)
        {
            var menyu = db.Taomlar.Find(model.Id);
            menyu.Nomi = model.Nomi;
            menyu.Narx = model.Narx;
            menyu.Tavsif= model.Tavsif;
            menyu.OshpazId = model.OshpazId;

            if (model.YangiRasm != null)
            {
                string path = Path.Combine("wwwroot/taomlar", model.EskiRasmUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                var oshpaz = db.Oshpazlar.Find(model.OshpazId);
                string fileName = Guid.NewGuid().ToString() 
                + Path.GetExtension(model.YangiRasm.FileName);

                string path2 = Path.Combine("wwwroot/taomlar", fileName);
                using (var stream = new FileStream(path2, FileMode.Create))
                {
                    model.YangiRasm.CopyTo(stream);
                }

                menyu.RasmUrl = "/taomlar/"+ fileName;
            }
            db.Taomlar.Update(menyu);
            db.SaveChanges();
            return RedirectToAction("Menyu");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMenyu(int id)
        {
            var taom = db.Taomlar.Find(id);
            if (taom != null)
            {
               
                if (!string.IsNullOrEmpty(taom.RasmUrl))
                {
                    string rasmPath = Path.Combine(env.WebRootPath, taom.RasmUrl.TrimStart('/'));
                    if (System.IO.File.Exists(rasmPath))
                        System.IO.File.Delete(rasmPath);
                }

                db.Taomlar.Remove(taom);
                db.SaveChanges();
            }

            return RedirectToAction("Menyu");
        }



        //Oshpazlar tayyor 

        public IActionResult Oshpazlar()
        {
            var list = db.Oshpazlar.ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult AddOshpazlar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOshpazlar(CreateOshpazVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var oshpaz = new Oshpaz
            {
                Ism = model.Ism,
                Familiya = model.Familiya,
                Lavozim = model.Lavozim,
                TajribaYillari = model.TajribaYillari
            };

            // RASM SAQLASH
            if (model.RasmUrl != null && model.RasmUrl.Length > 0)
            {
                string folder = Path.Combine(env.WebRootPath, "oshpazlar");
                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(model.RasmUrl.FileName);
                string filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                model.RasmUrl.CopyTo(stream);

                oshpaz.RasmUrl = "/oshpazlar/" + fileName;
            }
            else
            {
                oshpaz.RasmUrl = "/images/no-image.png";
            }


            db.Oshpazlar.Add(oshpaz);
            db.SaveChanges();

            return RedirectToAction("Oshpazlar", "AdminHome", new { area = "Admin" });
        }



        public IActionResult EditOshpazlar(int id)
        {
            var oshpaz = db.Oshpazlar.Find(id);
            if (oshpaz == null)
                return NotFound();

            var oshpazVM = new EditOshpazVM
            {
                Id = oshpaz.Id,
                Ism = oshpaz.Ism,
                Familiya = oshpaz.Familiya,
                Lavozim = oshpaz.Lavozim,
                TajribaYillari = oshpaz.TajribaYillari,
                EskiRasmUrl = string.IsNullOrEmpty(oshpaz.RasmUrl) ? "/images/no-image.png" : oshpaz.RasmUrl
            };

            return View(oshpazVM);
        }


        [HttpPost]
        public IActionResult EditOshpazlar(EditOshpazVM model)
        {
            var oshpaz = db.Oshpazlar.Find(model.Id);
            oshpaz.Ism = model.Ism;
            oshpaz.Familiya = model.Familiya;
            oshpaz.Lavozim = model.Lavozim;
            oshpaz.TajribaYillari= model.TajribaYillari;


            if (model.YangiRasm != null)
            {
                string path = Path.Combine("wwwroot/oshpazlar", model.EskiRasmUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string fileName = Guid.NewGuid().ToString() 
                + Path.GetExtension(model.YangiRasm.FileName);

                string path2 = Path.Combine("wwwroot/oshpazlar", fileName);
                using (var stream = new FileStream(path2, FileMode.Create))
                {
                    model.YangiRasm.CopyTo(stream);
                }

                oshpaz.RasmUrl = "/oshpazlar/" + fileName;

            }
            db.Oshpazlar.Update(oshpaz);
            db.SaveChanges();
            return RedirectToAction("Oshpazlar");

        }

     
        public IActionResult DeleteOshpazlar(int id)
        {
            var oshpaz = db.Oshpazlar.Find(id);
            string path = Path.Combine("wwwroot/oshpazlar", oshpaz.RasmUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            db.Oshpazlar.Remove(oshpaz);
            db.SaveChanges();
            return RedirectToAction("Oshpazlar");
        }

        public IActionResult Aloqa()
        {
            var xabarlar = db.Aloqalar
                .Include(x => x.Buyurtma)
                .OrderByDescending(x => x.Sana)
                .ToList();

            return View(xabarlar);
        }

        public IActionResult ReadAloqa(int id)
        {
            var xabar = db.Aloqalar
                .Include(x => x.Buyurtma)
                .FirstOrDefault(x => x.Id == id);

            if (xabar == null) return NotFound();

            return View(xabar);
        }
     


        public IActionResult AsosiySahifa()
        {
            var model = db.AsosiySahifalar.FirstOrDefault(); 
            return View(model);
        }


        public IActionResult EditAsosiySahifa(int? id)
        {
            AsosiySahifa model;

            if (id == null)
            {
                model = new AsosiySahifa(); 
            }
            else
            {
                model = db.AsosiySahifalar.FirstOrDefault(a => a.Id == id);
                if (model == null) return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAsosiySahifa(AsosiySahifa model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    db.AsosiySahifalar.Add(model);
                }
                else
                {
                    var dbModel = db.AsosiySahifalar.FirstOrDefault(a => a.Id == model.Id);
                    if (dbModel != null)
                    {
                        dbModel.Sarlavha = model.Sarlavha;
                        dbModel.Tavsif = model.Tavsif;
                        dbModel.RasmUrl = model.RasmUrl;
                    }
                }

                db.SaveChanges();
                TempData["Success"] = "Ma’lumotlar saqlandi!";
                return RedirectToAction("AsosiySahifa");
            }

            return View(model);
        }



        public IActionResult BizHaqimizda()
        {
            var model = db.BizHaqimizda.FirstOrDefault();
            return View(model);
        }

        [HttpGet]
        public IActionResult EditBizHaqimizda(int? id)
        {
            if (id == null)
            {
                return View(new BizHaqimizda());
            }

            var model = db.BizHaqimizda.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBizHaqimizda(BizHaqimizda model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    db.BizHaqimizda.Add(model);
                }
                else
                {
                    var existing = db.BizHaqimizda.First(x => x.Id == model.Id);
                    existing.Sarlavha = model.Sarlavha;
                    existing.Tavsif = model.Tavsif;
                    existing.TajribaYil = model.TajribaYil;
                    existing.OshpazlarSoni = model.OshpazlarSoni;
                }

                db.SaveChanges();
                return RedirectToAction(
                "BizHaqimizda",
                "AdminHome",
                 new { area = "Admin" }
        );
            }

            return View(model);
        }
    }
}
