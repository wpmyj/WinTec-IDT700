using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SumPos.Business
{
    public partial class SaleReturnForm : Form
    {
        public SaleReturnForm()
        {
            InitializeComponent();
            this.Size = new Size(this.panel1.Width + 4, this.panel1.Height + 4);
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }

        public string SaleNo
        {
            get 
            {
                return this.textBox1.Text; 
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.label2.Visible)
                this.label2.Visible = false;
        }
        /// <summary>
        /// ²¶×½´°¿Ú°´¼ü
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaleReturnForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (this.textBox1.Text == "")
                    {
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        this.label2.Visible = true;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    e.Handled = true;
                    break;
            }
        }

        private void SaleReturnForm_Closed(object sender, EventArgs e)
        {
            this.textBox1.TextChanged -= new System.EventHandler(this.textBox1_TextChanged);
            this.Closed -= new System.EventHandler(this.SaleReturnForm_Closed);
            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.SaleReturnForm_KeyDown);
        }
    }
}