using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace POS
{
    public partial class FrmInputQty : Form
    {
        public FrmInputQty()
        {
            InitializeComponent();
            this.Width = 220;
            this.Height = 100;
            this.Location =
               new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
               (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        public decimal Qty
        {
            get;
            set;
        }

        private void FrmInputQty_Load(object sender, EventArgs e)
        {
            tbQty.Text = string.Empty;
            tbQty.Focus();
            tbQty.SelectAll();
        }

        private void tbQty_GotFocus(object sender, EventArgs e)
        {
            tbQty.SelectAll();
        }

        private void FrmInputQty_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    try
                    {
                        Qty = decimal.Parse(tbQty.Text);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch
                    {
                        MessageBox.Show("数量非法");
                        return;
                    }
                    break;
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel;
                    break;
                    
            }
        }

    }
}