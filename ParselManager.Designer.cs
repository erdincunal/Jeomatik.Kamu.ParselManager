namespace Dashboard
{
    partial class Manager
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Bileşen Tasarımcısı üretimi kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Manager));
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("");
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listViewDava = new System.Windows.Forms.ListView();
            this.colDavaMahkeme = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDavaEsas = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDavaTarih = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colKararNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colKararTarihi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDavaBedel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.listViewKamu = new System.Windows.Forms.ListView();
            this.colKamuTuru = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colKamuAlan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colKamuBirim = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colKamuTutar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listViewMalik = new System.Windows.Forms.ListView();
            this.colMalikTC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMalikAdi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMalikBaba = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMalikHisse = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMalikTel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMalikAdres = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listViewMustemilat = new System.Windows.Forms.ListView();
            this.colMustemilatTanim = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMustemilatAdet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMustemilatBirim = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMustemilatTutar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMustemilatSahip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tabControlParsel = new System.Windows.Forms.TabControl();
            this.tabPageParsel = new System.Windows.Forms.TabPage();
            this.propertyGridParsel = new System.Windows.Forms.PropertyGrid();
            this.tabPageMalik = new System.Windows.Forms.TabPage();
            this.propertyGridMalik = new System.Windows.Forms.PropertyGrid();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabControlParsel.SuspendLayout();
            this.tabPageParsel.SuspendLayout();
            this.tabPageMalik.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listViewDava);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(618, 203);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Davalar";
            // 
            // listViewDava
            // 
            this.listViewDava.AllowColumnReorder = true;
            this.listViewDava.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDavaMahkeme,
            this.colDavaEsas,
            this.colDavaTarih,
            this.colKararNo,
            this.colKararTarihi,
            this.colDavaBedel});
            this.listViewDava.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDava.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listViewDava.FullRowSelect = true;
            this.listViewDava.HideSelection = false;
            this.listViewDava.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listViewDava.LargeImageList = this.imageList1;
            this.listViewDava.Location = new System.Drawing.Point(3, 19);
            this.listViewDava.MultiSelect = false;
            this.listViewDava.Name = "listViewDava";
            this.listViewDava.ShowItemToolTips = true;
            this.listViewDava.Size = new System.Drawing.Size(612, 181);
            this.listViewDava.SmallImageList = this.imageList1;
            this.listViewDava.TabIndex = 3;
            this.listViewDava.UseCompatibleStateImageBehavior = false;
            this.listViewDava.View = System.Windows.Forms.View.Details;
            this.listViewDava.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewDava_MouseDoubleClick);
            // 
            // colDavaMahkeme
            // 
            this.colDavaMahkeme.Text = "Mahkeme";
            this.colDavaMahkeme.Width = 220;
            // 
            // colDavaEsas
            // 
            this.colDavaEsas.Text = "Esas No";
            this.colDavaEsas.Width = 90;
            // 
            // colDavaTarih
            // 
            this.colDavaTarih.Text = "Dava Tarihi";
            this.colDavaTarih.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colDavaTarih.Width = 100;
            // 
            // colKararNo
            // 
            this.colKararNo.Text = "Karar No";
            this.colKararNo.Width = 90;
            // 
            // colKararTarihi
            // 
            this.colKararTarihi.Text = "Karar Tarihi";
            this.colKararTarihi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colKararTarihi.Width = 100;
            // 
            // colDavaBedel
            // 
            this.colDavaBedel.Text = "Mahkeme Bedeli";
            this.colDavaBedel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colDavaBedel.Width = 120;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "dead.png");
            this.imageList1.Images.SetKeyName(1, "man.png");
            this.imageList1.Images.SetKeyName(2, "woman.png");
            this.imageList1.Images.SetKeyName(3, "tree.png");
            this.imageList1.Images.SetKeyName(4, "wheat.png");
            this.imageList1.Images.SetKeyName(5, "dava27.png");
            this.imageList1.Images.SetKeyName(6, "dava10.png");
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.listViewKamu);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(618, 170);
            this.groupBox5.TabIndex = 31;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Alanlar";
            // 
            // listViewKamu
            // 
            this.listViewKamu.AllowColumnReorder = true;
            this.listViewKamu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colKamuTuru,
            this.colKamuAlan,
            this.colKamuBirim,
            this.colKamuTutar});
            this.listViewKamu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewKamu.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listViewKamu.FullRowSelect = true;
            this.listViewKamu.HideSelection = false;
            this.listViewKamu.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listViewKamu.LargeImageList = this.imageList1;
            this.listViewKamu.Location = new System.Drawing.Point(3, 19);
            this.listViewKamu.MultiSelect = false;
            this.listViewKamu.Name = "listViewKamu";
            this.listViewKamu.ShowItemToolTips = true;
            this.listViewKamu.Size = new System.Drawing.Size(612, 148);
            this.listViewKamu.SmallImageList = this.imageList1;
            this.listViewKamu.TabIndex = 3;
            this.listViewKamu.UseCompatibleStateImageBehavior = false;
            this.listViewKamu.View = System.Windows.Forms.View.Details;
            this.listViewKamu.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewKamu_MouseDoubleClick);
            // 
            // colKamuTuru
            // 
            this.colKamuTuru.Text = "Kamulaştırma Türü";
            this.colKamuTuru.Width = 160;
            // 
            // colKamuAlan
            // 
            this.colKamuAlan.Text = "Alan";
            this.colKamuAlan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colKamuAlan.Width = 120;
            // 
            // colKamuBirim
            // 
            this.colKamuBirim.Text = "Birim Değer";
            this.colKamuBirim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colKamuBirim.Width = 120;
            // 
            // colKamuTutar
            // 
            this.colKamuTutar.Text = "Tutar";
            this.colKamuTutar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colKamuTutar.Width = 180;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listViewMalik);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(708, 266);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Malikler";
            // 
            // listViewMalik
            // 
            this.listViewMalik.AllowColumnReorder = true;
            this.listViewMalik.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMalikTC,
            this.colMalikAdi,
            this.colMalikBaba,
            this.colMalikHisse,
            this.colMalikTel,
            this.colMalikAdres});
            this.listViewMalik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMalik.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listViewMalik.FullRowSelect = true;
            this.listViewMalik.HideSelection = false;
            this.listViewMalik.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3});
            this.listViewMalik.LargeImageList = this.imageList1;
            this.listViewMalik.Location = new System.Drawing.Point(3, 19);
            this.listViewMalik.MultiSelect = false;
            this.listViewMalik.Name = "listViewMalik";
            this.listViewMalik.ShowItemToolTips = true;
            this.listViewMalik.Size = new System.Drawing.Size(702, 244);
            this.listViewMalik.SmallImageList = this.imageList1;
            this.listViewMalik.TabIndex = 3;
            this.listViewMalik.UseCompatibleStateImageBehavior = false;
            this.listViewMalik.View = System.Windows.Forms.View.Details;
            this.listViewMalik.SelectedIndexChanged += new System.EventHandler(this.ListViewMalik_SelectedIndexChanged);
            this.listViewMalik.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewMalik_MouseDoubleClick);
            // 
            // colMalikTC
            // 
            this.colMalikTC.Text = "TC Kimlik No";
            this.colMalikTC.Width = 120;
            // 
            // colMalikAdi
            // 
            this.colMalikAdi.Text = "Malik";
            this.colMalikAdi.Width = 200;
            // 
            // colMalikBaba
            // 
            this.colMalikBaba.Text = "Baba Adı";
            this.colMalikBaba.Width = 100;
            // 
            // colMalikHisse
            // 
            this.colMalikHisse.Text = "Hisse";
            this.colMalikHisse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colMalikHisse.Width = 100;
            // 
            // colMalikTel
            // 
            this.colMalikTel.Text = "Telefon";
            this.colMalikTel.Width = 120;
            // 
            // colMalikAdres
            // 
            this.colMalikAdres.Text = "Adres";
            this.colMalikAdres.Width = 200;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listViewMustemilat);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(620, 377);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Müştemilat ve Mevsimlik Ürünler";
            // 
            // listViewMustemilat
            // 
            this.listViewMustemilat.AllowColumnReorder = true;
            this.listViewMustemilat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMustemilatTanim,
            this.colMustemilatAdet,
            this.colMustemilatBirim,
            this.colMustemilatTutar,
            this.colMustemilatSahip});
            this.listViewMustemilat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMustemilat.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listViewMustemilat.FullRowSelect = true;
            this.listViewMustemilat.HideSelection = false;
            this.listViewMustemilat.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4});
            this.listViewMustemilat.LargeImageList = this.imageList1;
            this.listViewMustemilat.Location = new System.Drawing.Point(3, 19);
            this.listViewMustemilat.MultiSelect = false;
            this.listViewMustemilat.Name = "listViewMustemilat";
            this.listViewMustemilat.ShowItemToolTips = true;
            this.listViewMustemilat.Size = new System.Drawing.Size(614, 355);
            this.listViewMustemilat.SmallImageList = this.imageList1;
            this.listViewMustemilat.TabIndex = 3;
            this.listViewMustemilat.UseCompatibleStateImageBehavior = false;
            this.listViewMustemilat.View = System.Windows.Forms.View.Details;
            this.listViewMustemilat.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewMusMev_MouseDoubleClick);
            // 
            // colMustemilatTanim
            // 
            this.colMustemilatTanim.Text = "Tanım";
            this.colMustemilatTanim.Width = 150;
            // 
            // colMustemilatAdet
            // 
            this.colMustemilatAdet.Text = "Miktar";
            this.colMustemilatAdet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colMustemilatAdet.Width = 70;
            // 
            // colMustemilatBirim
            // 
            this.colMustemilatBirim.Text = "Birim Fiyat";
            this.colMustemilatBirim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colMustemilatBirim.Width = 100;
            // 
            // colMustemilatTutar
            // 
            this.colMustemilatTutar.Text = "Tutar";
            this.colMustemilatTutar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colMustemilatTutar.Width = 120;
            // 
            // colMustemilatSahip
            // 
            this.colMustemilatSahip.Text = "Sahip";
            this.colMustemilatSahip.Width = 140;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1242, 647);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 4;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tabControlParsel);
            this.splitContainer4.Size = new System.Drawing.Size(1242, 266);
            this.splitContainer4.SplitterDistance = 708;
            this.splitContainer4.TabIndex = 0;
            // 
            // tabControlParsel
            // 
            this.tabControlParsel.Controls.Add(this.tabPageParsel);
            this.tabControlParsel.Controls.Add(this.tabPageMalik);
            this.tabControlParsel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlParsel.Location = new System.Drawing.Point(0, 0);
            this.tabControlParsel.Name = "tabControlParsel";
            this.tabControlParsel.SelectedIndex = 0;
            this.tabControlParsel.Size = new System.Drawing.Size(530, 266);
            this.tabControlParsel.TabIndex = 0;
            // 
            // tabPageParsel
            // 
            this.tabPageParsel.Controls.Add(this.propertyGridParsel);
            this.tabPageParsel.Location = new System.Drawing.Point(4, 27);
            this.tabPageParsel.Name = "tabPageParsel";
            this.tabPageParsel.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParsel.Size = new System.Drawing.Size(522, 235);
            this.tabPageParsel.TabIndex = 0;
            this.tabPageParsel.Text = "Parsel";
            this.tabPageParsel.UseVisualStyleBackColor = true;
            // 
            // propertyGridParsel
            // 
            this.propertyGridParsel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridParsel.HelpVisible = false;
            this.propertyGridParsel.Location = new System.Drawing.Point(3, 3);
            this.propertyGridParsel.Name = "propertyGridParsel";
            this.propertyGridParsel.Size = new System.Drawing.Size(516, 229);
            this.propertyGridParsel.TabIndex = 0;
            // 
            // tabPageMalik
            // 
            this.tabPageMalik.Controls.Add(this.propertyGridMalik);
            this.tabPageMalik.Location = new System.Drawing.Point(4, 27);
            this.tabPageMalik.Name = "tabPageMalik";
            this.tabPageMalik.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMalik.Size = new System.Drawing.Size(522, 235);
            this.tabPageMalik.TabIndex = 1;
            this.tabPageMalik.Text = "Malik";
            this.tabPageMalik.UseVisualStyleBackColor = true;
            // 
            // propertyGridMalik
            // 
            this.propertyGridMalik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridMalik.HelpVisible = false;
            this.propertyGridMalik.Location = new System.Drawing.Point(3, 3);
            this.propertyGridMalik.Name = "propertyGridMalik";
            this.propertyGridMalik.Size = new System.Drawing.Size(516, 229);
            this.propertyGridMalik.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer2.Size = new System.Drawing.Size(1242, 377);
            this.splitContainer2.SplitterDistance = 620;
            this.splitContainer2.TabIndex = 31;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer3.Panel2Collapsed = true;
            this.splitContainer3.Panel2MinSize = 5;
            this.splitContainer3.Size = new System.Drawing.Size(620, 377);
            this.splitContainer3.SplitterDistance = 25;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.groupBox5);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer5.Size = new System.Drawing.Size(618, 377);
            this.splitContainer5.SplitterDistance = 170;
            this.splitContainer5.TabIndex = 35;
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "Manager";
            this.Size = new System.Drawing.Size(1242, 647);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tabControlParsel.ResumeLayout(false);
            this.tabPageParsel.ResumeLayout(false);
            this.tabPageMalik.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.ListView listViewDava;
        internal System.Windows.Forms.ColumnHeader colDavaMahkeme;
        internal System.Windows.Forms.ColumnHeader colDavaEsas;
        internal System.Windows.Forms.ColumnHeader colDavaTarih;
        internal System.Windows.Forms.ColumnHeader colDavaBedel;
        internal System.Windows.Forms.GroupBox groupBox5;
        internal System.Windows.Forms.ListView listViewKamu;
        internal System.Windows.Forms.ColumnHeader colKamuTuru;
        internal System.Windows.Forms.ColumnHeader colKamuAlan;
        internal System.Windows.Forms.ColumnHeader colKamuBirim;
        internal System.Windows.Forms.ColumnHeader colKamuTutar;
        internal System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.ListView listViewMalik;
        internal System.Windows.Forms.ColumnHeader colMalikTC;
        internal System.Windows.Forms.ColumnHeader colMalikAdi;
        internal System.Windows.Forms.ColumnHeader colMalikBaba;
        internal System.Windows.Forms.ColumnHeader colMalikHisse;
        internal System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.ListView listViewMustemilat;
        internal System.Windows.Forms.ColumnHeader colMustemilatTanim;
        internal System.Windows.Forms.ColumnHeader colMustemilatAdet;
        internal System.Windows.Forms.ColumnHeader colMustemilatBirim;
        internal System.Windows.Forms.ColumnHeader colMustemilatTutar;
        internal System.Windows.Forms.ColumnHeader colMustemilatSahip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.TabControl tabControlParsel;
        private System.Windows.Forms.TabPage tabPageParsel;
        private System.Windows.Forms.TabPage tabPageMalik;
        private System.Windows.Forms.PropertyGrid propertyGridParsel;
        private System.Windows.Forms.PropertyGrid propertyGridMalik;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader colMalikTel;
        private System.Windows.Forms.ColumnHeader colMalikAdres;
        internal System.Windows.Forms.ColumnHeader colKararNo;
        internal System.Windows.Forms.ColumnHeader colKararTarihi;
    }
}
