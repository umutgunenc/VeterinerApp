using VeterinerApp.Data;
using VeterinerApp.Models.Entity;

namespace VeterinerApp.Models.ViewModel.Account
{
    public class ChangePaswordViewModel : AppUser
    {
        private readonly VeterinerContext _context;

        public ChangePaswordViewModel()
        {
            
        }
        public ChangePaswordViewModel(VeterinerContext context)
        {
            _context = context;
        }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
