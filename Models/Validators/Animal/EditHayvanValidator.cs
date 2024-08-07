using FluentValidation;
using System.Linq;
using System;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Animal;
using VeterinerApp.Models.Entity;
using System.IO;

namespace VeterinerApp.Models.Validators.Animal
{
    public class EditHayvanValidator : AbstractValidator<EditAnimalViewModel>
    {
        private readonly VeterinerContext _context;

        public EditHayvanValidator(VeterinerContext context)
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
                .Must(x => x <= DateTime.Now).WithMessage("Lütfen geçerli bir tarih giriniz.")
                .LessThanOrEqualTo(x => x.SahiplikTarihi).WithMessage("Hayvanı doğmadan önce sahiplenmezsiniz.");


            RuleFor(x => x.HayvanAnneId)
                .Must(anneId => !anneId.HasValue || _context.Hayvans.Any(a => a.HayvanId == anneId.Value))
                .WithMessage("Hayvanın annesi sistemde kayıtlı bir hayvan olmalıdır.")
                .Must((model, x) => beSameCins(model, x))
                .WithMessage("Hayvan annesi, eklenen hayvan ile aynı cins olmalıdır.")
                .Must((model, x) => beOlder(model, x))
                .WithMessage("Hayvan annesi, eklenen hayvandan büyük olmalıdır.Yanlış bir hayvan seçtiniz veya girilen bilgiler hatalı.")
                .Must(beGirl).WithMessage("Hayvan annesi dişi olmalıdır.");

            RuleFor(x => x.HayvanBabaId)
                .Must(babaId => !babaId.HasValue || _context.Hayvans.Any(a => a.HayvanId == babaId.Value))
                .WithMessage("Hayvanın babası sistemde kayıtlı bir hayvan olmalıdır.")
                .Must((model, x) => beSameCins(model, x))
                .WithMessage("Hayvan babası, eklenen hayvan ile aynı cins olmalıdır.")
                .Must((model, x) => beOlder(model, x))
                .WithMessage("Hayvan babası, eklenen hayvandan büyük olmalıdır.Yanlış bir hayvan seçtiniz veya girilen bilgiler hatalı.")
                .Must(beBoy).WithMessage("Hayvan babası erkek olmalıdır.");

            RuleFor(x => x.HayvanKilo)
                .NotNull().WithMessage("Lütfen hayvanın kilosunu giriniz.")
                .NotEmpty().WithMessage("Lütfen hayvanın kilosunu giriniz.")
                .GreaterThanOrEqualTo(0).WithMessage("Lütfen geçerli kilo değeri giriniz.");

            RuleFor(x => x.SahiplikTarihi)
                .NotEmpty().WithMessage("Lütfen bir tarih giriniz.")
                .NotNull().WithMessage("Lütfen bir tarih giriniz.")
                .Must(x => x <= DateTime.Now).WithMessage("Lütfen geçerli bir tarih giriniz.")
                .GreaterThanOrEqualTo(x => x.HayvanDogumTarihi).WithMessage("Hayvanı doğmadan önce sahiplenmezsiniz.");

            RuleFor(x => x.SahiplikCikisTarihi)
                .Must((model, x) => !x.HasValue || (model.SahiplikTarihi <= x.Value))
                .WithMessage("Sahiplikten çıkış tarihi, sahiplenme tarihinden büyük olmalıdır.")
                .GreaterThanOrEqualTo(x => x.HayvanDogumTarihi).WithMessage("Sahiplik çıkış tarihi, hayvan doğum tarihinden büyük olmalıdır.");

            RuleFor(x => x.HayvanOlumTarihi)
                .NotEmpty()
                .When(x => x.isDeath)
                .WithMessage("Lütfen ölüm tarihini giriniz.")
                .NotNull()
                .When(x => x.isDeath)
                .WithMessage("Lütfen ölüm tarihini giriniz.")
                .Must((model, x) => !x.HasValue)
                .When(x => !x.isDeath)
                .WithMessage("Lütfen hayvanı ölü olarak işaretleyin.")
                .Must((model, x) => model.HayvanDogumTarihi <= x.Value)
                .When(x => x.HayvanOlumTarihi.HasValue)
                .WithMessage("Ölüm tarihi, doğum tarihinden büyük olmalıdır.")
                .Must((model, x) => model.SahiplikTarihi <= x.Value)
                .When(x => x.HayvanOlumTarihi.HasValue)
                .WithMessage("Ölüm tarihi, sahiplik tarihinden büyük olmalıdır.");

            RuleFor(x => x.isDeath)
                .Must((model, x) => x == false || model.HayvanOlumTarihi.HasValue)
                .WithMessage("Lütfen hayvanın ölüm tarihini giriniz.");

            RuleFor(x => x.changePhoto)
                .Must((model, x) => !x || model.filePhoto != null)
                .WithMessage("Lütfen bir fotoğraf seçiniz.");


            RuleFor(x => x.filePhoto)
                .Empty().When(x => x.changePhoto == false).WithMessage("Fotoğraf yüklemek için kutucuğu işaretleyin.")
                .Null().When(x => x.changePhoto == false).WithMessage("Fotoğraf yüklemek için kutucuğu işaretleyin.");

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
            return baba != null && baba.HayvanCinsiyet == "E";
        }
    }
}
