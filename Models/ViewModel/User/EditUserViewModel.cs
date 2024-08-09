using Microsoft.AspNetCore.Http;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.User
{
    public class EditUserViewModel : AppUser
    {
        public IFormFile filePhoto { get; set; }
        public string PhotoOption { get; set; }

    }
}
