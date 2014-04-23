using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model;
namespace SumPos.Business
{
    public partial class EndBusinessForm : Form
    {
        float xftotal, payTotal, remainTotal, change;
        Model.PayType payType = PayType.现金;//付款方式

        public List<Model.PayFlow> payflowList = new List<PayFlow>();
        /// <summary>
        /// 销售标志 true：销售   false：退货
        /// </summary>
        bool isSaleReturn;
        public bool ok = false;
        public EndBusinessForm(float xftotal, bool isSaleReturn)
        {
            InitializeComponent();
            initEForm(xftotal, isSaleReturn);
 
        }

        /// <summary>
        /// 初始化窗口
        /// </summary>
        /// <param name="xftotal"></param>
        /// <param name="isSaleReturn"></param>
        public void initEForm(float xftotal, bool isSaleReturn)
        {
            payflowList.Clear();
            this.KeyPreview = true;
            this.isSaleReturn = isSaleReturn;
            this.xftotal = xftotal;
            if (isSaleReturn)
            {
                label2.Text = "退货合计";
                label1.Text = "退款金额";
            }
            remainTotal = xftotal;
            change = payTotal - xftotal;



            //showForm();
            payTotalTxt.Text = xftotal.ToString("f2");
            remainTotalTxt.Text = "0.00";
            changeTxt.Text = "0.00";
            xfTotalTxt.Text = xftotal.ToString("f2");
            payTotalTxt.Focus();
            payTotalTxt.SelectAll();
        }

        /// <summary>
        /// 显示窗口内容
        /// </summary>
        public void showForm()
        {
            xfTotalTxt.Text = xftotal.ToString("f2");
            payTotalTxt.Text = payTotal.ToString("f2");
            remainTotalTxt.Text = remainTotal.ToString("f2");
            changeTxt.Text = change.ToString("f2");
            label3.Text = payType.ToString() + "支付";
            payTotalTxt.Focus();
            if (remainTotal > 0)
            {
                payTotalTxt.SelectAll();
                payTotalTxt.ReadOnly = false;
                payTotalTxt.ForeColor = Color.White;
            }
            if (remainTotal == 0)
            {
                payTotalTxt.Select(0, 0);
                payTotalTxt.ReadOnly = true;
                payTotalTxt.ForeColor = Color.Yellow;

            }
        }

        private void EndBusinessForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (payTotalTxt.ReadOnly)
                    {
                        remainTotal = xftotal;
                        payTotal = 0;
                        change = payTotal - xftotal;
                        payType = PayType.现金;
                        showForm();
                    }
                    else
                    {
                        ok = false;
                        payType = PayType.现金;
                        payflowList.Clear();
                        this.Close();
                    }
                    break;
                case Keys.Enter:
                    if (this.payTotalTxt.Focused)
                    {
                        if (remainTotal >0)
                        {
                            //支付金额不足
                            try
                            {
                                float payje = float.Parse(payTotalTxt.Text);
                                if (payje <= xftotal + 100)
                                {
                                    payTotal = payje;
                                    remainTotal = xftotal - payTotal;
                                    if (xftotal - payTotal <= 0)
                                    {
                                        remainTotal = 0;
                                    }
                                    else
                                    {
                                        remainTotal = xftotal - payTotal;
                                    }
                                    change = payTotal - xftotal;

                                }
                                else
                                {
                                    MessageBox.Show("输入金额过大！请重新输入！");

                                }
                            }
                            catch
                            {
                                MessageBox.Show("输入金额不正确！请重新输入！");
                            }
                            finally
                            {
                                showForm();
                            }
                        }
                        else
                        {
                            //支付金额足够
                            savePayflow();
                            ok = true;
                            this.Close();
                        }

                    }
                    break;
                case Keys.F8:
                    if (payTotalTxt.ReadOnly == false)
                    {
                        if (isSaleReturn)
                        {
                            MessageBox.Show("退货不允许使用银行卡！");
                            break;
                        }

                        payType = PayType.银行卡;
                        payTotal = xftotal;
                        remainTotal = 0;
                        change = 0;
                        showForm();
                    }
                    break;
                case Keys.F9:
                    if (payTotalTxt.ReadOnly == false)
                    {
                        payType = PayType.现金;
                        showForm();
                    }
                    break;
            }
        }

        private void savePayflow()
        {
            Model.PayFlow payflow = new PayFlow();
            payflow.Paypmt = payType;
            payflow.RowNo = 1;
            if (isSaleReturn)
            {
                payflow.Total = 0 - xftotal;
            }
            else
            {
                payflow.Total = xftotal;
            }
            payflow.Ckic =CzCardType.磁卡;
            payflowList.Add(payflow);
        }

        private void payTotalTxt_LostFocus(object sender, EventArgs e)
        {
            payTotalTxt.Focus();
            payTotalTxt.SelectAll();
        }

        private void EndBusinessForm_Closed(object sender, EventArgs e)
        {
            this.KeyPreview = false;
        }

    }
}