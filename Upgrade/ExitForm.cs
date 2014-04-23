using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Upgrade
{
    public partial class ExitForm : Form
    {
        public string pwd
        {
            get
            {
                return this.textBox1.Text;
            }
        }
        public ExitForm()
        {
            InitializeComponent();
            this.Size = new Size(this.panel1.Width + 4, this.panel1.Height + 4);
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }

        private void ExitForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            { 
                case Keys.Return:
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;
            }
        }

        private void ExitForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.inputPanel1.InputMethods.Count; i++)
            {
                if (this.inputPanel1.InputMethods[i].Name == "¼üÅÌ")
                    this.inputPanel1.CurrentInputMethod = this.inputPanel1.InputMethods[i];
            }
        }
    }
}