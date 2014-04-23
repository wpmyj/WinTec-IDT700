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
            this.label2.Text = "正在清除注册信息及业务数据，请稍候．．．";
            Application.DoEvents();
            //清除数据
            bool ok = new Action.Sqlite.SqliteConfigAction(config.SqliteConnStr).sysReSet();
            if (ok)
            {

                //完成
                this.label2.Text = "操作已成功！请重启本机以重新进行设备注册．";
            }
            else
            {
                //未完成
                this.label2.Text = "操作失败！请重新初始化．";
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
                //处理功能按键F1-F4，对应界面底部的四个按钮
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

