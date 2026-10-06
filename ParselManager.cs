using KamuDatabase;
using Manager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Kamu;
using System.Globalization;

namespace Dashboard
{
    /// <summary>
    /// Seçili parselin malik, müştemilat, mevsimlik ürün, dava ve kamulaştırma özetlerini gösteren kullanıcı kontrolüdür.
    /// Veri okuma işlemlerini PresentationDataService üzerinden yapar ve düzenleme isteklerini olaylarla ana uygulamaya iletir.
    /// </summary>
    public partial class Manager : UserControl
    {
        internal ConnectionInfo DataConnectionInfo = new ConnectionInfo();
        private readonly Dictionary<string, Kisi> _owners = new Dictionary<string, Kisi>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<int, Dictionary<int, string>> _codeLists = new Dictionary<int, Dictionary<int, string>>();
        private PresentationDataService PresentationData => new PresentationDataService(DataConnectionInfo);
        private ListHelper Lists => new ListHelper(DataConnectionInfo);
        internal Dictionary<int, string> kisiAnlasmaDurumuDict;
        internal Dictionary<int, string> kisiDavetiyeAlinmaDurumuDict;
        internal Dictionary<int, string> kisiDavetiyeTebligDurumuDict;
        internal Dictionary<int, string> kisiGorusmeDurumuDict;
        internal Dictionary<int, string> kisiTescilDurumuDict;
        internal Dictionary<int, string> parselEdinimDurumuDict;
        internal Dictionary<int, string> parselIstimlakSerhiDict;
        internal Dictionary<int, string> parselIstimlakTuruDict;
        internal Dictionary<int, string> parselKadastralDurumDict;
        internal Dictionary<int, string> parselMalikTipiDict;

        //public string ParselGlobalID { get; set; }

        public Manager()
        {
            InitializeComponent();
            propertyGridParsel.PropertyValueChanged += PropertyGridParsel_PropertyValueChanged;
            propertyGridMalik.PropertyValueChanged += PropertyGridMalik_PropertyValueChanged;
            //ColorList();
            //splitContainer3.Panel1Collapsed = true;
            //splitContainer3.Panel1.Hide();
        }

        /// <summary>Kontrolü açık proje bağlantısına bağlar ve kod listesi önbelleklerini yeniler.</summary>
        public void SetConnection(ConnectionInfo dataConnectionInfo)
        {
            DataConnectionInfo = dataConnectionInfo ?? throw new ArgumentNullException(nameof(dataConnectionInfo));
            // Önceki projenin kod açıklamalarını ve seçili kayıtlarını yeni bağlantıya taşımayız.
            _codeLists.Clear();
            Clear(ListViewType.Tum);
            CreateLists();
        }

        /// <summary>ListView satırlarında gösterilen sayısal kodların açıklama sözlüklerini hazırlar.</summary>
        private void CreateLists()
        {
            ListHelper lists = Lists;
            kisiAnlasmaDurumuDict = lists.CreateComboDictionary(1);
            kisiDavetiyeAlinmaDurumuDict = lists.CreateComboDictionary(2);
            kisiDavetiyeTebligDurumuDict = lists.CreateComboDictionary(3);
            kisiGorusmeDurumuDict = lists.CreateComboDictionary(5);
            kisiTescilDurumuDict = lists.CreateComboDictionary(10);
            parselEdinimDurumuDict = lists.CreateComboDictionary(4);
            parselIstimlakSerhiDict = lists.CreateComboDictionary(6);
            parselIstimlakTuruDict = lists.CreateComboDictionary(7);
            parselKadastralDurumDict = lists.CreateComboDictionary(8);
            parselMalikTipiDict = lists.CreateComboDictionary(9);
        }
        //private void ColorList()
        //{
        //    ColorListViewHeader(ref listViewKamu, SystemColors.Menu, SystemColors.MenuText);
        //    ColorListViewHeader(ref listViewDava, SystemColors.Menu, SystemColors.MenuText);
        //    ColorListViewHeader(ref listViewMalik, SystemColors.Menu, SystemColors.MenuText);
        //    ColorListViewHeader(ref listViewMustemilat, SystemColors.Menu, SystemColors.MenuText);
        //}

        #region ForColorLVBackColor
        public static void ColorListViewHeader(ref ListView list, Color backColor, Color foreColor)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            // Her ListView için olayları yalnız bir kez bağla; yenilemeler sadece renkleri değiştirir.
            // Zayıf anahtar, kapatılmış kontrollerin statik önbellekte tutulmasını önler.
            HeaderColors colors = HeaderStyles.GetValue(list, key =>
            {
                key.DrawColumnHeader += DrawStyledHeader;
                key.DrawItem += BodyDraw;
                key.DrawSubItem += DrawStyledSubItem;
                return new HeaderColors();
            });
            colors.Background = backColor;
            colors.Foreground = foreColor;
            list.OwnerDraw = true;
            list.Invalidate();
        }

        private sealed class HeaderColors
        {
            internal Color Background;
            internal Color Foreground;
        }

        private static readonly System.Runtime.CompilerServices.ConditionalWeakTable<ListView, HeaderColors>
            HeaderStyles = new System.Runtime.CompilerServices.ConditionalWeakTable<ListView, HeaderColors>();

        private static void DrawStyledHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (sender is ListView list && HeaderStyles.TryGetValue(list, out HeaderColors colors))
                HeaderDraw(sender, e, colors.Background, colors.Foreground);
            else
                e.DrawDefault = true;
        }

        private static void DrawStyledSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
        private static void HeaderDraw(object sender, DrawListViewColumnHeaderEventArgs e, Color backColor, Color foreColor)
        {
            using (SolidBrush backBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            using (SolidBrush foreBrush = new SolidBrush(foreColor))
            {
                e.Graphics.DrawString(e.Header.Text, e.Font, foreBrush, e.Bounds);
            }
        }

        private static void BodyDraw(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        #endregion

        private static void ClearList(ListView list)
        {
            list.Items.Clear();
            // Items.Clear grupları silmez; aksi halde her yenilemede boş gruplar birikir.
            list.Groups.Clear();
        }

        /// <summary>İstenen görünümü temizler; kişi görünümü parselin diğer listelerini korur.</summary>
        public void Clear(ListViewType listViewType)
        {
            switch (listViewType)
            {
                case ListViewType.Kisi:
                    propertyGridMalik.SelectedObject = null;
                    tabPageMalik.Text = "Malik";
                    break;
                case ListViewType.Malik:
                    ClearList(listViewMalik);
                    propertyGridMalik.SelectedObject = null;
                    tabPageMalik.Text = "Malik";
                    colMalikHisse.Text = "Hisse";
                    ColorListViewHeader(ref listViewMalik, SystemColors.Menu, SystemColors.MenuText);
                    break;
                case ListViewType.Kamu:
                    ClearList(listViewKamu);
                    break;
                case ListViewType.Dava:
                    ClearList(listViewDava);
                    break;
                case ListViewType.Mustemilat:
                case ListViewType.Mevsimlik:
                    ClearList(listViewMustemilat);
                    break;
                default:
                    Clear(ListViewType.Malik);
                    ClearList(listViewKamu);
                    ClearList(listViewDava);
                    ClearList(listViewMustemilat);
                    propertyGridParsel.SelectedObject = null;
                    tabPageParsel.Text = "Parsel";
                    break;
            }
        }

        /// <summary>Parsel veya kişi kimliğiyle ilgili görünümü yükler. UI iş parçacığında çağrılmalıdır.</summary>
        public void FillListView(string GlobalID, ListViewType listViewType)
        {
            if (!Enum.IsDefined(typeof(ListViewType), listViewType))
                throw new ArgumentOutOfRangeException(nameof(listViewType));

            ListView[] lists = { listViewMalik, listViewKamu, listViewDava, listViewMustemilat };
            foreach (ListView list in lists)
                list.BeginUpdate();
            // Toplu güncelleme titremeyi azaltır. DoEvents kullanmayarak yükleme sırasında
            // yeniden giriş ve henüz tamamlanmamış listeler üzerinden seçim işlemlerini önleriz.
            try
            {
                Clear(listViewType);
                switch (listViewType)
                {
                    case ListViewType.Kisi: FillGridMalik(GlobalID); break;
                    case ListViewType.Malik: FillListViewMalik(GlobalID); break;
                    case ListViewType.Kamu: FillListViewKamu(GlobalID); break;
                    case ListViewType.Dava: FillListViewDava(GlobalID); break;
                    case ListViewType.Mustemilat:
                    case ListViewType.Mevsimlik: FillListViewMusMev(GlobalID); break;
                    case ListViewType.Tum:
                        FillGridParsel(GlobalID);
                        FillListViewMalik(GlobalID);
                        FillListViewKamu(GlobalID);
                        FillListViewDava(GlobalID);
                        FillListViewMusMev(GlobalID);
                        break;
                }
            }
            finally
            {
                foreach (ListView list in lists)
                    list.EndUpdate();
            }
        }
        #region MustemilatMevsimlik
        #region MustemilatMevsimlikDelegation
        public ListView ListViewMusMev
        {
            get => listViewMustemilat;
            set => listViewMustemilat = value;
        }

        public delegate void listViewMusMevMouseDoubleClickEventHandler(object Sender, MouseEventArgs e);
        public event listViewMusMevMouseDoubleClickEventHandler ListViewMustemilatMevsimlikMouseDoubleClick;

        private void ListViewMusMev_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewMustemilatMevsimlikMouseDoubleClick?.Invoke(this, e);
        }
        #endregion
        /// <summary>Parselin müştemilat ve mevsimlik kayıtlarını tek listede tür ayrımıyla gösterir.</summary>
        private void FillListViewMusMev(string parselGlobalID)
        {
            try
            {
                _owners.Clear();
                List<Mustemilat> Mustemilatlar = PresentationData.GetParcelFixtures(parselGlobalID);
                int countMustemilatlar = Mustemilatlar.Count;

                if (countMustemilatlar > 0)
                {
                    double dblToplamAdetMus = 0;
                    double dblToplamBedelMus = 0;
                    ListViewGroup lvgMus = new ListViewGroup("Müştemilat");
                    listViewMustemilat.Groups.Add(lvgMus);
                    MustemilatListele(lvgMus, Mustemilatlar, ref dblToplamAdetMus, ref dblToplamBedelMus);
                    if (countMustemilatlar > 1)
                    {
                        MustMevToplamListele(lvgMus, listViewMustemilat, dblToplamAdetMus, "N0", dblToplamBedelMus);
                    }
                }

                List<Mevsimlik> Mevsimlikler = PresentationData.GetParcelSeasonalProducts(parselGlobalID);
                int countMevsimlikler = Mevsimlikler.Count;

                if (countMevsimlikler > 0)
                {
                    double dblToplamAlanMev = 0;
                    double dblToplamBedelMev = 0;
                    ListViewGroup lvgMev = new ListViewGroup("Mevsimlik");
                    listViewMustemilat.Groups.Add(lvgMev);
                    MevsimlikListele(lvgMev, Mevsimlikler, ref dblToplamAlanMev, ref dblToplamBedelMev);
                    if (countMevsimlikler > 1)
                    {
                        MustMevToplamListele(lvgMev, listViewMustemilat, dblToplamAlanMev, "N", dblToplamBedelMev);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Kisi ReadOwner(Kisi owner)
        {
            // Veri hizmeti bazı kayıtlarda yalnız malik kimliğini verir. Tam kişiyi bir kez
            // oku; aynı yenilemede müştemilat ve mevsimlik satırları bu sonucu paylaşır.
            if (owner == null || string.IsNullOrWhiteSpace(owner.GlobalID))
                return owner;
            if (!_owners.TryGetValue(owner.GlobalID, out Kisi person))
                _owners[owner.GlobalID] = person = PresentationData.GetPerson(owner.GlobalID);
            return person ?? owner;
        }

        private void MustemilatListele(ListViewGroup lvg, List<Mustemilat> Mustemilatlar, ref double dblToplamAdet, ref double dblToplamBedel)
        {
            double dblTutar;
            foreach (Mustemilat MyMustemilat in Mustemilatlar)
            {
                Kisi MySahip = ReadOwner(MyMustemilat.Sahip);

                string strGlobalID = MyMustemilat.GlobalID;
                string strTanim = MyMustemilat.Tanim;
                double dblAdet = MyMustemilat.Adet;
                double dblBirimFiyat = MyMustemilat.BirimFiyat;
                double dblHisse = MyMustemilat.Pay / (double)(MyMustemilat.Payda);
                string strSahip = string.Empty;

                if (MySahip != null)
                {
                    strSahip = dblHisse < 1.0
                    ? $"{MySahip} ({MyMustemilat.Pay}/{MyMustemilat.Payda})"
                    : $"{MySahip}";
                }
                dblToplamAdet += dblAdet;
                dblTutar = dblAdet * dblBirimFiyat * dblHisse;
                dblToplamBedel += dblTutar;

                ListViewItem MustemilatItem = new ListViewItem(strTanim) { Name = ListViewType.Mustemilat.ToString(), ImageIndex = 3, Group = lvg };
                MustemilatItem.SubItems.Add(dblAdet.ToString());
                MustemilatItem.SubItems.Add(dblBirimFiyat.ToString("C"));
                MustemilatItem.SubItems.Add(dblTutar.ToString("C"));
                MustemilatItem.SubItems.Add(strSahip);
                MustemilatItem.Tag = MyMustemilat;
                listViewMustemilat.Items.Add(MustemilatItem);
            }
        }

        private void MevsimlikListele(ListViewGroup lvg, List<Mevsimlik> Mevsimlikler, ref double dblToplamAlan, ref double dblToplamBedel)
        {
            double dblTutar;
            foreach (Mevsimlik MyMevsimlik in Mevsimlikler)
            {
                Kisi MySahip = ReadOwner(MyMevsimlik.Sahip);

                string strGlobalID = MyMevsimlik.GlobalID;
                string strTanim = MyMevsimlik.Tanim;
                double dblAlan = MyMevsimlik.Alan;
                double dblBirimFiyat = MyMevsimlik.BirimFiyat;
                double dblHisse = MyMevsimlik.Pay / (double)(MyMevsimlik.Payda);
                string strSahip = string.Empty;

                if (MySahip != null)
                {
                    strSahip = dblHisse < 1.0
                    ? $"{MySahip} ({MyMevsimlik.Pay}/{MyMevsimlik.Payda})"
                    : $"{MySahip}";
                }
                dblToplamAlan += dblAlan;
                dblTutar = dblAlan * dblBirimFiyat * dblHisse;
                dblToplamBedel += dblTutar;

                ListViewItem MevsimlikItem = new ListViewItem(strTanim) { Name = ListViewType.Mevsimlik.ToString(), ImageIndex = 4, Group = lvg };
                MevsimlikItem.SubItems.Add($"{dblAlan:N}");
                MevsimlikItem.SubItems.Add($"{dblBirimFiyat:C}");
                MevsimlikItem.SubItems.Add($"{dblTutar:C}");
                MevsimlikItem.SubItems.Add(strSahip);
                MevsimlikItem.Tag = MyMevsimlik;
                listViewMustemilat.Items.AddRange(new ListViewItem[] { MevsimlikItem });
            }
        }

        private void MustMevToplamListele(ListViewGroup lvg, ListView listView, double dblToplamMiktar, string strToplamFormat, double dblToplamBedel)
        {
            ListViewItem Toplam = new ListViewItem("Toplam") { Name = "0", Group = lvg };
            Toplam.SubItems.Add(dblToplamMiktar.ToString(strToplamFormat));
            Toplam.SubItems.Add("");
            Toplam.SubItems.Add(dblToplamBedel.ToString("C"));
            Toplam.SubItems.Add("");
            Font MyFont = new Font("Trebuchet MS", 10, FontStyle.Bold);
            Toplam.Font = MyFont;
            //Toplam.BackColor = Color.LightYellow;
            listView.Items.AddRange(new ListViewItem[] { Toplam });
        }

        #endregion

        #region Dava
        #region DavaDelegation
        public ListView ListViewDava
        {
            get => listViewDava;
            set => listViewDava = value;
        }

        public delegate void listViewDavaMouseDoubleClickEventHandler(object Sender, MouseEventArgs e);
        public event listViewDavaMouseDoubleClickEventHandler ListViewDavaMouseDoubleClick;

        private void ListViewDava_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewDavaMouseDoubleClick?.Invoke(this, e);
        }
        #endregion
        private void FillListViewDava(string parselGlobalID)
        {
            try
            {
                List<Dava> Davalar = PresentationData.GetParcelLawsuits(parselGlobalID);


                // Tür kodlarının ardışık olması gerekmez; bilinmeyen kodlu kayıtları da göster.
                foreach (int i in Davalar.Select(item => item.Turu).Distinct().OrderBy(type => type))
                {
                    List<Dava> DavaTurleri = Davalar.Where(x => x.Turu == i).ToList();
                    if (DavaTurleri.Any())
                    {
                        string strDavaTuru = KodListeMetni(18, DavaTurleri.First().Turu);
                        ListViewGroup lvg = new ListViewGroup(strDavaTuru);
                        listViewDava.Groups.Add(lvg);
                        DavaListele(lvg, DavaTurleri);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DavaListele(ListViewGroup lvg, List<Dava> Davalar)
        {
            foreach (Dava MyDava in Davalar)
            {
                string strDavaTuru = KodListeMetni(18, MyDava.Turu);
                string strGlobalID = MyDava.GlobalID;
                string strMahkeme = MyDava.Mahkeme;
                string strEsasNo = MyDava.EsasNo;
                string strKararNo = MyDava.KararNo;
                string strKararTarihi = ValidDate(MyDava.KararTarihi) ? MyDava.KararTarihi.ToShortDateString() : string.Empty;
                string strDavaTarihi = ValidDate(MyDava.DavaTarihi) ? MyDava.DavaTarihi.ToShortDateString() : string.Empty;
                double dblBedel = MyDava.ToplamBedel;

                ListViewItem DavaItem = new ListViewItem(strMahkeme) { Name = strGlobalID, ImageIndex = MyDava.Turu + 4, Group = lvg };
                DavaItem.SubItems.Add(strEsasNo);
                DavaItem.SubItems.Add(strDavaTarihi);
                DavaItem.SubItems.Add(strKararNo);
                DavaItem.SubItems.Add(strKararTarihi);
                DavaItem.SubItems.Add($"{dblBedel:C}");
                DavaItem.Tag = MyDava;
                listViewDava.Items.Add(DavaItem);
            }
        }
        #endregion

        #region Kamu

        #region KamuDelegation
        public ListView ListViewKamu
        {
            get => listViewKamu;
            set => listViewKamu = value;
        }

        public delegate void listViewKamuMouseDoubleClickEventHandler(object Sender, MouseEventArgs e);
        public event listViewKamuMouseDoubleClickEventHandler ListViewKamuMouseDoubleClick;

        private void ListViewKamu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewKamuMouseDoubleClick?.Invoke(this, e);
        }
        #endregion

        private double FillListViewKamu(string parselGlobalID)
        {
            List<Kamulastirma> Alanlar = PresentationData.GetParcelAcquisitionAreas(parselGlobalID);

            double dblGenelToplamAlan = 0;
            double dblGenelToplamBedel = 0;
            int intAlanCesitSayisi = 0;
            // Kod listesi uzunluğundan tür üretmek yerine kayıtlardaki gerçek türleri grupla.
            foreach (int i in Alanlar.Select(item => item.Turu).Distinct().OrderBy(type => type))
            {
                double dblTutar = 0;
                double dblToplamAlan = 0;
                double dblToplamBedel = 0;
                //int index = i;
                List<Kamulastirma> AltAlanlar = Alanlar.Where(x => x.Turu == i).ToList();
                if (AltAlanlar.Any())
                {
                    intAlanCesitSayisi++;
                    ListViewGroup lvg = new ListViewGroup(KodListeMetni(13, AltAlanlar[0].Turu));
                    listViewKamu.Groups.Add(lvg);
                    int AlanSayisi = AlanListele(lvg, AltAlanlar, ref dblToplamAlan, ref dblTutar, ref dblToplamBedel);
                    dblGenelToplamAlan += dblToplamAlan;
                    dblGenelToplamBedel += dblToplamBedel;
                    if (AlanSayisi > 1)
                    {
                        ToplamListele(lvg, "Toplam", dblToplamAlan, dblToplamBedel);
                    }
                }
            }
            if (intAlanCesitSayisi > 1)
            {
                ListViewGroup lvg = new ListViewGroup("Genel Toplam");
                listViewKamu.Groups.Add(lvg);
                ToplamListele(lvg, "", dblGenelToplamAlan, dblGenelToplamBedel);
            }
            return dblGenelToplamAlan;
        }


        private int AlanListele(ListViewGroup lvg, List<Kamulastirma> AlanlarGrubu, ref double dblToplamAlan, ref double dblTutar, ref double dblToplamBedel)
        {
            int AlanSayisi = 0;
            foreach (Kamulastirma MyKamulastirma in AlanlarGrubu)
            {
                string strTanim = KodListeMetni(13, MyKamulastirma.Turu);
                Color colorTxt = Color.Gray;
                string strGlobalID = MyKamulastirma.GlobalID;
                double dblAlan = MyKamulastirma.Alan;
                double dblBedel = MyKamulastirma.Bedel;
                dblTutar = dblAlan * dblBedel;
                string strTuru = KodListeMetni(13, MyKamulastirma.Turu);
                string strAsamasi = KodListeMetni(15, MyKamulastirma.Asamasi);

                switch (MyKamulastirma.Asamasi)
                {
                    case 0:
                        {
                            strTanim = strTuru;
                            colorTxt = Color.Black;
                            dblToplamAlan += dblAlan;
                            dblTutar = dblAlan * dblBedel;
                            dblToplamBedel += dblTutar;
                            break;
                        }
                    case 1:
                        {
                            strTanim = $"{strAsamasi}";
                            colorTxt = Color.DarkGreen;
                            dblToplamAlan += dblAlan;
                            dblTutar = dblAlan * dblBedel;
                            dblToplamBedel += dblTutar;
                            break;
                        }
                    case 2:
                        {
                            strTanim = $"{strAsamasi}";
                            colorTxt = Color.Red;
                            dblToplamAlan -= dblAlan;
                            dblTutar = dblAlan * dblBedel;
                            dblToplamBedel -= dblTutar;
                            break;
                        }
                }

                ListViewItem KamuItem = new ListViewItem(strTanim) { Name = strGlobalID, ImageIndex = -1, Group = lvg };
                KamuItem.SubItems.Add(dblAlan.ToString("N2"));
                KamuItem.SubItems.Add(dblBedel.ToString("C"));
                KamuItem.SubItems.Add(dblTutar.ToString("C"));
                KamuItem.Tag = MyKamulastirma;
                KamuItem.ForeColor = colorTxt;

                listViewKamu.Items.Add(KamuItem);
                AlanSayisi++;
            }
            return AlanSayisi;
        }

        private void ToplamListele(ListViewGroup lvg, string MyText, double dblToplamAlan, double dblToplamBedel)
        {
            ListViewItem AlanToplam = new ListViewItem(MyText) { Name = string.Empty, Group = lvg };
            AlanToplam.SubItems.Add($"{dblToplamAlan:N2}");
            AlanToplam.SubItems.Add("");
            AlanToplam.SubItems.Add($"{dblToplamBedel:C}");
            AlanToplam.SubItems.Add("");
            AlanToplam.Font = new Font("Trebuchet MS", 10, FontStyle.Bold);
            //AlanToplam.BackColor = MyBackColor;
            listViewKamu.Items.AddRange(new ListViewItem[] { AlanToplam });
        }

        #endregion

        #region Malik
        #region MalikDelegation
        public ListView ListViewMalik
        {
            get => listViewMalik;
            set => listViewMalik = value;
        }

        public delegate void listViewMalikMouseDoubleClickEventHandler(object Sender, MouseEventArgs e);
        public event listViewMalikMouseDoubleClickEventHandler ListViewMalikMouseDoubleClick;

        private void ListViewMalik_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewMalikMouseDoubleClick?.Invoke(this, e);
        }
        #endregion
        private void FillListViewMalik(string parselGlobalID)
        {
            listViewMalik.BeginUpdate();
            try
            {
                List<Kisi> sahipler = PresentationData.GetParcelOwners(parselGlobalID);
                Dictionary<string, List<Kisi>> mirasAgaci = PresentationData.GetHeirHierarchy(
                    sahipler.Select(sahip => sahip.GlobalID));
                double HisseToplam = 0;
                foreach (Kisi MySahip in sahipler)
                {
                    Hisse MyHisse = MySahip?.Hisse;
                    if (MyHisse == null)
                        continue;

                    string strGlobalID = $"{MyHisse.KisiGlobalID} GlobalID kimlikli kişi bulunamadı";
                    string strKisiGlobalID = string.Empty;
                    string strTCKimlikNo = string.Empty;
                    string strMalik = string.Empty;
                    string strBaba = string.Empty;
                    string strHisse = string.Empty;
                    string strCinsiyet = string.Empty;
                    string strDurumu = string.Empty;
                    string strTelefon = string.Empty;
                    string strAdres = string.Empty;

                    if (MySahip != null)
                    {
                        strGlobalID = MyHisse.GlobalID;
                        strKisiGlobalID = MySahip.GlobalID;
                        strTCKimlikNo = MySahip.TCKimlikNo.ToString();
                        strMalik = MySahip.ToString();
                        strBaba = MySahip.Baba;
                        strHisse = MyHisse.ToString();
                        strTelefon = MySahip.Telefon;
                        strAdres = MySahip.Adres;
                        if (!string.IsNullOrEmpty(MySahip.Cinsiyet))
                        {
                            strCinsiyet = MySahip.Cinsiyet.Substring(0, 1).ToUpper();
                        }
                        strDurumu = (MySahip.Durumu ?? string.Empty).Trim().ToUpper();
                        if (MyHisse != null && MyHisse.Payda != 0)
                            HisseToplam += (double)MyHisse.Pay / MyHisse.Payda;                    
                    }

                    // İkon seçimi
                    int imageIndex = -1;
                    if (strDurumu == "ÖLÜ")
                        imageIndex = 0;
                    else if (strCinsiyet == "E")
                        imageIndex = 1;
                    else if (strCinsiyet == "K")
                        imageIndex = 2;

                    Color HisseColor = Color.Black;
                    switch (MyHisse.TescilDurumu)
                    {
                        case 1: HisseColor = Color.DarkGreen; break;
                        case 2: HisseColor = Color.DarkBlue; break;
                    }
                    ListViewItem HisseItem = new ListViewItem(strTCKimlikNo) { Name = strGlobalID, ImageIndex = imageIndex };
                    HisseItem.SubItems.Add(strMalik);
                    HisseItem.SubItems.Add(strBaba);
                    HisseItem.SubItems.Add(strHisse);
                    HisseItem.SubItems.Add(strTelefon);
                    HisseItem.SubItems.Add(strAdres);
                    //Font MyFont = new Font("Trebuchet MS", 10, FontStyle.Regular);
                    //HisseItem.Font = MyFont;
                    HisseItem.ForeColor = HisseColor;
                    HisseItem.Tag = MyHisse;
                    listViewMalik.Items.Add(HisseItem);
                    if (MySahip != null)
                    {
                        VarsaMirasciEkle(MySahip, 1, new HashSet<string> { MySahip.GlobalID }, mirasAgaci);
                    }


                }
                if (!HisselerDogru(HisseToplam))
                {
                    ListViewItem HisseHatasiItem = new ListViewItem("Hisse Hatası") { Name = "", ImageIndex = -1 };
                    HisseHatasiItem.SubItems.Add("");
                    HisseHatasiItem.SubItems.Add("");
                    HisseHatasiItem.SubItems.Add($"{HisseToplam}");
                    Font MyFont = new Font("Trebuchet MS", 10, FontStyle.Bold);
                    HisseHatasiItem.Font = MyFont;
                    HisseHatasiItem.ForeColor = Color.White;
                    HisseHatasiItem.BackColor = Color.Red;
                    listViewMalik.Items.Add(HisseHatasiItem);
                    colMalikHisse.Text = "Hisse (Hatalı)";
                    ColorListViewHeader(ref listViewMalik, Color.Red, Color.White);
                }
                else
                {
                    colMalikHisse.Text = "Hisse";
                    ColorListViewHeader(ref listViewMalik, SystemColors.Menu, SystemColors.MenuText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listViewMalik.EndUpdate();
            }
        }

        private void VarsaMirasciEkle(
            Kisi muris,
            int seviye,
            HashSet<string> ziyaretEdilenKisiler,
            Dictionary<string, List<Kisi>> mirasAgaci)
        {
            try
            {
                if (!mirasAgaci.TryGetValue(muris.GlobalID, out List<Kisi> kisiler))
                {
                    // Bazı eski veritabanlarında toplu miras sorgusu sonuç
                    // döndürmeyebiliyor. Ağaç görünümünde de kullanılan tekil
                    // sorguya yalnız gerektiğinde geri düşerek davranışı koru.
                    kisiler = PresentationData.GetHeirs(muris.GlobalID);
                    mirasAgaci[muris.GlobalID] = kisiler;
                }

                if (kisiler.Count == 0)
                    return;

                foreach (Kisi varis in kisiler)
                {
                    if (!string.IsNullOrEmpty(varis.Adi) && ziyaretEdilenKisiler.Add(varis.GlobalID))
                    {
                        string girinti = new string(' ', seviye * 4) + " ";
                        int imageIndex = 8;

                        if (!string.IsNullOrEmpty(varis.Durumu) &&
                            varis.Durumu.Trim().ToUpper(CultureInfo.InvariantCulture).StartsWith("Ö"))
                        {
                            imageIndex = 9;
                        }

                        ListViewItem varisItem = new ListViewItem(varis.TCKimlikNo.ToString())
                        {
                            Name = varis.GlobalID,
                            ImageIndex = imageIndex,
                            ForeColor = Color.DarkMagenta,
                            Tag = varis,
                            ToolTipText = $"{muris} mirasçısı"
                        };

                        varisItem.SubItems.Add(girinti + varis);
                        varisItem.SubItems.Add(varis.Baba);
                        varisItem.SubItems.Add("Mirasçı");
                        varisItem.SubItems.Add(varis.Telefon);
                        varisItem.SubItems.Add(varis.Adres);
                        listViewMalik.Items.Add(varisItem);

                        VarsaMirasciEkle(varis, seviye + 1, ziyaretEdilenKisiler, mirasAgaci);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private static bool HisselerDogru(double HisseToplam)
        {
            return Math.Abs(HisseToplam - 1.0) < 0.000000001;
        }
        #endregion

        private string KodListeMetni(int ListID, int Kod)
        {
            // Kod açıklamaları bağlantı değişene kadar önbellekte kalır; satır başına sorgu yapmayız.
            if (!_codeLists.TryGetValue(ListID, out Dictionary<int, string> values))
                _codeLists[ListID] = values = Lists.CreateComboDictionary(ListID);
            return values.TryGetValue(Kod, out string value) ? value : $"Hatalı Kodlama ({Kod})";
        }

        private bool ValidDate(DateTime dtTarih)
        {
            return dtTarih.Year > 1899;
        }

        private void FillGridParsel(string parselGlobalID)
        {
            try
            {
                Parsel MyParsel = PresentationData.GetParcel(parselGlobalID);
                if (MyParsel != null)
                {
                    tabControlParsel.TabPages[0].Text = MyParsel.ToString();
                    GridParselKodModel MyGridParsel = new GridParselKodModel(
                        MyParsel,
                        parselKadastralDurumDict,
                        parselMalikTipiDict,
                        parselIstimlakTuruDict,
                        parselIstimlakSerhiDict,
                        parselEdinimDurumuDict);
                    tabControlParsel.SelectedTab = tabPageParsel;
                    propertyGridParsel.SelectedObject = MyGridParsel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ListViewMalik_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listViewMalik.SelectedItems.Count > 0)
                {
                    object satirVerisi = listViewMalik.SelectedItems[0].Tag;
                    if (satirVerisi is Hisse hisse)
                    {
                        propertyGridMalik.SelectedObject = default;
                        FillGridMalik(hisse.KisiGlobalID, hisse);
                    }
                    else if (satirVerisi is Kisi mirasci)
                    {
                        propertyGridMalik.SelectedObject = default;
                        FillGridMalik(mirasci.GlobalID);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FillGridMalik(string kisiGlobalID, Hisse hisse = null)
        {
            propertyGridMalik.SelectedObject = null;
            tabPageMalik.Text = "Malik";
            try
            {
                Kisi MyKisi = PresentationData.GetPerson(kisiGlobalID);
                if (MyKisi != null)
                {
                    tabControlParsel.TabPages[1].Text = MyKisi.ToString();
                    GridMalikKodModel MyGridKisi = new GridMalikKodModel(
                        MyKisi,
                        hisse,
                        kisiAnlasmaDurumuDict,
                        kisiDavetiyeAlinmaDurumuDict,
                        kisiDavetiyeTebligDurumuDict,
                        kisiGorusmeDurumuDict,
                        kisiTescilDurumuDict);
                    tabControlParsel.SelectedTab = tabPageMalik;
                    propertyGridMalik.SelectedObject = MyGridKisi;
                }
            }
            catch (global::System.Exception)
            {
                throw;
            }
        }

        private void PropertyGridParsel_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            GridParselKodModel model = propertyGridParsel.SelectedObject as GridParselKodModel;
            if (model == null) return;

            try
            {
                model.Apply();
                if (!PresentationData.UpdateParcel(model.Source))
                    throw new InvalidOperationException("Parsel bilgisi kaydedilemedi.");
            }
            catch (Exception ex)
            {
                FillGridParsel(model.Source.GlobalID);
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PropertyGridMalik_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            GridMalikKodModel model = propertyGridMalik.SelectedObject as GridMalikKodModel;
            if (model == null) return;

            try
            {
                model.Apply();
                // Kişi ve hisse ayrı kayıtlardır. Yalnız değişen alanın ait olduğu kaydı yazmak,
                // ilgisiz güncellemeleri ve iki yazmadan birinin başarısız olması riskini azaltır.
                bool shareProperty = e.ChangedItem.PropertyDescriptor?.Name == nameof(GridMalikKodModel.TescilDurumu)
                    || e.ChangedItem.PropertyDescriptor?.Name == nameof(GridMalikKodModel.Dusunceler);
                if (!shareProperty && !PresentationData.UpdatePerson(model.Source))
                    throw new InvalidOperationException("Malik bilgisi kaydedilemedi.");
                if (shareProperty && model.Share != null && !PresentationData.UpdateShare(model.Share))
                    throw new InvalidOperationException("Hisse bilgisi kaydedilemedi.");
            }
            catch (Exception ex)
            {
                Hisse share = model.Share == null
                    ? null
                    : PresentationData.GetParcelShares(model.Share.ParselGlobalID)
                        .FirstOrDefault(item => item.GlobalID == model.Share.GlobalID);
                FillGridMalik(model.Source.GlobalID, share);
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
