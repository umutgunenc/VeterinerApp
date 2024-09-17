//using Microsoft.AspNetCore.Identity;
//using VeterinerApp.Data;

//var insan = _veterinerDbContext.Users.FirstOrDefault(x => x.InsanTckn == model.InsanTckn);
//var rol = _veterinerDbContext.UserRoles.FirstOrDefault(x => x.UserId == insan.Id);

//InsanDuzenleValidators validator = new(_veterinerDbContext);
//ValidationResult result = validator.Validate(model);
//if (!result.IsValid)
//{
//    foreach (var error in result.Errors)
//    {
//        ModelState.AddModelError("", error.ErrorMessage);
//    }

//    var insanSec = new InsanSecViewModel() { InsanTckn = model.InsanTckn };
//    var secilenKisi = _veterinerDbContext.Users
//    .Where(x => x.InsanTckn == insanSec.InsanTckn)
//    .Select(x => new InsanDuzenleViewModel
//    {
//        InsanAdi = x.InsanAdi,
//        InsanSoyadi = x.InsanSoyadi,
//        InsanTckn = x.InsanTckn,
//        Email = x.Email,
//        PhoneNumber = x.PhoneNumber,
//        DiplomaNo = x.DiplomaNo,
//        UserName = x.UserName,
//        CalisiyorMu = x.CalisiyorMu,
//        Maas = x.Maas,
//        rolId = _veterinerDbContext.UserRoles.Where(r => r.UserId == x.Id)
//            .Select(r => r.RoleId)
//            .FirstOrDefault(),
//        Roller = _veterinerDbContext.Roles.Select(r => new SelectListItem
//        {
//            Value = r.Id.ToString(),
//            Text = r.Name
//        }).ToList(),
//        rolAdi = _veterinerDbContext.Roles
//            .Where(r => r.Id == _veterinerDbContext.UserRoles
//                                .Where(r => r.UserId == x.Id)
//                                .Select(r => r.RoleId)
//                                .FirstOrDefault())
//            .Select(r => r.Name)
//            .FirstOrDefault()
//    }).FirstOrDefault();

//    ViewBag.SecilenKisi = secilenKisi;
//    return View("CalisanSec");
//}

//// Mevcut kullanıcı rolünü sil
//_veterinerDbContext.UserRoles.Remove(rol);
//_veterinerDbContext.SaveChanges();

//// Yeni kullanıcı rolünü ekle
//var yeniRol = new IdentityUserRole<string>
//{
//    RoleId = model.rolId,
//    UserId = insan.Id
//};
//_veterinerDbContext.UserRoles.Add(yeniRol);

//// Kullanıcı bilgilerini güncelle
//insan.InsanAdi = model.InsanAdi;
//insan.InsanSoyadi = model.InsanSoyadi;
//insan.Email = model.Email.ToLower();
//insan.PhoneNumber = model.PhoneNumber;
//insan.DiplomaNo = model.DiplomaNo;
//insan.UserName = model.UserName;
//insan.CalisiyorMu = model.CalisiyorMu;
//insan.Maas = model.Maas;

//_veterinerDbContext.SaveChanges();

//_veterinerDbContext.SaveChanges();
//TempData["KisiGuncellendi"] = $"{insan.InsanAdi} {insan.InsanSoyadi} adlı kişinin bilgileri başarı ile güncellendi.";
//return View("CalisanSec");