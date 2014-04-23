using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Xml;
using Action.Sqlite;
using ComClass;
namespace SumPos
{
    public partial class Login : BaseClass.BaseForm
    {
        public Login()
        {
            InitializeComponent();
        }



        /// <summary>
        /// 系统配置
        /// </summary>
        private Model.Config config = new Model.Config() ;
        



        private void Login_Load(object sender, EventArgs e)
        {

            ShowWaitMsg("正在打开程序...请稍后");
            Application.DoEvents();
            config = new Model.Config();

            #region 读取系统参数
            config = new Action.Sqlite.SqliteConfigAction(config.SqliteConnStr).getConfig();
            WebService.Url = config.WebServerUrl;//初始化webservice
            config.NetMac = ReadReg.GetMacAddr();
            #endregion

            HideWaitMsg();
            textBox1.Focus();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //处理功能按键F1-F4，对应界面底部的四个按钮
                case Keys.F1:
                    
                    break;
                case Keys.F2:
                    this.button2_Click(null, null);
                    break;
                case Keys.F3:
                    {
                        button3_Click(null, null);
                        break;
                    }
                case Keys.Enter:
                    {
                        if (textBox1.Focused == true && textBox1.Text.Trim() != string.Empty)
                        {
                            textBox2.Focus();
                        }
                        else if (textBox2.Focused == true)
                        {
                            button2_Click(null, null);
                        }
                        break;
                    }
                case Keys.Escape:
                    {
                        ExitForm exitForm = new ExitForm();
                        DialogResult ret = exitForm.ShowDialog();
                        if (ret == DialogResult.OK )
                        {
                            if (exitForm.pwd == "053283817927")
                            {
                                Application.Exit();
                            }
                            else
                            {
                                MessageBox.Show("密码错误！不能退出！");
                            }
                        }
                        break;
                    }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "999999999" && textBox2.Text == "12345678")
            {
                if (MessageBox.Show("确定要退出系统？！Esc 取消", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else if (textBox1.Text == "999999999" && textBox2.Text == "83817927")
            {
                //登陆成功,j进入设置界面
                SumPos.Setting.SystemParamSetForm mf = new SumPos.Setting.SystemParamSetForm();
                mf.Owner = this;
                mf.config = config;
                mf.ShowDialog();
                config = mf.config;
                mf.Dispose();
                button3_Click(null, null);
                label3.Text = string.Empty;
                // this.Hide();
            }
            else if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("请输入用户名密码！");
            }
            else
            {

                Application.DoEvents();
                ShowWaitMsg("正在登陆...");
                Model.User user = new Model.User();
                user.UserCode=textBox1.Text;
                user.EncryPassword=textBox2.Text;
                user = new Action.Sqlite.SqliteUserAction(config.SqliteConnStr).userLogon(user);
                HideWaitMsg();
                try
                {
                    //bool chkOk = WebService.chkInfo(config.PosNo, config.NetMac);
                    //if (chkOk == false)
                    //{
                    //    MessageBox.Show("验证本机登记信息失败！请与管理员联系！");
                    //    return;
                    //}
                    //MessageBox.Show("1");
                    //WebService ser = new WebService(config.WebServerUrl);
                    //MessageBox.Show("2");
                    string s1 = textBox1.Text;
                    string s2 = textBox2.Text;
                    //user = WebService.userLogon(s1, s2);
                    //user = ser.userLogon(textBox1.Text, textBox2.Text);
                    //MessageBox.Show("3");
                    if (user != null)
                    {
                        //MessageBox.Show("4");
                        if (user.UserCode != "" & user.UserCode != null)
                        {
                            //MessageBox.Show("5");
                            //登陆成功
                            MainForm mf = new MainForm();
                            mf.Owner = this;
                            mf.user = user;

                            mf.config = config;
                            mf.Show();
                            button3_Click(null, null);
                            label3.Text = string.Empty;
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("帐户或密码错误！");
                            label3.Text = string.Empty;
                            textBox2.Text = string.Empty;
                            textBox1.Focus();
                            textBox1.SelectAll();
                        }
                    }
                    else
                    {
                        throw new Exception("程序连接错误！");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //连接不到webservice
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox1.Focus();
        }
    }
}