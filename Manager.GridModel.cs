using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Dashboard
{
    public partial class Manager
    {
        /// <summary>Veritabanı kodlarını kullanıcıya açıklama olarak sunan parsel düzenleme modeli.</summary>
        private class GridParselKodModel
        {
            private readonly Dictionary<string, Dictionary<int, string>> _codeLists;

            public GridParselKodModel(
                Kamu.Parsel source,
                Dictionary<int, string> kadastralDurum,
                Dictionary<int, string> malikTipi,
                Dictionary<int, string> istimlakTuru,
                Dictionary<int, string> istimlakSerhi,
                Dictionary<int, string> edinimDurumu)
            {
                Source = source ?? throw new ArgumentNullException(nameof(source));
                _codeLists = new Dictionary<string, Dictionary<int, string>>
                {
                    [nameof(KadastralDurum)] = kadastralDurum,
                    [nameof(MalikTipi)] = malikTipi,
                    [nameof(IstimlakTuru)] = istimlakTuru,
                    [nameof(IstimlakSerhi)] = istimlakSerhi,
                    [nameof(EdinimDurumu)] = edinimDurumu
                };

                KadastralDurum = CodeText(kadastralDurum, source.KadastralDurum);
                MalikTipi = CodeText(malikTipi, source.MalikTipi);
                IstimlakTuru = CodeText(istimlakTuru, source.IstimlakTuru);
                IstimlakSerhi = CodeText(istimlakSerhi, source.IstimlakSerhi);
                EdinimDurumu = CodeText(edinimDurumu, source.EdinimDurumu);
                IstimlakDisi = source.IstimlakDisi;
                DavaDurumu10 = source.DavaDurumu10;
                DavaDurumu27 = source.DavaDurumu27;
                DevirDurumu = source.DevirDurumu;
                OdemeDurumu = source.OdemeDurumu;
                Aciklama = source.Aciklama;
            }

            internal Kamu.Parsel Source { get; }

            internal IEnumerable<string> GetStandardValues(string propertyName) =>
                propertyName != null && _codeLists.TryGetValue(propertyName, out Dictionary<int, string> values)
                    ? values.OrderBy(item => item.Key).Select(item => item.Value)
                    : Enumerable.Empty<string>();

            internal void Apply()
            {
                // Önce bütün kodları doğrula; bir seçim geçersizse Source kısmen değişmesin.
                int codeKadastralDurum = CodeValue(_codeLists[nameof(KadastralDurum)], KadastralDurum, Source.KadastralDurum);
                int codeMalikTipi = CodeValue(_codeLists[nameof(MalikTipi)], MalikTipi, Source.MalikTipi);
                int codeIstimlakTuru = CodeValue(_codeLists[nameof(IstimlakTuru)], IstimlakTuru, Source.IstimlakTuru);
                int codeIstimlakSerhi = CodeValue(_codeLists[nameof(IstimlakSerhi)], IstimlakSerhi, Source.IstimlakSerhi);
                int codeEdinimDurumu = CodeValue(_codeLists[nameof(EdinimDurumu)], EdinimDurumu, Source.EdinimDurumu);
                Source.KadastralDurum = codeKadastralDurum;
                Source.MalikTipi = codeMalikTipi;
                Source.IstimlakTuru = codeIstimlakTuru;
                Source.IstimlakSerhi = codeIstimlakSerhi;
                Source.EdinimDurumu = codeEdinimDurumu;
                Source.IstimlakDisi = IstimlakDisi;
                Source.DavaDurumu10 = DavaDurumu10;
                Source.DavaDurumu27 = DavaDurumu27;
                Source.DevirDurumu = DevirDurumu;
                Source.OdemeDurumu = OdemeDurumu;
                Source.Aciklama = Aciklama;
            }

            [Description("Parselin kadastral durumunu gösterir."), Category("Parsel"), DisplayName("Kadastral Durum"), TypeConverter(typeof(CodeTextConverter))]
            public string KadastralDurum { get; set; }

            [Description("Parselin malik tipini gösterir."), Category("Parsel"), DisplayName("Malik Tipi"), TypeConverter(typeof(CodeTextConverter))]
            public string MalikTipi { get; set; }

            [Description("Kamulaştırma türünü ifade eder."), Category("Kamulaştırma"), DisplayName("İstimlak Türü"), TypeConverter(typeof(CodeTextConverter))]
            public string IstimlakTuru { get; set; }

            [Description("Kamulaştırma şerh durumunu gösterir."), Category("Kamulaştırma"), DisplayName("İstimlak Şerhi"), TypeConverter(typeof(CodeTextConverter))]
            public string IstimlakSerhi { get; set; }

            [Description("Parselin edinim durumunu gösterir."), Category("Kamulaştırma"), DisplayName("Parsel Edinim Durumu"), TypeConverter(typeof(CodeTextConverter))]
            public string EdinimDurumu { get; set; }

            [Description("Parselin kamulaştırma dışı bırakılma durumunu gösterir."), Category("Kamulaştırma"), DisplayName("İstimlak Dışı"), TypeConverter(typeof(TurkishBooleanConverter))]
            public bool IstimlakDisi { get; set; }

            [Description("Parselin tescil davası durumunu gösterir."), Category("Dava"), DisplayName("Tescil Davası Durumu"), TypeConverter(typeof(TurkishBooleanConverter))]
            public bool DavaDurumu10 { get; set; }

            [Description("Parselin acele kamulaştırma davası durumunu gösterir."), Category("Dava"), DisplayName("Acele Kamulaştırma Davası Durumu"), TypeConverter(typeof(TurkishBooleanConverter))]
            public bool DavaDurumu27 { get; set; }

            [Description("Parselin devir durumunu gösterir."), Category("Kamulaştırma"), DisplayName("Parsel Devir Durumu"), TypeConverter(typeof(TurkishBooleanConverter))]
            public bool DevirDurumu { get; set; }

            [Description("Parselin bedel ödeme durumunu gösterir."), Category("Kamulaştırma"), DisplayName("Parsel Bedel Ödeme Durumu"), TypeConverter(typeof(TurkishBooleanConverter))]
            public bool OdemeDurumu { get; set; }

            [Description("Parsel hakkında açıklama gösterir."), Category("Parsel"), DisplayName("Açıklama")]
            public string Aciklama { get; set; }
        }

        /// <summary>Kişi alanları ile isteğe bağlı parsel hissesi alanlarını aynı PropertyGrid'de sunar.</summary>
        private class GridMalikKodModel
        {
            private readonly Dictionary<string, Dictionary<int, string>> _codeLists;

            public GridMalikKodModel(
                Kamu.Kisi source,
                Kamu.Hisse share,
                Dictionary<int, string> anlasmaDurumu,
                Dictionary<int, string> davetiyeAlinmaDurumu,
                Dictionary<int, string> davetiyeTebligDurumu,
                Dictionary<int, string> gorusmeDurumu,
                Dictionary<int, string> tescilDurumu)
            {
                Source = source ?? throw new ArgumentNullException(nameof(source));
                Share = share;
                _codeLists = new Dictionary<string, Dictionary<int, string>>
                {
                    [nameof(AnlasmaDurumu)] = anlasmaDurumu,
                    [nameof(DavetiyeAlinmaDurumu)] = davetiyeAlinmaDurumu,
                    [nameof(DavetiyeTebligDurumu)] = davetiyeTebligDurumu,
                    [nameof(GorusmeDurumu)] = gorusmeDurumu,
                    [nameof(TescilDurumu)] = tescilDurumu
                };

                AnlasmaDurumu = CodeText(anlasmaDurumu, source.AnlasmaDurumu);
                DavetiyeAlinmaDurumu = CodeText(davetiyeAlinmaDurumu, source.DavetiyeAlinmaDurumu);
                DavetiyeTebligDurumu = CodeText(davetiyeTebligDurumu, source.DavetiyeTebligDurumu);
                GorusmeDurumu = CodeText(gorusmeDurumu, source.GorusmeDurumu);
                AnlasmaDusunceler = source.AnlasmaDusunceler;
                TescilDurumu = share == null ? null : CodeText(tescilDurumu, share.TescilDurumu);
                Dusunceler = share?.Aciklama;
            }

            internal Kamu.Kisi Source { get; }
            internal Kamu.Hisse Share { get; }

            internal IEnumerable<string> GetStandardValues(string propertyName) =>
                propertyName != null && _codeLists.TryGetValue(propertyName, out Dictionary<int, string> values)
                    ? values.OrderBy(item => item.Key).Select(item => item.Value)
                    : Enumerable.Empty<string>();

            internal void Apply()
            {
                // Mirasçı görünümünde Share bulunmayabilir. Doğrulama tamamlanmadan
                // kişi veya hisse nesnesine değer yazma.
                int codeAnlasmaDurumu = CodeValue(_codeLists[nameof(AnlasmaDurumu)], AnlasmaDurumu, Source.AnlasmaDurumu);
                int codeDavetiyeAlinmaDurumu = CodeValue(_codeLists[nameof(DavetiyeAlinmaDurumu)], DavetiyeAlinmaDurumu, Source.DavetiyeAlinmaDurumu);
                int codeDavetiyeTebligDurumu = CodeValue(_codeLists[nameof(DavetiyeTebligDurumu)], DavetiyeTebligDurumu, Source.DavetiyeTebligDurumu);
                int codeGorusmeDurumu = CodeValue(_codeLists[nameof(GorusmeDurumu)], GorusmeDurumu, Source.GorusmeDurumu);
                int codeTescilDurumu = Share == null ? 0 : CodeValue(_codeLists[nameof(TescilDurumu)], TescilDurumu, Share.TescilDurumu);
                Source.AnlasmaDurumu = codeAnlasmaDurumu;
                Source.DavetiyeAlinmaDurumu = codeDavetiyeAlinmaDurumu;
                Source.DavetiyeTebligDurumu = codeDavetiyeTebligDurumu;
                Source.GorusmeDurumu = codeGorusmeDurumu;
                Source.AnlasmaDusunceler = AnlasmaDusunceler;
                if (Share != null)
                {
                    Share.TescilDurumu = codeTescilDurumu;
                    Share.Aciklama = Dusunceler;
                }
            }

            [Description("Malike davetiye gönderilme durumunu gösterir."), Category("Davet"), DisplayName("Davetiye Tebliğ Durumu"), TypeConverter(typeof(CodeTextConverter))]
            public string DavetiyeTebligDurumu { get; set; }

            [Description("Malikin davetiye alma durumunu gösterir."), Category("Davet"), DisplayName("Davetiye Alınma Durumu"), TypeConverter(typeof(CodeTextConverter))]
            public string DavetiyeAlinmaDurumu { get; set; }

            [Description("Malik ile görüşme durumunu gösterir."), Category("Görüşme"), DisplayName("Görüşme Durumu"), TypeConverter(typeof(CodeTextConverter))]
            public string GorusmeDurumu { get; set; }

            [Description("Malik ile anlaşma durumunu gösterir."), Category("Anlaşma"), DisplayName("Anlaşma Durumu"), TypeConverter(typeof(CodeTextConverter))]
            public string AnlasmaDurumu { get; set; }

            [Description("Malik ile anlaşma hakkında düşünceler."), Category("Anlaşma"), DisplayName("Anlaşma Düşünceler")]
            public string AnlasmaDusunceler { get; set; }

            [Description("Malikin hissesinin tescil durumunu gösterir."), Category("Kamulaştırma"), DisplayName("Tescil Durumu"), TypeConverter(typeof(CodeTextConverter))]
            public string TescilDurumu { get; set; }

            [Description("Malikin hissesi ile ilgili düşünceleri gösterir."), Category("Kamulaştırma"), DisplayName("Düşünceler")]
            public string Dusunceler { get; set; }
        }

        private static string CodeText(Dictionary<int, string> values, int code)
        {
            return values != null && values.TryGetValue(code, out string text) ? text : $"Hatalı Kodlama ({code})";
        }

        private static int CodeValue(Dictionary<int, string> values, string text, int currentCode)
        {
            // Eski verideki tanınmayan bir kod değiştirilmediyse koru. Böylece açıklama gibi
            // başka bir alanı kaydetmek için önce bütün eski kodları düzeltmek gerekmez.
            if (string.Equals(CodeText(values, currentCode), text, StringComparison.Ordinal))
                return currentCode;

            if (values != null)
            {
                foreach (KeyValuePair<int, string> item in values)
                {
                    if (string.Equals(item.Value, text, StringComparison.CurrentCulture))
                        return item.Key;
                }
            }

            throw new InvalidOperationException($"'{text}' geçerli bir kod-listesi değeri değil.");
        }

        public sealed class CodeTextConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                string propertyName = context?.PropertyDescriptor?.Name;
                IEnumerable<string> values = Enumerable.Empty<string>();
                if (context?.Instance is GridParselKodModel parcel)
                    values = parcel.GetStandardValues(propertyName);
                else if (context?.Instance is GridMalikKodModel owner)
                    values = owner.GetStandardValues(propertyName);

                return new StandardValuesCollection(values.ToArray());
            }
        }

        public sealed class TurkishBooleanConverter : BooleanConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string) && value is bool booleanValue)
                    return booleanValue ? "Evet" : "Hayır";

                return base.ConvertTo(context, culture, value, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string text)
                {
                    if (string.Equals(text, "Evet", StringComparison.CurrentCultureIgnoreCase)) return true;
                    if (string.Equals(text, "Hayır", StringComparison.CurrentCultureIgnoreCase)) return false;
                }

                return base.ConvertFrom(context, culture, value);
            }
        }
    }
}
