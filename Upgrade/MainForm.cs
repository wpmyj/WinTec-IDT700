using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using AnimateControl;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace Upgrade
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        private void MainForm_Closed(object sender, EventArgs e)
        {

        }

        //���button1������豸ע�����̣�ע����Ϻ�����鲢�����ն˳�������
        private void button1_Click(object sender, EventArgs e)
        {
            Step1Form step1Form = new Step1Form();
            DialogResult ret = step1Form.ShowDialog();
            if (ret == DialogResult.OK)
            {
                this.label1.Text = "�豸ע����ɣ�";
                Application.DoEvents();
                this.timer1.Enabled = true;
                this.button1.Visible = false;
            }
            step1Form.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExitForm exitForm = new ExitForm();
            DialogResult ret = exitForm.ShowDialog();
            if (ret == DialogResult.OK && exitForm.pwd == Config.ConfigInfo.EPW)
                Application.Exit();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //�����ܰ���F1-F4����Ӧ����ײ����ĸ���ť
                case Keys.F1:
                    if (this.button1.Visible == true)
                        this.button1_Click(null, null);
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                    if (this.button2.Visible == true)
                        this.button2_Click(null, null);
                    break;
                case Keys.Escape:
                case Keys.F4:
                    this.button4_Click(null, null);
                    break;
                case Keys.Enter:
                    if (textBox1.Focused)
                    {
                        button2_Click(null, null);
                    }
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Show();
            this.timer1.Enabled = false;

            //�ж��豸�Ƿ�δע�ᣬ���δע���Ƚ���ע��
            if (Config.ConfigInfo.CustomerId == "" || Config.ConfigInfo.CustomerId == null)
            {
                this.label1.Text = "�豸��δע�ᣬ\r\n�����豸ע�ᰴť����ע�ᡣ";
                this.button1.Visible = true;
                //this.Show();
                return;
            }

            ShowAnimate();

            //����豸�Ѿ�ע�ᣬ���鲢�����ն˳���
            this.label1.Text = "�������ӷ�����...";
            Application.DoEvents();

            //�ȵ���webservice�����ܲ��ܽ�������������������ӣ�ͬʱȡ�÷�����ʱ������ͬ���ն�ʱ��
            string localTime = "";
            bool isConnected = false;
            DateTime dtBegin = DateTime.Now;
            int tryTimes = 0;
            while (true)
            {
                try
                {
                    tryTimes++;
                    localTime = GetServerDateTime(Config.ConfigInfo.ServerIP).ToString();
                    isConnected = true;
                    break;
                }
                catch
                {
                    TimeSpan ts = DateTime.Now.Subtract(dtBegin);
                    if (ts.TotalSeconds > 30 && tryTimes >= 3)
                        break;
                }
            }
            if (isConnected)
            {
                //ͬ���ն�ʱ��
                SetDateTime(Convert.ToDateTime(localTime));

                UpdateSet();

                ////����ն˳�����û���°汾
                //this.label1.Text = "���ڼ��ϵͳ���²������°汾...";
                //Application.DoEvents();
                //UpgradeProgram();

                HideAnimate();
            }
            else
            {
                if (MessageBox.Show("����������ʧ�ܣ���Ҫ���ķ�������ַ��", "������Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    this.label1.Text = "�������µķ�������ַ";
                    label2.Visible = true;
                    textBox1.Visible = true;
                    button2.Visible = true;
                    textBox1.Focus();
                    //Application.Exit();
                }
                else
                {
                    this.label1.Text = "���ӷ�����ʧ�ܣ�";
                    Application.DoEvents();
                }
                HideAnimate();
                return;
            }

            this.label1.Text = "��������ϵͳ...";
            Application.DoEvents();

            //������ɺ󣬹رոó�������������
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = Config.ProcessAppPath;
            p.Start();
            //��ʱ5���رոó���
            System.Threading.Thread.Sleep(5000);
            Application.Exit();
        }

        //��ȡ������ʱ��
        private DateTime GetServerDateTime(string serverIP)
        {
            SqlConnection conn = new SqlConnection("Data Source=" + serverIP + ";Initial Catalog=FoodPalace;uid=sa;pwd=sa;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand command = new SqlCommand("select getdate() as systemtimes", conn);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                DateTime servertime = dr.GetDateTime(0);
                conn.Close();
                return servertime;
            }
            conn.Close();
            return DateTime.Now;
        }

        private bool UpdateSet()
        {
            this.label1.Text = "���ڸ���������Ϣ...";
            Application.DoEvents();
            //����������Ϣ
            Model.ConfigModel model = DAL.ConfigDAL.GetConfigInfo();

            SqlParameter[] cmdParms = { new SqlParameter("@CustomerId", SqlDbType.VarChar, 30) };
            cmdParms[0].Value = model.CustomerId;
            try
            {
                DataTable dtCustomer = Data.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetCustomer", cmdParms);
                if (dtCustomer.Rows.Count > 0)
                {
                    model.CustomerName = dtCustomer.Rows[0]["CustomerName"] == null ? string.Empty : dtCustomer.Rows[0]["CustomerName"].ToString();
                    model.LinkMan = dtCustomer.Rows[0]["LinkMan"] == null ? string.Empty : dtCustomer.Rows[0]["LinkMan"].ToString();
                    model.Phone = dtCustomer.Rows[0]["Phone"] == null ? string.Empty : dtCustomer.Rows[0]["Phone"].ToString();
                    model.Pwd = dtCustomer.Rows[0]["CustomerPwd"] == null ? string.Empty : dtCustomer.Rows[0]["CustomerPwd"].ToString();
                }

                DataTable dtConfig = Data.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetConfig", null);
                if (dtConfig.Rows.Count > 0)
                {
                    model.CompanyName = dtConfig.Rows[0]["FoodPalaceName"] == null ? string.Empty : dtConfig.Rows[0]["FoodPalaceName"].ToString();
                    if (dtConfig.Rows[0]["Remark"] != null)
                    {
                        string[] strRemark = dtConfig.Rows[0]["Remark"].ToString().Split('|');
                        model.Remark1 = strRemark[0];
                        if (strRemark.Length >= 2)
                        {
                            model.Remark2 = strRemark[1];
                        }
                        if (strRemark.Length == 3)
                        {
                            model.Remark3 = strRemark[2];
                        }
                    }
                }

                DAL.ConfigDAL.Update(model);
                return true;
            }
            catch
            {
                label1.Text = "������Ϣ����ʧ�ܣ�";
                Application.DoEvents();
                return false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "�������ӷ�����...";
                textBox1.Enabled = false;
                button2.Enabled = false;
                Application.DoEvents();
                DateTime time = GetServerDateTime(textBox1.Text.Trim());
                Model.ConfigModel configinfo = DAL.ConfigDAL.GetConfigInfo();
                configinfo.ServerIP = textBox1.Text.Trim();
                DAL.ConfigDAL.Update(configinfo);
                textBox1.Enabled = true;
                button2.Enabled = true;
                label2.Visible = false;
                textBox1.Visible = false;
                button2.Visible = false;
                Application.DoEvents();

                timer1.Enabled = true;
            }
            catch
            {
                label1.Text = "�޷����ӵ��÷���������ȷ�Ϻ����ԣ�";
                textBox1.Enabled = true;
                button2.Enabled = true;
                textBox1.Focus();
                textBox1.SelectAll();
            }
        }

        //private void UpgradeProgram()
        //{
        //    string oldVersion = Config.ConfigInfo.SoftWareVersion;
        //    string newVersion = string.Empty;
        //    DownLoadFile[] downLoadFileList;

        //    downLoadFileList = SmartWebService.Instance.Upgrade(oldVersion, ref newVersion);
        //    //������°汾��������ն˳���
        //    if (newVersion != "" && oldVersion != newVersion)
        //    {
        //        this.label1.Text = "���ڸ���ϵͳ...";
        //        Application.DoEvents();
        //        foreach (DownLoadFile downloadFile in downLoadFileList)
        //        {
        //            try
        //            {
        //                ZipClass.UnzipBytesToFile(downloadFile.FileContent, Config.CurrentPath + @"\" + downloadFile.FileName);
        //            }
        //            catch (Exception ex)
        //            {
        //                string ss = ex.Message;
        //            }
        //        }

        //        //�������ݿ��ﱣ��ı��ذ汾��Ϣ
        //        Config.ConfigInfo.softWareVersion = newVersion;
        //        DAL.ConfigDAL.Update(Config.ConfigInfo);

        //        //ִ�����ݿ���½ű�
        //        if (File.Exists(Config.DataBaseAlterFilePath))
        //        {
        //            StreamReader objReader = new StreamReader(Config.DataBaseAlterFilePath);

        //            System.Collections.ArrayList sqlCmdArray = new System.Collections.ArrayList();
        //            string sqlCmd = "";
        //            while (sqlCmd != null)
        //            {
        //                sqlCmd = objReader.ReadLine();
        //                if (sqlCmd != null)
        //                    sqlCmdArray.Add(sqlCmd);
        //            }
        //            SQLiteHelper.ExecuteNonQueryWithTran(Config.ConnectionString, sqlCmdArray);
        //            objReader.Close();
        //            File.Delete(Config.DataBaseAlterFilePath);
        //        }
        //    }
        //    HideAnimate();
        //}

        private AnimateCtl animCtl;
        private void ShowAnimate()
        {
            animCtl = new AnimateCtl(this.BackColor, 94, 150);
            animCtl.Location = new Point(340, 200);
            this.Controls.Add(animCtl);
            animCtl.BringToFront();
            animCtl.StartAnimation();
        }

        private void HideAnimate()
        {
            animCtl.StopAnimation();
            animCtl.Visible = false;
        }

        #region ����ϵͳʱ��
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

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = false;
        }
    }
}