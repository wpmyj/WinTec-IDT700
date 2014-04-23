namespace SumPos.Business
{
    partial class EndBusinessForm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.changeTxt = new System.Windows.Forms.TextBox();
            this.remainTotalTxt = new System.Windows.Forms.TextBox();
            this.payTotalTxt = new System.Windows.Forms.TextBox();
            this.xfTotalTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.HighlightText;
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(400, 320);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.changeTxt);
            this.panel2.Controls.Add(this.remainTotalTxt);
            this.panel2.Controls.Add(this.payTotalTxt);
            this.panel2.Controls.Add(this.xfTotalTxt);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(5, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(390, 260);
            // 
            // panel7
            // 
            this.panel7.Location = new System.Drawing.Point(162, 227);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(180, 2);
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(162, 169);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(180, 2);
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(160, 111);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(180, 2);
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(162, 53);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(180, 2);
            // 
            // changeTxt
            // 
            this.changeTxt.BackColor = System.Drawing.Color.Black;
            this.changeTxt.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Bold);
            this.changeTxt.ForeColor = System.Drawing.Color.White;
            this.changeTxt.Location = new System.Drawing.Point(162, 177);
            this.changeTxt.Name = "changeTxt";
            this.changeTxt.ReadOnly = true;
            this.changeTxt.Size = new System.Drawing.Size(180, 52);
            this.changeTxt.TabIndex = 16;
            this.changeTxt.Text = "0.00";
            // 
            // remainTotalTxt
            // 
            this.remainTotalTxt.BackColor = System.Drawing.Color.Black;
            this.remainTotalTxt.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Bold);
            this.remainTotalTxt.ForeColor = System.Drawing.Color.White;
            this.remainTotalTxt.Location = new System.Drawing.Point(160, 119);
            this.remainTotalTxt.Name = "remainTotalTxt";
            this.remainTotalTxt.ReadOnly = true;
            this.remainTotalTxt.Size = new System.Drawing.Size(180, 52);
            this.remainTotalTxt.TabIndex = 15;
            this.remainTotalTxt.Text = "0.00";
            // 
            // payTotalTxt
            // 
            this.payTotalTxt.BackColor = System.Drawing.Color.Black;
            this.payTotalTxt.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Bold);
            this.payTotalTxt.ForeColor = System.Drawing.Color.White;
            this.payTotalTxt.Location = new System.Drawing.Point(160, 61);
            this.payTotalTxt.Name = "payTotalTxt";
            this.payTotalTxt.Size = new System.Drawing.Size(180, 52);
            this.payTotalTxt.TabIndex = 14;
            this.payTotalTxt.Text = "0.00";
            this.payTotalTxt.LostFocus += new System.EventHandler(this.payTotalTxt_LostFocus);
            // 
            // xfTotalTxt
            // 
            this.xfTotalTxt.BackColor = System.Drawing.Color.Black;
            this.xfTotalTxt.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Bold);
            this.xfTotalTxt.ForeColor = System.Drawing.Color.White;
            this.xfTotalTxt.Location = new System.Drawing.Point(160, 3);
            this.xfTotalTxt.Name = "xfTotalTxt";
            this.xfTotalTxt.ReadOnly = true;
            this.xfTotalTxt.Size = new System.Drawing.Size(180, 52);
            this.xfTotalTxt.TabIndex = 13;
            this.xfTotalTxt.Text = "0.00";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(45, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 26);
            this.label5.Text = "剩余金额";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(45, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 26);
            this.label4.Text = "找零金额";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(45, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 26);
            this.label3.Text = "现金支付";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(45, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 26);
            this.label1.Text = "消费金额";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 45);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(140, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 24);
            this.label2.Text = "合计";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // EndBusinessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(400, 320);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EndBusinessForm";
            this.Text = "合计";
            this.TopMost = true;
            this.Closed += new System.EventHandler(this.EndBusinessForm_Closed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EndBusinessForm_KeyDown);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox changeTxt;
        private System.Windows.Forms.TextBox remainTotalTxt;
        private System.Windows.Forms.TextBox payTotalTxt;
        private System.Windows.Forms.TextBox xfTotalTxt;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;


    }
}