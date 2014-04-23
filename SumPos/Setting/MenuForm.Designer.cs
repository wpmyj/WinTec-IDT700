namespace SumPos.Setting
{
    partial class MenuForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.listViewMenu = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button4.Text = "返回";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button1.Text = "确定";
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.button5.Visible = false;
            // 
            // lblItem
            // 
            this.lblItem.BackColor = System.Drawing.Color.Gainsboro;
            this.lblItem.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Italic);
            this.lblItem.Text = "[系统设置]";
            // 
            // listViewMenu
            // 
            this.listViewMenu.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewMenu.BackColor = System.Drawing.Color.DodgerBlue;
            this.listViewMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMenu.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            listViewItem1.BackColor = System.Drawing.Color.DodgerBlue;
            listViewItem1.ImageIndex = 2;
            listViewItem1.Tag = "1";
            listViewItem1.Text = "1系统参数";
            listViewItem2.BackColor = System.Drawing.Color.DodgerBlue;
            listViewItem2.ImageIndex = 4;
            listViewItem2.Tag = "0";
            listViewItem2.Text = "０设备注销";
            this.listViewMenu.Items.Add(listViewItem1);
            this.listViewMenu.Items.Add(listViewItem2);
            this.listViewMenu.LargeImageList = this.imageList1;
            this.listViewMenu.Location = new System.Drawing.Point(0, 28);
            this.listViewMenu.Name = "listViewMenu";
            this.listViewMenu.Size = new System.Drawing.Size(800, 423);
            this.listViewMenu.TabIndex = 2;
            this.listViewMenu.ItemActivate += new System.EventHandler(this.listViewMenu_ItemActivate);
            // 
            // imageList1
            // 
            this.imageList1.ImageSize = new System.Drawing.Size(100, 100);
            this.imageList1.Images.Clear();
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.listViewMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuForm";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.Closed += new System.EventHandler(this.MenuForm_Closed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MenuForm_KeyDown);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.listViewMenu, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewMenu;
        private System.Windows.Forms.ImageList imageList1;
    }
}
