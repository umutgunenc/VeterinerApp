using System.Collections.Generic;
using System.Linq;
using System;

public class SayfalamaListesi<T>
{
    public int SayfaNumarasi { get; private set; }
    public int ToplamSayfa { get; private set; }
    public int ToplamKayitSayisi { get; private set; }
    public List<T> Veriler { get; private set; }

    public SayfalamaListesi(List<T> veriler, int sayfaNumarasi,int sayfadaGosterilecekKayitSayisi)
    {
        ToplamKayitSayisi =veriler.Count;
        SayfaNumarasi = sayfaNumarasi;
        ToplamSayfa = (int)Math.Ceiling(ToplamKayitSayisi / (double)sayfadaGosterilecekKayitSayisi);
        Veriler = veriler;
    }

    public bool OncekiSayfaVar => SayfaNumarasi > 1;
    public bool SonrakiSayfaVar => SayfaNumarasi < ToplamSayfa;

    public static SayfalamaListesi<T> Olustur(IQueryable<T> veriler, int sayfaNumarasi, int sayfadaGosterilecekKayitSayisi)
    {
        var toplamKayit = veriler.Count();
        var elemanlar = veriler.Skip((sayfaNumarasi - 1) * sayfadaGosterilecekKayitSayisi).Take(sayfadaGosterilecekKayitSayisi).ToList();

        return new SayfalamaListesi<T>(elemanlar, sayfaNumarasi, sayfadaGosterilecekKayitSayisi);
    }
}
