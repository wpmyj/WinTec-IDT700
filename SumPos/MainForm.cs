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

        private WebService ser;

        public MainForm()
        {
            InitializeComponent();
        }

        //选中第一个菜单项
        private void MainForm_Load(object sender, EventArgs e)
        {

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
                    //充值统计
                    Business.ChongZhiQueryForm chzhQueryForm = new SumPos.Business.ChongZhiQueryForm();
                    chzhQueryForm.config = config;
                    chzhQueryForm.ShowDialog();
                    chzhQueryForm.Dispose();
                    break;

                case "4":
                    //系统设置
                    Setting.MenuForm settingMenuForm = new SumPos.Setting.MenuForm();
                    settingMenuForm.config = config;
                    settingMenuForm.user = user;
                    settingMenuForm.ShowDialog();
                    settingMenuForm.Dispose();
                    break;

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
            
        }

    }
}

