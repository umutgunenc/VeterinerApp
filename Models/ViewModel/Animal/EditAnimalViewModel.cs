using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Fonksiyonlar;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Animal
{
    public class EditAnimalViewModel : Hayvan
    {
        private readonly VeterinerDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public EditAnimalViewModel(VeterinerDBContext context)
        {
            _context = context;
        }

        public string Rengi { get; set; }
        public string Cinsi { get; set; }
        public int CinsId { get; set; }
        public string Turu { get; set; }
        public int TurId { get; set; }  
        public bool IsDeath { get; set; }
        public string PhotoOption { get; set; }
        public List<SelectListItem> Turler { get; set; }
        public List<SelectListItem> Cinsler { get; set; }
        public List<SelectListItem> Renkler { get; set; }
        public List<SelectListItem> CinsiyetListesi { get; set; }
        public AppUser Sahip { get; set; }
        public string SahipTckn { get; set; }
        public string SahipAdSoyad { get; set; }
        public List<SelectListItem> HayvanAnneList { get; set; }
        public List<SelectListItem> HayvanBabaList { get; set; }
        public DateTime SahiplikTarihi { get; set; }
        public DateTime? SahiplikCikisTarihi { get; set; }
        public IFormFile filePhoto { get; set; }
        public string Imza { get; set; }

        private List<SelectListItem> TurAdlariniGetir()
        {
            return _context.Turler.Select(t => new SelectListItem
            {
                Text = t.TurAdi,
                Value = t.TurId.ToString()
            }).ToList();
        }
        private List<SelectListItem> CinsAdlariniGetir()
        {
            return _context.Cinsler.Select(c => new SelectListItem
            {
                Text = c.CinsAdi,
                Value = c.CinsId.ToString()
            }).ToList();
        }
        private List<SelectListItem> RenkleriGetir()
        {
            return _context.Renkler.Select(r => new SelectListItem
            {
                Text = r.RenkAdi,
                Value = r.RenkId.ToString()
            }).ToList();
        }
        private List<SelectListItem> CinsiyetleriGetir()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Erkek", Value = "E" },
                new SelectListItem { Text = "Dişi", Value = "D" }
            };
        }
        private List<SelectListItem> AnneListesiniGetir()
        {
            var hayvanlar = _context.Hayvanlar.Select(h => new
            {
                sahipTckn = _context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanTckn).FirstOrDefault(),
                sahipAdSoyad = _context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanAdi + " " + sh.AppUser.InsanSoyadi).FirstOrDefault(),
                h.HayvanAdi,
                h.HayvanId,
                h.HayvanCinsiyet,
                h.CinsTur.Cins.CinsAdi,
                h.CinsTur.Tur.TurAdi,
                h.Renk.RenkAdi,
                hayvanDogumTarihi = h.HayvanDogumTarihi.ToString("dd-MM-yyyy")
            }).ToList();

            return hayvanlar.Where(h => h.HayvanCinsiyet == "D" || h.HayvanCinsiyet == "d").Select(h => new SelectListItem
            {
                Text = $"{h.sahipTckn.Substring(0, 3) + new string('*', Math.Max(h.sahipTckn.Length - 6, 0)) + h.sahipTckn.Substring(h.sahipTckn.Length - 3)} " +
                                    $"{h.sahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.sahipAdSoyad.Length - 4, 0)) + h.sahipAdSoyad.Substring(h.sahipAdSoyad.Length - 2)} - " +
                                    $"{h.HayvanAdi} {h.CinsAdi} {h.TurAdi} {h.RenkAdi} {h.hayvanDogumTarihi}",
                Value = h.HayvanId.ToString()
            }).ToList();
        }
        private List<SelectListItem> BabaListesiniGetir()
        {
            var hayvanlar = _context.Hayvanlar.Select(h => new
            {
                sahipTckn = _context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanTckn).FirstOrDefault(),
                sahipAdSoyad = _context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanAdi + " " + sh.AppUser.InsanSoyadi).FirstOrDefault(),
                h.HayvanAdi,
                h.HayvanId,
                h.HayvanCinsiyet,
                h.CinsTur.Cins.CinsAdi,
                h.CinsTur.Tur.TurAdi,
                h.Renk.RenkAdi,
                hayvanDogumTarihi = h.HayvanDogumTarihi.ToString("dd-MM-yyyy")
            }).ToList();

            return hayvanlar.Where(h => h.HayvanCinsiyet == "E" || h.HayvanCinsiyet == "e").Select(h => new SelectListItem
            {
                Text = $"{h.sahipTckn.Substring(0, 3) + new string('*', Math.Max(h.sahipTckn.Length - 6, 0)) + h.sahipTckn.Substring(h.sahipTckn.Length - 3)} " +
                                    $"{h.sahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.sahipAdSoyad.Length - 4, 0)) + h.sahipAdSoyad.Substring(h.sahipAdSoyad.Length - 2)} - " +
                                    $"{h.HayvanAdi} {h.CinsAdi} {h.TurAdi} {h.RenkAdi} {h.hayvanDogumTarihi}",
                Value = h.HayvanId.ToString()
            }).ToList();
        }
        private string SignatureOlustur(int hayvanId, string TCKN)
        {
            return Signature.CreateSignature(hayvanId, TCKN);
        }

        public EditAnimalViewModel ModelOlustur(Hayvan hayvan, AppUser user)
        {

            return new EditAnimalViewModel(_context)
            {
                HayvanAdi = hayvan.HayvanAdi,
                HayvanId = hayvan.HayvanId,
                Rengi = hayvan.Renk.RenkAdi,
                RenkId = hayvan.RenkId,
                Cinsi = hayvan.CinsTur.Cins.CinsAdi,
                CinsId = hayvan.CinsTur.CinsId,
                Turu = hayvan.CinsTur.Tur.TurAdi,
                TurId = hayvan.CinsTur.TurId,
                HayvanKilo = hayvan.HayvanKilo,
                HayvanCinsiyet = hayvan.HayvanCinsiyet,
                HayvanDogumTarihi = hayvan.HayvanDogumTarihi,
                HayvanOlumTarihi = hayvan.HayvanOlumTarihi,
                IsDeath = hayvan.HayvanOlumTarihi == null ? false : true,
                HayvanAnneId = hayvan.HayvanAnneId,
                HayvanBabaId = hayvan.HayvanBabaId,
                HayvanAnneList = AnneListesiniGetir(),
                HayvanBabaList = BabaListesiniGetir(),
                Cinsler = CinsAdlariniGetir(),
                Turler = TurAdlariniGetir(),
                Renkler = RenkleriGetir(),
                CinsiyetListesi = CinsiyetleriGetir(),
                ImgUrl = hayvan.ImgUrl,
                Imza = SignatureOlustur(hayvan.HayvanId, hayvan.Sahipler.Where(h=>h.HayvanId==hayvan.HayvanId).Select(s=>s.AppUser.InsanTckn).FirstOrDefault()),
                Sahip = user,
                SahipTckn = user.InsanTckn,
            };
        }
        /// <summary>
        /// Viewden gelen modeli olusturur. View i tekrardan geriye dondurmek icin kullanilir.
        /// </summary>
        /// <param name="Model">Viewden gelen model</param>
        /// <param name="User">Sisteme login olmus olan kullanıcı</param>
        /// <returns>EditAnimalView şeklinde viewden denen modeli olusturur.</returns>
        public EditAnimalViewModel ModelOlustur(EditAnimalViewModel model, AppUser user)
        {
            return new EditAnimalViewModel(_context)
            {
                HayvanAdi = model.HayvanAdi,
                HayvanId = model.HayvanId,
                Rengi = model.Renk.RenkAdi,
                RenkId = model.RenkId,
                Cinsi = model.CinsTur.Cins.CinsAdi,
                CinsId = model.CinsTur.CinsId,
                Turu = model.CinsTur.Tur.TurAdi,
                TurId = model.CinsTur.TurId,
                HayvanKilo = model.HayvanKilo,
                HayvanCinsiyet = model.HayvanCinsiyet,
                HayvanDogumTarihi = model.HayvanDogumTarihi,
                HayvanOlumTarihi = model.HayvanOlumTarihi,
                IsDeath = model.HayvanOlumTarihi == null ? false : true,
                HayvanAnneId = model.HayvanAnneId,
                HayvanBabaId = model.HayvanBabaId,
                HayvanAnneList = AnneListesiniGetir(),
                HayvanBabaList = BabaListesiniGetir(),
                Cinsler = CinsAdlariniGetir(),
                Turler = TurAdlariniGetir(),
                Renkler = RenkleriGetir(),
                CinsiyetListesi = CinsiyetleriGetir(),
                ImgUrl = model.ImgUrl,
                Imza = SignatureOlustur(model.HayvanId, model.Sahipler.Where(h => h.HayvanId == model.HayvanId).Select(s => s.AppUser.InsanTckn).FirstOrDefault()),
                Sahip = user,
                SahipTckn = user.InsanTckn,
            };
        }

    }
}
