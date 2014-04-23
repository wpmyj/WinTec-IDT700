using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace SumPos.Setting
{
    public partial class SystemParamSetForm : BaseClass.BaseForm
    {
        public Model.Config config;

        public SystemParamSetForm()
        {

            InitializeComponent();

        }

        private void SystemParamSetForm_Load(object sender, EventArgs e)
        {
            showConfig();
        }

        /// <summary>
        /// 显示设置
        /// </summary>
        private void showConfig()
        {
            PosNoCBox.Text = (config.PosNo == null ? "" : config.PosNo);
            grpnoTxt.Text = config.Grpno;
            CompanyNameTxt.Text = config.CompanyName == null ? "" : config.CompanyName;
            DeptNameTxt.Text = config.DeptName == null ? "" : config.DeptName;
            ServerAddTxt.Text = config.ServerAdd;
            checkBox1.Checked = config.OffLineSale;
            ManTxtBx.Text = config.PosId;
            dataSaveDaysCmTxt.Text = config.DataSaveDays.ToString();
            CzkTypeCBox.SelectedIndex = (int)config.CzkType;
            printBillCkbox.Checked = config.PrintBill == Model.PrintBillFlag.打印 ? true : false;
            readCardModeChkBx.Checked = config.ReadCardMode == Model.ReadCardByHand.允许手动 ? true : false;
        }
        /// <summary>
        /// 点击保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("系统参数设置不正确可能会造成设备不能正常工作，确定要保存吗？", "保存确认",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;

            #region 组装新config model
            config.PosNo = PosNoCBox.Text;
            config.CompanyName = CompanyNameTxt.Text;
            config.DeptName = DeptNameTxt.Text;
            config.ServerAdd = ServerAddTxt.Text;
            config.CzkType = (Model.CzCardType)CzkTypeCBox.SelectedIndex;
            config.PrintBill = printBillCkbox.Checked == true ? Model.PrintBillFlag.打印 : Model.PrintBillFlag.不打印;
            config.ReadCardMode = readCardModeChkBx.Checked == true ? Model.ReadCardByHand.允许手动 : Model.ReadCardByHand.禁止手动;
            config.Str1 = str1TxBx.Text;
            config.Str2 = str2TxBx.Text;
            config.Str3 = str3TxBx.Text;
            config.DataSaveDays = int.Parse(dataSaveDaysCmTxt.Text);
            config.OffLineSale = checkBox1.Checked;
            
            #endregion


            try
            {
                WebService.Url = config.WebServerUrl;//更新webservice地址



                #region 验证终端有效性
                ShowWaitMsg("正在验证终端有效性....");

                if (!WebService.chkInfo(config.PosNo, config.PosId))
                {
                    MessageBox.Show("同步失败！本终端未注册！请联系管理员！");
                    return;
                }
                #endregion

                #region 更新系统参数
                ShowWaitMsg("正在同步系统参数....");
                Model.Config cfgtmp = WebService.syncCfg(config.PosNo);
                config.Grpno = cfgtmp.Grpno;
                config.CompanyName = cfgtmp.CompanyName;
                config.DeptName = cfgtmp.DeptName;

                showConfig();

                ShowWaitMsg("正在保存系统参数....");
                bool ok = new Action.Sqlite.SqliteConfigAction(config.SqliteConnStr).saveConfig(config);
                if (ok)
                {
                    MessageBox.Show("系统参数保存成功！");
                }
                else
                {
                    MessageBox.Show("保存失败！");
                }

                #endregion
            }
            catch
            {
                MessageBox.Show("同步失败！");
            }
            finally
            {
                //this.Show();
                HideWaitMsg();
            }




        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = false;
            this.Close();
        }

        private void SystemParamSetForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //处理功能按键F1-F4，对应界面底部的四个按钮
                case Keys.F1:
                    this.button1_Click(null, null);
                    break;
                case Keys.F2:
                    this.button2_Click(null, null);
                    break;
                case Keys.F3:
                    this.button3_Click(null, null);
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F4:
                    this.button4_Click(null, null);
                    break;
                case Keys.Enter:
                    Keys_Enter();
                    break;

            }
        }

        /// <summary>
        /// 处理回车事件
        /// </summary>
        private void Keys_Enter()
        {
            if (PosNoCBox.Focused)
            {
                CompanyNameTxt.Focus();
                CompanyNameTxt.SelectAll();
            }
            else if (CompanyNameTxt.Focused)
            {
                DeptNameTxt.Focus();
                DeptNameTxt.SelectAll();
            }
            else if (DeptNameTxt.Focused)
            {
                ServerAddTxt.Focus();
                ServerAddTxt.SelectAll();
            }
            else if (ServerAddTxt.Focused)
            {
                CzkTypeCBox.Focus();
            }
            else if (CzkTypeCBox.Focused)
            {
                printBillCkbox.Focus();
            }
        }


        private void SystemParamSetForm_Closed(object sender, EventArgs e)
        {
            this.button1.Click -= new System.EventHandler(this.button1_Click);
            //this.button2.Click -= new System.EventHandler(this.button2_Click);
            //this.button3.Click -= new System.EventHandler(this.button3_Click);
            this.button4.Click -= new System.EventHandler(this.button4_Click);
            //this.button5.Click -= new System.EventHandler(this.button5_Click);
            this.Load -= new System.EventHandler(this.SystemParamSetForm_Load);
            this.Closed -= new System.EventHandler(this.SystemParamSetForm_Closed);
            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.SystemParamSetForm_KeyDown);
        }

        private void PosNoCBox_LostFocus(object sender, EventArgs e)
        {
            if (PosNoCBox.Text.Length != 3)
            {
                MessageBox.Show("终端号必须为3位！请重新输入！");
                PosNoCBox.Focus();
                PosNoCBox.SelectAll();
            }
        }

        private void ManTxtBx_GotFocus(object sender, EventArgs e)
        {

            for (int i = 0; i < this.inputPanel1.InputMethods.Count; i++)
            {
                if (this.inputPanel1.InputMethods[i].Name == "拼音输入")
                    this.inputPanel1.CurrentInputMethod = this.inputPanel1.InputMethods[i];
            }

            this.inputPanel1.Enabled = true;
        }

        private void str1TxBx_GotFocus(object sender, EventArgs e)
        {

            for (int i = 0; i < this.inputPanel1.InputMethods.Count; i++)
            {
                if (this.inputPanel1.InputMethods[i].Name == "拼音输入")
                    this.inputPanel1.CurrentInputMethod = this.inputPanel1.InputMethods[i];
            }

            this.inputPanel1.Enabled = true;
        }

        private void str2TxBx_GotFocus(object sender, EventArgs e)
        {

            for (int i = 0; i < this.inputPanel1.InputMethods.Count; i++)
            {
                if (this.inputPanel1.InputMethods[i].Name == "拼音输入")
                    this.inputPanel1.CurrentInputMethod = this.inputPanel1.InputMethods[i];
            }

            this.inputPanel1.Enabled = true;
        }

        private void str3TxBx_GotFocus(object sender, EventArgs e)
        {

            for (int i = 0; i < this.inputPanel1.InputMethods.Count; i++)
            {
                if (this.inputPanel1.InputMethods[i].Name == "拼音输入")
                    this.inputPanel1.CurrentInputMethod = this.inputPanel1.InputMethods[i];
            }

            this.inputPanel1.Enabled = true;
        }

        private void ManTxtBx_LostFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = false;
        }

        private void str1TxBx_LostFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = false;
        }

        private void str2TxBx_LostFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = false;
        }

        private void str3TxBx_LostFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = false;
        }

        private void dataSaveDaysCmTxt_LostFocus(object sender, EventArgs e)
        {
            try
            {
                int i = int.Parse(dataSaveDaysCmTxt.Text);
            }
            catch
            {
                MessageBox.Show("请输入正确天数！");
            }
        }

        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 申请注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            
        }

    }
}

