namespace Manager
{
    public class Panel
    {
        public int ToplamMalikSayisi;
        public double ToplamKamulastirmaAlani;
        public double ToplamKamulastirmaBedeli;
        public double ToplamMustemilatBedeli;
        public double ToplamMevsimlikBedeli;
        public double ToplamDava27Bedeli;
        public double ToplamDava10Bedeli;
    }
    public enum ListViewType
    {
        Tum = 0,
        Malik = 1,
        Kamu = 2,
        Dava = 3,
        Mustemilat = 4,
        Mevsimlik = 5,
        Kisi = 6
    }
}
