using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace Upgrade
{
    public partial class Step3Form : Form
    {
        public Step3Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WaitForm waitForm = new WaitForm("正在保存设备注册信息，请稍候...");
            waitForm.Show();
            Application.DoEvents();
            
            //本机
            //保存商户信息、服务器ip地址、软件版本
            try
            {
                DAL.ConfigDAL.Update(Config.ConfigInfo);
                waitForm.Close();
                Application.DoEvents();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                button1.Visible = false;
                button4.Visible = false;
                MessageBox.Show("保存失败，请重启设备重新注册！");
                waitForm.Close();
                Application.DoEvents();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void Step3Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //处理功能按键F1-F4，对应界面底部的四个按钮
                case Keys.Return:
                case Keys.F1:
                    if (this.button1.Visible == true)
                        this.button1_Click(null, null);
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                    break;
                case Keys.Escape:
                case Keys.F4:
                    this.button4_Click(null, null);
                    break;
            }
        }

        private void Step3Form_Closed(object sender, EventArgs e)
        {
            RealseEvent();
        }

        private void RealseEvent()
        {
            this.button1.Click -= new System.EventHandler(this.button1_Click);
            //this.button2.Click -= new System.EventHandler(this.button2_Click);
            //this.button3.Click -= new System.EventHandler(this.button3_Click);
            this.button4.Click -= new System.EventHandler(this.button4_Click);
            this.Closed -= new System.EventHandler(this.Step3Form_Closed);
            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.Step3Form_KeyDown);
        }
    }
}