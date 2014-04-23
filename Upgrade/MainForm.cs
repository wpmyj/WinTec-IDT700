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

        //点击button1进入初设备注册流程，注册完毕后进入检查并更新终端程序流程
        private void button1_Click(object sender, EventArgs e)
        {
            Step1Form step1Form = new Step1Form();
            DialogResult ret = step1Form.ShowDialog();
            if (ret == DialogResult.OK)
            {
                this.label1.Text = "设备注册完成！";
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
                //处理功能按键F1-F4，对应界面底部的四个按钮
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

            //判断设备是否未注册，如果未注册先进行注册
            if (Config.ConfigInfo.CustomerId == "" || Config.ConfigInfo.CustomerId == null)
            {
                this.label1.Text = "设备尚未注册，\r\n请点击设备注册按钮进行注册。";
                this.button1.Visible = true;
                //this.Show();
                return;
            }

            ShowAnimate();

            //如果设备已经注册，则检查并更新终端程序
            this.label1.Text = "正在连接服务器...";
            Application.DoEvents();

            //先调用webservice测试能不能建立与服务器的网络连接，同时取得服务器时间用来同步终端时钟
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
                //同步终端时钟
                SetDateTime(Convert.ToDateTime(localTime));

                UpdateSet();

                ////检查终端程序有没有新版本
                //this.label1.Text = "正在检查系统更新并下载新版本...";
                //Application.DoEvents();
                //UpgradeProgram();

                HideAnimate();
            }
            else
            {
                if (MessageBox.Show("服务器连接失败，需要更改服务器地址吗？", "错误信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    this.label1.Text = "请输入新的服务器地址";
                    label2.Visible = true;
                    textBox1.Visible = true;
                    button2.Visible = true;
                    textBox1.Focus();
                    //Application.Exit();
                }
                else
                {
                    this.label1.Text = "连接服务器失败！";
                    Application.DoEvents();
                }
                HideAnimate();
                return;
            }

            this.label1.Text = "正在启动系统...";
            Application.DoEvents();

            //更新完成后，关闭该程序，启动主程序
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = Config.ProcessAppPath;
            p.Start();
            //延时5秒后关闭该程序
            System.Threading.Thread.Sleep(5000);
            Application.Exit();
        }

        //获取服务器时间
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
            this.label1.Text = "正在更新配置信息...";
            Application.DoEvents();
            //更新配置信息
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
                label1.Text = "配置信息更新失败！";
                Application.DoEvents();
                return false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "正在连接服务器...";
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
                label1.Text = "无法连接到该服务器，请确认后再试！";
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
        //    //如果有新版本，则更新终端程序
        //    if (newVersion != "" && oldVersion != newVersion)
        //    {
        //        this.label1.Text = "正在更新系统...";
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

        //        //更新数据库里保存的本地版本信息
        //        Config.ConfigInfo.softWareVersion = newVersion;
        //        DAL.ConfigDAL.Update(Config.ConfigInfo);

        //        //执行数据库更新脚本
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