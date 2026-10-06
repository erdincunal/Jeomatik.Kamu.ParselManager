using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Manager;

internal static class RegressionTests
{
    private static void Check(bool condition, string message)
    {
        if (!condition) throw new Exception(message);
        Console.WriteLine("PASS: " + message);
    }

    [STAThread]
    private static void Main()
    {
        using (var control = new Dashboard.Manager())
        {
            var list = control.ListViewKamu;
            list.Items.Clear();
            list.Items.Add("parcel");
            control.Clear(ListViewType.Kisi);
            Check(list.Items.Count == 1, "Person clear preserves parcel lists");
            for (int i = 0; i < 20; i++)
            {
                list.Groups.Add(new ListViewGroup("old"));
                control.Clear(ListViewType.Kamu);
                Check(list.Items.Count == 0 && list.Groups.Count == 0, "Refresh clears rows and groups");
            }
        }

        using (var list = new ListView())
        using (var bitmap = new Bitmap(100, 30))
        using (var graphics = Graphics.FromImage(bitmap))
        {
            int drawCount = 0;
            list.Columns.Add("Header");
            list.DrawColumnHeader += (sender, args) => drawCount++;
            var localList = list;
            for (int i = 0; i < 20; i++)
                Dashboard.Manager.ColorListViewHeader(ref localList, Color.Red, Color.White);
            var draw = typeof(ListView).GetMethod("OnDrawColumnHeader", BindingFlags.Instance | BindingFlags.NonPublic);
            draw.Invoke(list, new object[] { new DrawListViewColumnHeaderEventArgs(graphics,
                new Rectangle(0, 0, 100, 30), 0, list.Columns[0], ListViewItemStates.Default,
                Color.White, Color.Red, list.Font) });
            Check(drawCount == 1, "Existing header handlers still run once");
            var styles = typeof(Dashboard.Manager).GetField("HeaderStyles", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
            var get = styles.GetType().GetMethod("TryGetValue");
            object[] parameters = { list, null };
            Check((bool)get.Invoke(styles, parameters), "Header style persists across refreshes");
        }

        var codeValue = typeof(Dashboard.Manager).GetMethod("CodeValue", BindingFlags.Static | BindingFlags.NonPublic);
        var codes = new Dictionary<int, string> { { 1, "Known" } };
        Check((int)codeValue.Invoke(null, new object[] { codes, "Hatalı Kodlama (99)", 99 }) == 99,
            "Unknown unchanged codes are preserved");
        Check((int)codeValue.Invoke(null, new object[] { codes, "Known", 99 }) == 1,
            "Unknown codes can be corrected");
        bool rejected = false;
        try { codeValue.Invoke(null, new object[] { codes, "Invalid", 1 }); }
        catch (TargetInvocationException ex) { rejected = ex.InnerException is InvalidOperationException; }
        Check(rejected, "Invalid changed codes are rejected");
        var parcel = new Kamu.Parsel { KadastralDurum = 1, MalikTipi = 1 };
        codes.Add(2, "Other");
        var gridType = typeof(Dashboard.Manager).GetNestedType("GridParselKodModel", BindingFlags.NonPublic);
        var grid = Activator.CreateInstance(gridType, new object[] { parcel, codes, codes, codes, codes, codes });
        gridType.GetProperty("KadastralDurum").SetValue(grid, "Other", null);
        gridType.GetProperty("MalikTipi").SetValue(grid, "Invalid", null);
        rejected = false;
        try { gridType.GetMethod("Apply", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(grid, null); }
        catch (TargetInvocationException ex) { rejected = ex.InnerException is InvalidOperationException; }
        Check(rejected && parcel.KadastralDurum == 1, "Validation failure does not partially modify parcel");
        var converter = new Dashboard.Manager.CodeTextConverter();
        Check(converter.GetStandardValues(null).Count == 0, "Converter tolerates missing context");
    }
}
