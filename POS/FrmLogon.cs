using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Model;
using PubGlobal;
using Trans;
using BaseClass;
namespace POS
{
    public partial class FrmLogon : FrmBase
    {
        FrmConfig frmConfig = new FrmConfig();//配置窗口
        FrmInputPassword frmInputPassword = new FrmInputPassword();//密码输入

        public FrmLogon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 按Enter 进行下一步
        /// </summary>
        private void NextStep()
        {
            if (tbUserCode.Focused && tbUserCode.Text.Length>0)
            {
                tbPassword.Focus();
            }
            else if (tbPassword.Focused)
            {
                button1.Focus();
            }
        }

        //按键捕获
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //处理功能按键F1-F4，对应界面底部的四个按钮
                case Keys.Enter:
                    NextStep();
                    break;
                case Keys.F1:
                    this.button1_Click(null, null);
                    break;
                case Keys.F2:
                    this.button2_Click(null, null);
                    break;
                case Keys.F3:
                    this.button3_Click(null, null);
                    break;
                case Keys.F4:
                    this.button4_Click(null, null);
                    break;
                case Keys.F5:
                    this.button5_Click(null, null);
                    break;
                case Keys.Escape:
                    //Exit();
                    break;
            }
        }
        //登录
        private void button1_Click(object sender, EventArgs e)
        {
            if (tbUserCode.Text == "999999999" && tbPassword.Text == "12345678")
            {
                if (MessageBox.Show("退出系统？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                return;
            }

            if (string.IsNullOrEmpty(tbUserCode.Text))
            {
                MessageBox.Show("请输入用户名称");
                tbUserCode.Focus();
                return;
            }
            string msg;
            ShowWaitMsg("正在登陆");
            //初始化webservice
            TransModule.Init(PubGlobal.SysConfig.WebServiceUrl, tbUserCode.Text, tbPassword.Text, PubGlobal.SysConfig.DeptCode);
            if (!TransModule.Logon(out msg))
            {
                //登陆失败
                MessageBox.Show(msg);
                tbUserCode.Focus();
            }
            else
            {
                //登陆成功
                this.DialogResult = DialogResult.OK;
            }
            HideWaitMsg();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (frmInputPassword.ShowDialog() == DialogResult.OK)
            {
                if (frmInputPassword.Password == "83817927")
                {
                    frmConfig.ShowDialog();
                }
                else
                {
                    MessageBox.Show("密码错误");
                    tbUserCode.Focus();
                }
            }
        }

        private void tbUserCode_GotFocus(object sender, EventArgs e)
        {
            tbUserCode.SelectAll();
        }

        private void tbPassword_GotFocus(object sender, EventArgs e)
        {
            tbPassword.SelectAll();
        }

        private void FrmLogon_Load(object sender, EventArgs e)
        {
            tbUserCode.Focus();
            tbPassword.Text = string.Empty;
            PubGlobal.SysConfig.User = null;
        }
    }


    /// <summary>
    ///  取系统可用内存
    /// </summary>
    //class Class1
    //{
    //    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    //    private struct MEMORY_INFO
    //    {
    //        public uint dwLength;
    //        public uint dwMemoryLoad;
    //        public uint dwTotalPhys;
    //        public uint dwAvailPhys;
    //        public uint dwTotalPageFile;
    //        public uint dwAvailPageFile;
    //        public uint dwTotalVirtual;
    //        public uint dwAvailVirtual;
    //    }
    //    [System.Runtime.InteropServices.DllImport("Coredll.dll")]
    //    private static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);

    //    public static string GetMemory()
    //    {
    //        Class1.MEMORY_INFO MemInfo = new Class1.MEMORY_INFO();
    //        GlobalMemoryStatus(ref MemInfo);

    //        return Convert.ToString(Convert.ToInt64(MemInfo.dwAvailPhys.ToString()) / 1024 / 1024);
    //    }
    //}
}

