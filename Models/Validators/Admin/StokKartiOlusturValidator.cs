using FluentValidation;
using VeterinerApp.Models.Validators.ValidateFunctions;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators.Admin
{
    public class StokKartiOlusturValidator : AbstractValidator<StokKartiOlusturViewModel>
    {
        public StokKartiOlusturValidator()
        {

            RuleFor(x => x.StokAdi)
                .NotEmpty().WithMessage("Stok adı boş olamaz.")
                .NotNull().WithMessage("Stok adı boş olamaz.")
                .MaximumLength(50).WithMessage("Stok adı en fazla 50 karakter olabilir.")
                .Must(FunctionsValidator.BeUniqueStokAdi).WithMessage("Sistem aynı isimde bir stok bulunmaktadır.");

            RuleFor(x=>x.BirimId)
                .NotNull().WithMessage("Birim seçimi yapılmalıdır.")
                .NotEmpty().WithMessage("Birim seçimi yapılmalıdır.")
                .Must(FunctionsValidator.BeBirim).WithMessage("Seçilen birim sistemde kayıtlı değil.");

            RuleFor(x => x.KategoriId)
                .NotNull().WithMessage("Kategori seçimi yapılmalıdır.")
                .NotEmpty().WithMessage("Kategori seçimi yapılmalıdır.")
                .Must(FunctionsValidator.BeKategori).WithMessage("Seçilen kategori sistemde kayıtlı değil.");

            RuleFor(x => x.StokBarkod)
                .NotNull().WithMessage("Barkod boş olamaz.")
                .NotEmpty().WithMessage("Barkod boş olamaz.")
                .MaximumLength(50).WithMessage("Barkod en fazla 50 karakter olabilir.")
                .Must(FunctionsValidator.BeUniqueBarkod).WithMessage("Sistem aynı barkod numarasına sahip bir stok bulunmaktadır.");
        }




    }
}
