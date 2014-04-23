namespace SumPos.Business
{
    partial class SaleForm
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
            this.label91 = new System.Windows.Forms.Label();
            this.payFlowDataGrid = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cardNoTxtBx = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.payJeTxtBx = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.NameTxtBx = new System.Windows.Forms.Label();
            this.totalTxtBox = new System.Windows.Forms.Label();
            this.payQtyHjTxtBx = new System.Windows.Forms.Label();
            this.payHjTxtBx = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button4.Location = new System.Drawing.Point(560, 2);
            this.button4.Size = new System.Drawing.Size(100, 38);
            this.button4.TabIndex = 3;
            this.button4.Text = "退款";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button1.Location = new System.Drawing.Point(170, 2);
            this.button1.Size = new System.Drawing.Size(100, 38);
            this.button1.Text = "上翻";
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button3.Location = new System.Drawing.Point(430, 2);
            this.button3.Size = new System.Drawing.Size(100, 38);
            this.button3.TabIndex = 2;
            this.button3.Text = "充值";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button2.Location = new System.Drawing.Point(300, 2);
            this.button2.Size = new System.Drawing.Size(100, 38);
            this.button2.TabIndex = 1;
            this.button2.Text = "下翻";
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 440);
            this.panel1.Size = new System.Drawing.Size(800, 40);
            // 
            // panel2
            // 
            this.panel2.Size = new System.Drawing.Size(800, 28);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.button5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button5.Location = new System.Drawing.Point(690, 2);
            this.button5.Size = new System.Drawing.Size(100, 38);
            this.button5.Text = "取消";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lblItem
            // 
            this.lblItem.BackColor = System.Drawing.Color.Gainsboro;
            this.lblItem.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Italic);
            this.lblItem.Text = "[销售]";
            // 
            // label91
            // 
            this.label91.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.label91.Location = new System.Drawing.Point(450, 450);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(73, 80);
            this.label91.Text = "合计：";
            // 
            // payFlowDataGrid
            // 
            this.payFlowDataGrid.BackColor = System.Drawing.Color.Black;
            this.payFlowDataGrid.BackgroundColor = System.Drawing.Color.Black;
            this.payFlowDataGrid.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular);
            this.payFlowDataGrid.ForeColor = System.Drawing.Color.White;
            this.payFlowDataGrid.Location = new System.Drawing.Point(3, 3);
            this.payFlowDataGrid.Name = "payFlowDataGrid";
            this.payFlowDataGrid.RowHeadersVisible = false;
            this.payFlowDataGrid.SelectionBackColor = System.Drawing.Color.White;
            this.payFlowDataGrid.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            this.payFlowDataGrid.Size = new System.Drawing.Size(368, 315);
            this.payFlowDataGrid.TabIndex = 16;
            this.payFlowDataGrid.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn6);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn7);
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "流水号";
            this.dataGridTextBoxColumn2.MappingName = "Flow_no";
            this.dataGridTextBoxColumn2.Width = 150;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "卡号";
            this.dataGridTextBoxColumn6.MappingName = "Hykh";
            this.dataGridTextBoxColumn6.Width = 100;
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Format = "F2";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "金额";
            this.dataGridTextBoxColumn7.MappingName = "Total";
            this.dataGridTextBoxColumn7.Width = 110;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(0, 392);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 4);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(182, 410);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 21);
            this.label5.Text = "结算总额：";
            // 
            // panel9
            // 
            this.panel9.Location = new System.Drawing.Point(180, 396);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(2, 64);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(4, 410);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 21);
            this.label4.Text = "结算笔数：";
            // 
            // panel11
            // 
            this.panel11.Location = new System.Drawing.Point(388, 29);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(0, 0);
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "名称 ";
            this.dataGridTextBoxColumn1.MappingName = "Spellcode";
            this.dataGridTextBoxColumn1.Width = 175;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "金额";
            this.dataGridTextBoxColumn3.MappingName = "Amount";
            this.dataGridTextBoxColumn3.Width = 80;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "数量";
            this.dataGridTextBoxColumn4.MappingName = "Quantity";
            this.dataGridTextBoxColumn4.Width = 80;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "金额";
            this.dataGridTextBoxColumn5.MappingName = "Amount";
            this.dataGridTextBoxColumn5.Width = 80;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 23);
            this.label1.Text = "结前金额：";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(4, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 21);
            this.label6.Text = "刷卡记录：";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(430, 334);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 30);
            this.label3.Text = "消费金额：";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(430, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 30);
            this.label10.Text = "姓名：";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(430, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 30);
            this.label7.Text = "卡号：";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(430, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 30);
            this.label8.Text = "余额：";
            // 
            // cardNoTxtBx
            // 
            this.cardNoTxtBx.BackColor = System.Drawing.Color.Black;
            this.cardNoTxtBx.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Regular);
            this.cardNoTxtBx.ForeColor = System.Drawing.Color.White;
            this.cardNoTxtBx.Location = new System.Drawing.Point(577, 62);
            this.cardNoTxtBx.Name = "cardNoTxtBx";
            this.cardNoTxtBx.Size = new System.Drawing.Size(188, 52);
            this.cardNoTxtBx.TabIndex = 70;
            this.cardNoTxtBx.Text = "123456";
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(577, 105);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(188, 2);
            // 
            // panel14
            // 
            this.panel14.Location = new System.Drawing.Point(577, 362);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(188, 2);
            // 
            // payJeTxtBx
            // 
            this.payJeTxtBx.BackColor = System.Drawing.Color.Black;
            this.payJeTxtBx.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Regular);
            this.payJeTxtBx.ForeColor = System.Drawing.Color.White;
            this.payJeTxtBx.Location = new System.Drawing.Point(577, 321);
            this.payJeTxtBx.Name = "payJeTxtBx";
            this.payJeTxtBx.Size = new System.Drawing.Size(188, 52);
            this.payJeTxtBx.TabIndex = 71;
            this.payJeTxtBx.Text = "0";
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(577, 191);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(188, 2);
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(577, 278);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(188, 2);
            // 
            // panel7
            // 
            this.panel7.Location = new System.Drawing.Point(408, 31);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(2, 360);
            // 
            // panel8
            // 
            this.panel8.AutoScroll = true;
            this.panel8.Controls.Add(this.payFlowDataGrid);
            this.panel8.Location = new System.Drawing.Point(14, 65);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(374, 321);
            // 
            // NameTxtBx
            // 
            this.NameTxtBx.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Regular);
            this.NameTxtBx.ForeColor = System.Drawing.Color.White;
            this.NameTxtBx.Location = new System.Drawing.Point(577, 152);
            this.NameTxtBx.Name = "NameTxtBx";
            this.NameTxtBx.Size = new System.Drawing.Size(188, 52);
            this.NameTxtBx.Text = "张三";
            // 
            // totalTxtBox
            // 
            this.totalTxtBox.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Regular);
            this.totalTxtBox.ForeColor = System.Drawing.Color.White;
            this.totalTxtBox.Location = new System.Drawing.Point(577, 237);
            this.totalTxtBox.Name = "totalTxtBox";
            this.totalTxtBox.Size = new System.Drawing.Size(188, 52);
            this.totalTxtBox.Text = "0.00";
            // 
            // payQtyHjTxtBx
            // 
            this.payQtyHjTxtBx.Font = new System.Drawing.Font("Arial", 25F, System.Drawing.FontStyle.Regular);
            this.payQtyHjTxtBx.ForeColor = System.Drawing.Color.White;
            this.payQtyHjTxtBx.Location = new System.Drawing.Point(106, 396);
            this.payQtyHjTxtBx.Name = "payQtyHjTxtBx";
            this.payQtyHjTxtBx.Size = new System.Drawing.Size(73, 40);
            this.payQtyHjTxtBx.Text = "0";
            // 
            // payHjTxtBx
            // 
            this.payHjTxtBx.Font = new System.Drawing.Font("Arial", 25F, System.Drawing.FontStyle.Regular);
            this.payHjTxtBx.ForeColor = System.Drawing.Color.White;
            this.payHjTxtBx.Location = new System.Drawing.Point(268, 396);
            this.payHjTxtBx.Name = "payHjTxtBx";
            this.payHjTxtBx.Size = new System.Drawing.Size(142, 40);
            this.payHjTxtBx.Text = "0.00";
            // 
            // SaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.payHjTxtBx);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel14);
            this.Controls.Add(this.payJeTxtBx);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cardNoTxtBx);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.NameTxtBx);
            this.Controls.Add(this.totalTxtBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.payQtyHjTxtBx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SaleForm";
            this.Load += new System.EventHandler(this.SaleForm_Load);
            this.Closed += new System.EventHandler(this.SaleForm_Closed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SaleForm_KeyDown);
            this.Controls.SetChildIndex(this.payQtyHjTxtBx, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.totalTxtBox, 0);
            this.Controls.SetChildIndex(this.NameTxtBx, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.panel11, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.panel9, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.cardNoTxtBx, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.payJeTxtBx, 0);
            this.Controls.SetChildIndex(this.panel14, 0);
            this.Controls.SetChildIndex(this.panel6, 0);
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this.panel4, 0);
            this.Controls.SetChildIndex(this.panel7, 0);
            this.Controls.SetChildIndex(this.panel8, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.payHjTxtBx, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.DataGrid payFlowDataGrid;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox cardNoTxtBx;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.TextBox payJeTxtBx;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label NameTxtBx;
        private System.Windows.Forms.Label totalTxtBox;
        private System.Windows.Forms.Label payQtyHjTxtBx;
        private System.Windows.Forms.Label payHjTxtBx;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn7;
    }
}
