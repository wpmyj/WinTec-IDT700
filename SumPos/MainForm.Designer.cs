namespace SumPos
{
    partial class MainForm
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.listViewMenu = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button4.Text = "ע��";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button1.Text = "ȷ��";
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
            this.lblItem.Text = "[���˵�]";
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
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource6"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource7"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource8"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource9"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource10"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource11"))));
            // 
            // listViewMenu
            // 
            this.listViewMenu.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewMenu.BackColor = System.Drawing.Color.DodgerBlue;
            this.listViewMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMenu.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.listViewMenu.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.BackColor = System.Drawing.Color.DodgerBlue;
            listViewItem1.ImageIndex = 0;
            listViewItem1.Tag = "1";
            listViewItem1.Text = "������ˢ��";
            listViewItem2.BackColor = System.Drawing.Color.DodgerBlue;
            listViewItem2.ImageIndex = 1;
            listViewItem2.Tag = "2";
            listViewItem2.Text = "��ˢ��ͳ��";
            listViewItem3.ImageIndex = 2;
            listViewItem3.Tag = "3";
            listViewItem3.Text = "����ֵͳ��";
            this.listViewMenu.Items.Add(listViewItem1);
            this.listViewMenu.Items.Add(listViewItem2);
            this.listViewMenu.Items.Add(listViewItem3);
            this.listViewMenu.LargeImageList = this.imageList1;
            this.listViewMenu.Location = new System.Drawing.Point(0, 28);
            this.listViewMenu.Name = "listViewMenu";
            this.listViewMenu.Size = new System.Drawing.Size(800, 423);
            this.listViewMenu.TabIndex = 4;
            this.listViewMenu.ItemActivate += new System.EventHandler(this.listViewMenu_ItemActivate);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.listViewMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
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
