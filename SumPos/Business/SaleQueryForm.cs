using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SumPos.Business
{
    public partial class SaleQueryForm : BaseClass.BaseForm
    {
        public Model.Config config;

        public SaleQueryForm()
        {
            ShowWaitMsg("正在统计数据...");
            InitializeComponent();
        }

        private void SaleQueryForm_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Today;
            this.dateTimePicker2.Value = DateTime.Today;
            this.button1_Click(null, null);
        }

        private void SaleQueryForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                case Keys.F1://统计
                    this.button1_Click(null, null);
                    break;
                case Keys.F2://开始日期
                    this.button2_Click(null, null);
                    break;
                case Keys.F3://结束日期
                    this.button3_Click(null, null);
                    break;
                case Keys.F4://打印
                    this.button4_Click(null, null);
                    break;
                case Keys.Escape:
                case Keys.F5://返回
                    this.button5_Click(null, null);
                    break;
                case Keys.Up:
                    UpRow(this.dataGrid1);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    DownRow(this.dataGrid1, saleHzRpt.Count);
                    e.Handled = true;
                    break;
            }
        }

        List<Model.SaleHzRpt> saleHzRpt;

        private void BindingDataGrid()
        {
            this.dataGrid1.DataSource = null;
            this.dataGridTableStyle1.MappingName = saleHzRpt.GetType().Name;
            this.dataGrid1.DataSource = saleHzRpt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ShowWaitMsg("正在统计数据...");

            saleHzRpt = null;
            saleHzRpt=new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).listSaleHzRpt(dateTimePicker1.Value.ToString("yyyy-MM-dd"),dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            BindingDataGrid();
            if (this.dataGrid1.VisibleRowCount > 0)
            {
                this.dataGrid1.Select(0);
            }
            HideWaitMsg();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dateTimePicker1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dateTimePicker2.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
             short sta = WintecIDT700.ReadPrinterState();
            //if (sta != 0)
            //{
            //    if (sta == 1)
            //        MessageBox.Show("打印机缺纸！");
            //    else if (sta == 2)
            //        MessageBox.Show("打印机温度过高！");
            //}
            //else
            //{
                ShowWaitMsg("正在打印，请稍候...");
                //string[] printHeader = new string[6];
                //printHeader[0] = "           销售统计 ";
                //printHeader[1] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
                //printHeader[2] = this.dateTimePicker1.Value.ToString("yyyy-MM-dd") 
                //    + "至" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd");
                //printHeader[1] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
                //printHeader[3] = " 编号    名称    单价 数量 金额 ";
                //printHeader[4] = "－－－－－－－－－－－－－－－－";
                //WintecSmart.WintecIDT700.PrintLineStr(printHeader, 6, 1);

                //int lines = 0;
                //string qty = "0";
                //string amount = "0";
                //string[] printStr = new string[60];
                //foreach (Model.SellDetail sellDetail in sellDetailLst)
                //{
                //    if (sellDetail.Productid != "合计：")
                //    {
                //        printStr[lines] = sellDetail.Productid.PadRight(6, ' ')
                //                        + (sellDetail.Productname.Length > (30 - sellDetail.Productid.Length) / 2 ?
                //                        sellDetail.Productname.Substring(0, (30 - sellDetail.Productid.Length) / 2).PadRight(10,' ') :
                //                        sellDetail.Productname.PadRight(10, ' '))
                //                        + sellDetail.Price.PadRight(4, ' ')
                //                        + sellDetail.Quantity.PadRight(4, ' ')
                //                        + sellDetail.Amount;
                //        lines += 1;
                //        if (lines == 60)
                //        {
                //            WintecIDT700.PrintLineStr(printStr, 60, 1);
                //            lines = 0;
                //        }
                //    }
                //    else
                //    {
                //        qty = sellDetail.Quantity;
                //        amount = sellDetail.Amount;
                //    }
                //}
                //if (lines > 0)
                //    WintecIDT700.PrintLineStr(printStr, lines, 1);
                //string[] printEnd = new string[3];
                //printEnd[0] = "－－－－－－－－－－－－－－－－";
                //printEnd[1] = "合计                  " + qty.PadRight(4, ' ') + amount;
                //printEnd[2] = "－－－－－－－－－－－－－－－－";
                //WintecIDT700.PrintLineStr(printEnd, 3, 1);

                string[] printHeader = new string[1];
                printHeader[0] = "           销售统计 ";
                PrintOneLine(printHeader, true);
                printHeader[0] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
                PrintOneLine(printHeader, true);
                printHeader[0] = this.dateTimePicker1.Value.ToString("yyyy-MM-dd")
                    + "至" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd");
                PrintOneLine(printHeader, true);
                printHeader[0] = "－－－－－－－－－－－－－－－－";
                PrintOneLine(printHeader, false);
                printHeader[0] = " 编号    名称     单价 数量 金额 ";
                PrintOneLine(printHeader, false);
                printHeader[0] = "－－－－－－－－－－－－－－－－";
                PrintOneLine(printHeader, false);

                int qty = 0;
                float total = 0.00f;
                string[] printStr = new string[60];

                foreach (Model.SaleHzRpt rpt in saleHzRpt)
                {
                    qty = qty + rpt.Qty;
                    total = total + rpt.Total;    
                    printStr[0] = rpt.InCode.PadRight(6, ' ')
                                    + (rpt.FName.Length > 6 ?
                                    rpt.FName.Substring(0, 6).PadRight(6, '　') :
                                    rpt.FName.PadRight(6, '　'))
                                    + rpt.Price.ToString("F2").PadLeft(4, ' ')
                                    + rpt.Qty.ToString().PadLeft(3, ' ')+" "
                                    + rpt.Total.ToString("F2").PadLeft(4, ' ');
                    PrintOneLine(printStr, true);

                }
                
                string[] printEnd = new string[1];
                printEnd[0] = "－－－－－－－－－－－－－－－－";
                PrintOneLine(printEnd, false);
                printEnd[0] = "合计                    " + qty.ToString().PadLeft(2, ' ')+"  " + total.ToString("F2").PadLeft(2, ' ');
                PrintOneLine(printEnd, false);
                printEnd[0] = "－－－－－－－－－－－－－－－－";
                PrintOneLine(printEnd, false);
                WintecIDT700.FeedPaper(200);
                HideWaitMsg();
            //}
        }

        private void PrintOneLine(string[] printStr, bool iFeed)
        {
            WintecIDT700.PrintLineStr(printStr, 1, 1);
            if (iFeed)
            {
                WintecIDT700.FeedPaper(10);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            saleHzRpt.Clear();
            saleHzRpt = null;
            this.Close();
        }

        private void SaleQueryForm_Closed(object sender, EventArgs e)
        {
            this.button1.Click -= new System.EventHandler(this.button1_Click);
            this.button2.Click -= new System.EventHandler(this.button2_Click);
            this.button3.Click -= new System.EventHandler(this.button3_Click);
            this.button4.Click -= new System.EventHandler(this.button4_Click);
            this.button5.Click -= new System.EventHandler(this.button5_Click);
            this.Load -= new System.EventHandler(this.SaleQueryForm_Load);
            this.Closed -= new System.EventHandler(this.SaleQueryForm_Closed);
            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.SaleQueryForm_KeyDown);
        }

        
    }
}

