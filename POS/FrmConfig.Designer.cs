namespace POS
{
    partial class FrmConfig
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
            this.lblShopno = new System.Windows.Forms.Label();
            this.tbPosNO = new System.Windows.Forms.TextBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbDept = new System.Windows.Forms.RadioButton();
            this.rbStype = new System.Windows.Forms.RadioButton();
            this.tbPrintCount = new System.Windows.Forms.TextBox();
            this.tbAPPVersion = new System.Windows.Forms.TextBox();
            this.tbDeptCode = new System.Windows.Forms.TextBox();
            this.tbStype = new System.Windows.Forms.TextBox();
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
            this.button1.Text = "保存";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button3.Text = "";
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.button2.Text = "更新";
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 451);
            this.panel1.Size = new System.Drawing.Size(800, 29);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label6);
            this.panel2.Size = new System.Drawing.Size(800, 28);
            this.panel2.Controls.SetChildIndex(this.lblItem, 0);
            this.panel2.Controls.SetChildIndex(this.label6, 0);
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
            this.lblItem.Location = new System.Drawing.Point(468, 4);
            this.lblItem.Size = new System.Drawing.Size(132, 24);
            this.lblItem.Text = "";
            // 
            // lblShopno
            // 
            this.lblShopno.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lblShopno.Location = new System.Drawing.Point(33, 51);
            this.lblShopno.Name = "lblShopno";
            this.lblShopno.Size = new System.Drawing.Size(113, 29);
            this.lblShopno.Text = "收款机号:";
            // 
            // tbPosNO
            // 
            this.tbPosNO.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.tbPosNO.Location = new System.Drawing.Point(174, 49);
            this.tbPosNO.Name = "tbPosNO";
            this.tbPosNO.Size = new System.Drawing.Size(176, 31);
            this.tbPosNO.TabIndex = 4;
            this.tbPosNO.GotFocus += new System.EventHandler(this.tbPosNO_GotFocus);
            // 
            // tbServer
            // 
            this.tbServer.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.tbServer.Location = new System.Drawing.Point(174, 130);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(176, 31);
            this.tbServer.TabIndex = 6;
            this.tbServer.Text = "192.168.88.98";
            // 
            // lblIP
            // 
            this.lblIP.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lblIP.Location = new System.Drawing.Point(33, 132);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(113, 29);
            this.lblIP.Text = "服务器:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(33, 294);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 29);
            this.label1.Text = "打印联数:";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(33, 375);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 29);
            this.label5.Text = "程序版本:";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(231, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 24);
            this.label6.Text = "[系统设置]";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(403, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 29);
            this.label2.Text = "端口号：";
            // 
            // tbPort
            // 
            this.tbPort.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.tbPort.Location = new System.Drawing.Point(500, 130);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(138, 31);
            this.tbPort.TabIndex = 95;
            this.tbPort.Text = "8080";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(33, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 29);
            this.label3.Text = "显示商品：";
            // 
            // rbDept
            // 
            this.rbDept.Checked = true;
            this.rbDept.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.rbDept.Location = new System.Drawing.Point(174, 215);
            this.rbDept.Name = "rbDept";
            this.rbDept.Size = new System.Drawing.Size(100, 20);
            this.rbDept.TabIndex = 98;
            this.rbDept.Text = "部门";
            // 
            // rbStype
            // 
            this.rbStype.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.rbStype.Location = new System.Drawing.Point(403, 215);
            this.rbStype.Name = "rbStype";
            this.rbStype.Size = new System.Drawing.Size(100, 20);
            this.rbStype.TabIndex = 99;
            this.rbStype.TabStop = false;
            this.rbStype.Text = "品类";
            // 
            // tbPrintCount
            // 
            this.tbPrintCount.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.tbPrintCount.Location = new System.Drawing.Point(174, 292);
            this.tbPrintCount.Name = "tbPrintCount";
            this.tbPrintCount.Size = new System.Drawing.Size(79, 31);
            this.tbPrintCount.TabIndex = 100;
            this.tbPrintCount.Text = "1";
            // 
            // tbAPPVersion
            // 
            this.tbAPPVersion.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.tbAPPVersion.Location = new System.Drawing.Point(174, 373);
            this.tbAPPVersion.Name = "tbAPPVersion";
            this.tbAPPVersion.ReadOnly = true;
            this.tbAPPVersion.Size = new System.Drawing.Size(206, 31);
            this.tbAPPVersion.TabIndex = 101;
            this.tbAPPVersion.Text = "1.0.0.0";
            // 
            // tbDeptCode
            // 
            this.tbDeptCode.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.tbDeptCode.Location = new System.Drawing.Point(250, 211);
            this.tbDeptCode.Name = "tbDeptCode";
            this.tbDeptCode.Size = new System.Drawing.Size(100, 31);
            this.tbDeptCode.TabIndex = 109;
            // 
            // tbStype
            // 
            this.tbStype.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.tbStype.Location = new System.Drawing.Point(500, 211);
            this.tbStype.Name = "tbStype";
            this.tbStype.Size = new System.Drawing.Size(138, 31);
            this.tbStype.TabIndex = 110;
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.tbStype);
            this.Controls.Add(this.tbDeptCode);
            this.Controls.Add(this.tbAPPVersion);
            this.Controls.Add(this.tbPrintCount);
            this.Controls.Add(this.rbStype);
            this.Controls.Add(this.rbDept);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblShopno);
            this.Controls.Add(this.tbPosNO);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmConfig";
            this.Text = "YaoHuoForm";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConfigForm_KeyDown);
            this.Controls.SetChildIndex(this.tbPosNO, 0);
            this.Controls.SetChildIndex(this.lblShopno, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.tbServer, 0);
            this.Controls.SetChildIndex(this.lblIP, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.tbPort, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.rbDept, 0);
            this.Controls.SetChildIndex(this.rbStype, 0);
            this.Controls.SetChildIndex(this.tbPrintCount, 0);
            this.Controls.SetChildIndex(this.tbAPPVersion, 0);
            this.Controls.SetChildIndex(this.tbDeptCode, 0);
            this.Controls.SetChildIndex(this.tbStype, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblShopno;
        private System.Windows.Forms.TextBox tbPosNO;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbDept;
        private System.Windows.Forms.RadioButton rbStype;
        private System.Windows.Forms.TextBox tbPrintCount;
        private System.Windows.Forms.TextBox tbAPPVersion;
        private System.Windows.Forms.TextBox tbDeptCode;
        private System.Windows.Forms.TextBox tbStype;
    }
}