using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Animal;


#nullable disable

namespace VeterinerApp.Models.Validators.Animal
{
    public partial class HayvanValidator : AbstractValidator<AddAnimalViewModel>
    {
        private readonly VeterinerContext _context;

        public HayvanValidator(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.HayvanAdi)
                .MaximumLength(50).WithMessage("Hayvan adı maksimum 50 karakter uzunluğunda olmalı.")
                .NotEmpty().WithMessage("Lütfen hayvan adı giriniz")
                .NotNull().WithMessage("Lütfen hayvan adı giriniz");

            RuleFor(x => x.RenkId)
                .NotEmpty().WithMessage("Lütfen bir renk seçiniz.")
                .NotNull().WithMessage("Lütfen bir renk seçiniz.")
                .Must(MustBeRenk).WithMessage("Seçilen renk sistemde bulunmamaktadır.");

            RuleFor(x => x.CinsId)
                .NotEmpty().WithMessage("Lütfen bir cins seçiniz")
                .NotNull().WithMessage("Lütfen bir cins seçiniz")
                .Must(MustBeCins).WithMessage("Seçilen cins sistemde bulunmamaktadır.");

            RuleFor(x => x.TurId)
                .NotEmpty().WithMessage("Lütfen bir tür seçiniz")
                .NotNull().WithMessage("Lütfen bir tür seçiniz")
                .Must(MustBeTur).WithMessage("Seçilen tür sistemde bulunmamaktadır.");

            RuleFor(x => x.HayvanCinsiyet)
                .NotNull().WithMessage("Lütfen bir cinsiyet seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir cinsiyet seçiniz.")
                .Must(x => x == "e" || x == "E" || x == "d" || x == "D").WithMessage("Erkek için E, dişi için D yazınız.");

            RuleFor(x => x.HayvanDogumTarihi)
                .NotEmpty().WithMessage("Lütfen bir tarih giriniz")
                .NotNull().WithMessage("Lütfen bir tarih giriniz")
                .Must(x => x <= DateTime.Now).WithMessage("Lütfen geçerli bir tarih giriniz.");

            RuleFor(x => x.HayvanAnneId)
                .Must(anneId => !anneId.HasValue || _context.Hayvans.Any(a => a.HayvanId == anneId.Value))
                .WithMessage("Hayvanın annesi sistemde kayıtlı bir hayvan olmalıdır.")
                .Must((model, x) => beSameCins(model, x))
                .WithMessage("Hayvan annesi, eklenen hayvan ile aynı cins olmalıdır.")
                .Must((model, x) => beOlder(model, x))
                .WithMessage("Hayvan annesi, eklenen hayvandan büyük olmalıdır.")
                .Must(beGirl).WithMessage("Hayvan annesi dişi olmalıdır.");

            RuleFor(x => x.HayvanBabaId)
                .Must(babaId => !babaId.HasValue || _context.Hayvans.Any(a => a.HayvanId == babaId.Value))
                .WithMessage("Hayvanın babası sistemde kayıtlı bir hayvan olmalıdır.")
                .Must((model, x) => beSameCins(model, x))
                .WithMessage("Hayvan babası, eklenen hayvan ile aynı cins olmalıdır.")
                .Must((model, x) => beOlder(model, x))
                .WithMessage("Hayvan babası, eklenen hayvandan büyük olmalıdır.")
                .Must(beBoy).WithMessage("Hayvan babası dişi olmalıdır.");

            //todo: hayvan kilo kontrolü yapılacak, ondalık değer girilmiyor
            RuleFor(x => x.HayvanKilo)
                .NotNull().WithMessage("Lütfen hayvanın kilosunu giriniz.")
                .NotEmpty().WithMessage("Lütfen hayvanın kilosunu giriniz.")
                .Must(x => x >= 0).WithMessage("Lütfen geçerli kilo değeri giriniz.");

        }

        private bool MustBeRenk(int girilenRenk)
        {
            return _context.Renks.Any(x => x.Id == girilenRenk);
        }

        private bool MustBeCins(int girilenCins)
        {
            return _context.TurCins.Any(x => x.CinsId == girilenCins);
        }

        private bool MustBeTur(int girilenTur)
        {
            return _context.TurCins.Any(x => x.TurId == girilenTur);
        }

        private bool beSameCins(Hayvan model, int? parentID)
        {
            if (!parentID.HasValue)
                return true;

            var parent = _context.Hayvans.FirstOrDefault(a => a.HayvanId == parentID.Value);
            return parent != null && parent.CinsId == model.CinsId;
        }

        private bool beOlder(Hayvan model, int? parentID)
        {
            if (!parentID.HasValue)
                return true;

            var parent = _context.Hayvans.FirstOrDefault(a => a.HayvanId == parentID.Value);
            return parent != null && parent.HayvanDogumTarihi < model.HayvanDogumTarihi;
        }

        private bool beGirl(int? anneId)
        {
            if (!anneId.HasValue)
                return true;

            var anne = _context.Hayvans.FirstOrDefault(a => a.HayvanId == anneId.Value);
            return anne != null && anne.HayvanCinsiyet == "D";
        }

        private bool beBoy(int? babaId)
        {
            if (!babaId.HasValue)
                return true;

            var baba = _context.Hayvans.FirstOrDefault(a => a.HayvanId == babaId.Value);
            return baba != null && baba.HayvanCinsiyet == "D";
        }


    }
}