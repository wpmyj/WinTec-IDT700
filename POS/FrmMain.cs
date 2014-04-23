using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.IO;
using PubGlobal;
using Trans;


namespace POS
{
    public partial class FrmMain : BaseClass.FrmBase
    {
        FrmLogon frmLogon = new FrmLogon();
        FrmSale frmSale = new FrmSale();
        FrmQuery frmQuery = new FrmQuery();
        public FrmMain()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            SetFullScreen(true, ref  rectangle);

            ShowWaitMsg("正在读取系统参数");
            #region 读取系统参数
            try
            {
                using (StreamReader rd = new StreamReader(PubGlobal.Envionment.VersionFilePath))
                {
                    PubGlobal.Envionment.APPVersion = rd.ReadLine();
                    rd.Close();
                }
            }
            catch (Exception ex)
            {
                PubGlobal.Envionment.APPVersion = "1.0.0.0";//找不到版本信息文件，默认为1.0.0.0版
            }
            try
            {
                using (StreamReader rd = new StreamReader(PubGlobal.Envionment.ConfigFilePath))
                {
                    while (!rd.EndOfStream)
                    {
                        string info = rd.ReadLine();
                        if (string.IsNullOrEmpty(info))
                        {
                            continue;
                        }
                        switch (info.Split('=')[0])
                        {
                            case "PosNO":
                                PubGlobal.SysConfig.PosNO = info.Split('=')[1];
                                break;
                            case "Server":
                                PubGlobal.SysConfig.Server = info.Split('=')[1];
                                break;
                            case "Port":
                                PubGlobal.SysConfig.Port = info.Split('=')[1];
                                break;
                            case "DeptCode":
                                PubGlobal.SysConfig.DeptCode = info.Split('=')[1];
                                break;
                            case "StypeCode":
                                PubGlobal.SysConfig.Stype = info.Split('=')[1];
                                break;
                            case "ShowGoodsMode":
                                PubGlobal.SysConfig.GetGoodsByDept = (info.Split('=')[1] == "1");
                                break;
                            case "PrintCount":
                                try
                                {
                                    PubGlobal.SysConfig.PrintCount = int.Parse(info.Split('=')[1]);
                                }
                                catch
                                {
                                    PubGlobal.SysConfig.PrintCount = 0;
                                }
                                break;
                        }
                    }
                    rd.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统错误：" + ex.Message);
            }
            #endregion
            HideWaitMsg();

            #region 登录
            if (frmLogon.ShowDialog() == DialogResult.Cancel)
            {
                this.Close();
            }
            #endregion

            InitializeComponent();
            Update();//系统更新
        }

        //捕获按键
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!this.Enabled)
            {
                e.Handled = true;
                return;
            }
            switch (e.KeyChar)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    foreach (ListViewItem lvi in this.listViewMenu.Items)
                    {
                        if (lvi.Tag.ToString() == e.KeyChar.ToString())
                        {
                            lvi.Focused = true;
                            this.listViewMenu.FocusedItem.Selected = true;
                            ExecutePro(lvi.Tag.ToString());
                        }
                    }
                    e.Handled = true;
                    break;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.Enabled)
            {
                e.Handled = true;
                return;
            }
            switch (e.KeyCode)
            {
                //处理功能按键F1-F4，对应界面底部的四个按钮
                case Keys.F1:
                    this.button1_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.Escape:
                case Keys.F5:
                    this.button5_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.F11:
                case Keys.Return:
                    button1_Click(null, null);
                    e.Handled = true;
                    return;
            }
        }

        //捕获listViewMenu双击事件
        private void listViewMenu_ItemActivate(object sender, EventArgs e)
        {
            if (this.listViewMenu.FocusedItem != null)
                ExecutePro(this.listViewMenu.FocusedItem.Tag.ToString());
        }

        //按钮点击事件
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listViewMenu.FocusedItem != null)
            {
                this.listViewMenu.FocusedItem.Selected = true;
                ExecutePro(this.listViewMenu.FocusedItem.Tag.ToString());
            }
            this.listViewMenu.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确实要注销吗？", "注销当前用户", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                == DialogResult.Yes)
            {
                if (frmLogon.ShowDialog() == DialogResult.Cancel)
                {
                    this.Close();//退出系统
                }
                else
                {
                    Update();
                    this.listViewMenu.Focus();
                }

            }
        }

        //处理按键、listViewMenu双击事件、按钮点击事件，打开对应的功能界面
        private void ExecutePro(string opType)
        {
            if (this.listViewMenu.Enabled == false)
                return;
            switch (opType)
            {
                case "1"://菜品销售
                    frmSale.ShowDialog();
                    break;

                case "2"://报表查询
                    frmQuery.ShowDialog();
                    break;

                case "3"://更新
                    Update();
                    break;
            }
        }

        private new  void Update()
        {
            string msg;

            ShowWaitMsg("正在检查系统更新");
            #region 系统更新
            OnlineDataSync();
            #endregion

            ShowWaitMsg("正在更新系统参数");
            #region 读取远程参数
            if (!TransModule.GetConfig(out msg))
            {
                MessageBox.Show("下载系统参数出错：" + msg + "\r\n请联系管理员", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            #endregion

            ShowWaitMsg("正在更新菜品信息");
            #region 读取菜品
            if (!TransModule.GetGoods(out msg))
            {
                MessageBox.Show("下载菜品信息错误！");
            }
            else
            {
                frmSale.RefreshGoodsMenu();
            }
            #endregion

            HideWaitMsg();
        }

        private void FrmMain_Closing(object sender, CancelEventArgs e)
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            SetFullScreen(false, ref  rectangle);
        }

        /// <summary>
        /// 系统更新
        /// </summary>
        public void OnlineDataSync()
        {
            int isUpdate = 0;
            string msg;
            try
            {
                isUpdate = TransModule.CheckUpdate(out msg);
                if (isUpdate==1)
                {
                    //更新引导程序
                    string path = PubGlobal.Envionment.UpdateFolderPath + PubGlobal.Envionment.UPDATE_EXE_FILE_NAME;
                    if (File.Exists(path))
                    {
                        File.Delete(PubGlobal.Envionment.UpdateAppPath);
                        File.Move(path,PubGlobal.Envionment.UpdateAppPath);
                        File.Delete(path);
                    }
                    //关闭本程序,启动更新程序
                    //更新完成后，关闭更新程序，启动主程序
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = PubGlobal.Envionment.UpdateAppPath;

                    p.Start();
                    //关闭该程序
                    HideWaitMsg();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            HideWaitMsg();
        }
    }
}

