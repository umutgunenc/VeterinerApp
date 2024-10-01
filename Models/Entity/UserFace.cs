

namespace VeterinerApp.Models.Entity
{
    public class UserFace
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FaceImgUrl { get; set; }

        public AppUser User { get; set; }
    }
}
