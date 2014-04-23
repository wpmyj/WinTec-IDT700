using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using AnimateControl;

namespace Upgrade
{
    public partial class WaitForm : Form
    {
        private AnimateCtl animCtl;
        public WaitForm()
        {
            InitializeComponent();
            this.Size = new Size(this.panel1.Width + 4, this.panel1.Height + 4);
            this.Location =
                new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }

        string msg = "";
        public string Msg
        {
            get
            {
                return msg;
            }
            set
            {
                msg = value;
                this.label1.Text = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">提示信息</param>
        public WaitForm(string _msg)
        {
            InitializeComponent();
            this.label1.Text = _msg;
            this.msg = _msg;
            this.Size = new Size(this.panel1.Width + 4, this.panel1.Height + 4);
            this.Location =
                new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

            animCtl = new AnimateCtl(Color.DodgerBlue, 94, 150);
            
            this.panel1.Controls.Add(animCtl);
            animCtl.Location = new Point(95, 20);
            animCtl.BringToFront();
            animCtl.StartAnimation();
        }

        private void WaitForm_Closed(object sender, EventArgs e)
        {
            animCtl.StopAnimation();
        }
    }
}