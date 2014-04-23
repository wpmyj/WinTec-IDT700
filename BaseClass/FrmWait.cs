using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BaseClass;
using System.Reflection;

namespace BaseClass
{
    public partial class FrmWait : Form
    {
        private AnimateCtl animCtl;
        public FrmWait()
        {
            InitializeComponent();
            this.Size = new Size(this.panel1.Width + 4, this.panel1.Height + 4);
            this.Location =
                new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
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
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// 
        public FrmWait(string _msg)
        {
            InitializeComponent();
            this.label1.Text = _msg;
            this.msg = _msg;
            this.Size = new Size(this.panel1.Width + 4, this.panel1.Height + 4);
            this.Location =
                new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            
            animCtl = new AnimateCtl(Color.DodgerBlue,94, 150);
            animCtl.Location = new Point(180, 20);
            this.panel1.Controls.Add(animCtl);
            animCtl.BringToFront();
        }

        private void WaitForm_Closed(object sender, EventArgs e)
        {
            animCtl.StopAnimation();
            //animCtl.Dispose();
            //animCtl = null;
        }

        private void WaitForm_Activated(object sender, EventArgs e)
        {
            animCtl.StartAnimation();
        }

        private void WaitForm_Deactivate(object sender, EventArgs e)
        {
            animCtl.StopAnimation();
        }
    }
}