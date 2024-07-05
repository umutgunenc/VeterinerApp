using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class MuayeneValidators : AbstractValidator<Muayene>
    {
        private readonly VeterinerContext _context;
        public MuayeneValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.IlacBarkod)
                .MaximumLength(100).WithMessage("Barkod numarası en fazla 100 karakter olabilir.")
                .Must(BeIlac).WithMessage("Girilen ilaç sisteme kayıtlı değil.")
                .Must(BeInStock).WithMessage("Seçilen ilacın stok sayısı 0'dır.");

            RuleFor(x => x.MuayeneTarihi)
                .NotEmpty().WithMessage("Muayene tarihini giriniz.")
                .NotNull().WithMessage("Muayene tarihini giriniz.")
                .Must(x => x is DateTime).WithMessage("Lütfen bir tarih giriniz.")
                .Must(x => x <= DateTime.Now).WithMessage("Lütfen geçerli bir tarih giriniz.");

            RuleFor(x => x.Aciklama)
                .NotEmpty().WithMessage("Lütfen açıklama giriniz.")
                .NotNull().WithMessage("Lütfen açıklama giriniz.");

            RuleFor(x => x.HekimkTckn)
                .NotEmpty().WithMessage("Lütfen hekime ait TCKN'yi giriniz.")
                .NotNull().WithMessage("Lütfen hekime ait TCKN'yi giriniz.")
                .Matches("^[0-9]*$").WithMessage("TCKN numarası sadece rakamlardan oluşmalıdır.")
                .Must(beInsan).WithMessage("Seçilen kişi sisteme kayıtlı değil")
                .Must(beVet).WithMessage("Sadece veteriner sisteme giriş yapabilir.")
                .Must(beAktifCalisan).WithMessage("Seçilen veteriner çalışmıyor.");

            RuleFor(x => x.SonrakiMuayeneTarihi)
                .Must(x => x is DateTime).WithMessage("Lütfen bir tarih giriniz.")
                .Must(x => x >= DateTime.Now).WithMessage("Lütfen geçerli bir tarih giriniz.");

            RuleFor(x => x.HayvanId)
                .NotNull().WithMessage("Muayene olacak hayvanı seçiniz.")
                .NotEmpty().WithMessage("Muayene olacak hayvanı seçiniz.")
                .Must(x => x is int).WithMessage("Seçilen hayvanın ID'si yanlış.")
                .Must(beHayvan).WithMessage("Seçilen hayvan sistemde bulunamadı.")
                .Must(beNotDead).WithMessage("Seçilen hayvan ölü olduğu için muayene edilemez.");

            RuleFor(x => x.MuayeneNo)
                .NotNull().WithMessage("Muayene numarası girilmeli.")
                .NotEmpty().WithMessage("Muayene numarası girilmeli.")
                .Must(x => x is int).WithMessage("Seçilen Muayeninin numarası yanlış.")
                .Must(beUnique).WithMessage("Geçerli bir muayene numarası giriniz.");

            RuleFor(x => x.MuayeneId)
                .NotNull().WithMessage("Muayene numarası girilmeli.")
                .NotEmpty().WithMessage("Muayene numarası girilmeli.")
                .Must(x => x is int).WithMessage("Seçilen Muayeninin numarası yanlış.")
                .Must(beUniqueID).WithMessage("Geçerli bir muayene numarası giriniz.");


        }
        private bool beUniqueID(int MuayeneId)
        {
            return !_context.Muayenes.Any(x => x.MuayeneId == MuayeneId);
        }
        private bool beUnique(int muayeneNo)
        {
            return !_context.Muayenes.Any(x=>x.MuayeneNo == muayeneNo);
        }
        private bool beNotDead(int hayvanId)
        {
            var hayvan = _context.Hayvans.FirstOrDefault(x => x.HayvanId==hayvanId);
            if (hayvan!=null && hayvan.HayvanOlumTarihi==null) 
                return true; 
            return false;

        }
        private bool beHayvan(int hayvanId)
        {
            return _context.Hayvans.Any(x=>x.HayvanId==hayvanId);
        }
        private bool beAktifCalisan(string hekimTCKN)
        {
            var vet = _context.Insans.FirstOrDefault(x => x.InsanTckn.ToUpper() == hekimTCKN.ToUpper());
            if (vet.CalisiyorMu == true)
                return true;
            return false;
        }
        private bool beInsan(string hekimTCKN)
        {
            var insan = _context.Insans.FirstOrDefault(x => x.InsanTckn.ToUpper() == hekimTCKN.ToUpper());
            if (insan == null)
                return false;
            return true;
        }
        private bool beVet(string hekimTCKN)
        {
            var vet = _context.Insans.FirstOrDefault(x => x.InsanTckn.ToUpper() == hekimTCKN.ToUpper());
            if (vet != null && vet.Rol.RolAdi.ToUpper() == "veteriner".ToUpper())
                return true;
            return false;
        }
        private bool BeIlac(string barkod)
        {
            return _context.Ilacs.Any(x => x.IlacBarkod.ToUpper() == barkod.ToUpper());
        }
        private bool BeInStock(string barkod)
        {
            var stock = _context.Stoks.FirstOrDefault(x => x.StokBarkod.ToUpper() == barkod.ToUpper());
            if (stock != null && stock.StokSayisi > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
