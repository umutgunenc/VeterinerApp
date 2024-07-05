using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using VeterinerApp.Data;
using VeterinerApp.Models.Entity;
using VeterinerApp.Models.ViewModel.Admin;

namespace VeterinerApp.Models.Validators
{
    public class CinsSilValidator : AbstractValidator<CinsViewModel>
    {   private readonly VeterinerContext _context;

        public CinsSilValidator(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Lütfen bir cins seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir cins seçiniz.")
                .Must(beCins).WithMessage("Listede olmayan bir cinsi silemezsiniz.")
                .Must(notBeTur).WithMessage("Silinecek cinse ait tanımlı tür olduğu için silme işlemi gerçekleştirilemedi.");
        }
        private bool beCins(int id)
        {
            return _context.Cins.Any(x=>x.Id == id);
        }
        private bool notBeTur(int rolId)
        {
            return !_context.TurCins.Any(x => x.CinsId == rolId);
        }
    }
}