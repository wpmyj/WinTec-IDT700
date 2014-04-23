namespace POS
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.listViewMenu = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPracticeMode = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button4.Visible = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button1.Visible = false;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button3.Visible = false;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 451);
            this.panel1.Size = new System.Drawing.Size(800, 29);
            // 
            // panel2
            // 
            this.panel2.Size = new System.Drawing.Size(800, 28);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button5.Text = "退出";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lblItem
            // 
            this.lblItem.BackColor = System.Drawing.Color.Gainsboro;
            this.lblItem.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Italic);
            // 
            // imageList1
            // 
            this.imageList1.ImageSize = new System.Drawing.Size(113, 100);
            this.imageList1.Images.Clear();
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource6"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource7"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource8"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource9"))));
            // 
            // listViewMenu
            // 
            this.listViewMenu.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewMenu.BackColor = System.Drawing.Color.DodgerBlue;
            this.listViewMenu.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.listViewMenu.ForeColor = System.Drawing.Color.White;
            this.listViewMenu.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.BackColor = System.Drawing.Color.DodgerBlue;
            listViewItem1.ForeColor = System.Drawing.Color.White;
            listViewItem1.ImageIndex = 0;
            listViewItem1.Tag = "1";
            listViewItem1.Text = "１菜品销售";
            listViewItem2.BackColor = System.Drawing.Color.DodgerBlue;
            listViewItem2.ForeColor = System.Drawing.Color.White;
            listViewItem2.ImageIndex = 4;
            listViewItem2.Tag = "2";
            listViewItem2.Text = "２报表查询";
            listViewItem3.BackColor = System.Drawing.Color.DodgerBlue;
            listViewItem3.ForeColor = System.Drawing.Color.White;
            listViewItem3.ImageIndex = 8;
            listViewItem3.Tag = "3";
            listViewItem3.Text = "３更新";
            this.listViewMenu.Items.Add(listViewItem1);
            this.listViewMenu.Items.Add(listViewItem2);
            this.listViewMenu.Items.Add(listViewItem3);
            this.listViewMenu.LargeImageList = this.imageList1;
            this.listViewMenu.Location = new System.Drawing.Point(-1, 27);
            this.listViewMenu.Name = "listViewMenu";
            this.listViewMenu.Size = new System.Drawing.Size(800, 450);
            this.listViewMenu.TabIndex = 4;
            this.listViewMenu.ItemActivate += new System.EventHandler(this.listViewMenu_ItemActivate);
            this.listViewMenu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.listViewMenu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(228, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 24);
            this.label1.Text = "[主菜单]";
            // 
            // lblPracticeMode
            // 
            this.lblPracticeMode.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lblPracticeMode.ForeColor = System.Drawing.Color.Red;
            this.lblPracticeMode.Location = new System.Drawing.Point(334, 2);
            this.lblPracticeMode.Name = "lblPracticeMode";
            this.lblPracticeMode.Size = new System.Drawing.Size(132, 24);
            this.lblPracticeMode.Text = "[练习模式]";
            this.lblPracticeMode.Visible = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.listViewMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMain";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmMain_Closing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.Controls.SetChildIndex(this.listViewMenu, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listViewMenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPracticeMode;

    }
}
