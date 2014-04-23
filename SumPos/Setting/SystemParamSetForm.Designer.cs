namespace SumPos.Setting
{
    partial class SystemParamSetForm
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
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ServerAddTxt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.PosNoCBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CompanyNameTxt = new System.Windows.Forms.TextBox();
            this.CustomerNameTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CzkTypeCBox = new System.Windows.Forms.ComboBox();
            this.printBillCkbox = new System.Windows.Forms.CheckBox();
            this.readCardModeChkBx = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ManTxtBx = new System.Windows.Forms.TextBox();
            this.str1TxBx = new System.Windows.Forms.TextBox();
            this.str2TxBx = new System.Windows.Forms.TextBox();
            this.str3TxBx = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
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
            this.button1.Text = "保存";
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
            this.lblItem.Text = "[系统参数设置]";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(14, 412);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(177, 20);
            this.label7.Text = "历史数据保留时间";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label7.Visible = false;
            // 
            // comboBox5
            // 
            this.comboBox5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.comboBox5.Items.Add("30");
            this.comboBox5.Items.Add("60");
            this.comboBox5.Items.Add("90");
            this.comboBox5.Items.Add("120");
            this.comboBox5.Location = new System.Drawing.Point(216, 406);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(100, 26);
            this.comboBox5.TabIndex = 17;
            this.comboBox5.Visible = false;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label8.Location = new System.Drawing.Point(-63, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(177, 20);
            this.label8.Text = "服务器地址";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ServerAddTxt
            // 
            this.ServerAddTxt.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.ServerAddTxt.Location = new System.Drawing.Point(141, 132);
            this.ServerAddTxt.Name = "ServerAddTxt";
            this.ServerAddTxt.Size = new System.Drawing.Size(492, 26);
            this.ServerAddTxt.TabIndex = 23;
            this.ServerAddTxt.Text = "192.168.0.99";
            this.ServerAddTxt.GotFocus += new System.EventHandler(this.textBox1_GotFocus);
            this.ServerAddTxt.LostFocus += new System.EventHandler(this.textBox1_LostFocus);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label10.Location = new System.Drawing.Point(335, 412);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 20);
            this.label10.Text = "天";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label10.Visible = false;
            // 
            // PosNoCBox
            // 
            this.PosNoCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.PosNoCBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.PosNoCBox.Items.Add("0001");
            this.PosNoCBox.Items.Add("0002");
            this.PosNoCBox.Items.Add("0003");
            this.PosNoCBox.Items.Add("0004");
            this.PosNoCBox.Items.Add("0005");
            this.PosNoCBox.Items.Add("0006");
            this.PosNoCBox.Items.Add("0007");
            this.PosNoCBox.Items.Add("0008");
            this.PosNoCBox.Items.Add("0009");
            this.PosNoCBox.Items.Add("0010");
            this.PosNoCBox.Location = new System.Drawing.Point(141, 47);
            this.PosNoCBox.Name = "PosNoCBox";
            this.PosNoCBox.Size = new System.Drawing.Size(100, 26);
            this.PosNoCBox.TabIndex = 20;
            this.PosNoCBox.Text = "0001";
            this.PosNoCBox.LostFocus += new System.EventHandler(this.PosNoCBox_LostFocus);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label11.Location = new System.Drawing.Point(-63, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(177, 20);
            this.label11.Text = "终端号";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(-63, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 20);
            this.label3.Text = "单位名称";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(251, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 20);
            this.label4.Text = "商户名称";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CompanyNameTxt
            // 
            this.CompanyNameTxt.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.CompanyNameTxt.Location = new System.Drawing.Point(141, 84);
            this.CompanyNameTxt.Name = "CompanyNameTxt";
            this.CompanyNameTxt.Size = new System.Drawing.Size(179, 26);
            this.CompanyNameTxt.TabIndex = 21;
            this.CompanyNameTxt.Text = "美食广场";
            this.CompanyNameTxt.GotFocus += new System.EventHandler(this.textBox1_GotFocus);
            this.CompanyNameTxt.LostFocus += new System.EventHandler(this.textBox1_LostFocus);
            // 
            // CustomerNameTxt
            // 
            this.CustomerNameTxt.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.CustomerNameTxt.Location = new System.Drawing.Point(454, 84);
            this.CustomerNameTxt.Name = "CustomerNameTxt";
            this.CustomerNameTxt.Size = new System.Drawing.Size(179, 26);
            this.CustomerNameTxt.TabIndex = 22;
            this.CustomerNameTxt.Text = "档口";
            this.CustomerNameTxt.GotFocus += new System.EventHandler(this.textBox1_GotFocus);
            this.CustomerNameTxt.LostFocus += new System.EventHandler(this.textBox1_LostFocus);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(24, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 20);
            this.label5.Text = "卡类型";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CzkTypeCBox
            // 
            this.CzkTypeCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.CzkTypeCBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.CzkTypeCBox.Items.Add("磁卡");
            this.CzkTypeCBox.Items.Add("射频卡");
            this.CzkTypeCBox.Location = new System.Drawing.Point(141, 219);
            this.CzkTypeCBox.Name = "CzkTypeCBox";
            this.CzkTypeCBox.Size = new System.Drawing.Size(100, 26);
            this.CzkTypeCBox.TabIndex = 25;
            // 
            // printBillCkbox
            // 
            this.printBillCkbox.Location = new System.Drawing.Point(141, 266);
            this.printBillCkbox.Name = "printBillCkbox";
            this.printBillCkbox.Size = new System.Drawing.Size(100, 20);
            this.printBillCkbox.TabIndex = 26;
            this.printBillCkbox.Text = "打印小票";
            // 
            // readCardModeChkBx
            // 
            this.readCardModeChkBx.Location = new System.Drawing.Point(141, 325);
            this.readCardModeChkBx.Name = "readCardModeChkBx";
            this.readCardModeChkBx.Size = new System.Drawing.Size(240, 20);
            this.readCardModeChkBx.TabIndex = 34;
            this.readCardModeChkBx.Text = "允许手动输入卡号";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(-63, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 20);
            this.label2.Text = "本机序号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ManTxtBx
            // 
            this.ManTxtBx.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.ManTxtBx.Location = new System.Drawing.Point(141, 176);
            this.ManTxtBx.Name = "ManTxtBx";
            this.ManTxtBx.ReadOnly = true;
            this.ManTxtBx.Size = new System.Drawing.Size(492, 26);
            this.ManTxtBx.TabIndex = 44;
            this.ManTxtBx.GotFocus += new System.EventHandler(this.ManTxtBx_GotFocus);
            this.ManTxtBx.LostFocus += new System.EventHandler(this.ManTxtBx_LostFocus);
            // 
            // str1TxBx
            // 
            this.str1TxBx.Location = new System.Drawing.Point(454, 263);
            this.str1TxBx.Name = "str1TxBx";
            this.str1TxBx.Size = new System.Drawing.Size(179, 23);
            this.str1TxBx.TabIndex = 53;
            this.str1TxBx.GotFocus += new System.EventHandler(this.str1TxBx_GotFocus);
            this.str1TxBx.LostFocus += new System.EventHandler(this.str1TxBx_LostFocus);
            // 
            // str2TxBx
            // 
            this.str2TxBx.Location = new System.Drawing.Point(454, 292);
            this.str2TxBx.Name = "str2TxBx";
            this.str2TxBx.Size = new System.Drawing.Size(179, 23);
            this.str2TxBx.TabIndex = 54;
            this.str2TxBx.GotFocus += new System.EventHandler(this.str2TxBx_GotFocus);
            this.str2TxBx.LostFocus += new System.EventHandler(this.str2TxBx_LostFocus);
            // 
            // str3TxBx
            // 
            this.str3TxBx.Location = new System.Drawing.Point(454, 321);
            this.str3TxBx.Name = "str3TxBx";
            this.str3TxBx.Size = new System.Drawing.Size(179, 23);
            this.str3TxBx.TabIndex = 55;
            this.str3TxBx.GotFocus += new System.EventHandler(this.str3TxBx_GotFocus);
            this.str3TxBx.LostFocus += new System.EventHandler(this.str3TxBx_LostFocus);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(478, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 20);
            this.label6.Text = "票尾";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SystemParamSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.str3TxBx);
            this.Controls.Add(this.str2TxBx);
            this.Controls.Add(this.str1TxBx);
            this.Controls.Add(this.ManTxtBx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.readCardModeChkBx);
            this.Controls.Add(this.printBillCkbox);
            this.Controls.Add(this.CzkTypeCBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CustomerNameTxt);
            this.Controls.Add(this.CompanyNameTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PosNoCBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ServerAddTxt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBox5);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SystemParamSetForm";
            this.Load += new System.EventHandler(this.SystemParamSetForm_Load);
            this.Closed += new System.EventHandler(this.SystemParamSetForm_Closed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SystemParamSetForm_KeyDown);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.comboBox5, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.ServerAddTxt, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.PosNoCBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.CompanyNameTxt, 0);
            this.Controls.SetChildIndex(this.CustomerNameTxt, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.CzkTypeCBox, 0);
            this.Controls.SetChildIndex(this.printBillCkbox, 0);
            this.Controls.SetChildIndex(this.readCardModeChkBx, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ManTxtBx, 0);
            this.Controls.SetChildIndex(this.str1TxBx, 0);
            this.Controls.SetChildIndex(this.str2TxBx, 0);
            this.Controls.SetChildIndex(this.str3TxBx, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ServerAddTxt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox PosNoCBox;
        private System.Windows.Forms.Label label11;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CompanyNameTxt;
        private System.Windows.Forms.TextBox CustomerNameTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CzkTypeCBox;
        private System.Windows.Forms.CheckBox printBillCkbox;
        private System.Windows.Forms.CheckBox readCardModeChkBx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ManTxtBx;
        private System.Windows.Forms.TextBox str1TxBx;
        private System.Windows.Forms.TextBox str2TxBx;
        private System.Windows.Forms.TextBox str3TxBx;
        private System.Windows.Forms.Label label6;
    }
}
