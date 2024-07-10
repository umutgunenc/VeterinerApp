using System.Collections.Generic;
using System.Linq;
using System;

public class SayfalamaListesi<T>
{
    public int SayfaNumarasi { get; private set; }
    public int ToplamSayfa { get; private set; }
    public List<T> Elemanlar { get; private set; }

    public SayfalamaListesi(List<T> elemanlar, int toplamKayit, int sayfaNumarasi, int sayfaBoyutu)
    {
        SayfaNumarasi = sayfaNumarasi;
        ToplamSayfa = (int)Math.Ceiling(toplamKayit / (double)sayfaBoyutu);
        Elemanlar = elemanlar;
    }

    public bool OncekiSayfaVar => SayfaNumarasi > 1;
    public bool SonrakiSayfaVar => SayfaNumarasi < ToplamSayfa;

    public static SayfalamaListesi<T> Olustur(IQueryable<T> kaynak, int sayfaNumarasi, int sayfaBoyutu)
    {
        var toplamKayit = kaynak.Count();
        var elemanlar = kaynak.Skip((sayfaNumarasi - 1) * sayfaBoyutu).Take(sayfaBoyutu).ToList();
        return new SayfalamaListesi<T>(elemanlar, toplamKayit, sayfaNumarasi, sayfaBoyutu);
    }
}
