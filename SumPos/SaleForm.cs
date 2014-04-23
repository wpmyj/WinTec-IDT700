using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
using SumPos.SumSerivice;

namespace SumPos.Business
{
    public partial class SaleForm : BaseClass.BaseForm
    {
        public Model.Config config;
        public Model.User user;
        private Model.CzCard czCard;
        private List<Model.PayFlow> flowList = new List<Model.PayFlow>();//本次登陆结算记录



        private const int RFCardCom = 2;    //默认COM2
        private const int RFCardBaudRate = 19200;  //波特率:默认19200


        System.Threading.Thread readCardThread;//读卡线程

        IntPtr comHandle = IntPtr.Zero;    //串口句柄
        //定义一个委托
        delegate void HandleInterfaceShowCardNoDelegate(Model.CzCard card);
        //读取射频卡模块返回值。
        HandleInterfaceShowCardNoDelegate interfaceShowCardNoDelegate;

        public SaleForm(Model.Config config)
        {
            ShowWaitMsg("正在打开，请稍候...");
            InitializeComponent();
            this.config = config;
            SumPos.SumSerivice.SumPosWebService a = new SumPosWebService();
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
            //实例化委托对象 
            interfaceShowCardNoDelegate = new HandleInterfaceShowCardNoDelegate(ShowCardNo);
            //打开串口
            comHandle = WintecIDT700.OpenComm(RFCardCom, RFCardBaudRate);
            //启用线程，读取卡号

            readCardThread = new System.Threading.Thread(new System.Threading.ThreadStart(SearchCard));
            readCardThread.Priority = ThreadPriority.Lowest;
            readCardThread.IsBackground = true;
            readCardThread.Start();

            #region 初始化界面
            cardNoTxtBx.Text = string.Empty;
            NameTxtBx.Text = string.Empty;
            totalTxtBox.Text = "0.00";
            BindingDataGrid();
            if (config.ReadCardMode == Model.ReadCardByHand.禁止手动)
            {
                cardNoTxtBx.ReadOnly = true;// true;
            }
            #endregion
            HideWaitMsg();

            //#region 初始化webservice
            //ser = new WebService(config.WebServerUrl);
            //#endregion

            cardNoTxtBx.Focus();


        }

        //bool testbl = false;

        //捕获按键事件。
        private void SaleForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.cardNoTxtBx.Text == "" && this.cardNoTxtBx.Focused == false)
            {
                //如果商品编码文本框为空，则该文本框获得焦点
                this.cardNoTxtBx.Focus();
            }
            switch (e.KeyCode)
            {
                case Keys.F1:
                    //上页
                    this.button1_Click(null, null);
                    break;
                case Keys.F2:
                    //下页
                    this.button2_Click(null, null);
                    break;
                case Keys.F3:
                    //充值
                    this.button3_Click(null, null);
                    break;
                case Keys.F4:
                    //退款
                    this.button4_Click(null, null);
                    break;
                case Keys.F5:
                    //取消
                    this.button5_Click(null, null);
                    break;
                case Keys.F11:
                    //结算
                    break;
                case Keys.Escape:
                    //退出
                    this.Close();
                    break;
                case Keys.Up:
                    UpRow(payFlowDataGrid);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    DownRow(payFlowDataGrid);
                    e.Handled = true;
                    break;
                case Keys.Return:
                    ExecutePro();
                    e.Handled = true;
                    break;
            }
        }



        bool open = true;
        /// <summary>
        /// 读卡线程类
        /// </summary>
        private void SearchCard()
        {
            WintecIDT700.OpenMSR();
            WintecIDT700.Init_msr();
            string hyzh = string.Empty;
            try
            {
                while (open)
                {
                    Thread.Sleep(500);

                    if (czCard == null)
                    {
                        try
                        {
                            if (config.CzkType == Model.CzCardType.磁卡)
                            {
                                //读磁卡
                                hyzh = readCCard();
                            }
                            else
                            {
                                //读射频卡

                                hyzh = readICCard();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("读卡失败，请重新刷卡！");
                        }

                        if (hyzh != string.Empty)
                        {
                            try
                            {
                                #region 记录储值卡信息

                                czCard = WebService.loadCzCardByEnCryStr(hyzh);
                                #endregion

                                if (!czCard.CanVerifyMark)
                                {
                                    WintecIDT700.Beep(100);
                                    Thread.Sleep(50);
                                    WintecIDT700.Beep(100);
                                    Thread.Sleep(50);
                                    WintecIDT700.Beep(100);
                                    MessageBox.Show("卡金额校验失败！请联系管理员！");
                                    czCard = null;
                                    return;
                                }

                                if (czCard.Hyzh == null || czCard.Hyzh == "")
                                {
                                    //检索不到匹配卡
                                    WintecIDT700.Beep(100);
                                    Thread.Sleep(50);
                                    WintecIDT700.Beep(100);
                                    Thread.Sleep(50);
                                    WintecIDT700.Beep(100);
                                    MessageBox.Show("此卡无效！");
                                }
                                else
                                {
                                    //读卡成功
                                    if (czCard.Stat == Model.CzkStat.挂失)
                                    {
                                        MessageBox.Show("此卡已挂失");
                                        czCard = null;
                                        return;
                                    }
                                    else if (czCard.Stat == Model.CzkStat.禁用)
                                    {
                                        MessageBox.Show("此卡已禁用");
                                        czCard = null;
                                        return;
                                    }
                                    else
                                    {
                                        this.BeginInvoke(interfaceShowCardNoDelegate, new Model.CzCard[] { czCard });
                                    }

                                    WintecIDT700.Beep(100);
                                }
                            }
                            catch (Exception ex)
                            {


                                MessageBox.Show("通讯异常，请重新刷卡！");


                                //MessageBox.Show(ex.Message);
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                WintecIDT700.CloseMSR();
            }

        }


        /// <summary>
        /// 读取磁卡
        /// </summary>
        /// <returns></returns>
        private string readCCard()
        {
            string inCardno = string.Empty;
            byte[] data = new byte[266];
            try
            {
                System.Threading.Thread.CurrentThread.Join(100);
                if (WintecIDT700.CheckMSRIO() == 1)
                {
                    if (WintecIDT700.ReadMSRData(data) == 1)
                    {

                        WintecIDT700.Beep(100);

                        //if (ret <= 0)
                        //{
                        inCardno = Encoding.UTF8.GetString(data, 79, 40);
                        inCardno = inCardno.Substring(0, inCardno.IndexOf('\0'));
                        //}
                        //try
                        //{
                        //    inCardno = inCardno.Substring(1, inCardno.IndexOf('?') - 1);
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine(ex.Message);
                        //}
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                WintecIDT700.Init_msr();
            }
            return inCardno;
        }



        /// <summary>
        /// 读射频卡
        /// </summary>
        /// <returns></returns>
        private string readICCard()
        {
            string pwd = string.Empty;
            byte[] pwdbs = new byte[6];

            #region 初始化射频卡密码
            pwd = "FFFFFFFFFFFF";

            for (int i = 0; i < pwdbs.Length; i++)
            {
                pwdbs[i] = Convert.ToByte(pwd.Substring(0, 2), 16);

                pwd = pwd.Substring(2);
            }
            #endregion



            byte block = WintecIDT700.RF_MAKE_BLOCK(1, 0);
            //1扇区，0块
            string cardno = string.Empty;
            byte[] buff = new byte[16];

            cardno = string.Empty;
            try
            {
                #region 寻卡
                int rfid = WintecIDT700.RF_SearchCard(Convert.ToByte(2), 0);
                #endregion

                if (rfid != -1)
                {
                    #region 校验密码
                    int key = WintecIDT700.RF_Authentication(Convert.ToByte(2), 0, block, pwdbs, 0x60);
                    #endregion

                    if (key == 1)
                    {
                        //密码校验成功
                        //读取卡号
                        WintecIDT700.Beep(100);

                        #region 读取数据
                        WintecIDT700.RF_ReadBlock(Convert.ToByte(2), 0, block, buff);
                        #endregion

                        foreach (byte b in buff)
                        {
                            cardno = cardno + Convert.ToString(b, 16);
                        }

                        cardno = cardno.Substring(0, cardno.IndexOf("ffffff"));

                    }
                }
            }
            catch
            {
                throw;
            }
            return cardno;
        }


        /// <summary>
        /// 显示刷卡结果
        /// </summary>
        /// <param name="card"></param>
        private void ShowCardNo(Model.CzCard card)
        {
            if (card != null)
            {
                cardNoTxtBx.Text = card.Hykh;
                NameTxtBx.Text = card.CustName;
                totalTxtBox.Text = card.Total.ToString("F2");
                payJeTxtBx.Focus();
                payJeTxtBx.SelectAll();
            }
            else
            {
                cardNoTxtBx.Text = string.Empty;
                NameTxtBx.Text = string.Empty;
                totalTxtBox.Text = "0.00";
                payJeTxtBx.Text = "0";
                cardNoTxtBx.Focus();
            }
        }


        int height = 50;

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int y = panel8.AutoScrollPosition.Y;
            int x = panel8.AutoScrollPosition.X;
            panel8.AutoScrollPosition = new Point(x, y - height);
            Application.DoEvents();
        }


        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            int y = panel8.AutoScrollPosition.Y + height;
            int x = panel8.AutoScrollPosition.X;
            panel8.AutoScrollPosition = new Point(x, y + height);
            panel8.Refresh();
            Application.DoEvents();
        }

        /// <summary>
        ///充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (czCard != null)
            {
                ChongZhiForm czWin = new ChongZhiForm(czCard, config, user);
                czWin.ShowDialog();
                czWin.Dispose();
                czCard = null;
                ShowCardNo(czCard);
            }
            else
            {
                MessageBox.Show("请先刷卡！");
            }
        }



        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            //if (czCard == null)
            //{
            //    MessageBox.Show("请先刷卡！");
            //    return;
            //}


            /********取消退款模式******/
            if (isTH)
            {
                tkPayflow = null;
                payJeTxtBx.Text = "0";
                payJeTxtBx.Focus();
                payJeTxtBx.SelectAll();
                button4.Text = "退款";
                label3.Text = "消费金额：";
                label3.ForeColor = Color.White;
                czCard = null;
                ShowCardNo(czCard);
                isTH = !isTH;
                return;
            }

            /********进入退款模式******/
            if (!isTH)
            {
                MessageBox.Show("进入退款模式！请输入流水号！");
                Business.SaleReturnForm thForm = new SaleReturnForm();
                thForm.ShowDialog();
                if (thForm.DialogResult == DialogResult.Cancel)
                {
                    //取消退货

                    MessageBox.Show("退货已取消！");
                    return;
                }


                isTH = !isTH;


                czCard = new Model.CzCard();//阻断刷卡线程
                ShowCardNo(czCard);

                //payJeTxtBx.Focus();
                //payJeTxtBx.SelectAll();
                button4.Text = "取消退款";
                label3.Text = "退款金额：";
                label3.ForeColor = Color.Red;

                string thNo = thForm.textBox1.Text;
                tkPayflow = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getPayFlow(thNo);
                if (tkPayflow.Tradetype == Model.FlowTradeType.退货)
                {
                    MessageBox.Show("该流水不能退款！");
                    tkPayflow = null;
                    payJeTxtBx.Focus();
                    payJeTxtBx.SelectAll();
                    button4_Click(null, null);
                    return;
                }
                if (tkPayflow.Flow_no == string.Empty || tkPayflow.Flow_no == null)
                {
                    MessageBox.Show("该流水不存在！");
                    tkPayflow = null;
                    payJeTxtBx.Focus();
                    payJeTxtBx.SelectAll();
                    button4_Click(null, null);
                    return;
                }
                payJeTxtBx.Text = tkPayflow.Total.ToString("F2");
                czCard = null;
                MessageBox.Show("请刷卡....");
            }
        }




        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            initCzkcard();//初始化储值卡显示

        }





        //处理回车键
        private void ExecutePro()
        {
            if (this.cardNoTxtBx.Text == string.Empty)
            {
                this.cardNoTxtBx.Focus();
                return;
            }
            else if (this.cardNoTxtBx.Focused)
            {
                //输入卡号后回车
                if (cardNoTxtBx.Text != string.Empty)
                {
                    Model.CzCard card = WebService.loadCzCardByKh(cardNoTxtBx.Text);

                    if (!card.CanVerifyMark)
                    {
                        WintecIDT700.Beep(100);
                        Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        MessageBox.Show("卡金额校验失败！请联系管理员！");
                        card = null;
                        return;
                    }

                    if (card.Hyzh == null)
                    {
                        WintecIDT700.Beep(100);
                        Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        MessageBox.Show("此卡无效！");
                        cardNoTxtBx.Focus();
                        cardNoTxtBx.SelectAll();
                    }
                    else
                    {
                        //读卡成功
                        if (card.Stat == Model.CzkStat.挂失)
                        {
                            MessageBox.Show("此卡已挂失");
                            return;
                        }
                        else if (card.Stat == Model.CzkStat.禁用)
                        {
                            MessageBox.Show("此卡已禁用");
                            return;
                        }

                        czCard = card;
                        WintecIDT700.Beep(100);
                        ShowCardNo(card);
                        payJeTxtBx.Focus();
                    }

                }
            }
            else if (payJeTxtBx.Focused)
            {
                //输入金额后回车，结算
                //if (czCard != null)
                //{
                //    EndBusiness();
                //    czCard = null;
                //    ShowCardNo(czCard);
                //}
                EndBusiness();


            }

        }



        /// <summary>
        /// 绑定刷卡记录数据源
        /// </summary>
        private void BindingDataGrid()
        {
            this.payFlowDataGrid.DataSource = null;
            this.dataGridTableStyle1.MappingName = flowList.GetType().Name;
            this.payFlowDataGrid.DataSource = flowList;
        }

        bool isTH = false;
        Model.PayFlow tkPayflow;
        bool isPay = false;
        //结算
        private void EndBusiness()
        {




            if (isPay == false)
            {
                isPay = true;
                bool payOk = false;

                Model.PayFlow payFlow;//临时付款流水
                Model.TradeFlow tradeFlow;//临时交易总表流水
                string flow_no = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getFlow_no(config.PosNo);
                float payJe = 0;

                #region 检验交易有效性
                try
                {

                    if (czCard == null)
                    {
                        MessageBox.Show("请先刷卡！");
                        isPay = false;
                        return;
                    }


                    payJe = float.Parse(payJeTxtBx.Text);
                    if (payJeTxtBx.Text.IndexOf('.') != -1)
                    {
                        string[] payStr = payJeTxtBx.Text.Split('.');
                        if (payStr[1].Length == 3)
                        {
                            MessageBox.Show("小数点后只允许输入2位！请重新输入！");
                            isPay = false;
                            payJeTxtBx.Focus();
                            payJeTxtBx.SelectAll();
                            return;
                        }

                    }
                    if (payJe == 0)
                    {
                        MessageBox.Show("金额不能为0！请重新输入！");
                        isPay = false;
                        payJeTxtBx.Focus();
                        payJeTxtBx.SelectAll();
                        isPay = false;
                        return;
                    }

                    if (payJe >= 100000)
                    {
                        MessageBox.Show("金额过大！请重新输入金额！");
                        isPay = false;
                        payJeTxtBx.Focus();
                        payJeTxtBx.SelectAll();
                        return;
                    }
                    if (payJe < 0)
                    {
                        MessageBox.Show("金额不能为负！");
                        isPay = false;
                        payJeTxtBx.Focus();
                        payJeTxtBx.SelectAll();
                        return;
                    }
                    if (isTH)
                    {
                        if (tkPayflow.Cardno != czCard.Hyzh)
                        {
                            MessageBox.Show("此流水消费卡号记录与当前卡号不一致！");
                            isPay = false;
                            czCard = null;
                            ShowCardNo(czCard);
                            payJeTxtBx.Focus();
                            payJeTxtBx.SelectAll();
                            return;
                        }
                        if (payJe > tkPayflow.Total)
                        {
                            MessageBox.Show("退款金额大于交易金额，请重新输入！");
                            isPay = false;
                            payJeTxtBx.Focus();
                            payJeTxtBx.SelectAll();
                            return;
                        }

                    }

                }
                catch
                {
                    MessageBox.Show("输入金额无效！请重新输入！");
                    isPay = false;
                    payJeTxtBx.Focus();
                    payJeTxtBx.SelectAll();
                    return;
                }


                #endregion

                /*********开始交易**************************/
                payFlow = new Model.PayFlow();
                tradeFlow = new Model.TradeFlow();
                short coe = 1;//结算系数，销售为1，退货为-1
                try
                {
                    if (isTH)
                    {
                        //退货
                        coe = -1;
                    }

                    if (float.Parse(payJeTxtBx.Text) <= czCard.Total)
                    {
                        #region 组装tradeflow
                        tradeFlow.Flow_no = flow_no;
                        tradeFlow.Operater = user.UserCode;
                        tradeFlow.Payje = float.Parse(payJeTxtBx.Text) * coe;
                        tradeFlow.Change = 0;
                        tradeFlow.Posno = config.PosNo;
                        tradeFlow.Qty = 1 * coe;
                        tradeFlow.Sdate = DateTime.Now.ToString("yyyy-MM-dd");
                        tradeFlow.Squadno = "1";
                        tradeFlow.Stime = DateTime.Now.ToString("HH:mm:ss");
                        tradeFlow.Total = float.Parse(payJeTxtBx.Text) * coe;
                        tradeFlow.TradeType =isTH? Model.FlowTradeType.退货:Model.FlowTradeType.销售;
                        tradeFlow.Zkje = 0;
                        tradeFlow.Bcjf = 0;
                        tradeFlow.Scjf = 0;
                        tradeFlow.CustCode = czCard.Hykh;
                        tradeFlow.ShopNo = user.Mdept;
                        //tradeFlow.CustFlag = "";
                        #endregion

                        #region 组装payflow
                        payFlow.Ckic = config.CzkType;
                        payFlow.Cardno = czCard.Hyzh;
                        payFlow.Hykh = czCard.Hykh;
                        payFlow.Flow_id = 1;
                        payFlow.Flow_no = tradeFlow.Flow_no;
                        payFlow.Operater = tradeFlow.Operater;
                        payFlow.PayAmount = 0;
                        payFlow.Paypmt = Model.FlowPayType.储值卡;
                        payFlow.Posno = tradeFlow.Posno;
                        payFlow.Scye = czCard.Total;
                        payFlow.Sdate = tradeFlow.Sdate;
                        payFlow.Squadno = tradeFlow.Squadno;
                        payFlow.Stime = tradeFlow.Stime;
                        payFlow.Total = tradeFlow.Total;
                        payFlow.Tradetype = tradeFlow.TradeType;
                        payFlow.Shopno = tradeFlow.ShopNo;
                        #endregion

                        ShowWaitMsg("正在进行结算，请稍侯...");
                        cardNoTxtBx.BackColor = Color.Black;
                        payJeTxtBx.BackColor = Color.Black;
                        Application.DoEvents();
                        #region 远程扣减金额，保存流水
                        string ok = WebService.Pay(tradeFlow, payFlow);
                        #endregion

                        if (ok != "error")
                        {
                            //扣减金额成功
                            tradeFlow.Flag = (Model.FlowUpLoadFlag)int.Parse(ok);
                            payFlow.Flag = tradeFlow.Flag;

                            #region 保存至本地
                            bool success = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).savePay(tradeFlow, payFlow);
                            #endregion

                            if (!success)
                            {
                                MessageBox.Show("刷卡成功，但保存至本地表失败，请联系管理员！");
                            }
                            else
                            {
                                if (config.PrintBill == Model.PrintBillFlag.打印)
                                {
                                    PrintBill(tradeFlow);
                                }
                                //MessageBox.Show("交易成功！");
                                List<Model.PayFlow> payList = new List<Model.PayFlow>();
                                payList.Add(payFlow);
                                payList.AddRange(flowList);
                                flowList = payList;


                                #region 更新流水号
                                string newFlowno = flow_no.Substring(flow_no.Length - 4, 4);
                                int no = int.Parse(newFlowno);
                                no = no + 1;
                                newFlowno = no.ToString();
                                while (newFlowno.Length < 4)
                                {
                                    newFlowno = "0" + newFlowno;
                                }
                                new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).saveFlow_no(newFlowno);
                                #endregion

                                if (isTH)
                                {
                                    #region 更新退货记录
                                    new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).payflowTh(tkPayflow.Flow_no);
                                    #endregion
                                }

                                #region 更新交易列表
                                BindingDataGrid();
                                #endregion

                                #region 更新底端信息显示
                                payQtyHjTxtBx.Text = (int.Parse(payQtyHjTxtBx.Text) + 1).ToString();
                                payHjTxtBx.Text = (float.Parse(payHjTxtBx.Text) + tradeFlow.Total).ToString("F2");
                                #endregion

                            }
                            payOk = true;

                        }
                        else
                        {
                            MessageBox.Show("刷卡失败，请重试！");
                        }
                    }
                    else
                    {
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        MessageBox.Show("您的余额已不足！");
                        payOk = false;

                    }
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    MessageBox.Show("结算出错，请重试！");
                    payOk = false;
                }
                finally
                {
                    isPay = false;
                    if (payOk)
                    {
                        isTH = false;
                        tkPayflow = null;
                        initCzkcard();//初始化储值卡显示
                        HideWaitMsg();
                        cardNoTxtBx.Focus();
                    }
                    else
                    {
                        payJeTxtBx.Focus();
                        payJeTxtBx.SelectAll();
                        HideWaitMsg();
                    }
                    label3.ForeColor = Color.White;
                    label3.Text = "消费金额：";

                    button4.Text = "退款";
                    

                }
            }
        }

        /// <summary>
        /// 初始化储值卡显示
        /// </summary>
        private void initCzkcard()
        {
            WintecIDT700.Init_msr();
            czCard = null;
            ShowCardNo(czCard);
        }

        //打印
        void PrintBill(Model.TradeFlow tradeFlow)
        {
            string[] printStr = new string[25];
            int index = 0;
            //string[] printStr1 = new string[1];
            //for (int i = 0; i < ((8 - AppConfig.ConfigInfo.CompanyName.Trim().Length) / 2); i++)
            //{
            //    printStr1[0] += "　";
            //}
            //printStr1[0] += AppConfig.ConfigInfo.CompanyName;
            //WintecIDT700.PrintLineStr(printStr1, 1, 4);
            //WintecIDT700.FeedPaper(40);

            printStr[++index] = "         " + config.CompanyName;
            printStr[++index] = "店名：" + config.CustomerName;
            printStr[++index] = "单据编号：" + tradeFlow.Flow_no;
            printStr[++index] = "打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            printStr[++index] = "机器编号：" + config.PosNo;
            printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            printStr[++index] = "卡号：" + czCard.Hykh;
            printStr[++index] = string.Empty;
            printStr[++index] = "姓名：" + czCard.CustName;
            printStr[++index] = string.Empty;
            printStr[++index] = "卡内原余额：" + czCard.Total.ToString("F2");
            printStr[++index] = string.Empty;
            printStr[++index] = (isTH?"退款金额：":"消费金额：") + tradeFlow.Total.ToString("F2") + "元";
            printStr[++index] = string.Empty;
            printStr[++index] = "卡内现余额：" + (czCard.Total - tradeFlow.Total).ToString("F2");
            printStr[++index] = string.Empty;
            printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            printStr[++index] = config.Str1;
            printStr[++index] = string.Empty;
            printStr[++index] = config.Str2;
            printStr[++index] = string.Empty;
            printStr[++index] = config.Str3;

            WintecIDT700.PrintLineStr(printStr, printStr.Length, 1);
            WintecIDT700.FeedPaper(200);

            //string[] printStr = new string[1];
            //for (int i = 0; i < ((8 - AppConfig.ConfigInfo.CompanyName.Trim().Length) / 2); i++)
            //{
            //    printStr[0] += "　";
            //}
            //printStr[0] += AppConfig.ConfigInfo.CompanyName;
            //WintecIDT700.PrintLineStr(printStr, 1, 4);
            //WintecIDT700.FeedPaper(40);

            //printStr[0] = "档口：" + AppConfig.ConfigInfo.CustomerName;
            //PrintOneLine(printStr, false);
            //printStr[0] = "单据编号：" + tracenumber + (isSaleReturn == false ? "" : "[退]");
            //PrintOneLine(printStr, false);
            //printStr[0] = "打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //PrintOneLine(printStr, false);
            //printStr[0] = "营业日期：" + DateTime.Now.ToString("yyyy-MM-dd");
            //PrintOneLine(printStr, false);
            //printStr[0] = "机器编号：" + AppConfig.ConfigInfo.PosNo;
            //PrintOneLine(printStr, false);
            //printStr[0] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            //PrintOneLine(printStr, false);
            //printStr[0] = "序号 编号    名称     数量 金额 ";
            //PrintOneLine(printStr, false);
            //printStr[0] = "------------------------------";
            //PrintOneLine(printStr, false);
            //int count = 1;
            //foreach (Model.SellDetail SellDetail in saleTempList)
            //{
            //    printStr[0] =
            //        count.ToString().PadRight(4, ' ') +
            //        SellDetail.Productid.PadRight(6, ' ') +
            //        (SellDetail.Productname.Length > 6 ? SellDetail.Productname.Substring(0, 6).PadRight(6, '　') : SellDetail.Productname.PadRight(6, '　')) +
            //        SellDetail.Quantity.PadLeft(3, ' ') +
            //        SellDetail.Amount.PadLeft(6, ' ');
            //    PrintOneLine(printStr, false);
            //    printStr[0] = "------------------------------";
            //    PrintOneLine(printStr, false);
            //    qty += decimal.Parse(SellDetail.Quantity);
            //    amount += decimal.Parse(SellDetail.Amount);
            //    count++;
            //}
            //printStr[0] = "合计：                   " + SumAmount.ToString().PadLeft(6, ' ');
            //PrintOneLine(printStr, false);
            //printStr[0] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            //PrintOneLine(printStr, true);
            //printStr[0] = "           卡片信息           ";
            //PrintOneLine(printStr, false);
            //WintecIDT700.FeedPaper(20);
            //printStr[0] = "卡    号：" + CardNumber;
            //PrintOneLine(printStr, false);
            //if (!isSaleReturn)
            //{
            //    printStr[0] = "卡 类 型：" + CardTypename;
            //    PrintOneLine(printStr, false);
            //    printStr[0] = "优惠金额：" + Youhuiamount;
            //    PrintOneLine(printStr, false);
            //    printStr[0] = "折 扣 率：" + discount * 100 + "折";
            //    PrintOneLine(printStr, false);
            //    printStr[0] = "卡内原余额：" + txtOriginal.Text.Trim();
            //    PrintOneLine(printStr, false);
            //    printStr[0] = "卡内现余额：" + txtBalance.Text.Trim();
            //    PrintOneLine(printStr, false);
            //}
            //printStr[0] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            //PrintOneLine(printStr, true);
            //if (AppConfig.ConfigInfo.Remark1.Trim() != string.Empty)
            //{
            //    printStr[0] = AppConfig.ConfigInfo.Remark1;
            //    PrintOneLine(printStr, false);
            //}
            //if (AppConfig.ConfigInfo.Remark2.Trim() != string.Empty)
            //{
            //    printStr[0] = AppConfig.ConfigInfo.Remark2;
            //    PrintOneLine(printStr, false);
            //}
            //if (AppConfig.ConfigInfo.Remark3.Trim() != string.Empty)
            //{
            //    printStr[0] = AppConfig.ConfigInfo.Remark3;
            //    PrintOneLine(printStr, false);
            //}
            //WintecIDT700.FeedPaper(200);
        }

        private string PreviousTraceNumber = string.Empty;
        private string PreviousCardNumber = string.Empty;
        private string PreviousHeji = string.Empty;
        private string PreviousOriginal = string.Empty;
        private string PreviousBalance = string.Empty;
        private string PreviousCardtypename = string.Empty;
        private string Previousdiscount = string.Empty;
        private string PreviousYouhuiamount = string.Empty;



        /*
        //打印前一笔销售的票据
        private void PrintPreviousNotes()
        {
            float amount = 0.00f;
            int qty = 0;
            int index = 0;
            string[] printStr = new string[(isSaleReturn == false ? 22 : 19) + PreviousSaleTempList.Count * 2];
            //string[] printStr1 = new string[1];
            //for (int i = 0; i < ((8 - AppConfig.ConfigInfo.CompanyName.Trim().Length) / 2); i++)
            //{
            //    printStr1[0] += "　";
            //}
            //printStr1[0] += AppConfig.ConfigInfo.CompanyName;
            //WintecIDT700.PrintLineStr(printStr1, 1, 4);
            //WintecIDT700.FeedPaper(40);           
            printStr[++index] = "         "+config.CompanyName;
            printStr[++index] = "档口：" + config.CustomerName;
            printStr[++index] = "单据编号：" + PreviousTraceNumber + (isSaleReturn == false ? "" : "[退]");
            printStr[++index] = "打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            printStr[++index] = "机器编号：" + config.PosNo;
            printStr[++index] = "序号 名称      单价   数量 金额 ";
            printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            int count = 1;
            foreach (Model.SaleFlow saleFlow in PreviousSaleTempList)
            {
                printStr[++index] =
                                    count.ToString().PadRight(4, ' ') +
                                    (saleFlow.FName.Length > 6 ? saleFlow.FName.Substring(0, 6).PadRight(6, '　') : saleFlow.FName.PadRight(6, '　')) +
                                    saleFlow.Price.ToString("F2").PadRight(6, ' ') +
                                    saleFlow.Qty.ToString().PadLeft(2, ' ') +
                                    saleFlow.Real_total.ToString("F2").PadLeft(3, ' ');
                printStr[++index] = "------------------------------";
                qty += qty;
                amount += saleFlow.Total;
                count++;
            }
  
            printStr[++index] = "------------------------------";
            printStr[++index] = "合计：                   " + amount.ToString().PadLeft(6, ' ');
            printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            printStr[++index] = "           卡片信息           ";
            printStr[++index] = "卡号：" + PreviousCardNumber;
            if (!isSaleReturn)
            {
              //  printStr[++index] = "优惠金额：" + PreviousYouhuiamount;
               // printStr[++index] = "折 扣 率：" + float.Parse(Previousdiscount == string.Empty ? "0" : Previousdiscount) * 100 + "%";
                printStr[++index] = "卡内原余额：" + PreviousOriginal;
                printStr[++index] = "卡内现余额：" + PreviousBalance;
            }
            printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            printStr[++index] = "版权所有@商盟软件  ";
            printStr[++index] = "电话:053283817927 ";

            WintecIDT700.PrintLineStr(printStr, printStr.Length, 1);
            WintecIDT700.FeedPaper(200);

            //printStr[0] = "档口：" + AppConfig.ConfigInfo.CustomerName;
            //PrintOneLine(printStr, true);
            //printStr[0] = "单据编号：" + PreviousTraceNumber + (isSaleReturn == false ? "" : "[退]");
            //PrintOneLine(printStr, true);
            //printStr[0] = "打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //PrintOneLine(printStr, true);
            //printStr[0] = "营业日期：" + DateTime.Now.ToString("yyyy-MM-dd");
            //PrintOneLine(printStr, true);
            //printStr[0] = "机器编号：" + AppConfig.ConfigInfo.PosNo;
            //PrintOneLine(printStr, true);
            //printStr[0] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            //PrintOneLine(printStr, false);
            //printStr[0] = "序号 编号    名称     数量 金额 ";
            //PrintOneLine(printStr, false);
            //printStr[0] = "------------------------------";
            //PrintOneLine(printStr, false);
            //int count = 1;
            //foreach (Model.SellDetail SellDetail in PreviousSaleTempList)
            //{
            //    printStr[0] =
            //        count.ToString().PadRight(4, ' ') +
            //        SellDetail.Productid.PadRight(6, ' ') +
            //        (SellDetail.Productname.Length > 6 ? SellDetail.Productname.Substring(0, 6).PadRight(6, '　') : SellDetail.Productname.PadRight(6, '　')) +
            //        SellDetail.Quantity.PadLeft(3, ' ') +
            //        SellDetail.Amount.PadLeft(6, ' ');
            //    PrintOneLine(printStr, false);
            //    printStr[0] = "------------------------------";
            //    PrintOneLine(printStr, false);
            //    qty += decimal.Parse(SellDetail.Quantity);
            //    amount += decimal.Parse(SellDetail.Amount);
            //    count++;
            //}
            //printStr[0] = "合计：                   " + PreviousHeji.PadLeft(6, ' ');
            //PrintOneLine(printStr, false);
            //printStr[0] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            //PrintOneLine(printStr, true);
            //printStr[0] = "           卡片信息           ";
            //PrintOneLine(printStr, false);
            //WintecIDT700.FeedPaper(20);
            //printStr[0] = "卡    号：" + PreviousCardNumber;
            //PrintOneLine(printStr, true);
            //if (!isSaleReturn)
            //{
            //    printStr[0] = "卡 类 型：" + CardTypename;
            //    PrintOneLine(printStr, true);
            //    printStr[0] = "优惠金额：" + Youhuiamount;
            //    PrintOneLine(printStr, true);
            //    printStr[0] = "折 扣 率：" + discount * 100 + "折";
            //    PrintOneLine(printStr, true);
            //    printStr[0] = "卡内原余额：" + txtOriginal.Text.Trim();
            //    PrintOneLine(printStr, true);
            //    printStr[0] = "卡内现余额：" + txtBalance.Text.Trim();
            //    PrintOneLine(printStr, true);
            //}
            //printStr[0] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            //PrintOneLine(printStr, true);
            //if (AppConfig.ConfigInfo.Remark1.Trim() != string.Empty)
            //{
            //    printStr[0] = AppConfig.ConfigInfo.Remark1;
            //    PrintOneLine(printStr, true);
            //}
            //if (AppConfig.ConfigInfo.Remark2.Trim() != string.Empty)
            //{
            //    printStr[0] = AppConfig.ConfigInfo.Remark2;
            //    PrintOneLine(printStr, true);
            //}
            //if (AppConfig.ConfigInfo.Remark3.Trim() != string.Empty)
            //{
            //    printStr[0] = AppConfig.ConfigInfo.Remark3;
            //    PrintOneLine(printStr, true);
            //}
            //WintecIDT700.FeedPaper(200);

        }

        */

        private void PrintOneLine(string[] printStr, bool iFeed)
        {
            WintecIDT700.PrintLineStr(printStr, 1, 1);
            if (iFeed)
            {
                WintecIDT700.FeedPaper(10);
            }
        }

        private void SaleForm_Closed(object sender, EventArgs e)
        {
            WintecIDT700.CloseComm(comHandle);

            this.open = false;
        }


    }
}