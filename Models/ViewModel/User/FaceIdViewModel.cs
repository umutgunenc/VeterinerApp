using Microsoft.AspNetCore.Http;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.User
{
    public class FaceIdViewModel :AppUser
    {
        public IFormFile[] filePhoto { get; set; }
    }
}
