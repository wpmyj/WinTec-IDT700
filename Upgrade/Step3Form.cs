using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace Upgrade
{
    public partial class Step3Form : Form
    {
        public Step3Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WaitForm waitForm = new WaitForm("���ڱ����豸ע����Ϣ�����Ժ�...");
            waitForm.Show();
            Application.DoEvents();
            
            //����
            //�����̻���Ϣ��������ip��ַ������汾
            try
            {
                DAL.ConfigDAL.Update(Config.ConfigInfo);
                waitForm.Close();
                Application.DoEvents();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                button1.Visible = false;
                button4.Visible = false;
                MessageBox.Show("����ʧ�ܣ��������豸����ע�ᣡ");
                waitForm.Close();
                Application.DoEvents();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void Step3Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //�����ܰ���F1-F4����Ӧ����ײ����ĸ���ť
                case Keys.Return:
                case Keys.F1:
                    if (this.button1.Visible == true)
                        this.button1_Click(null, null);
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                    break;
                case Keys.Escape:
                case Keys.F4:
                    this.button4_Click(null, null);
                    break;
            }
        }

        private void Step3Form_Closed(object sender, EventArgs e)
        {
            RealseEvent();
        }

        private void RealseEvent()
        {
            this.button1.Click -= new System.EventHandler(this.button1_Click);
            //this.button2.Click -= new System.EventHandler(this.button2_Click);
            //this.button3.Click -= new System.EventHandler(this.button3_Click);
            this.button4.Click -= new System.EventHandler(this.button4_Click);
            this.Closed -= new System.EventHandler(this.Step3Form_Closed);
            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.Step3Form_KeyDown);
        }
    }
}