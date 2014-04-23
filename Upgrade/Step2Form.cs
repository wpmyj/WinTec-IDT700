using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Upgrade
{
    public partial class Step2Form : Form
    {
        public Step2Form()
        {
            InitializeComponent();
            this.inputPanel1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.textBox1.Text.Length == 0) return;
            //获取商户信息
            this.Enabled = false;
            WaitForm waitForm = new WaitForm("正在从服务器获取商户信息，请稍候...");
            waitForm.Show();
            Application.DoEvents();

            DataTable dt = new DataTable();
            try
            {
                dt = GetCustomerInfo();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                waitForm.Close();
                MessageBox.Show("获取商户信息失败！");
                this.Enabled = true;
                this.textBox1.Focus();
                this.textBox1.SelectAll();
                return;
            }

            if (dt.Rows.Count != 0)
            {
                //进入下一步，核对商户信息
                Config.ConfigInfo.CustomerId = dt.Rows[0]["CustomerId"].ToString();
                Config.ConfigInfo.CustomerName = dt.Rows[0]["CustomerName"].ToString();
                Config.ConfigInfo.LinkMan = dt.Rows[0]["Linkman"].ToString();
                Config.ConfigInfo.Phone = dt.Rows[0]["Phone"].ToString();
                Config.ConfigInfo.HistoryDataKeepTime = "30";
                Config.ConfigInfo.EPW = "741590";

                Step3Form step3Form = new Step3Form();
                step3Form.label6.Text = this.textBox1.Text;
                step3Form.label7.Text = dt.Rows[0]["CustomerName"].ToString();
                step3Form.label8.Text = dt.Rows[0]["Linkman"].ToString();
                step3Form.label9.Text = dt.Rows[0]["Phone"].ToString();
                waitForm.Close();
                Application.DoEvents();
                this.Enabled = true;
                DialogResult ret = step3Form.ShowDialog();
                if (ret == DialogResult.Retry)
                {
                    this.textBox1.Focus();
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                waitForm.Close();
                MessageBox.Show("服务器数据库中未找到该商户信息，\r\n请确认您输入的商户编号是否正确！");
                this.Enabled = true;
                this.textBox1.Focus();
                this.textBox1.SelectAll();
            }
        }

        private DataTable GetCustomerInfo()
        {
            string cmdText = "select CustomerId,CustomerName,Linkman,Phone from t_Customer where CustomerID = " + textBox1.Text.Trim();
            SqlConnection conn = new SqlConnection("Data Source=" + Config.ConfigInfo.ServerIP + ";Initial Catalog=FoodPalace;uid=sa;pwd=sa;");
            try
            {
                conn.Open();
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmdText, conn);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandTimeout = 3600;

                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();

                return dt;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        private void Step2Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //处理功能按键F1-F4，对应界面底部的四个按钮
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void Step2Form_Closed(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = false; 
            RealseEvent();
        }

        private void RealseEvent()
        {
            this.button1.Click -= new System.EventHandler(this.button1_Click);
            //this.button2.Click -= new System.EventHandler(this.button2_Click);
            //this.button3.Click -= new System.EventHandler(this.button3_Click);
            this.button4.Click -= new System.EventHandler(this.button4_Click);
            this.Closed -= new System.EventHandler(this.Step2Form_Closed);
            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.Step2Form_KeyDown);
        }
    }
}