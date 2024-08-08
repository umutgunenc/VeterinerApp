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

        public static bool MustBeRenk(int girilenRenk)
        {
            return _context.Renks.Any(x => x.Id == girilenRenk);
        }
        public static bool MustBeCins(int girilenCins)
        {
            return _context.TurCins.Any(x => x.CinsId == girilenCins);
        }
        public static bool MustBeTur(int girilenTur)
        {
            return _context.TurCins.Any(x => x.TurId == girilenTur);
        }
        public static bool beSameCins(Hayvan model, int? parentID)
        {
            if (!parentID.HasValue)
                return true;

            var parent = _context.Hayvans.FirstOrDefault(a => a.HayvanId == parentID.Value);
            return parent != null && parent.CinsId == model.CinsId;
        }
        public static bool beOlder(Hayvan model, int? parentID)
        {
            if (!parentID.HasValue)
                return true;

            var parent = _context.Hayvans.FirstOrDefault(a => a.HayvanId == parentID.Value);
            return parent != null && parent.HayvanDogumTarihi < model.HayvanDogumTarihi;
        }
        public static bool beGirl(int? anneId)
        {
            if (!anneId.HasValue)
                return true;

            var anne = _context.Hayvans.FirstOrDefault(a => a.HayvanId == anneId.Value);
            return anne != null && anne.HayvanCinsiyet == "D";
        }
        public static bool beBoy(int? babaId)
        {
            if (!babaId.HasValue)
                return true;

            var baba = _context.Hayvans.FirstOrDefault(a => a.HayvanId == babaId.Value);
            return baba != null && baba.HayvanCinsiyet == "E";
        }
        public static bool ValidRadio(string value)
        {
            return radioValues.Contains(value);
        }
        public static bool HaveValidExtension(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }
        public static bool TcDogrula(string tcKimlikNo)
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
        public static bool BeUniqueKullaniciAdi(string kullaniciAdi)
        {
            if (string.IsNullOrEmpty(kullaniciAdi))
                return true;
            return !_context.Users.Any(x => x.UserName.ToUpper() == kullaniciAdi.ToUpper());
        }
        public static bool UniqueTel(string TelNo)
        {
            return !_context.Users.Any(x => x.PhoneNumber.ToUpper() == TelNo.ToUpper());
        }
        public static bool UniqueTCKN(string girilenTCKN)
        {
            return !_context.Users.Any(x => x.InsanTckn.ToUpper() == girilenTCKN.ToUpper());
        }
        public static bool UniqueEmail(string insanMail)
        {
            if (string.IsNullOrEmpty(insanMail))
                return true;

            return !_context.Insans.Any(x => x.Email.ToUpper() == insanMail.ToUpper() || x.Email.ToLower() == insanMail.ToLower());
        }
        public static bool BeUniqueCins(string girilenDeger)
        {
            return !_context.Cins.Any(x => x.cins.ToUpper() == girilenDeger.ToUpper());
        }
        public static bool beCins(int id)
        {
            return _context.Cins.Any(x => x.Id == id);
        }
        public static bool notBeTur(int rolId)
        {
            return !_context.TurCins.Any(x => x.CinsId == rolId);
        }
        public static bool beRol(int rolId)
        {
            return _context.Roles.Any(x => x.Id == rolId);
        }

        //TODO : Bu iki fonksiyonu kontrol et
        public static bool BeUniqueOrNullDiplomaNo(string diplomaNumarasi)
        {
            if (string.IsNullOrEmpty(diplomaNumarasi))
            {
                return true;
            }
            return !_context.Users.Any(x => x.DiplomaNo.ToUpper() == diplomaNumarasi.ToUpper());
        }
        public static bool BeUniqueOrNullDiplomaNo(string InsanTckn, string diplomaNo)
        {
            if (string.IsNullOrEmpty(diplomaNo))
                return true;

            return !_context.Users.Any(x => x.DiplomaNo == diplomaNo && x.InsanTckn.ToUpper() != InsanTckn.ToUpper());
        }
        public static bool BeUniqueKullaniciAdi(string InsanTckn, string kullaniciAdi)
        {
            if (string.IsNullOrEmpty(kullaniciAdi))
                return true;

            return !_context.Users.Any(x => x.UserName.ToUpper() == kullaniciAdi.ToUpper() && x.InsanTckn != InsanTckn);
        }
        public static bool UniqueTel(string InsanTckn, string insanTel)
        {
            if (string.IsNullOrEmpty(insanTel))
                return true;

            return !_context.Users.Any(x => x.PhoneNumber == insanTel && x.InsanTckn != InsanTckn);
        }
        public static bool IsRoleMatching(int rolId, List<string> validRoles)
        {
            var role = _context.Roles.Find(rolId);
            return role != null && validRoles.Contains(role.Name.ToUpper());
        }
        public static bool UniqueEmailSelf(string InsanTckn, string insanMail)
        {
            if (string.IsNullOrEmpty(insanMail))
                return true;

            return !_context.Users
                .Any(x => x.Email.ToUpper() == insanMail.ToUpper() && x.InsanTckn != InsanTckn);
        }
        public static bool notUniqueTCKN(string girilenTCKN)
        {
            return _context.Users.Any(x => x.InsanTckn.ToUpper() == girilenTCKN.ToUpper());
        }
        public static bool BeUniqueRenk(string girilenDeger)
        {
            return !_context.Renks.Any(x => x.renk.ToUpper() == girilenDeger.ToUpper());
        }
        public static bool beRenk(int Id)
        {
            return _context.Renks.Any(x => x.Id == Id);
        }
        public static bool beNotUsedRenk(int Id)
        {
            return !_context.Hayvans.Any(x => x.RenkId == Id);
        }
        public static bool notBeInsan(int rolId)
        {
            return !_context.UserRoles.Any(x => x.RoleId == rolId);
        }
        public static bool roller(string roller)
        {
            roller = roller.ToUpper();
            if (roller == "admin".ToUpper() || roller == "veteriner".ToUpper() || roller == "çalışan".ToUpper() || roller == "müşteri".ToUpper())
                return true;
            return false;
        }
        public static bool beUniqueRol(string rol)
        {
            return !_context.Roles.Any(x => x.Name.ToUpper() == rol.ToUpper());
        }
        public static bool notMatch(int turId)
        {
            return !_context.TurCins.Where(x => x.TurId == turId).Any();
        }
        public static bool beTur(int TurId)
        {
            return _context.Turs.Any(x => x.Id == TurId);
        }
        public static bool beTurCins(int id)
        {
            return _context.TurCins.Any(x => x.Id == id);
        }
        public static bool beNotUsedTurCins(int id)
        {
            var cinsId = _context.TurCins.Where(x => x.Id == id).Select(x => x.CinsId).FirstOrDefault();
            var turId = _context.TurCins.Where(x => x.Id == id).Select(x => x.TurId).FirstOrDefault();
            return !_context.Hayvans.Where(x => x.CinsId == cinsId && x.TurId == turId).Any();
        }
        public static bool beUniqueTur(string girilenTur)
        {
            return !_context.Turs.Any(t => t.tur.ToUpper() == girilenTur.ToUpper());
        }
        public static bool beNotTurCins(int id)
        {
            return !_context.TurCins.Any(x => x.TurId == id);
        }


        public static readonly List<string> radioValues = new List<string>
        {
            "keepPhoto",
            "changePhoto",
            "useDefault"
        };
        public static readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

    }
}

