using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Account
{
    public class RegisterViewModel : AppUser
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int rolId { get; set; }
    }
}
