using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SumPos.SumSerivice;
namespace SumPos.Business
{
    /// <summary>
    /// 充值窗口
    /// </summary>
    public partial class ChongZhiForm : BaseClass.BaseForm
    {
        private Model.Config config;
        Model.CzCard card;
        Model.User user;
        public ChongZhiForm(Model.CzCard card,Model.Config config,Model.User user)
        {
            InitializeComponent();
            this.card = card;
            this.config = config;
            this.user = user;
            payJeTxtBx.Focus();
            payJeTxtBx.SelectAll();
        }




        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        bool isPay = false;
        /// <summary>
        /// 点击充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (isPay == false)
            {
                pay();
            }
        }



        private void pay()
        {
            isPay = true;
            float payJe = 0;
            bool error = true;
            try
            {
                payJe = float.Parse(payJeTxtBx.Text);
            }
            catch
            {
                error = false;
            }
            if (payJeTxtBx.Text == string.Empty || payJe == 0 || !error)
            {
                MessageBox.Show("请重新输入充值金额！");
                payJeTxtBx.Focus();
                payJeTxtBx.SelectAll();
                isPay = false;
                return;
            }
            if (payJe >=100000)
            {
                MessageBox.Show("金额过大！请重新输入充值金额！");
                payJeTxtBx.Focus();
                payJeTxtBx.SelectAll();
                isPay = false;
                return;
            }
            if (payJe < 0)
            {
                MessageBox.Show("金额不能为负！请重新输入充值金额！");
                payJeTxtBx.Focus();
                payJeTxtBx.SelectAll();
                isPay = false;
                return;
            }
                Model.CzCardChZhFlow chZhFlow = new Model.CzCardChZhFlow();

                #region 组装czflow
                chZhFlow.IncardNo = card.Hyzh;
                chZhFlow.OutcardNo = card.Hykh;
                chZhFlow.PosNo = config.PosNo;
                chZhFlow.UserCode = user.UserCode;
                chZhFlow.UserName = user.UserName;
                chZhFlow.Bcye = card.Total;
                chZhFlow.Czje = float.Parse(payJeTxtBx.Text);
                #endregion

                try
                {
                    Model.CzCardChZhRst rst = WebService.czkChZh(chZhFlow);
                    if (rst.Rst)
                    {

                        #region 保存本地流水
                        bool ok = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).saveChZhFlow(rst, user);
                        #endregion

                        if (!ok)
                        {
                            MessageBox.Show("保存本地充值流水失败，请联系管理员！");
                        }

                        #region 打印小票
                        if (config.PrintBill == Model.PrintBillFlag.打印)
                        {
                            PrintBill(rst);
                        }
                        #endregion

                        MessageBox.Show("充值成功！\r\n卡号：" + rst.OutCardno + "\r\n充值前余额：" + rst.Scye.ToString("F2") + "元\r\n充值金额：" + rst.Czje.ToString("F2") + "元\r\n当前余额：" + rst.Dqye.ToString("F2") + "元！");
                    }
                    else
                    {
                        MessageBox.Show("充值失败！");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    isPay = false;
                    this.Close();
                }
        }
        private void ChongZhiForm_Load(object sender, EventArgs e)
        {
            cardNoTxtBx.Text = card.Hykh;
            NameTxtBx.Text = card.CustName;
            totalTxtBox.Text = card.Total.ToString("F2");
            
        }

        //打印
        void PrintBill(Model.CzCardChZhRst rst)
        {
            string[] printStr = new string[25];
            int index = 0;
            printStr[++index] = "         " + config.CompanyName;
            printStr[++index] = "店名：" + config.CustomerName;
            printStr[++index] = "单据编号：" + rst.Pzno;
            printStr[++index] = "充值时间时间：" +rst.Sdate;
            printStr[++index] = "机器编号：" + config.PosNo;
            printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            printStr[++index] = "卡号：" + rst.OutCardno;
            printStr[++index] = string.Empty;
            printStr[++index] = "充值前余额："+rst.Scye.ToString("F2")+"元";
            printStr[++index] = string.Empty;
            printStr[++index] = "本次充值：" + rst.Czje.ToString("F2") + "元";
            printStr[++index] = string.Empty;
            printStr[++index] = "卡内现余额：" + rst.Dqye.ToString("F2")+"元";
            printStr[++index] = string.Empty;
            printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            printStr[++index] = config.Str1;
            printStr[++index] = string.Empty;
            printStr[++index] = config.Str2;
            printStr[++index] = string.Empty;
            printStr[++index] = config.Str3;


            WintecIDT700.PrintLineStr(printStr, printStr.Length, 1);
            WintecIDT700.FeedPaper(200);

        }


        //捕获按键事件。
        private void ChongZhiForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.F3:
                    //充值
                    this.button3_Click(null, null);
                    break;
                case Keys.Enter:
                    this.button3_Click(null,null);
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F5:
                    //退出
                    this.Close();
                    break;
            }
        }



    }
}

