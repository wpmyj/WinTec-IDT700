using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SumPos.Setting
{
    public partial class ResetForm : BaseClass.BaseForm
    {
        public Model.Config config;
        public ResetForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Visible=false;
            this.button4.Visible = false;
            this.pictureBox3.Visible = false;
            this.label1.Visible = false;
            //this.label2.TextAlign = ContentAlignment.TopCenter;
            this.label2.Text = "�������ע����Ϣ��ҵ�����ݣ����Ժ򣮣���";
            Application.DoEvents();
            //�������
            bool ok = new Action.Sqlite.SqliteConfigAction(config.SqliteConnStr).sysReSet();
            if (ok)
            {

                //���
                this.label2.Text = "�����ѳɹ������������������½����豸ע�ᣮ";
            }
            else
            {
                //δ���
                this.label2.Text = "����ʧ�ܣ������³�ʼ����";
            }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //�����ܰ���F1-F4����Ӧ����ײ����ĸ���ť
                case Keys.F1:
                    if (this.button1.Visible)
                        this.button1_Click(null, null);
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                    break;
                case Keys.Escape:
                case Keys.F4:
                    if (this.button4.Visible)
                        this.button4_Click(null, null);
                    break;
            }
        }

        private void ResetForm_Closed(object sender, EventArgs e)
        {
            this.button1.Click -= new System.EventHandler(this.button1_Click);
            //this.button2.Click -= new System.EventHandler(this.button2_Click);
            //this.button3.Click -= new System.EventHandler(this.button3_Click);
            this.button4.Click -= new System.EventHandler(this.button4_Click);
            //this.button5.Click -= new System.EventHandler(this.button5_Click);
            this.Closed -= new System.EventHandler(this.ResetForm_Closed);
            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.ResetForm_KeyDown);
        }

       
    }
}

