using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Fonksiyonlar;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.Animal;
using VeterinerApp.Models.ViewModel.Animal;

namespace VeterinerApp.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly VeterinerDBContext _context;
        private readonly IEmailSender _emailSender;
        public AnimalController(UserManager<AppUser> usermanager, VeterinerDBContext context, IEmailSender emailSender)
        {
            _userManager = usermanager;
            _context = context;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult AddAnimal()
        {
            AddAnimalViewModel model = new(_context);

            model.HayvanAnneListesi = model.AnnelerListesiOlustur();
            model.HayvanBabaListesi = model.BabalarListesiOlustur();
            model.RenklerListesi = model.RenkListesiOlustur();
            model.TurlerListesi = model.TurListesiOlustur();
            model.CinslerListesi = model.CinsListesiOlustur();
            model.CinsiyetListesi = model.CinsiyetListesiOlustur();

            return View(model);

        }

        public JsonResult CinsleriGetir(int turId)
        {
            var cinsler = _context.CinsTur
                .Where(ct => ct.TurId == turId)
                .Join(_context.Cinsler,
                      ct => ct.CinsId,
                      c => c.CinsId,
                      (ct, c) => new SelectListItem
                      {
                          Text = c.CinsAdi,
                          Value = c.CinsId.ToString()
                      }).ToList();

            return Json(cinsler);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimal(AddAnimalViewModel model)
        {

            var validator = new HayvanEkleValidator();
            var result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                model.HayvanAnneListesi = model.AnnelerListesiOlustur();
                model.HayvanBabaListesi = model.BabalarListesiOlustur();
                model.RenklerListesi = model.RenkListesiOlustur();
                model.TurlerListesi = model.TurListesiOlustur();
                model.CinslerListesi = model.CinsListesiOlustur();
                model.CinsiyetListesi = model.CinsiyetListesiOlustur();

                return View(model);

            };

            Hayvan hayvan = new Hayvan();


            hayvan.HayvanAdi = model.HayvanAdi.ToUpper();
            hayvan.HayvanCinsiyet = model.HayvanCinsiyet;
            hayvan.HayvanKilo = model.HayvanKilo;
            hayvan.HayvanDogumTarihi = model.HayvanDogumTarihi;
            hayvan.HayvanOlumTarihi = model.HayvanOlumTarihi;
            hayvan.HayvanAnneId = model.HayvanAnneId;
            hayvan.HayvanBabaId = model.HayvanBabaId;
            var cinsTur = _context.CinsTur
                                    .Where(ct => ct.CinsId == model.SecilenCinsId && ct.TurId == model.SecilenTurId)
                                    .FirstOrDefault();
            hayvan.CinsTur = cinsTur;
            hayvan.RenkId = model.RenkId;




            if (model.PhotoOption == "changePhoto" && model.filePhoto != null)
            {
                var dosyaUzantısı = Path.GetExtension(model.filePhoto.FileName);
                var dosyaAdi = string.Format($"{Guid.NewGuid()}{dosyaUzantısı}");
                var hayvanKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\animals", hayvan.HayvanId.ToString());

                if (!Directory.Exists(hayvanKlasoru))
                {
                    Directory.CreateDirectory(hayvanKlasoru);
                }

                var filePath = Path.Combine(hayvanKlasoru, dosyaAdi);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.filePhoto.CopyToAsync(stream);
                }

                // Web URL'sini oluşturma
                var fileUrl = $"/img/animals/{hayvan.HayvanId}/{dosyaAdi}";

                // Veritabanına URL'yi kaydetme
                hayvan.ImgUrl = fileUrl;
            }
            else
            {
                hayvan.ImgUrl = null;

            }

            SahipHayvan sahipHayvan = new SahipHayvan();
            sahipHayvan.SahiplenmeTarihi = model.SahiplenmeTarihi;
            sahipHayvan.SahipId = _userManager.GetUserAsync(User).Result.Id;
            sahipHayvan.HayvanId = hayvan.HayvanId;

            await _context.Hayvanlar.AddAsync(hayvan);
            await _context.SahipHayvan.AddAsync(sahipHayvan);
            await _context.SaveChangesAsync();

            TempData["AddAnimal"] = $"{model.HayvanAdi} isimli hayvan başarıyla eklendi.";

            return RedirectToAction();

        }

        [HttpGet]
        public async Task<IActionResult> Information(int hayvanId)
        {
            var hayvan = await _context.Hayvanlar.FindAsync(hayvanId);
            var kullanici = await _userManager.GetUserAsync(User);

            var kullaniciHayvani = _context.SahipHayvan
                .Where(sh => sh.HayvanId == hayvanId && sh.SahipId == kullanici.Id)
                .FirstOrDefault();

            if (kullaniciHayvani == null)
            {
                return View("BadRequest");
            }

            HayvanlarBilgiViewModel model = new(_context);
            model = model.HayvanBilgileriniGetir(hayvan, kullanici);

            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> EditAnimal(int hayvanId)
        {

            var hayvan = await _context.Hayvanlar.FindAsync(hayvanId);
            var user = await _userManager.GetUserAsync(User);

            var userHayvan = _context.SahipHayvan
                .Where(sh => sh.HayvanId == hayvanId && sh.AppUser.InsanTckn == user.InsanTckn)
                .FirstOrDefault();

            if (userHayvan == null)
            {
                return View("BadRequest");
            }
            EditAnimalViewModel model = new(_context);
            model = model.ModelOlustur(hayvan, user);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditAnimal(EditAnimalViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!Signature.VerifySignature(model.HayvanId, model.SahipTckn, model.Imza))
            {
                return View("Badrequest");
            }

            EditHayvanValidator validator = new EditHayvanValidator();
            ValidationResult result = validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                EditAnimalViewModel returnModel = new(_context);

                return View("EditAnimal", returnModel.ModelOlustur(model, user));
            }


            var hayvan = await _context.Hayvanlar.FindAsync(model.HayvanId);
            if (model.PhotoOption == "changePhoto" && model.filePhoto != null)
            {
                var dosyaUzantısı = Path.GetExtension(model.filePhoto.FileName);

                var dosyaAdi = string.Format($"{Guid.NewGuid()}{dosyaUzantısı}");
                var hayvanKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\animals", hayvan.HayvanId.ToString());

                if (!Directory.Exists(hayvanKlasoru))
                {
                    Directory.CreateDirectory(hayvanKlasoru);
                }
                var eskiFotograflar = Directory.GetFiles(hayvanKlasoru);
                if (eskiFotograflar.Length > 0)
                {
                    foreach (var eskiFotograf in eskiFotograflar)
                    {
                        System.IO.File.Delete(eskiFotograf);
                    }
                }


                var filePath = Path.Combine(hayvanKlasoru, dosyaAdi);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.filePhoto.CopyToAsync(stream);
                }

                // Web URL'sini oluşturma
                var fileUrl = $"/img/animals/{hayvan.HayvanId}/{dosyaAdi}";

                hayvan.ImgUrl = fileUrl;

            }
            else if (model.PhotoOption == "changePhoto" && model.filePhoto == null)
            {
                hayvan.ImgUrl = _context.Hayvanlar.Find(model.HayvanId).ImgUrl;
            }
            else if (model.PhotoOption == "deletePhoto")
            {
                hayvan.ImgUrl = null;
            }
            else if (model.PhotoOption == "keepPhoto")
            {
                hayvan.ImgUrl = _context.Hayvanlar.Find(model.HayvanId).ImgUrl;
            }

            var hayvanSahibi = _context.SahipHayvan
                .Where(sh => sh.HayvanId == model.HayvanId && sh.AppUser.InsanTckn == user.InsanTckn)
                .FirstOrDefault();

            if (model.SahiplikCikisTarihi != null)
            {
                var sahipSayisi = _context.SahipHayvan.Count(sh => sh.HayvanId == model.HayvanId);
                if (sahipSayisi > 1)
                {
                    _context.SahipHayvan.Remove(hayvanSahibi);
                    _context.SaveChanges();

                    return RedirectToAction("Information", "User");
                }

                _context.SahipHayvan.Remove(hayvanSahibi);
                _context.SaveChanges();
                _context.Hayvanlar.Remove(hayvan);
                _context.SaveChanges();
                return RedirectToAction("Information", "User");
            }

            hayvan.HayvanAdi = model.HayvanAdi.ToUpper();
            hayvan.CinsTur.CinsId = model.CinsId;
            hayvan.CinsTur.TurId = model.TurId;
            hayvan.RenkId = model.RenkId;
            hayvan.HayvanCinsiyet = model.HayvanCinsiyet;
            hayvan.HayvanKilo = model.HayvanKilo;
            hayvan.HayvanDogumTarihi = model.HayvanDogumTarihi;
            hayvan.HayvanOlumTarihi = model.HayvanOlumTarihi;
            hayvan.HayvanAnneId = model.HayvanAnneId;
            hayvan.HayvanBabaId = model.HayvanBabaId;
            hayvan.ImgUrl = model.ImgUrl;
            _context.Hayvanlar.Update(hayvan);
            _context.SaveChanges();
            TempData["Edit"] = "Hayvan bilgileri başarıyla güncellendi.";

            EditAnimalViewModel editedModel = new(_context);
            editedModel = editedModel.ModelOlustur(hayvan, user);

            return View("EditAnimal", editedModel);

        }

        [HttpGet]
        public IActionResult AddSahip(int hayvanId)
        {
            var hayvan = _context.Hayvanlar.Find(hayvanId);
            var user = _userManager.GetUserAsync(User).Result;

            var userHayvan = _context.SahipHayvan
                .Where(sh => sh.HayvanId == hayvanId && sh.AppUser.InsanTckn == user.InsanTckn)
                .FirstOrDefault();

            if (userHayvan == null)
            {
                return View("BadRequest");
            }
            var signature = Signature.CreateSignature(hayvanId, user.InsanTckn);

            AddNewSahipViewModel ViewModel = new(_context);
            ViewModel = ViewModel.ViewModelOlustur(hayvan, signature, user);


            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddSahip(AddNewSahipViewModel model)
        {

            if (!Signature.VerifySignature(model.HayvanId, model.UserTCKN, model.Signature))
            {
                return View("Badrequest");
            }

            AddYeniSahipValidator validator = new AddYeniSahipValidator();
            ValidationResult result = validator.Validate(model);

            var hayvan = _context.Hayvanlar.Find(model.HayvanId);

            AddNewSahipViewModel returnModel = new(_context);
            returnModel = returnModel.ViewModelOlustur(hayvan, model.Signature, _userManager.GetUserAsync(User).Result);
            returnModel.YeniSahipTCKN = model.YeniSahipTCKN;
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View(returnModel);
            }

            var yeniSahip = _context.Users.Where(u => u.InsanTckn == model.YeniSahipTCKN).FirstOrDefault();
            var acceptUrl = Url.Action("EmailConfirmYeniSahip", "Animal", new { hayvanId = model.HayvanId, yeniSahipTCKN = model.YeniSahipTCKN, imza = Signature.CreateSignature(model.HayvanId, model.UserTCKN) }, Request.Scheme, Request.Host.Value);
            var declineUrl = Url.Action("EmailRejectYeniSahip", "Animal", new { hayvanId = model.HayvanId, yeniSahipTCKN = model.YeniSahipTCKN, imza = Signature.CreateSignature(model.HayvanId, model.UserTCKN) }, Request.Scheme, Request.Host.Value);
            var cinsiyet = hayvan.HayvanCinsiyet == "D" ? "Dişi" : "Erkek";
            var dogumTarihi = hayvan.HayvanDogumTarihi.ToString("dd-MM-yyyy");
            var olumTarihi = hayvan.HayvanOlumTarihi != null ? hayvan.HayvanOlumTarihi?.ToString("dd-MM-yyyy") : "Hayatta";
            var sahipAdSoyad = _userManager.GetUserAsync(User).Result.InsanAdi + " " + _userManager.GetUserAsync(User).Result.InsanSoyadi;
            var turAdi = hayvan.CinsTur.Tur.TurAdi;
            var cinsAdi = hayvan.CinsTur.Cins.CinsAdi;
            var renkAdi = hayvan.Renk.RenkAdi;
            var hayvanAdi = hayvan.HayvanAdi;
            // Uygulamanızın geçerli URL'sini almak için
            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            string resimYolu = hayvan.ImgUrl != null ? hayvan.ImgUrl : Url.Content("/img/animal.png");
            string imgUrl = $"{baseUrl}{resimYolu}";


            string mailBody = $@"
            <!DOCTYPE html>
            <html>
            <head>

             <style>
                  body {{
                      font-family: Arial, sans-serif;
                      color: #333;
                      margin: 0;
                      padding: 0;
                      font-size: 1.2rem;
                      background-color: #f8f9fa;
                  }}
                  h2 {{font-size: 1.8rem;
                  }}
                  h3 {{font-size: 1.6rem;
                  }}
                  h6 {{font-size: 1.2rem;
                  }}
                  .container {{
                      padding: 20px;
                      max-width: 600px;
                      margin: auto;
                      background-color: #ffffff;
                      border-radius: 8px;
                      box-shadow: 0px 0px 10px rgba(0,0,0,0.1);
                  }}
                  .header {{
                      background-color: #343a40;
                      color: white;
                      padding: 10px;
                      border-radius: 8px 8px 0 0;
                      text-align: center;
                  }}
                  .content {{
                      padding: 20px;
                  }}
                  .btn {{
                      display: inline-block;
                      padding: 10px 20px;
                      color: white !important;
                      text-decoration: none;
                      border-radius: 5px;
                      font-weight: bold;
                      text-align: center;
                      margin-top: 10px;
                      background-color: #6c757d;
                      border: none;
                  }}
                  .btn:hover {{
                      background-color: #5a6268;
                  }}
                  .btn-secondary {{
                      background-color: #6c757d;
                      margin-right: 10px;
                  }}
                  .img-container {{
                      text-align: center;
                      margin-bottom: 20px;
                  }}
                  .img-container img {{
                      border-radius: 8px;
                      max-width: 100%;
                      height: auto;
                      object-fit: cover;
                  }}
                  .details {{
                      margin-top: 20px;
                  }}
                  .details h6 {{
                      margin: 0 0 10px 0;
                  }}
                  .footer {{
                      text-align: center;
                      padding: 20px;
                      font-size: 12px;
                      color: #888;
                      border-top: 1px solid #e9ecef;
                      margin-top: 20px;
                  }}
            </style>
            </head>
            <body>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h2>Hayvan Sahiplendirme Talebi</h2>
                    </div>
                    <div class='content'>
                        <h3>Merhaba {yeniSahip.InsanAdi} {yeniSahip.InsanSoyadi},</h3>
                        <p>{sahipAdSoyad} isimli kullanıcı size bir hayvan sahiplendirme talebi gönderdi. Aşağıda hayvana ait bilgileri bulabilirsiniz:</p>
                        
                        <div class='img-container'>
                            <img src='{imgUrl}' alt='{hayvanAdi}' />
                        </div>

                        <div class='details'>
                            <h6><strong>Hayvan Adı:</strong> {hayvanAdi}</h6>
                            <h6><strong>Türü:</strong> {turAdi}</h6>
                            <h6><strong>Cinsi:</strong> {cinsAdi}</h6>
                            <h6><strong>Rengi:</strong> {renkAdi}</h6>
                            <h6><strong>Kilosu:</strong> {hayvan.HayvanKilo} kg</h6>
                            <h6><strong>Cinsiyeti:</strong> {cinsiyet}</h6>
                            <h6><strong>Doğum Tarihi:</strong> {dogumTarihi}</h6>
                            <h6><strong>Ölüm Tarihi:</strong> {olumTarihi}</h6>
                        </div>

                        <div>
                            <a href='{acceptUrl}' class='btn btn-secondary'>Kabul Et</a>
                            <a href='{declineUrl}' class='btn btn-secondary'>Red Et</a>
                        </div>
                    </div>
                    <div class='footer'>
                        <p>Bu e-postayı aldıysanız ancak bu talebi yapmadıysanız, lütfen bizimle iletişime geçin.</p>
                    </div>
                </div>
            </body>
            </html>
            ";


            try
            {
                _emailSender.SendEmailAsync(yeniSahip.Email, "Hayvan Sahiplenme", mailBody);
                ViewBag.Mail = "Yeni sahip ekleme talebi, kişinin mail adresine başarıyla gönderildi.";

            }
            catch (Exception ex)
            {
                ViewBag.MailHata = "Mail gönderilirken bir hata oluştu. Hata: " + ex.Message;
                return View(returnModel);

            }

            return View(returnModel);
        }

        public async Task<IActionResult> EmailConfirmYeniSahip(int hayvanId, string yeniSahipTCKN, string imza)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user.InsanTckn != yeniSahipTCKN)
            {
                return View("BadRequest");
            }
            else
            {
                if (!Signature.VerifySignature(hayvanId, yeniSahipTCKN, imza))
                {
                    return View("BadRequest");
                }

                var hayvan = _context.Hayvanlar.Find(hayvanId);
                var yeniSahip = _context.Users.Where(u => u.InsanTckn == yeniSahipTCKN).FirstOrDefault();
                if (!_context.SahipHayvan.Any(x => x.HayvanId == hayvanId && x.SahipId == yeniSahip.Id))
                {
                    _context.SahipHayvan.Add(new SahipHayvan
                    {
                        HayvanId = hayvanId,
                        SahipId = yeniSahip.Id,
                        SahiplenmeTarihi = DateTime.Now
                    });
                    if (_context.SaveChanges() > 0)
                    {
                        TempData["YeniHayvanEklendi"] = $"{hayvan.HayvanAdi.ToUpper()} isimli yeni evcil hayvanınız hesabınıza başarı ile eklendi.";
                    }
                    else
                    {
                        TempData["YeniHayvanEklendiHata"] = $"{hayvan.HayvanAdi.ToUpper()} isimli evcil hayvan hesabınıza eklenirken bir hata oluştu.";
                    }
                }
                else
                {
                    TempData["HayvanSahibisiniz"] = $"{hayvan.HayvanAdi.ToUpper()} isimli evcil hayvanın zaten sahibisiniz.";
                }

                return RedirectToAction("Information", "User");
            }

        }

        public async Task<IActionResult> EmailRejectYeniSahip(int hayvanId, string yeniSahipTCKN, string imza)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user.InsanTckn != yeniSahipTCKN)
            {
                return View("BadRequest");
            }
            else
            {
                if (!Signature.VerifySignature(hayvanId, yeniSahipTCKN, imza))
                {
                    return View("BadRequest");
                }
                var hayvan = _context.Hayvanlar.Find(hayvanId);

                TempData["EvcilHayvanRed"] = $"{hayvan.HayvanAdi.ToUpper()} isimli evcil hayvanı hesabınıza eklemek istemediniz.";
                return RedirectToAction("Information", "User");
            }
        }
    }
}
