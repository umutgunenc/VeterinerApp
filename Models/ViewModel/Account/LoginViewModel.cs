using Microsoft.AspNetCore.Mvc;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Account
{
    public class LoginViewModel : AppUser
    {
        public int rolId { get; set; }

    }
}
