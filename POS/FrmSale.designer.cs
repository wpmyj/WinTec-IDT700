namespace POS
{
    partial class FrmSale
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSale));
            this.label91 = new System.Windows.Forms.Label();
            this.mSaleFlowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgSaleFlow = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lvGoods = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.lblInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mSaleFlowBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button4.TabIndex = 3;
            this.button4.Text = "退货";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button1.Text = "数量";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button3.TabIndex = 2;
            this.button3.Text = "总清";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button2.TabIndex = 1;
            this.button2.Text = "删除";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 451);
            this.panel1.Size = new System.Drawing.Size(800, 29);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblInfo);
            this.panel2.Size = new System.Drawing.Size(800, 28);
            this.panel2.Controls.SetChildIndex(this.lblItem, 0);
            this.panel2.Controls.SetChildIndex(this.lblInfo, 0);
            this.panel2.Controls.SetChildIndex(this.label1, 0);
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
            this.lblItem.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lblItem.Location = new System.Drawing.Point(377, 1);
            this.lblItem.Size = new System.Drawing.Size(132, 24);
            this.lblItem.Text = "";
            // 
            // label91
            // 
            this.label91.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label91.Location = new System.Drawing.Point(450, 450);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(73, 80);
            this.label91.Text = "合计：";
            // 
            // mSaleFlowBindingSource
            // 
            this.mSaleFlowBindingSource.AllowNew = false;
            this.mSaleFlowBindingSource.DataSource = typeof(Model.MSaleFlow);
            this.mSaleFlowBindingSource.Sort = "";
            // 
            // dgSaleFlow
            // 
            this.dgSaleFlow.BackColor = System.Drawing.Color.Silver;
            this.dgSaleFlow.BackgroundColor = System.Drawing.Color.Silver;
            this.dgSaleFlow.DataSource = this.mSaleFlowBindingSource;
            this.dgSaleFlow.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.dgSaleFlow.ForeColor = System.Drawing.Color.DimGray;
            this.dgSaleFlow.Location = new System.Drawing.Point(3, 278);
            this.dgSaleFlow.Name = "dgSaleFlow";
            this.dgSaleFlow.PreferredRowHeight = 23;
            this.dgSaleFlow.RowHeadersVisible = false;
            this.dgSaleFlow.SelectionBackColor = System.Drawing.Color.White;
            this.dgSaleFlow.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            this.dgSaleFlow.Size = new System.Drawing.Size(793, 167);
            this.dgSaleFlow.TabIndex = 16;
            this.dgSaleFlow.TableStyles.Add(this.dataGridTableStyle1);
            this.dgSaleFlow.CurrentCellChanged += new System.EventHandler(this.dgSaleFlow_CurrentCellChanged);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn7);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn6);
            this.dataGridTableStyle1.MappingName = "MSaleFlow";
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "序号";
            this.dataGridTextBoxColumn2.MappingName = "RowNo";
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "编码";
            this.dataGridTextBoxColumn7.MappingName = "Incode";
            this.dataGridTextBoxColumn7.NullText = "0";
            this.dataGridTextBoxColumn7.Width = 100;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "品名";
            this.dataGridTextBoxColumn3.MappingName = "Fname";
            this.dataGridTextBoxColumn3.Width = 250;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "F2";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "单价";
            this.dataGridTextBoxColumn4.MappingName = "Price";
            this.dataGridTextBoxColumn4.NullText = "0";
            this.dataGridTextBoxColumn4.Width = 100;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "数量";
            this.dataGridTextBoxColumn5.MappingName = "Qty";
            this.dataGridTextBoxColumn5.NullText = "0";
            this.dataGridTextBoxColumn5.Width = 100;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "F2";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "金额";
            this.dataGridTextBoxColumn6.MappingName = "Total";
            this.dataGridTextBoxColumn6.NullText = "0";
            this.dataGridTextBoxColumn6.Width = 100;
            // 
            // lvGoods
            // 
            this.lvGoods.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.lvGoods.BackColor = System.Drawing.Color.White;
            this.lvGoods.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lvGoods.ForeColor = System.Drawing.Color.Blue;
            this.lvGoods.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.BackColor = System.Drawing.Color.White;
            listViewItem1.ForeColor = System.Drawing.Color.Blue;
            listViewItem1.ImageIndex = 6;
            listViewItem1.Text = "十全大补汤";
            this.lvGoods.Items.Add(listViewItem1);
            this.lvGoods.LargeImageList = this.imageList1;
            this.lvGoods.Location = new System.Drawing.Point(3, 28);
            this.lvGoods.Name = "lvGoods";
            this.lvGoods.Size = new System.Drawing.Size(793, 244);
            this.lvGoods.SmallImageList = this.imageList1;
            this.lvGoods.TabIndex = 20;
            this.lvGoods.ItemActivate += new System.EventHandler(this.lvGoods_ItemActivate);
            // 
            // imageList1
            // 
            this.imageList1.ImageSize = new System.Drawing.Size(80, 80);
            this.imageList1.Images.Clear();
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource6"))));
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(413, 4);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(209, 20);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(233, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 24);
            this.label1.Text = "[菜品销售]";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "编码";
            this.dataGridTextBoxColumn1.MappingName = "Code";
            this.dataGridTextBoxColumn1.Width = 100;
            // 
            // FrmSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.dgSaleFlow);
            this.Controls.Add(this.lvGoods);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSale";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SaleForm_KeyDown);
            this.Controls.SetChildIndex(this.lvGoods, 0);
            this.Controls.SetChildIndex(this.dgSaleFlow, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mSaleFlowBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.DataGrid dgSaleFlow;
        private System.Windows.Forms.ListView lvGoods;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn7;
        private System.Windows.Forms.BindingSource mSaleFlowBindingSource;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        

    }
}
