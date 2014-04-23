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
        /// ��ʾ����
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
            printBillCkbox.Checked = config.PrintBill == Model.PrintBillFlag.��ӡ ? true : false;
            readCardModeChkBx.Checked = config.ReadCardMode == Model.ReadCardByHand.�����ֶ� ? true : false;
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ϵͳ�������ò���ȷ���ܻ�����豸��������������ȷ��Ҫ������", "����ȷ��",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;

            #region ��װ��config model
            config.PosNo = PosNoCBox.Text;
            config.CompanyName = CompanyNameTxt.Text;
            config.DeptName = DeptNameTxt.Text;
            config.ServerAdd = ServerAddTxt.Text;
            config.CzkType = (Model.CzCardType)CzkTypeCBox.SelectedIndex;
            config.PrintBill = printBillCkbox.Checked == true ? Model.PrintBillFlag.��ӡ : Model.PrintBillFlag.����ӡ;
            config.ReadCardMode = readCardModeChkBx.Checked == true ? Model.ReadCardByHand.�����ֶ� : Model.ReadCardByHand.��ֹ�ֶ�;
            config.Str1 = str1TxBx.Text;
            config.Str2 = str2TxBx.Text;
            config.Str3 = str3TxBx.Text;
            config.DataSaveDays = int.Parse(dataSaveDaysCmTxt.Text);
            config.OffLineSale = checkBox1.Checked;
            
            #endregion


            try
            {
                WebService.Url = config.WebServerUrl;//����webservice��ַ



                #region ��֤�ն���Ч��
                ShowWaitMsg("������֤�ն���Ч��....");

                if (!WebService.chkInfo(config.PosNo, config.PosId))
                {
                    MessageBox.Show("ͬ��ʧ�ܣ����ն�δע�ᣡ����ϵ����Ա��");
                    return;
                }
                #endregion

                #region ����ϵͳ����
                ShowWaitMsg("����ͬ��ϵͳ����....");
                Model.Config cfgtmp = WebService.syncCfg(config.PosNo);
                config.Grpno = cfgtmp.Grpno;
                config.CompanyName = cfgtmp.CompanyName;
                config.DeptName = cfgtmp.DeptName;

                showConfig();

                ShowWaitMsg("���ڱ���ϵͳ����....");
                bool ok = new Action.Sqlite.SqliteConfigAction(config.SqliteConnStr).saveConfig(config);
                if (ok)
                {
                    MessageBox.Show("ϵͳ��������ɹ���");
                }
                else
                {
                    MessageBox.Show("����ʧ�ܣ�");
                }

                #endregion
            }
            catch
            {
                MessageBox.Show("ͬ��ʧ�ܣ�");
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
                //�����ܰ���F1-F4����Ӧ����ײ����ĸ���ť
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
        /// ����س��¼�
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
                MessageBox.Show("�ն˺ű���Ϊ3λ�����������룡");
                PosNoCBox.Focus();
                PosNoCBox.SelectAll();
            }
        }

        private void ManTxtBx_GotFocus(object sender, EventArgs e)
        {

            for (int i = 0; i < this.inputPanel1.InputMethods.Count; i++)
            {
                if (this.inputPanel1.InputMethods[i].Name == "ƴ������")
                    this.inputPanel1.CurrentInputMethod = this.inputPanel1.InputMethods[i];
            }

            this.inputPanel1.Enabled = true;
        }

        private void str1TxBx_GotFocus(object sender, EventArgs e)
        {

            for (int i = 0; i < this.inputPanel1.InputMethods.Count; i++)
            {
                if (this.inputPanel1.InputMethods[i].Name == "ƴ������")
                    this.inputPanel1.CurrentInputMethod = this.inputPanel1.InputMethods[i];
            }

            this.inputPanel1.Enabled = true;
        }

        private void str2TxBx_GotFocus(object sender, EventArgs e)
        {

            for (int i = 0; i < this.inputPanel1.InputMethods.Count; i++)
            {
                if (this.inputPanel1.InputMethods[i].Name == "ƴ������")
                    this.inputPanel1.CurrentInputMethod = this.inputPanel1.InputMethods[i];
            }

            this.inputPanel1.Enabled = true;
        }

        private void str3TxBx_GotFocus(object sender, EventArgs e)
        {

            for (int i = 0; i < this.inputPanel1.InputMethods.Count; i++)
            {
                if (this.inputPanel1.InputMethods[i].Name == "ƴ������")
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
                MessageBox.Show("��������ȷ������");
            }
        }

        /// <summary>
        /// ͬ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ����ע��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            
        }

    }
}

