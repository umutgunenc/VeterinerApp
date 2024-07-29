using FluentValidation;
using System.Collections.Generic;
using VeterinerApp.Models.Entity;
using VeterinerApp.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
#nullable disable

namespace VeterinerApp.Models.Validators
{
    public partial class MaasOdemeleriValidators : AbstractValidator<MaasOdemeleri>
    {
        private readonly VeterinerContext _context;
        public MaasOdemeleriValidators(VeterinerContext context)
        {
            _context = context;

            RuleFor(x => x.CalisanTckn)
                .NotEmpty().WithMessage("Çalışan TCKN bilgisi eksik")
                .NotNull().WithMessage("Çalışan TCKN bilgisi eksik");
                //.Must(MustBeCalisan).WithMessage("Maaş ödemesi yapılacak kişi sisteme kayıtlı değil.");

            RuleFor(x => x.OdemeTarihi)
                .NotEmpty().WithMessage("Maaşın ödeme tarihini giriniz")
                .NotNull().WithMessage("Maaşın ödeme tarihini giriniz")
                .Must(x => x is DateTime).WithMessage("Lütfen tarih giriniz");

            RuleFor(x => x.OdenenTutar)
                .NotNull().WithMessage("Çalışanın maaş bilgisini giriniz.")
                .NotEmpty().WithMessage("Çalışanın maaş bilgisini giriniz.")
                .Must(MustBeMaas).WithMessage("Maaş bilgisi yanlış");

        }

        //private bool MustBeCalisan(string TCKN)
        //{
        //    var person = _context.Insans.Include(i => i.Rol).FirstOrDefault(x => x.InsanTckn == TCKN & x.CalisiyorMu==true);

        //    if (person == null)
        //    {
        //        return false;
        //    }

        //    return person.Rol.RolAdi.ToUpper() == "ADMIN" ||
        //           person.Rol.RolAdi.ToUpper() == "ÇALIŞAN" ||
        //           person.Rol.RolAdi.ToUpper() == "VETERİNER";
        //}
        private bool MustBeMaas(MaasOdemeleri maaslar, double odenenTutar)
        {
            var person = _context.Insans.FirstOrDefault(x => x.InsanTckn == maaslar.CalisanTckn);
            if (person == null)
            {
                return false;
            }

            return person.Maas.HasValue && person.Maas.Value == odenenTutar;
        }

    }
}
