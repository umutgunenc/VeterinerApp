using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Validators.Account;
using VeterinerApp.Models.Validators.User;
using VeterinerApp.Models.ViewModel.Account;
using VeterinerApp.Models.ViewModel.Animal;
using VeterinerApp.Models.ViewModel.User;

namespace VeterinerApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly VeterinerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserController(VeterinerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Information()
        {
            var user = await _userManager.GetUserAsync(User);

            // Kullanıcının sahip olduğu hayvanların ID'lerini al
            var hayvanIdler = _context.SahipHayvans
                .Where(s => s.SahipTckn == user.InsanTckn)
                .Select(h => h.HayvanId)
                .ToList();

            // Hayvanların detaylarını al
            List<HayvanlarViewModel> hayvanlar = await _context.Hayvans
                .Where(h => hayvanIdler.Contains(h.HayvanId))
                .Select(h => new HayvanlarViewModel
                {
                    HayvanId = h.HayvanId,
                    HayvanAdi = h.HayvanAdi,
                    HayvanCinsiyet = h.HayvanCinsiyet,
                    HayvanKilo = h.HayvanKilo,
                    HayvanDogumTarihi = h.HayvanDogumTarihi,
                    HayvanOlumTarihi = h.HayvanOlumTarihi,
                    HayvanAnneAdi = _context.Hayvans.Where(ha => ha.HayvanId == h.HayvanAnneId).Select(ha => ha.HayvanAdi).FirstOrDefault(),
                    HayvanBabaAdi = _context.Hayvans.Where(hb => hb.HayvanId == h.HayvanBabaId).Select(hb => hb.HayvanAdi).FirstOrDefault(),
                    TurAdi = _context.Turs.Where(t => t.Id == h.TurId).Select(t => t.tur).FirstOrDefault(),
                    CinsAdi = _context.Cins.Where(c => c.Id == h.CinsId).Select(c => c.cins).FirstOrDefault(),
                    RenkAdi = _context.Renks.Where(r => r.Id == h.RenkId).Select(r => r.renk).FirstOrDefault(),

                })
                .ToListAsync();

            var tuplle = (user, hayvanlar);
            return View(tuplle);

        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePaswordViewModel model)
        {

            var user = _userManager.GetUserAsync(User).Result;

            ChangePaswordValidators Validator = new ChangePaswordValidators();
            var result = Validator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View();
            }

            var resultChangePasword = _userManager.ChangePasswordAsync(user,model.OldPassword, model.NewPassword);
            if(!resultChangePasword.Result.Succeeded)
            {
                ModelState.AddModelError("OldPassword", "Eski şifreniz yanlış.");
                return View();
            }
            else
            {
                user.SifreOlusturmaTarihi = System.DateTime.Now;
                user.SifreGecerlilikTarihi = System.DateTime.Now.AddDays(120);
                await _userManager.UpdateAsync(user);
                TempData["PasswordChanged"] = $"Sayın {user.UserName}, Şifreniz başarıyla değiştirildi.";
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            EditUserViewModel model = new();
            model.InsanAdi = user.InsanAdi.ToUpper();
            model.InsanSoyadi = user.InsanSoyadi.ToUpper();
            model.Email = user.Email.ToLower();
            model.UserName = user.UserName.ToUpper();
            model.PhoneNumber = user.PhoneNumber;
            model.Id = user.Id;
            model.ImgURL = user.ImgURL;


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEdit (EditUserViewModel model)
        {
            UserEditValidator validator = new();
            ValidationResult result = validator.Validate(model);
            var user = await _context.Users.FindAsync(model.Id);

            EditUserViewModel returnModel = new ()
            {
                InsanAdi = model.InsanAdi.ToUpper(),
                InsanSoyadi = model.InsanSoyadi.ToUpper(),
                Email = model.Email.ToLower(),
                UserName = model.UserName.ToUpper(),
                PhoneNumber = model.PhoneNumber,
                Id = model.Id,
                ImgURL = user.ImgURL

            };

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View("EditUser", returnModel);
            }

            user.InsanAdi = model.InsanAdi.ToUpper();
            user.InsanSoyadi = model.InsanSoyadi.ToUpper();
            user.Email = model.Email.ToLower();
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.UserName.ToUpper();

            if (model.PhotoOption == "changePhoto" && model.filePhoto != null)
            {
                var dosyaUzantısı = Path.GetExtension(model.filePhoto.FileName);

                var dosyaAdi = string.Format($"{Guid.NewGuid()}{dosyaUzantısı}");
                var userKlasoru = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\user", user.Id.ToString());

                if (!Directory.Exists(userKlasoru))
                {
                    Directory.CreateDirectory(userKlasoru);
                }
                var eskiFotograflar = Directory.GetFiles(userKlasoru);

                foreach (var eskiFotograf in eskiFotograflar)
                {
                    System.IO.File.Delete(eskiFotograf);
                }
                var filePath = Path.Combine(userKlasoru, dosyaAdi);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.filePhoto.CopyToAsync(stream);
                }

                // Web URL'sini oluşturma
                var fileUrl = $"/img/user/{user.Id}/{dosyaAdi}";

                user.ImgURL = fileUrl;

            }
            else if (model.PhotoOption == "changePhoto" && model.filePhoto == null)
            {
                user.ImgURL = _context.Users.FindAsync(model.Id).Result.ImgURL;
            }
            else if (model.PhotoOption == "deletePhoto")
            {
                user.ImgURL = null;
            }
            else if (model.PhotoOption == "keepPhoto")
            {
                user.ImgURL = _context.Users.FindAsync(model.Id).Result.ImgURL;
            }

            _userManager.UpdateAsync(user).Wait();
            returnModel.ImgURL = user.ImgURL;

            TempData["EditUser"] = $"{user.InsanAdi} {user.InsanSoyadi} isimli kişinin kullanıcı bilgileri başarıyla güncellendi.";

            return View("EditUser",returnModel);
        }
    }
}
