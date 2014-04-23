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
        FrmConfig frmConfig = new FrmConfig();//���ô���
        FrmInputPassword frmInputPassword = new FrmInputPassword();//��������

        public FrmLogon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��Enter ������һ��
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

        //��������
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //�����ܰ���F1-F4����Ӧ����ײ����ĸ���ť
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
        //��¼
        private void button1_Click(object sender, EventArgs e)
        {
            if (tbUserCode.Text == "999999999" && tbPassword.Text == "12345678")
            {
                if (MessageBox.Show("�˳�ϵͳ��", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                return;
            }

            if (string.IsNullOrEmpty(tbUserCode.Text))
            {
                MessageBox.Show("�������û�����");
                tbUserCode.Focus();
                return;
            }
            string msg;
            ShowWaitMsg("���ڵ�½");
            //��ʼ��webservice
            TransModule.Init(PubGlobal.SysConfig.WebServiceUrl, tbUserCode.Text, tbPassword.Text, PubGlobal.SysConfig.DeptCode);
            if (!TransModule.Logon(out msg))
            {
                //��½ʧ��
                MessageBox.Show(msg);
                tbUserCode.Focus();
            }
            else
            {
                //��½�ɹ�
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
                    MessageBox.Show("�������");
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
    ///  ȡϵͳ�����ڴ�
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

