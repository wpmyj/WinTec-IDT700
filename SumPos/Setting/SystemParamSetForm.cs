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
            PosNoCBox.Text = (config.PosNo==null?"":config.PosNo);
            CompanyNameTxt.Text = config.CompanyName==null?"":config.CompanyName;
            CustomerNameTxt.Text = config.CustomerName==null?"":config.CustomerName;
            ServerAddTxt.Text = config.ServerAdd;
            ManTxtBx.Text = config.NetMac;
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
            Model.Config newConfig = new Model.Config();
            newConfig.PosNo = PosNoCBox.Text;
            newConfig.CompanyName = CompanyNameTxt.Text;
            newConfig.CustomerName = CustomerNameTxt.Text;
            newConfig.ServerAdd = ServerAddTxt.Text;
            newConfig.CzkType = (Model.CzCardType)CzkTypeCBox.SelectedIndex;
            newConfig.PrintBill = printBillCkbox.Checked == true ? Model.PrintBillFlag.打印 : Model.PrintBillFlag.不打印;
            newConfig.ReadCardMode = readCardModeChkBx.Checked == true ? Model.ReadCardByHand.允许手动 : Model.ReadCardByHand.禁止手动;
            newConfig.Str1 = str1TxBx.Text;
            newConfig.Str2 = str2TxBx.Text;
            newConfig.Str3 = str3TxBx.Text;
            
            #endregion



            ShowWaitMsg("正在保存数据,请稍候...");
            bool ok = new Action.Sqlite.SqliteConfigAction(config.SqliteConnStr).saveConfig(newConfig);
            HideWaitMsg();
            if (ok)
            {
                MessageBox.Show("保存成功！重启设备后生效！");
            }
            else
            {
                MessageBox.Show("保存失败！");
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
                    break;
                case Keys.F3:
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
                CustomerNameTxt.Focus();
                CustomerNameTxt.SelectAll();
            }
            else if (CustomerNameTxt.Focused)
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

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            
            for (int i = 0; i < this.inputPanel1.InputMethods.Count; i++)
            {
                if (this.inputPanel1.InputMethods[i].Name == "拼音输入")
                    this.inputPanel1.CurrentInputMethod = this.inputPanel1.InputMethods[i];
            }
            
            this.inputPanel1.Enabled = true;
        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = false;
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
            this.ServerAddTxt.GotFocus -= new System.EventHandler(this.textBox1_GotFocus);
            this.ServerAddTxt.LostFocus -= new System.EventHandler(this.textBox1_LostFocus);
        }

        private void PosNoCBox_LostFocus(object sender, EventArgs e)
        {
            if (PosNoCBox.Text.Length != 4)
            {
                MessageBox.Show("终端号必须为4位！请重新输入！");
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

    }
}

