using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SumPos.Business
{
    public partial class ChongZhiQueryForm : BaseClass.BaseForm
    {
        public Model.Config config;

        public ChongZhiQueryForm()
        {
            InitializeComponent();
        }

        List<Model.CzCardChZhRst> chzhFlowList;

        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void BindingDataGrid()
        {
            this.dataGrid1.DataSource = null;
            this.dataGridTableStyle1.MappingName = chzhFlowList.GetType().Name;
            this.dataGrid1.DataSource = chzhFlowList;
        }


        float total = 0;
        int count = 0;

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ShowWaitMsg("正在统计数据...");

            chzhFlowList = null;
            chzhFlowList = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).listChZhFlow(dateTimePicker1.Value, dateTimePicker2.Value);


            total = 0;
            count = 0;
            foreach (Model.CzCardChZhRst flow in chzhFlowList)
            {
                total = total + flow.Czje;
                count++;
            }
            BindingDataGrid();
            HideWaitMsg();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (chzhFlowList != null)
            {
                chzhFlowList.Clear();
            }
            chzhFlowList = null;
            this.Close();
        }

        private void ChongZhiQueryForm_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Today;
            this.dateTimePicker2.Value = DateTime.Today;
            this.button1_Click(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowWaitMsg("正在打印，请稍候...");
            printBill();
            HideWaitMsg();
        }
        private void printBill()
        {
            string[] printStr = new string[13];
            int index = 0;
            printStr[index++] = "           充值统计 ";
            printStr[index++] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            printStr[index++] = this.dateTimePicker1.Value.ToString("yyyy-MM-dd")
                + "至" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd");
            printStr[index++] = string.Empty;
            printStr[index++] = "总金额：" + total.ToString("F2");
            printStr[index++] = string.Empty;
            printStr[index++] = "总笔数：" + count;
            printStr[++index] = string.Empty;
            printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            printStr[++index] = "版权所有@商盟软件  ";
            printStr[++index] = "电话:0532-83817927 ";
            WintecIDT700.PrintLineStr(printStr, printStr.Length, 1);
            WintecIDT700.FeedPaper(200);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dateTimePicker1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dateTimePicker2.Focus();
        }


        private void ChongZhiQueryForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    this.button1_Click(null, null);
                    break;
                case Keys.F2:
                    this.button2_Click(null,null);
                    break;
                case Keys.F3:
                    this.button3_Click(null, null);
                    break;
                case Keys.F4:
                    this.button4_Click(null, null);
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F5:
                    //退出
                    this.Close();
                    break;
                case Keys.Up:
                    UpRow(this.dataGrid1);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    DownRow(this.dataGrid1);
                    e.Handled = true;
                    break;
            }
        }
    }
}

