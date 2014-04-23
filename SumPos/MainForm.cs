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
using System.Data.SqlClient;
namespace SumPos
{
    public partial class MainForm : BaseClass.BaseForm
    {
        /// <summary>
        /// 当前登陆用户
        /// </summary>
        public Model.User user;

        public Model.Config config;

        public MainForm()
        {
            InitializeComponent();
        }

        //选中第一个菜单项
        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.Enabled = false;
            #region 同步
            /*
            Thread dataSyncThread = new Thread(new ThreadStart(DataSyncStart));
            dataSyncThread.Priority = ThreadPriority.Lowest;
            dataSyncThread.IsBackground = false;
             */
            if (MessageBox.Show("是否同步数据？！", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {

                DataSyncStart();
            }
            #endregion

        }

        //捕获按键
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
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
                case '#':
                case '.':
                    foreach (ListViewItem lvi in this.listViewMenu.Items)
                    {
                        if (lvi.Tag.ToString() == e.KeyChar.ToString())
                        {
                            lvi.Focused = true;
                            this.listViewMenu.FocusedItem.Selected = true;
                            ExecutePro(lvi.Tag.ToString());
                        }
                    }
                    break;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //处理功能按键F1-F4，对应界面底部的四个按钮
                case Keys.F1:
                    this.button1_Click(null, null);
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                    break;
                case Keys.Escape:
                case Keys.F4:
                    this.button4_Click(null, null);
                    break;
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

        private void button4_Click(object sender, EventArgs e)
        {
            if(new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).checkFlowUpload())
            {
                if (MessageBox.Show("有未上传销售数据，是否现在上传？！",null,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    DataSyncStart();
                }
                
            }
            this.Owner.Show();
            //this.Dispose();
            this.Close();
        }

        //处理按键、listViewMenu双击事件、按钮点击事件，打开对应的功能界面
        private void ExecutePro(string opType)
        {
            if (this.listViewMenu.Enabled == false)
                return;
            switch (opType)
            {
                case "1":
                    //销售
                    Business.SaleForm saleForm = new Business.SaleForm(config);
                    saleForm.config = config;
                    saleForm.user = user;
                    saleForm.ShowDialog();
                    saleForm.Dispose();
                    break;
                case "2":
                    //销售统计
                    Business.SaleQueryForm saleQueryForm = new SumPos.Business.SaleQueryForm();
                    saleQueryForm.config = config;
                    saleQueryForm.ShowDialog();
                    saleQueryForm.Dispose();
                    break;
                case "3":
                    ////系统设置
                    //Setting.MenuForm settingMenuForm = new SumPos.Setting.MenuForm();
                    //settingMenuForm.config = config;
                    //settingMenuForm.user = user;
                    //settingMenuForm.ShowDialog();
                    //settingMenuForm.Dispose();
                    //break;
                case "#":
                    //数据同步
                    DataSyncStart();
                    break;

            }
        }

        bool isDataSyncing = false;

        /// <summary>
        /// 进行数据同步
        /// </summary>
        private void DataSyncStart()
        {

                Application.DoEvents();

                bool ok = false;//操作是否成功标志
                Cursor.Current = Cursors.WaitCursor;
                Cursor.Show();

                try
                {
                    #region 验证终端有效性
                    ShowWaitMsg("正在验证终端有效性....");
                    ok=WebService.chkInfo(config.PosNo,config.PosId);
                    if (!ok)
                    {
                        MessageBox.Show("同步失败！本终端未注册！请联系管理员！");

                        isDataSyncing = false;
                        this.Show();
                        Cursor.Current = Cursors.Default;
                        Cursor.Hide();
                        HideWaitMsg();
                        this.listViewMenu.Focus();

                        return;
                    }
                    #endregion

                    #region 更新系统参数
                    ShowWaitMsg("正在同步系统参数....");
                    Model.Config cfgtmp = WebService.syncCfg(config.PosNo);
                    config.Grpno = cfgtmp.Grpno;
                    config.CompanyName = cfgtmp.CompanyName;
                    config.DeptName = cfgtmp.DeptName;
                    new Action.Sqlite.SqliteConfigAction(config.SqliteConnStr).saveConfig(config);
                    #endregion



                    #region 获取用户列表
                    ShowWaitMsg("正在同步用户列表....");
                    List<Model.User> userList = WebService.syncUser(config.Grpno);
                    #endregion

                    if (userList.Count > 0)
                    {
                        #region 保存用户列表
                        ok = new Action.Sqlite.SqliteUserAction(config.SqliteConnStr).saveUser(userList);
                        if (!ok)
                        {
                            MessageBox.Show("用户列表更新失败！");
                        }
                        #endregion
                    }
                    #region 获取商品列表
                    ShowWaitMsg("正在同步商品列表...");
                    List<Model.Spxinxi> spxinxiList = WebService.syncSpxinxi(config.Grpno);
                    #endregion

                    if (spxinxiList.Count > 0)
                    {
                        #region 保存商品列表
                        ok = new Action.Sqlite.SqliteSpxinxiAction(config.SqliteConnStr).saveSpxinxi(spxinxiList);
                        if (!ok)
                        {
                            MessageBox.Show("商品信息同步失败！");
                        }
                        #endregion
                    }


                    ShowWaitMsg("正在上传销售数据....");
                    #region 获取未上传销售数据
                    List<Model.SaleFlow> upLoadSaleFlow = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getSaleFlowNoUpLoad();
                    List<Model.PayFlow> upLoadPayFlow = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getPayFlowNoUpload();
                    #endregion

                    if (upLoadSaleFlow.Count > 0 || upLoadPayFlow.Count > 0)
                    {
                        #region 上传销售数据
                        ok = WebService.syncFlow(upLoadSaleFlow, upLoadPayFlow);
                        #endregion


                        if (ok)
                        {

                            #region 更新本地上传标志
                            ok = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).updateFlowFlag();
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("销售数据上传失败！");
                        }
                    }

                    #region 删除过期数据
                    ShowWaitMsg("正在删除过期数据...");
                    ok = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).clearFlowFrom(DateTime.Now.AddDays(0 - config.DataSaveDays).ToString("yyyy-MM-dd"));
                    #endregion

                    if (!ok)
                    {
                        MessageBox.Show("删除过期数据失败！");
                    }
                    this.Enabled = true;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数据同步失败！");
                }
                finally
                {

                    isDataSyncing = false;
                    this.Show();
                    Cursor.Current = Cursors.Default;
                    Cursor.Hide();
                    HideWaitMsg();
                    this.listViewMenu.Focus();
                }
            //this.listViewMenu.Items[0].Focused = true;
            //this.listViewMenu.Items[0].Selected = true;
        }
    }
}

