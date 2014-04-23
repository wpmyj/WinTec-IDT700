using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using devices;
using Devices;
using Model;
using System.Threading;
using BaseClass;
using Trans;

namespace POS
{
    //程序开启时 初始化读卡线程 并阻塞线程
    //每次开启窗口时，主线程释放Mutex ，线程开始读卡
    //读卡结束后，线程释放mutex，主线程获得mutex，阻塞线程
    public partial class FrmSettle : Form
    {
        static Thread readThread ;
        bool readSuccess = false;
        bool tradeSuccess = false;
        #region 显示等待提示窗口
        static FrmWait frmWait ;//等待窗口
        /// <summary>
        /// 显示等待提示窗口
        /// </summary>
        /// <param name="msg">提示信息</param>
        public void ShowWaitMsg(string _msg)
        {
            frmWait.Msg = _msg;
            frmWait.Show();
            Application.DoEvents();
        }
        /// <summary>
        /// 关闭等待提示窗口
        /// </summary>
        public void HideWaitMsg()
        {
            frmWait.Hide();
            this.Show();   
            this.Focus();
        }
        /// <summary>
        /// 显示等待光标
        /// </summary>
        public void ShowWaitCursor()
        {
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();
        }
        /// <summary>
        /// 隐藏等待光标
        /// </summary>
        public void HideWaitCursor()
        {
            Cursor.Current = Cursors.Default;
            Cursor.Hide();
        }
        #endregion

        #region 消息提示窗口
        /// <summary>
        /// 显示错误消息提示框
        /// </summary>
        /// <param name="msg"></param>
        private void ShowErrorMsgBox(string msg)
        {
            MessageBox.Show(msg,string.Empty,MessageBoxButtons.OK,MessageBoxIcon.Hand,MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 显示消息提示框
        /// </summary>
        /// <param name="msg"></param>
        private void ShowInfoMsgBox(string msg)
        {
            MessageBox.Show(msg, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }
        #endregion

        /// <summary>
        /// 成功后关闭窗口
        /// </summary>
        private void CloseForm()
        {
            this.DialogResult = tradeSuccess ? DialogResult.OK : DialogResult.Cancel;
        }

        #region 委托
        delegate void DlgShowMsg(string msg);//显示消息委托类
        DlgShowMsg dlgShowErrorMsgBox;//显示错误消息提示框
        DlgShowMsg dlgShowInfoMsgBox;//显示普通消息提示窗口
        DlgShowMsg dlgShowWaitMsg;//显示等待窗口

        delegate void DlgHideMsg();//隐藏消息委托类
        DlgHideMsg  dlgHideWaitMsg;//隐藏等待窗口

        delegate void DlgCloseForm();//关闭窗口委托类
        DlgCloseForm dlgCloseForm;
        #endregion

        public FrmSettle()
        {
            InitializeComponent();
            this.Width = 530;
            this.Height = 330;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            if (frmWait == null)
            {
                frmWait = new FrmWait("");
            }
            //初始化委托
            dlgShowWaitMsg=new DlgShowMsg(ShowWaitMsg);
            dlgHideWaitMsg=new DlgHideMsg(HideWaitMsg);
            dlgShowErrorMsgBox = new DlgShowMsg(ShowErrorMsgBox);
            dlgShowInfoMsgBox = new DlgShowMsg(ShowInfoMsgBox);
            dlgCloseForm = new DlgCloseForm(CloseForm);
            //初始化线程
            readThread = new Thread(new ThreadStart(ReadCard));
            readThread.IsBackground = true;
            readThread.Start();
        }

        #region IC卡
        static ICCard icCard;

        static Mutex readCardStatus=new Mutex(true);//读卡状态标志
        /// <summary>
        /// 读卡
        /// </summary>
        private void ReadCard()
        {
            icCard= new ICCard("com2");//初始化
            string inCardNo,msg;
            while (true)//
            {
                readCardStatus.WaitOne();//等待窗口开启
                #region 寻卡
                if (!readSuccess && icCard.SearchIC())
                {
                    Thread.Sleep(100);
                    #region 读卡号
                    try
                    {
                        inCardNo = CardStrFormater.GetCardNo(icCard.ReadIC("0", "1", PubGlobal.SysConfig.CzkPassword), PubGlobal.SysConfig.CzkLen);
                        if (!string.IsNullOrEmpty(inCardNo))
                        {
                            readSuccess = true;
                           
                            WintecIDT700.Beep(100);
                        }
                    }
                    catch
                    {
                        //读卡失败，释放资源，重新读卡
                        //this.Invoke(dlgShowErrorMsgBox, "读卡错误");
                        readCardStatus.ReleaseMutex();

                        readSuccess = false;
                        continue;
                    }
                    #endregion

                    Thread.Sleep(100);
                    this.Invoke(dlgShowWaitMsg, "正在读取金额");
                    #region 读金额
                    if (!TransModule.GetCzCard(inCardNo, out msg))
                    {
                        //读取金额失败
                        this.Invoke(dlgShowErrorMsgBox, "读取金额失败\r\n错误原因:" + msg);
                        this.Invoke(dlgHideWaitMsg);
                        readSuccess = false;
                        readCardStatus.ReleaseMutex();
                        continue;
                    }
                    #endregion

                    if (!WriteCard(PubGlobal.BussinessVar.card.Total - PubGlobal.BussinessVar.Total,1, out msg))
                    {
                        MessageBox.Show("写金额失败");
                    }

                    Thread.Sleep(100);
                    this.Invoke(dlgShowWaitMsg, "正在交易");
                    #region 交易
                    //PubGlobal.BussinessVar.card.Total = CardStrFormater.GetMoney(icCard.ReadIC("1", "0", PubGlobal.SysConfig.CzkPassword));
                    //WintecIDT700.Beep(100);
                    if (Trade(out msg))
                    {
                        this.Invoke(dlgShowInfoMsgBox, "交易成功\r\n卡余额：" + PubGlobal.BussinessVar.card.Total);
                        tradeSuccess = true;
                    }
                    else
                    {
                        this.Invoke(dlgShowErrorMsgBox, "交易失败\r\n失败原因：" + msg);
                        //交易失败
                        if (!WriteCard(PubGlobal.BussinessVar.card.Total, 1,out msg))
                        {
                            MessageBox.Show("写卡失败");
                        }
                        PubGlobal.BussinessVar.card = null;

                    }
                    #endregion

                    this.Invoke(dlgHideWaitMsg);
                }
                #endregion
                readCardStatus.ReleaseMutex();//释放阻塞
                this.Invoke(dlgCloseForm);//关闭窗口
                Thread.Sleep(500);//线程休眠500毫秒
            }
        }

        /// <summary>
        /// 写卡
        /// </summary>
        private bool WriteCard(decimal money,int i, out string msg)
        {
            if (i >= 3)
            {
                msg = "3次写卡失败。请联系管理员。";
                return false;
            }

            try
            {
                #region 校验卡号
                if (PubGlobal.BussinessVar.card.InCardno == CardStrFormater.GetCardNo(icCard.ReadIC("0", "1", PubGlobal.SysConfig.CzkPassword), PubGlobal.SysConfig.CzkLen))
                {
                    #region 写金额
                    msg = "写卡成功";
                    bool ok= icCard.WriteIC("1", "0", PubGlobal.SysConfig.CzkPassword, CardStrFormater.FormateMoney(money));
                    if (ok)
                    {
                        WintecIDT700.Beep(100);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    #endregion
                }
                else
                {
                    throw new Exception("写卡失败");
                }
                #endregion
            }
            catch
            {
                return WriteCard(money, i++, out msg);
            }
        }

        #endregion

        //private void TestTrade()
        //{
        //    string inCardNo = "000001",msg;
        //    this.Invoke(dlgShowWaitMsg, "正在读取金额");
        //    this.Invoke(dlgHideWaitMsg);
        //    #region 读金额
        //    if (!TransModule.GetCzCard(inCardNo, out msg))
        //    {
        //        //读取金额失败
        //        this.Invoke(dlgShowErrorMsgBox, "读取金额失败\r\n错误原因:" + msg);
        //        this.Invoke(dlgHideWaitMsg);
        //        readSuccess = false;
        //        readCardStatus.ReleaseMutex();
        //    }
        //    #endregion

        //    Thread.Sleep(100);
        //    this.Invoke(dlgShowWaitMsg, "正在交易");
        //    #region 交易
        //    //PubGlobal.BussinessVar.card.Total = CardStrFormater.GetMoney(icCard.ReadIC("1", "0", PubGlobal.SysConfig.CzkPassword));
        //    //WintecIDT700.Beep(100);
        //    if (Trade(out msg))
        //    {
        //        this.Invoke(dlgShowInfoMsgBox, "交易成功\r\n卡余额：" + PubGlobal.BussinessVar.card.Total);
        //        this.Invoke(dlgCloseForm);//关闭窗口
        //    }
        //    else
        //    {
        //        this.Invoke(dlgShowErrorMsgBox, "交易失败\r\n失败原因：" + msg);
        //        //交易失败
        //        PubGlobal.BussinessVar.card = null;
        //        readSuccess = false;
        //    }
        //    #endregion

        //    this.Invoke(dlgHideWaitMsg);
        //}
        /// <summary>
        /// 交易
        /// </summary>
        /// <returns></returns>
        private bool Trade(out string msg)
        {
            if (PubGlobal.BussinessVar.card.Total-PubGlobal.BussinessVar.card.CzkType.KbMoney < PubGlobal.BussinessVar.Total)
            {
                msg = "余额不足。\r\n当前卡余额为" + PubGlobal.BussinessVar.card.Total + "元";
                return false;
            }
            MPayFlow payFlow = new MPayFlow();
            payFlow.Operater = PubGlobal.SysConfig.User.UserCode;
            payFlow.Paypmt = "12";
            payFlow.PosNO = PubGlobal.SysConfig.PosNO;
            payFlow.Ref_no = PubGlobal.BussinessVar.card.InCardno;
            payFlow.RowNO = 1;
            payFlow.Sa_date = DateTime.Today;
            payFlow.Sa_time = DateTime.Now.ToString("HH:mm:ss");
            COMM.SerialNoBuilder.GetSerialNo();//获取流水号
            payFlow.SerialNo = PubGlobal.SysConfig.PosNO + payFlow.Sa_date.ToString("yyMMdd")+ PubGlobal.BussinessVar.SerialNo;
            payFlow.Squadno = "1";
            payFlow.Total = PubGlobal.BussinessVar.Total;

            foreach (MSaleFlow saleFlow in PubGlobal.BussinessVar.saleFlowList)
            {
                saleFlow.SerialNo = payFlow.SerialNo;
                saleFlow.Sa_date = payFlow.Sa_date;
                saleFlow.Sa_time = payFlow.Sa_time;
            }

            PubGlobal.BussinessVar.payFlowList.Add(payFlow);
            this.Invoke(dlgShowWaitMsg, "正在交易");
            if (!TransModule.SaveTrade(out msg))
            {
                PubGlobal.BussinessVar.card = null;
                readSuccess = false;
                return false;
            }
            else
            {
                COMM.SerialNoBuilder.CommitSerialNo();//提交流水号
                PrintBill();//交易成功，打印小票
                return true;
            }
        }

        #region 打印

        //打印
        private void  PrintBill()
        {
            WintecIDT700.DrawerOpen();
            //打印小票头
            int lineCount=6;
            int index = -1;
            if (!string.IsNullOrEmpty(PubGlobal.SysConfig.PrnHeader1))
                lineCount++;
            if (!string.IsNullOrEmpty(PubGlobal.SysConfig.PrnHeader2))
                lineCount++;
            if (!string.IsNullOrEmpty(PubGlobal.SysConfig.PrnHeader3))
                lineCount++;

            string[] printHeader = new string[lineCount];
            if (!string.IsNullOrEmpty(PubGlobal.SysConfig.PrnHeader1))
            {
                printHeader[++index] = PubGlobal.SysConfig.PrnHeader1;
            }
            if (!string.IsNullOrEmpty(PubGlobal.SysConfig.PrnHeader2))
            {
                printHeader[++index] = PubGlobal.SysConfig.PrnHeader2;
            }
            if (!string.IsNullOrEmpty(PubGlobal.SysConfig.PrnHeader3))
            {
                printHeader[++index] = PubGlobal.SysConfig.PrnHeader3;
            }

            printHeader[++index] = "收款员：" + PubGlobal.SysConfig.User.UserCode;
            printHeader[++index] = "流水号：" + PubGlobal.BussinessVar.payFlowList[0].SerialNo;
            printHeader[++index] = "时间：" + PubGlobal.BussinessVar.payFlowList[0].Sa_date.ToString("yyyy-MM-dd") + " " + PubGlobal.BussinessVar.payFlowList[0].Sa_time;
            printHeader[++index] = "菜品名称                ";
            printHeader[++index] = "    单价      数量      金额";
            printHeader[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            WintecIDT700.PrintLineStr(printHeader, lineCount, 3);
            //打印小票体
            index = 0;
            int printeLineOnetime = 60;
            string[] printStr = new string[printeLineOnetime];
            foreach (Model.MSaleFlow saleFlow in PubGlobal.BussinessVar.saleFlowList)
            {
                printStr[index++] = saleFlow.Fname;
                printStr[index++] =
                    "  " +
                    saleFlow.Price.ToString("F2").PadLeft(9, ' ') +
                    saleFlow.Qty.ToString("F2").PadLeft(9, ' ') +
                    saleFlow.Total.ToString("F2").PadLeft(9, ' ');
                if (index == printeLineOnetime)
                {
                    WintecIDT700.PrintLineStr(printStr, printeLineOnetime, 1);
                    index = 0;
                    for (int i = 0; i < printeLineOnetime; i++)
                        printStr[i] = string.Empty;
                }
            }
            if (index > 0)
                WintecIDT700.PrintLineStr(printStr, index, 3);

            //打印小票尾
            index = -1;
            lineCount = 5;
            if (!string.IsNullOrEmpty(PubGlobal.SysConfig.PrnFooter1))
                lineCount++;
            if (!string.IsNullOrEmpty(PubGlobal.SysConfig.PrnFooter2))
                lineCount++;
            if (!string.IsNullOrEmpty(PubGlobal.SysConfig.PrnFooter3))
                lineCount++;

            string[] printEnd = new string[lineCount];
            printEnd[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            printEnd[++index] = "合    计：" + PubGlobal.BussinessVar.Total.ToString("F2");
            printEnd[++index] = "会员卡号：" + PubGlobal.BussinessVar.card.OutCardNo;
            printEnd[++index] = "余    额：" + PubGlobal.BussinessVar.card.Total.ToString("F2");
            printEnd[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            if (!string.IsNullOrEmpty(PubGlobal.SysConfig.PrnFooter1))
            {
                printEnd[++index] = PubGlobal.SysConfig.PrnFooter1;
            }
            if (!string.IsNullOrEmpty(PubGlobal.SysConfig.PrnFooter2))
            {
                printEnd[++index] = PubGlobal.SysConfig.PrnFooter2;
            }
            if (!string.IsNullOrEmpty(PubGlobal.SysConfig.PrnFooter3))
            {
                printEnd[++index] = PubGlobal.SysConfig.PrnFooter3;
            }
            WintecIDT700.PrintLineStr(printEnd, lineCount, 3);
            WintecIDT700.FeedPaper(200);

        }

        #endregion

        private void FrmSettle_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    readCardStatus.WaitOne();//窗口关闭，阻塞读卡线程
                    this.DialogResult = DialogResult.Cancel;
                    break;
                case Keys.Enter:
                    //TestTrade();
                    break;
            }
            e.Handled = true;
        }

        private void FrmSettle_Load(object sender, EventArgs e)
        {
            PubGlobal.BussinessVar.Total = 0;
            foreach (MSaleFlow saleFlow in PubGlobal.BussinessVar.saleFlowList)
            {
                PubGlobal.BussinessVar.Total += saleFlow.Total;
            }
            tbHj.Text = PubGlobal.BussinessVar.Total.ToString("F2");
            readSuccess = false;
            tradeSuccess = false;
            readCardStatus.ReleaseMutex();//窗口打开，释放阻塞，放行读卡线程
        }

        private void FrmSettle_Closing(object sender, CancelEventArgs e)
        {
            readCardStatus.WaitOne();//获取mutex
        }

    }
}