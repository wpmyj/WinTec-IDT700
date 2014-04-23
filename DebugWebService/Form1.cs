using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DebugWebService
{
    public partial class Form1 : Form
    {
        WebService ser;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<Model.User> list = ser.syncUser();
                dataGridView1.DataSource = list;
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Model.TradeFlow> tradeFlowList = new List<Model.TradeFlow>();
            List<Model.PayFlow> payflowList = new List<Model.PayFlow>();

            Model.TradeFlow tradeFlow1 = new Model.TradeFlow();
            tradeFlow1.Flow_no = "000011111111111111111111001";
            tradeFlow1.Operater = "10002";
            tradeFlow1.Payje = 100;
            tradeFlow1.Change = 0;
            tradeFlow1.Posno = "001";
            tradeFlow1.Qty = 1 ;
            tradeFlow1.Sdate = DateTime.Now.ToString("yyyy-MM-dd");
            tradeFlow1.Squadno = "1";
            tradeFlow1.Stime = DateTime.Now.ToString("HH:mm:ss");
            tradeFlow1.Total = 100;
            tradeFlow1.TradeType = Model.FlowTradeType.销售;
            tradeFlow1.Zkje = 0;
            tradeFlow1.Bcjf = 0;
            tradeFlow1.Scjf = 0;
            tradeFlowList.Add(tradeFlow1);

            Model.TradeFlow tradeFlow2 = new Model.TradeFlow();
            tradeFlow2.Flow_no = "0000002";
            tradeFlow2.Operater = "10002";
            tradeFlow2.Payje = 200;
            tradeFlow2.Change = 0;
            tradeFlow2.Posno = "001";
            tradeFlow2.Qty = 1;
            tradeFlow2.Sdate = DateTime.Now.ToString("yyyy-MM-dd");
            tradeFlow2.Squadno = "1";
            tradeFlow2.Stime = DateTime.Now.ToString("HH:mm:ss");
            tradeFlow2.Total = 200;
            tradeFlow2.TradeType = Model.FlowTradeType.销售;
            tradeFlow2.Zkje = 0;
            tradeFlow2.Bcjf = 0;
            tradeFlow2.Scjf = 0;
            tradeFlowList.Add(tradeFlow2);


            Model.PayFlow payFlow1 = new Model.PayFlow();
            payFlow1.Ckic = Model.CzCardType.磁卡;
            payFlow1.Cardno = "0001";
            payFlow1.Flow_id = 1;
            payFlow1.Flow_no = "0000001";
            payFlow1.Operater = "10002";
            payFlow1.PayAmount = 0;
            payFlow1.Paypmt = Model.FlowPayType.储值卡;
            payFlow1.Posno = "001";
            payFlow1.Scye = 100;
            payFlow1.Sdate = DateTime.Now.ToString("yyyy-MM-dd");
            payFlow1.Squadno = "1";
            payFlow1.Stime = DateTime.Now.ToString("HH:mm:ss");
            payFlow1.Total = 100;
            payFlow1.Tradetype = Model.FlowTradeType.销售;
            payflowList.Add(payFlow1);

            
            Model.PayFlow payFlow2 = new Model.PayFlow();
            payFlow2.Ckic = Model.CzCardType.磁卡;
            payFlow2.Cardno = "0001";
            payFlow2.Flow_id = 1;
            payFlow2.Flow_no = "0000002";
            payFlow2.Operater = "10002";
            payFlow2.PayAmount = 0;
            payFlow2.Paypmt = Model.FlowPayType.储值卡;
            payFlow2.Posno = "001";
            payFlow2.Scye = 200;
            payFlow2.Sdate = DateTime.Now.ToString("yyyy-MM-dd");
            payFlow2.Squadno = "1";
            payFlow2.Stime = DateTime.Now.ToString("HH:mm:ss");
            payFlow2.Total = 200;
            payFlow2.Tradetype = Model.FlowTradeType.销售;
            payflowList.Add(payFlow2);

            try
            {
                bool ok = ser.syncFlow(tradeFlowList, payflowList);

                MessageBox.Show(ok.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        Model.CzCard card;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                card = ser.loadCzkCardByJm(textBox5.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (card.Hyzh !=null)
            {
                textBox1.Text = card.Hykh;
                textBox2.Text = card.CustName;
                textBox3.Text = card.Total.ToString("F2");
                textBox4.Focus();
                textBox4.SelectAll();
            }
            else
            {
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {


            Model.PayFlow payFlow = new Model.PayFlow();
            Model.TradeFlow tradeFlow = new Model.TradeFlow();
            #region 组装tradeflow
            tradeFlow.Flow_no = "aaaaaa";
            tradeFlow.Operater = "super";
            tradeFlow.Payje = float.Parse(textBox4.Text);
            tradeFlow.Change = 0;
            tradeFlow.Posno = "999";
            tradeFlow.Qty = 1;
            tradeFlow.Sdate = DateTime.Now.ToString("yyyy-MM-dd");
            tradeFlow.Squadno = "1";
            tradeFlow.Stime = DateTime.Now.ToString("HH:mm:ss");
            tradeFlow.Total = float.Parse(textBox4.Text);
            tradeFlow.TradeType = Model.FlowTradeType.销售;
            tradeFlow.Zkje = 0;
            tradeFlow.Bcjf = 0;
            tradeFlow.Scjf = 0;
            #endregion

            #region 组装payflow
            payFlow.Ckic = Model.CzCardType.磁卡;
            payFlow.Cardno = textBox5.Text;
            payFlow.Flow_id = 1;
            payFlow.Flow_no = tradeFlow.Flow_no;
            payFlow.Operater = tradeFlow.Operater;
            payFlow.PayAmount = 0;
            payFlow.Paypmt = Model.FlowPayType.储值卡;
            payFlow.Posno = tradeFlow.Posno;
            payFlow.Scye = card.Total;
            payFlow.Sdate = tradeFlow.Sdate;
            payFlow.Squadno = tradeFlow.Squadno;
            payFlow.Stime = tradeFlow.Stime;
            payFlow.Total = tradeFlow.Total;
            payFlow.Tradetype = tradeFlow.TradeType;
            #endregion


            string ok = string.Empty;
            try
            {
                ok = ser.Pay(tradeFlow, payFlow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (ok == "0")
            {
                MessageBox.Show("结算成功，但流水上传失败");
            }
            else if (ok == "1")
            {
                MessageBox.Show("结算成功");
            }
            else
            {
                MessageBox.Show("结算失败");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ser = new WebService(textBox6.Text);
            if (ser.Url == textBox6.Text)
            {
                MessageBox.Show("保存成功");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
