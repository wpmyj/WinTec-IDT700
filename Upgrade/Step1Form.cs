using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Upgrade
{
    public partial class Step1Form : Form
    {
        public Step1Form()
        {
            InitializeComponent();
            this.inputPanel1.Enabled = true;
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length == 0) return;
            this.Enabled = false;
            //先测试输入的服务器ip地址是否能连通
            WaitForm waitForm = new WaitForm("正在尝试连接服务器，请稍候...");
            waitForm.Show();
            Application.DoEvents();

            DateTime datetime = DateTime.Now;

            try
            {
                datetime = GetServerDateTime();
            }
            catch (Exception ex)
            {
                waitForm.Close();
                MessageBox.Show("连接服务器失败，\r\n请确认您输入的服务器IP地址是否正确！");
                this.Enabled = true;
                this.textBox1.Focus();
                this.textBox1.SelectAll();
                return;
            }

            //同步终端时钟
            SetDateTime(datetime);

            //保存输入的IP地址
            Config.ConfigInfo.ServerIP = this.textBox1.Text.Trim();

            //进入下一步，注册商户信息
            waitForm.Close();
            Step2Form step2Form = new Step2Form();
            this.Enabled = true;
            DialogResult ret = step2Form.ShowDialog();
            if (ret == DialogResult.Retry)
            {
                this.textBox1.Focus();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        //获取服务器时间
        private DateTime GetServerDateTime()
        {
            SqlConnection conn = new SqlConnection("Data Source=" + textBox1.Text.Trim() + ";Initial Catalog=FoodPalace;uid=sa;pwd=sa;");
            if (conn.State == ConnectionState.Closed)
            {

                conn.Open();
            }
            SqlCommand command = new SqlCommand("select getdate() as systemtimes", conn);
            SqlDataReader dr = command.ExecuteReader();

            
            DateTime datetime = DateTime.Now;
            while (dr.Read())
            {
                datetime = dr.GetDateTime(0);
            }
            conn.Close();
            return datetime;
        }

        private void Step1Form_KeyDown(object sender, KeyEventArgs e)
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
            e.Handled = true;
        }

        //private void textBox1_GotFocus(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < this.inputPanel1.InputMethods.Count; i++)
        //    {
        //        if (this.inputPanel1.InputMethods[i].Name == "键盘")
        //            this.inputPanel1.CurrentInputMethod = this.inputPanel1.InputMethods[i];
        //    }
        //    this.inputPanel1.Enabled = true;
        //}

        //private void textBox1_LostFocus(object sender, EventArgs e)
        //{
        //    this.inputPanel1.Enabled = false;
        //}

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        #region 设置系统时间
        private struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        [System.Runtime.InteropServices.DllImport("Coredll.dll")]
        private static extern bool SetLocalTime(ref SYSTEMTIME lpSystemTime);

        public static void SetDateTime(DateTime dt)
        {
            SYSTEMTIME sysTime = new SYSTEMTIME();

            sysTime.wYear = Convert.ToUInt16(dt.Year);
            sysTime.wMonth = Convert.ToUInt16(dt.Month);
            sysTime.wDay = Convert.ToUInt16(dt.Day);
            sysTime.wDayOfWeek = Convert.ToUInt16(dt.DayOfWeek);
            sysTime.wHour = Convert.ToUInt16(dt.Hour);
            sysTime.wMinute = Convert.ToUInt16(dt.Minute);
            sysTime.wSecond = Convert.ToUInt16(dt.Second);
            sysTime.wMilliseconds = Convert.ToUInt16(dt.Millisecond);
            SetLocalTime(ref sysTime);
        }
        #endregion

        private void Step1Form_Closed(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = false;
            RealseEvent();
        }

        private void RealseEvent()
        {
            this.button1.Click -= new System.EventHandler(this.button1_Click);
            //this.button2.Click -= new System.EventHandler(this.button2_Click);
            //this.button3.Click -= new System.EventHandler(this.button3_Click);
            this.button4.Click -= new System.EventHandler(this.button4_Click);
            //this.textBox1.GotFocus -= new System.EventHandler(this.textBox1_GotFocus);
            //this.textBox1.LostFocus -= new System.EventHandler(this.textBox1_LostFocus);
            this.Closed -= new System.EventHandler(this.Step1Form_Closed);
            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.Step1Form_KeyDown);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}