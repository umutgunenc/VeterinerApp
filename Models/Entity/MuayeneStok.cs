namespace VeterinerApp.Models.Entity
{
    public class MuayeneStok
    {
        public int Id { get; set; }
        public int MuayeneId { get; set; }
        public int StokId { get; set; }

        public virtual Muayene Muayene { get; set; }
        public virtual Stok Stok { get; set; }
    }
}
