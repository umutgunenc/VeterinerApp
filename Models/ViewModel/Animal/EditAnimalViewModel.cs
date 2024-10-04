using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerApp.Data;
using VeterinerApp.Fonksiyonlar;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.Enum;

namespace VeterinerApp.Models.ViewModel.Animal
{
    public class EditAnimalViewModel : Hayvan
    {

        private string Rengi { get; set; }
        private string Cinsi { get; set; }
        public int CinsId { get; set; }
        private string Turu { get; set; }
        public int TurId { get; set; }  
        public bool IsDeath { get; set; }
        public string PhotoOption { get; set; }
        public List<SelectListItem> Turler { get; set; }
        public List<SelectListItem> Cinsler { get; set; }
        public List<SelectListItem> Renkler { get; set; }
        public List<SelectListItem> CinsiyetListesi { get; set; }
        private AppUser Sahip { get; set; }
        public string SahipTckn { get; set; }
        public List<SelectListItem> HayvanAnneList { get; set; }
        public List<SelectListItem> HayvanBabaList { get; set; }
        public DateTime SahiplikTarihi { get; set; }
        public DateTime? SahiplikCikisTarihi { get; set; }
        public IFormFile filePhoto { get; set; }
        public string Imza { get; set; }

        private List<SelectListItem> CinsiyetleriGetir()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Erkek", Value = Cinsiyet.Erkek.ToString() },
                new SelectListItem { Text = "Dişi", Value = Cinsiyet.Dişi.ToString() }
            };
        }
        private async Task<List<SelectListItem>> TurAdlariniGetirAsync(VeterinerDBContext context)
        {
            return await context.Turler.Select(t => new SelectListItem
            {
                Text = t.TurAdi,
                Value = t.TurId.ToString()
            }).ToListAsync();
        }
        private async Task<List<SelectListItem>> CinsAdlariniGetirAsync(VeterinerDBContext context)
        {
            return await context.Cinsler.Select(c => new SelectListItem
            {
                Text = c.CinsAdi,
                Value = c.CinsId.ToString()
            }).ToListAsync();
        }
        private async Task<List<SelectListItem>> RenkleriGetirAsync(VeterinerDBContext context)
        {
            return await context.Renkler.Select(r => new SelectListItem
            {
                Text = r.RenkAdi,
                Value = r.RenkId.ToString()
            }).ToListAsync();
        }
        private async Task<List<SelectListItem>> AnnelerListesiOlusturAsync(VeterinerDBContext context)
        {
            HayvanAnneList = new();
            bool disiHayvanVarMi = context.Hayvanlar.Any(h => h.HayvanCinsiyet == Cinsiyet.Dişi);
            if (disiHayvanVarMi)
            {
                var disiHayvanlar = await context.Hayvanlar.Where(h => h.HayvanCinsiyet == Cinsiyet.Dişi)
                     .Select(h => new
                     {
                         sahipTckn = context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanTckn).FirstOrDefault(),
                         sahipAdSoyad = context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanAdi + " " + sh.AppUser.InsanSoyadi).FirstOrDefault(),
                         h.HayvanAdi,
                         h.HayvanId,
                         h.HayvanCinsiyet,
                         CinsAdi = context.Cinsler.Where(c => c.CinsId == h.CinsTur.CinsId).Select(c => c.CinsAdi).FirstOrDefault(),
                         TurAdi = context.Turler.Where(t => t.TurId == h.CinsTur.TurId).Select(t => t.TurAdi).FirstOrDefault(),
                         RenkAdi = context.Renkler.Where(r => r.RenkId == h.RenkId).Select(r => r.RenkAdi).FirstOrDefault(),
                         hayvanDogumTarihi = h.HayvanDogumTarihi.ToString("dd-MM-yyyy")
                     })
                     .ToListAsync();

                return disiHayvanlar.Select(h => new SelectListItem
                {
                    Text = $"{h.sahipTckn.Substring(0, 3) + new string('*', Math.Max(h.sahipTckn.Length - 6, 0)) + h.sahipTckn.Substring(h.sahipTckn.Length - 3)} " +
                                    $"{h.sahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.sahipAdSoyad.Length - 4, 0)) + h.sahipAdSoyad.Substring(h.sahipAdSoyad.Length - 2)} - " +
                                    $"{h.HayvanAdi} {h.CinsAdi} {h.TurAdi} {h.RenkAdi} {h.hayvanDogumTarihi}",
                    Value = h.HayvanId.ToString()
                }).ToList();
            }

            return HayvanAnneList;



        }
        private async Task<List<SelectListItem>> BabalarListesiOlusturAsync(VeterinerDBContext context)
        {
            HayvanBabaList = new();
            bool erkekHayvanVarMi = context.Hayvanlar.Any(h => h.HayvanCinsiyet == Cinsiyet.Erkek);
            if (erkekHayvanVarMi)
            {
                var erkekHayvanlar = await context.Hayvanlar.Where(h => h.HayvanCinsiyet == Cinsiyet.Dişi)
                     .Select(h => new
                     {
                         sahipTckn = context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanTckn).FirstOrDefault(),
                         sahipAdSoyad = context.SahipHayvan.Where(sh => sh.HayvanId == h.HayvanId).Select(sh => sh.AppUser.InsanAdi + " " + sh.AppUser.InsanSoyadi).FirstOrDefault(),
                         h.HayvanAdi,
                         h.HayvanId,
                         h.HayvanCinsiyet,
                         CinsAdi = context.Cinsler.Where(c => c.CinsId == h.CinsTur.CinsId).Select(c => c.CinsAdi).FirstOrDefault(),
                         TurAdi = context.Turler.Where(t => t.TurId == h.CinsTur.TurId).Select(t => t.TurAdi).FirstOrDefault(),
                         RenkAdi = context.Renkler.Where(r => r.RenkId == h.RenkId).Select(r => r.RenkAdi).FirstOrDefault(),
                         hayvanDogumTarihi = h.HayvanDogumTarihi.ToString("dd-MM-yyyy")
                     })
                     .ToListAsync();

                return erkekHayvanlar.Select(h => new SelectListItem
                {
                    Text = $"{h.sahipTckn.Substring(0, 3) + new string('*', Math.Max(h.sahipTckn.Length - 6, 0)) + h.sahipTckn.Substring(h.sahipTckn.Length - 3)} " +
                                    $"{h.sahipAdSoyad.Substring(0, 2) + new string('*', Math.Max(h.sahipAdSoyad.Length - 4, 0)) + h.sahipAdSoyad.Substring(h.sahipAdSoyad.Length - 2)} - " +
                                    $"{h.HayvanAdi} {h.CinsAdi} {h.TurAdi} {h.RenkAdi} {h.hayvanDogumTarihi}",
                    Value = h.HayvanId.ToString()
                }).ToList();
            }

            return HayvanBabaList;
        }

        private async Task<string> RenkAdiniGetirAsync(Hayvan hayvan, VeterinerDBContext context)
        {
            return await context.Renkler.Where(r => r.RenkId == hayvan.RenkId).Select(r => r.RenkAdi).FirstOrDefaultAsync();
        }
        private async Task<string> CinsAdiniGetirAsync(Hayvan hayvan, VeterinerDBContext context)
        {
            return await context.Cinsler
                .Where(c => c.CinsId == context.CinsTur
                    .Where(ct => ct.Id == hayvan.CinsTurId)
                    .Select(ct => ct.CinsId).FirstOrDefault())
                .Select(c => c.CinsAdi).FirstOrDefaultAsync();
        }
        private async Task<string> TurAdiniGetirAsync(Hayvan hayvan, VeterinerDBContext context)
        {
            return await context.Turler
                 .Where(t => t.TurId == context.CinsTur
                     .Where(ct => ct.Id == hayvan.CinsTurId)
                     .Select(ct => ct.TurId).FirstOrDefault())
                 .Select(t => t.TurAdi).FirstOrDefaultAsync();
        }
        private async Task<int> CinsIdGetirAsync(Hayvan hayvan, VeterinerDBContext context)
        {
            return await context.Cinsler
                 .Where(c => c.CinsId == context.CinsTur
                     .Where(ct => ct.Id == hayvan.CinsTurId)
                     .Select(ct => ct.CinsId).FirstOrDefault())
                 .Select(c => c.CinsId).FirstOrDefaultAsync();
        }
        private async Task<int> TurIdGetirAsync(Hayvan hayvan, VeterinerDBContext context)
        {
            return await context.Turler
                 .Where(t => t.TurId == context.CinsTur
                     .Where(ct => ct.Id == hayvan.CinsTurId)
                     .Select(ct => ct.TurId).FirstOrDefault())
                 .Select(t => t.TurId).FirstOrDefaultAsync();
        }

        private async Task<DateTime> SahiplikTarihiniGetirAsync(Hayvan hayvan, VeterinerDBContext context, AppUser user)
        {
            return await context.SahipHayvan
                .Where(sh => sh.HayvanId == hayvan.HayvanId && sh.SahipId == user.Id)
                .Select(sh => sh.SahiplenmeTarihi)
                .FirstOrDefaultAsync();
        }
        private async Task<DateTime?> SahiplikCikisTarihiniGetirAsync(Hayvan hayvan, VeterinerDBContext context, AppUser user)
        {
            return await context.SahipHayvan
                .Where(sh => sh.HayvanId == hayvan.HayvanId && sh.SahipId == user.Id)
                .Select(sh => sh.SahiplenmeCikisTarihi)
                .FirstOrDefaultAsync();
        }
        private string SignatureOlustur(int hayvanId, string TCKN)
        {
            return Signature.CreateSignature(hayvanId.ToString(), TCKN);
        }


        public async Task<EditAnimalViewModel> ModelOlusturAsync(Hayvan hayvan, AppUser user, VeterinerDBContext context)
        {
            return new EditAnimalViewModel()
            {
                HayvanAdi = hayvan.HayvanAdi,
                HayvanId = hayvan.HayvanId,
                Rengi = await RenkAdiniGetirAsync(hayvan, context),
                RenkId = hayvan.RenkId,
                Cinsi = await CinsAdiniGetirAsync(hayvan, context),
                CinsId = await CinsIdGetirAsync(hayvan,context),
                Turu = await TurAdiniGetirAsync(hayvan, context),
                TurId = await TurIdGetirAsync(hayvan, context),
                HayvanKilo = hayvan.HayvanKilo,
                HayvanCinsiyet = hayvan.HayvanCinsiyet,
                HayvanDogumTarihi = hayvan.HayvanDogumTarihi,
                HayvanOlumTarihi = hayvan.HayvanOlumTarihi,
                SahiplikTarihi= await SahiplikTarihiniGetirAsync(hayvan,context,user),
                SahiplikCikisTarihi = await SahiplikCikisTarihiniGetirAsync(hayvan,context,user),                
                IsDeath = hayvan.HayvanOlumTarihi == null ? false : true,
                HayvanAnneId = hayvan.HayvanAnneId,
                HayvanBabaId = hayvan.HayvanBabaId,
                HayvanAnneList = await AnnelerListesiOlusturAsync(context),
                HayvanBabaList = await BabalarListesiOlusturAsync(context),
                Cinsler = await CinsAdlariniGetirAsync(context),
                Turler = await TurAdlariniGetirAsync(context),
                Renkler = await RenkleriGetirAsync(context),
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
        public async Task<EditAnimalViewModel> ModelOlusturAsync(EditAnimalViewModel model, AppUser user, VeterinerDBContext context)
        {
            return new EditAnimalViewModel()
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
                HayvanAnneList = await AnnelerListesiOlusturAsync(context),
                HayvanBabaList = await BabalarListesiOlusturAsync(context),
                Cinsler = await CinsAdlariniGetirAsync(context),
                Turler = await TurAdlariniGetirAsync(context),
                Renkler = await RenkleriGetirAsync(context),
                CinsiyetListesi = CinsiyetleriGetir(),
                ImgUrl = model.ImgUrl,
                Imza = SignatureOlustur(model.HayvanId, model.Sahipler.Where(h => h.HayvanId == model.HayvanId).Select(s => s.AppUser.InsanTckn).FirstOrDefault()),
                Sahip = user,
                SahipTckn = user.InsanTckn,
            };
        }


        public async Task<(bool,List<Hayvan>)> CocuklariGetirAsync(Hayvan parent, VeterinerDBContext context)
        {
            var CocukListesi = new List<Hayvan>();
            CocukListesi = await context.Hayvanlar.Where(h => h.HayvanAnneId == parent.HayvanId || h.HayvanBabaId == parent.HayvanId).ToListAsync();
            if (CocukListesi.Count==0)
                return (false, CocukListesi);
            return (true, CocukListesi);

        }

    }
}
