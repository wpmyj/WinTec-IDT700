using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SumPos.Business
{
    public partial class CardReadForm : Form
    {
        Model.CzCard czCard;//当前卡
        Model.Config config;//参数
        bool open = true;
        private const int RFCardCom = 2;    //默认COM2
        private const int RFCardBaudRate = 19200;  //波特率:默认19200
        System.Threading.Thread readCardThread;//读卡线程

        IntPtr comHandle = IntPtr.Zero;    //串口句柄
        //定义一个委托
        delegate void HandleInterfaceShowCardNoDelegate(Model.CzCard card);
        //读取射频卡模块返回值。
        HandleInterfaceShowCardNoDelegate interfaceShowCardNoDelegate;


        public CardReadForm(Model.Config config)
        {
            InitializeComponent();
            this.config = config;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardReadForm_Load(object sender, EventArgs e)
        {
            //实例化委托对象 
            interfaceShowCardNoDelegate = new HandleInterfaceShowCardNoDelegate(ShowCardNo);

            if (config.CzkType == Model.CzCardType.射频卡)
            {
                //打开串口
                comHandle = WintecIDT700.OpenComm(RFCardCom, RFCardBaudRate);
            }

            //启用线程，读取卡号

            readCardThread = new System.Threading.Thread(new System.Threading.ThreadStart(SearchCard));
            readCardThread.Priority = ThreadPriority.Lowest;
            readCardThread.IsBackground = true;
            readCardThread.Start();

        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardReadForm_Closing(object sender, CancelEventArgs e)
        {
            WintecIDT700.CloseComm(comHandle);
            open = false;
        }


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

                                if (czCard.Incardno == null || czCard.Incardno == "")
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
                                    }
                                    else if (czCard.Stat == Model.CzkStat.禁用)
                                    {
                                        MessageBox.Show("此卡已禁用");
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
            //if (card != null)
            //{
            //    cardNoTxt.Text = card.Outcardno;

            //    totalTxt.Text = card.Total.ToString("F2");//卡余额
            //    if (card.Total >= noPayTotal)
            //    {
            //        //储值卡金额足够
            //        bkPayJeTxtBx.Text = noPayTotal.ToString("F2");//本卡消费
            //    }
            //    else
            //    {
            //        //储值卡金额不足
            //        bkPayJeTxtBx.Text = card.Total.ToString("F2");
            //    }
            //    bkPayJeTxtBx.Focus();
            //    bkPayJeTxtBx.SelectAll();
            //}
            //else
            //{
            //    czkTotalTxt.Text = "0.00";
            //    remainToalTxtbox.Text = "0.00";
            //    lblcardtypename.Text = string.Empty;
            //}
        }



    }
}