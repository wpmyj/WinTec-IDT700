using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Trans;

namespace POS
{
    public partial class FrmConfig : BaseClass.FrmBase
    {
        public FrmConfig()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            tbPosNO.Text = PubGlobal.SysConfig.PosNO;
            tbServer.Text = PubGlobal.SysConfig.Server;
            tbPort.Text = PubGlobal.SysConfig.Port;
            rbDept.Checked = PubGlobal.SysConfig.GetGoodsByDept;
            rbStype.Checked = !PubGlobal.SysConfig.GetGoodsByDept;
            tbDeptCode.Text = PubGlobal.SysConfig.DeptCode;
            tbStype.Text = PubGlobal.SysConfig.Stype;
            tbPrintCount.Text = PubGlobal.SysConfig.PrintCount.ToString();
            tbAPPVersion.Text = PubGlobal.Envionment.APPVersion;
        }

        private void ConfigForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                case Keys.F1:
                    button1_Click(null, null);
                    break;
                case Keys.F2:
                    button2_Click(null, null);
                    break;
                case Keys.F3:
                    button3_Click(null, null);
                    break;
                case Keys.Escape:
                case Keys.F5:
                    button5_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        private bool SaveConfig(string version ,string PosNO,string server,string port,string deptCode,
            string StypeCode,string ShowGoodsMode,string Printcount,out string msg)
        {
            try
            {
                StreamWriter wr = new StreamWriter(PubGlobal.Envionment.ConfigFilePath,false);
                wr.WriteLine("version=" + version);
                wr.WriteLine("PosNO="+PosNO);
                wr.WriteLine("Server=" + server);
                wr.WriteLine("Port=" + port);
                wr.WriteLine("DeptCode=" + deptCode);
                wr.WriteLine("StypeCode=" + StypeCode);
                wr.WriteLine("ShowGoodsMode=" + ShowGoodsMode);
                wr.WriteLine("PrintCount=" + Printcount);
                wr.Close();
                msg = "保存成功";
                return true;
            }
            catch (Exception ex)
            {
                msg = "配置文件打开失败：" + ex.Message;
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg;
            if (string.IsNullOrEmpty(tbPosNO.Text))
            {
                MessageBox.Show("请输入收款机号");
                tbPosNO.Focus();
                return;
            }
            if (TransModule.HelloWorld(tbServer.Text, tbPort.Text, out msg))
            {
                //测试通过
                if (MessageBox.Show("测试成功，是否保存参数？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == DialogResult.Yes)
                {
                    //保存参数
                    if (SaveConfig(tbAPPVersion.Text,tbPosNO.Text,tbServer.Text,tbPort.Text,tbDeptCode.Text,tbStype.Text,rbDept.Checked?"1":"0",tbPrintCount.Text,out msg))
                    {
                        MessageBox.Show("保存成功！请重新启动设备");
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("保存失败，是否重试？", "错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1)
                            == DialogResult.Retry)
                        {
                            button1_Click(null, null);
                        }
                    }
                }
            }
            else
            {
                //测试失败
                if (MessageBox.Show("连接失败，是否重试？", "错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1) 
                    == DialogResult.Retry)
                {
                    button1_Click(null, null);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("是否需要强制更新？！强制更新后将清空本地数据！", "强制更新", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            //{

            //    if (rdbGPRS.Checked)
            //    {
            //        ShowWaitMsg("正在拨号");
            //        while (true)
            //        {
            //            this.ShowWaitMsg("正在拨号，请稍候...");
            //            if (WintecIDT700.GPRSConnect() == false)
            //            {
            //                HideWaitMsg();
            //                if (DialogResult.Yes ==
            //                MessageBox.Show("拨号失败！可能SIM卡安装不正确或已欠费，要转为脱机模式吗？\r\n选择【Yes】将转为脱机模式\r\n选择【No】将重试拨号"
            //                    , "确认信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            //                {
            //                    OnLine = false;
            //                    this.Enabled = true;
            //                    break;
            //                }
            //            }
            //            else
            //            {
            //                break;
            //            }
            //        }
            //    }

            //    if ((OnLine && rdbGPRS.Checked) || (!rdbGPRS.Checked))
            //    {
            //        //拨号，且online=true
            //        //或者 非拨号
            //        //强制更新
            //        ShowWaitMsg("正在检查系统更新,请稍候...");
            //        Application.DoEvents();
            //        bool isUpdate = false;
            //        string newVersion = string.Empty;
            //        string oldVersion = "0";
            //        try
            //        {
            //            SmartWebService.SetInstanceUrl(string.Format(@"http://{0}:{1}/Service.asmx", txtIP.Text, txtPort.Text));
            //            isUpdate = SmartWebService.Instance.Upgrade("", "", "", oldVersion, ref newVersion);
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //            HideWaitMsg();
            //            return;
            //        }
            //        if (isUpdate)
            //        {
            //            ShowWaitMsg("正在进行系统更新,请稍候...");
            //            Application.DoEvents();
            //            //更新引导程序
            //            string path = Config.CurrentPath + @"\Update\Upgrade.exe";
            //            if (File.Exists(path))
            //            {
            //                File.Delete(Config.UpgradeAppPath);
            //                File.Move(Config.CurrentPath + @"\Update\Upgrade.exe", Config.UpgradeAppPath);
            //                File.Delete(Config.CurrentPath + @"\Update\Upgrade.exe");
            //            }
            //            //DAL.ConfigDAL.UpdateVersion(newVersion,oldVersion);
            //            //关闭本程序,启动更新程序
            //            //更新完成后，关闭更新程序，启动主程序
            //            System.Diagnostics.Process p = new System.Diagnostics.Process();
            //            p.StartInfo.FileName = Config.UpdateAppPath;

            //            p.Start();
            //            //关闭该程序


            //            //System.Threading.Thread.Sleep(2000);
            //            HideWaitMsg();
            //            //this.Invoke(Cls);
            //            //throw new Exception("Close");
            //        }

            //        HideWaitMsg();
            //    }
            //}
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void tbPosNO_GotFocus(object sender, EventArgs e)
        {
            tbPosNO.SelectAll();
        }


    }
}