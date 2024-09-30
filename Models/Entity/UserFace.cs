using Emgu.CV.Features2D;
using System;

namespace VeterinerApp.Models.Entity
{
    public class UserFace
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] FaceData { get; set; }

        public AppUser User { get; set; }
    }
}
