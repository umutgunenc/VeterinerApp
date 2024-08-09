using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.Validators.ValidateFunctions
{
    public static class FunctionsValidator
    {

        private static readonly VeterinerContext _context = new VeterinerContext();

        public static bool BeRenk(int Id)
        {
            return _context.Renks.Any(x => x.Id == Id);
        }
        public static bool BeCins(int id)
        {
            return _context.Cins.Any(x => x.Id == id);
        }
        public static bool BeTur(int TurId)
        {
            return _context.Turs.Any(x => x.Id == TurId);
        }
        public static bool BeTurCins(int id)
        {
            return _context.TurCins.Any(x => x.Id == id);
        }
        public static bool BeSameCins(Hayvan model, int? parentID)
        {
            if (!parentID.HasValue)
                return true;

            var parent = _context.Hayvans.FirstOrDefault(a => a.HayvanId == parentID.Value);
            return parent != null && parent.CinsId == model.CinsId;
        }
        public static bool BeOlder(Hayvan model, int? parentID)
        {
            if (!parentID.HasValue)
                return true;

            var parent = _context.Hayvans.FirstOrDefault(a => a.HayvanId == parentID.Value);
            return parent != null && parent.HayvanDogumTarihi < model.HayvanDogumTarihi;
        }
        public static bool BeGirl(int? anneId)
        {
            if (!anneId.HasValue)
                return true;

            var anne = _context.Hayvans.FirstOrDefault(a => a.HayvanId == anneId.Value);
            return anne != null && anne.HayvanCinsiyet == "D";
        }
        public static bool BeBoy(int? babaId)
        {
            if (!babaId.HasValue)
                return true;

            var baba = _context.Hayvans.FirstOrDefault(a => a.HayvanId == babaId.Value);
            return baba != null && baba.HayvanCinsiyet == "E";
        }
        public static bool BeRol(int rolId)
        {
            return _context.Roles.Any(x => x.Id == rolId);
        }
        public static bool BeAllowedRol(string roller)
        {
            roller = roller.ToUpper();
            if (roller == "admin".ToUpper() || roller == "veteriner".ToUpper() || roller == "çalışan".ToUpper() || roller == "müşteri".ToUpper())
                return true;
            return false;
        }
        public static bool BeValidRadioPhotoAdd(string value)
        {
            return radioValues.Contains(value);
        }
        public static bool BeValidExtensionForPhoto(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensionsForPhoto.Contains(extension);
        }
        public static bool BeUsedTCKN(string girilenTCKN)
        {
            return _context.Users.Any(x => x.InsanTckn.ToUpper() == girilenTCKN.ToUpper());
        }
        public static bool BeValidTCKN(string tcKimlikNo)
        {
            bool returnvalue = false;
            if (tcKimlikNo.Length == 11)
            {
                Int64 ATCNO, BTCNO, TcNo;
                long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;

                TcNo = Int64.Parse(tcKimlikNo);

                ATCNO = TcNo / 100;
                BTCNO = TcNo / 100;

                C1 = ATCNO % 10; ATCNO = ATCNO / 10;
                C2 = ATCNO % 10; ATCNO = ATCNO / 10;
                C3 = ATCNO % 10; ATCNO = ATCNO / 10;
                C4 = ATCNO % 10; ATCNO = ATCNO / 10;
                C5 = ATCNO % 10; ATCNO = ATCNO / 10;
                C6 = ATCNO % 10; ATCNO = ATCNO / 10;
                C7 = ATCNO % 10; ATCNO = ATCNO / 10;
                C8 = ATCNO % 10; ATCNO = ATCNO / 10;
                C9 = ATCNO % 10; ATCNO = ATCNO / 10;
                Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
                Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);

                returnvalue = ((BTCNO * 100) + (Q1 * 10) + Q2 == TcNo);
            }
            return returnvalue;
        }
        public static bool BeValidPasswordDate(string username, string password)
        {
            return _context.Users.Any(x => x.UserName.ToUpper() == username.ToUpper() && x.SifreGecerlilikTarihi >= DateTime.Now);
        }
        public static bool BeRegisteredParentAnimal(int? animalId)
        {
            return !animalId.HasValue || _context.Hayvans.Any(a => a.HayvanId == animalId.Value);
        }

        public static bool LoginSucceed(AppUser user)
        {
            return _context.Users.Any(x => x.UserName == user.UserName && x.PasswordHash == user.PasswordHash); 
        
        }

        //public static async Task<bool> BeCorrectOldPasswordAsync(string oldPassword, string userName, UserManager<AppUser> userManager, CancellationToken cancellationToken)
        //{
        //    var user = await userManager.FindByNameAsync(userName);
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return await userManager.CheckPasswordAsync(user, oldPassword);
        //}

        public static bool BeUniqueTCKN(string girilenTCKN)
        {
            return !_context.Users.Any(x => x.InsanTckn.ToUpper() == girilenTCKN.ToUpper());
        }
        public static bool BeUniqueCins(string girilenDeger)
        {
            return !_context.Cins.Any(x => x.cins.ToUpper() == girilenDeger.ToUpper());
        }
        public static bool BeUniqueTur(string girilenTur)
        {
            return !_context.Turs.Any(t => t.tur.ToUpper() == girilenTur.ToUpper());
        }
        public static bool BeUniqueRol(string rol)
        {
            return !_context.Roles.Any(x => x.Name.ToUpper() == rol.ToUpper());
        }
        public static bool BeUniqueRenk(string girilenDeger)
        {
            return !_context.Renks.Any(x => x.renk.ToUpper() == girilenDeger.ToUpper());
        }
        public static bool BeUniqueTel(string TelNo)
        {
            return !_context.Users.Any(x => x.PhoneNumber.ToUpper() == TelNo.ToUpper());
        }
        /// <summary>
        /// Girilen kişi dışındaki telefon numarası benzersiz olmalıdır.
        /// </summary>
        /// <param name="InsanTckn"></param>
        /// <param name="insanTel"></param>
        /// <returns></returns>
        public static bool BeUniqueTel(int id, string insanTel)
        {
            if (string.IsNullOrEmpty(insanTel))
                return true;

            return !_context.Users.Any(x => x.PhoneNumber == insanTel && x.Id != id);
        }
        public static bool BeUniqueOrNullDiplomaNo(string diplomaNumarasi)
        {
            if (string.IsNullOrEmpty(diplomaNumarasi))
            {
                return true;
            }
            return !_context.Users.Any(x => x.DiplomaNo.ToUpper() == diplomaNumarasi.ToUpper());
        }
        /// <summary>
        /// Girilen kişi dışındaki diploma numarası benzersiz olmalıdır.
        /// </summary>
        /// <param name="InsanTckn"></param>
        /// <param name="diplomaNo"></param>
        /// <returns></returns>
        public static bool BeUniqueOrNullDiplomaNo(int id, string diplomaNo)
        {
            if (string.IsNullOrEmpty(diplomaNo))
                return true;

            return !_context.Users.Any(x => x.DiplomaNo == diplomaNo && x.Id != id);
        }
        public static bool BeUniqueKullaniciAdi(string kullaniciAdi)
        {
            if (string.IsNullOrEmpty(kullaniciAdi))
                return true;
            return !_context.Users.Any(x => x.UserName.ToUpper() == kullaniciAdi.ToUpper());
        }
        /// <summary>
        /// Girilen kişi dışındaki kullanıcı adı benzersiz olmalıdır.
        /// </summary>
        /// <param name="InsanTckn"></param>
        /// <param name="kullaniciAdi"></param>
        /// <returns></returns>
        public static bool BeUniqueKullaniciAdi(int id, string kullaniciAdi)
        {
            if (string.IsNullOrEmpty(kullaniciAdi))
                return true;

            return !_context.Users.Any(x => x.UserName.ToUpper() == kullaniciAdi.ToUpper() && x.Id != id);
        }
        public static bool BeUniqueEmail(string insanMail)
        {
            if (string.IsNullOrEmpty(insanMail))
                return true;

            return !_context.Insans.Any(x => x.Email.ToUpper() == insanMail.ToUpper() || x.Email.ToLower() == insanMail.ToLower());
        }
        /// <summary>
        /// Girilen kişi dışındaki mail adresi bezersiz olmalıdır.
        /// </summary>
        /// <param name="InsanTckn"></param>
        /// <param name="insanMail"></param>
        /// <returns></returns>
        public static bool BeUniqueEmail(int id, string insanMail)
        {
            if (string.IsNullOrEmpty(insanMail))
                return true;

            return !_context.Users
                .Any(x => x.Email.ToUpper() == insanMail.ToUpper() && x.Id != id);
        }


        public static bool IsRoleMatching(int rolId, List<string> validRoles)
        {
            var role = _context.Roles.Find(rolId);
            return role != null && validRoles.Contains(role.Name.ToUpper());
        }
        public static bool BeNotUsedRenk(int Id)
        {
            return !_context.Hayvans.Any(x => x.RenkId == Id);
        }
        public static bool BeNotUsedTurCins(int id)
        {
            var cinsId = _context.TurCins.Where(x => x.Id == id).Select(x => x.CinsId).FirstOrDefault();
            var turId = _context.TurCins.Where(x => x.Id == id).Select(x => x.TurId).FirstOrDefault();
            return !_context.Hayvans.Where(x => x.CinsId == cinsId && x.TurId == turId).Any();
        }
        public static bool BeNotMatchedRol(int rolId)
        {
            return !_context.UserRoles.Any(x => x.RoleId == rolId);
        }
        public static bool BeNotMatchedCins(int cinsId)
        {
            return !_context.TurCins.Any(x => x.CinsId == cinsId);
        }
        public static bool BeNotMatchTurCins(int turId)
        {
            return !_context.TurCins.Where(x => x.TurId == turId).Any();
        }
        public static bool BeNotMatchedTur(int id)
        {
            return !_context.TurCins.Any(x => x.TurId == id);
        }


        private static readonly List<string> radioValues = new List<string>
        {
            "keepPhoto",
            "changePhoto",
            "useDefault"
        };
        private static readonly string[] allowedExtensionsForPhoto = { ".jpg", ".jpeg", ".png", ".gif" };

    }
}

