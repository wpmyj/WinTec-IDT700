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
    public partial class FrmInputPassword : Form
    {
        public FrmInputPassword()
        {
            InitializeComponent();
            this.Width = 220;
            this.Height = 100;
            this.Location =
               new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
               (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        public string Password
        {
            get;
            set;
        }

        private void txtPrice_GotFocus(object sender, EventArgs e)
        {

        }

        private void FrmInputPassword_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Password = tbPassword.Text;
                    this.DialogResult=DialogResult.OK;
                    break;
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel;
                    break;

            }
        }

        private void FrmInputPassword_Activated(object sender, EventArgs e)
        {
            tbPassword.Text = string.Empty;
            tbPassword.Focus();
        }

        private void tbPassword_GotFocus(object sender, EventArgs e)
        {
            tbPassword.SelectAll();
        }

    }
}