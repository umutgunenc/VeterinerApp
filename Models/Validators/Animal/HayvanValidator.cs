using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using VeterinerApp.Models.ViewModel.Animal;
using Microsoft.AspNetCore.Http;
using System.IO;
using VeterinerApp.Models.Validators.ValidateFunctions;


#nullable disable

namespace VeterinerApp.Models.Validators.Animal
{
    public partial class HayvanValidator : AbstractValidator<AddAnimalViewModel>
    {

        public HayvanValidator()
        {

            RuleFor(x => x.HayvanAdi)
                .MaximumLength(50).WithMessage("Hayvan adı maksimum 50 karakter uzunluğunda olmalı.")
                .NotEmpty().WithMessage("Lütfen hayvan adı giriniz")
                .NotNull().WithMessage("Lütfen hayvan adı giriniz");

            RuleFor(x => x.RenkId)
                .NotEmpty().WithMessage("Lütfen bir renk seçiniz.")
                .NotNull().WithMessage("Lütfen bir renk seçiniz.")
                .Must(FunctionsValidator.BeRenk).WithMessage("Seçilen renk sistemde bulunmamaktadır.");

            RuleFor(x => x.CinsId)
                .NotEmpty().WithMessage("Lütfen bir cins seçiniz")
                .NotNull().WithMessage("Lütfen bir cins seçiniz")
                .Must(FunctionsValidator.BeCins).WithMessage("Seçilen cins sistemde bulunmamaktadır.");

            RuleFor(x => x.TurId)
                .NotEmpty().WithMessage("Lütfen bir tür seçiniz")
                .NotNull().WithMessage("Lütfen bir tür seçiniz")
                .Must(FunctionsValidator.BeTur).WithMessage("Seçilen tür sistemde bulunmamaktadır.");

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
                .Must(FunctionsValidator.BeRegisteredParentAnimal)
                .WithMessage("Hayvanın annesi sistemde kayıtlı bir hayvan olmalıdır.")
                .Must((model, x) => FunctionsValidator.BeSameCins(model, x))
                .WithMessage("Hayvan annesi, eklenen hayvan ile aynı cins olmalıdır.")
                .Must((model, x) => FunctionsValidator.BeOlder(model, x))
                .WithMessage("Hayvan annesi, eklenen hayvandan büyük olmalıdır.Yanlış bir hayvan seçtiniz veya girilen bilgiler hatalı.")
                .Must(FunctionsValidator.BeGirl).WithMessage("Hayvan annesi dişi olmalıdır.");

            RuleFor(x => x.HayvanBabaId)
                .Must(FunctionsValidator.BeRegisteredParentAnimal)
                .WithMessage("Hayvanın babası sistemde kayıtlı bir hayvan olmalıdır.")
                .Must((model, x) => FunctionsValidator.BeSameCins(model, x))
                .WithMessage("Hayvan babası, eklenen hayvan ile aynı cins olmalıdır.")
                .Must((model, x) => FunctionsValidator.BeOlder(model, x))
                .WithMessage("Hayvan babası, eklenen hayvandan büyük olmalıdır.Yanlış bir hayvan seçtiniz veya girilen bilgiler hatalı.")
                .Must(FunctionsValidator.BeBoy).WithMessage("Hayvan babası erkek olmalıdır.");

            RuleFor(x => x.HayvanKilo)
                .NotNull().WithMessage("Lütfen hayvanın kilosunu giriniz.")
                .NotEmpty().WithMessage("Lütfen hayvanın kilosunu giriniz.")
                .GreaterThanOrEqualTo(0).WithMessage("Lütfen geçerli kilo değeri giriniz.");

            RuleFor(x => x.SahiplikTarihi)
                .NotEmpty().WithMessage("Lütfen bir tarih giriniz.")
                .NotNull().WithMessage("Lütfen bir tarih giriniz.")
                .Must(x => x <= DateTime.Now).WithMessage("Lütfen geçerli bir tarih giriniz.")
                .GreaterThanOrEqualTo(x => x.HayvanDogumTarihi).WithMessage("Hayvanı doğmadan önce sahiplenmezsiniz.");

            RuleFor(x => x.PhotoOption)
                .NotEmpty().WithMessage("Lütfen bir seçenek seçiniz.")
                .NotNull().WithMessage("Lütfen bir seçenek seçiniz.")
                .Must(FunctionsValidator.BeValidRadioPhotoAdd).WithMessage("Geçersiz seçenek.");

            RuleFor(x => x.filePhoto)
                .Must(FunctionsValidator.BeValidExtensionForPhoto)
                .WithMessage("Yalnızca jpg, jpeg, png ve bmp uzantılı dosyalar yüklenebilir.")
                .When(x => x.PhotoOption == "changePhoto" && x.filePhoto != null)
                .WithName("filePhoto");

            RuleFor(x => x.filePhoto)
                .Empty()
                .When(x => x.PhotoOption == "keepPhoto" || x.PhotoOption == "deletePhoto")
                .WithMessage("Fotoğraf yüklemek doğru seçeneği seçiniz.");

            RuleFor(x => x.filePhoto)
                .Must(x => x.Length < 10485760)
                .When(x => x.filePhoto != null)
                .WithMessage("Fotoğraf boyutu 10MB'dan küçük olmalıdır.");

        }



    }
}