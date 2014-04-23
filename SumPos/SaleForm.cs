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
        private List<Model.PayFlow> flowList = new List<Model.PayFlow>();//���ε�½�����¼



        private const int RFCardCom = 2;    //Ĭ��COM2
        private const int RFCardBaudRate = 19200;  //������:Ĭ��19200


        System.Threading.Thread readCardThread;//�����߳�

        IntPtr comHandle = IntPtr.Zero;    //���ھ��
        //����һ��ί��
        delegate void HandleInterfaceShowCardNoDelegate(Model.CzCard card);
        //��ȡ��Ƶ��ģ�鷵��ֵ��
        HandleInterfaceShowCardNoDelegate interfaceShowCardNoDelegate;

        public SaleForm(Model.Config config)
        {
            ShowWaitMsg("���ڴ򿪣����Ժ�...");
            InitializeComponent();
            this.config = config;
            SumPos.SumSerivice.SumPosWebService a = new SumPosWebService();
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
            //ʵ����ί�ж��� 
            interfaceShowCardNoDelegate = new HandleInterfaceShowCardNoDelegate(ShowCardNo);
            //�򿪴���
            comHandle = WintecIDT700.OpenComm(RFCardCom, RFCardBaudRate);
            //�����̣߳���ȡ����

            readCardThread = new System.Threading.Thread(new System.Threading.ThreadStart(SearchCard));
            readCardThread.Priority = ThreadPriority.Lowest;
            readCardThread.IsBackground = true;
            readCardThread.Start();

            #region ��ʼ������
            cardNoTxtBx.Text = string.Empty;
            NameTxtBx.Text = string.Empty;
            totalTxtBox.Text = "0.00";
            BindingDataGrid();
            if (config.ReadCardMode == Model.ReadCardByHand.��ֹ�ֶ�)
            {
                cardNoTxtBx.ReadOnly = true;// true;
            }
            #endregion
            HideWaitMsg();

            //#region ��ʼ��webservice
            //ser = new WebService(config.WebServerUrl);
            //#endregion

            cardNoTxtBx.Focus();


        }

        //bool testbl = false;

        //���񰴼��¼���
        private void SaleForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.cardNoTxtBx.Text == "" && this.cardNoTxtBx.Focused == false)
            {
                //�����Ʒ�����ı���Ϊ�գ�����ı����ý���
                this.cardNoTxtBx.Focus();
            }
            switch (e.KeyCode)
            {
                case Keys.F1:
                    //��ҳ
                    this.button1_Click(null, null);
                    break;
                case Keys.F2:
                    //��ҳ
                    this.button2_Click(null, null);
                    break;
                case Keys.F3:
                    //��ֵ
                    this.button3_Click(null, null);
                    break;
                case Keys.F4:
                    //�˿�
                    this.button4_Click(null, null);
                    break;
                case Keys.F5:
                    //ȡ��
                    this.button5_Click(null, null);
                    break;
                case Keys.F11:
                    //����
                    break;
                case Keys.Escape:
                    //�˳�
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
        /// �����߳���
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
                            if (config.CzkType == Model.CzCardType.�ſ�)
                            {
                                //���ſ�
                                hyzh = readCCard();
                            }
                            else
                            {
                                //����Ƶ��

                                hyzh = readICCard();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("����ʧ�ܣ�������ˢ����");
                        }

                        if (hyzh != string.Empty)
                        {
                            try
                            {
                                #region ��¼��ֵ����Ϣ

                                czCard = WebService.loadCzCardByEnCryStr(hyzh);
                                #endregion

                                if (!czCard.CanVerifyMark)
                                {
                                    WintecIDT700.Beep(100);
                                    Thread.Sleep(50);
                                    WintecIDT700.Beep(100);
                                    Thread.Sleep(50);
                                    WintecIDT700.Beep(100);
                                    MessageBox.Show("�����У��ʧ�ܣ�����ϵ����Ա��");
                                    czCard = null;
                                    return;
                                }

                                if (czCard.Hyzh == null || czCard.Hyzh == "")
                                {
                                    //��������ƥ�俨
                                    WintecIDT700.Beep(100);
                                    Thread.Sleep(50);
                                    WintecIDT700.Beep(100);
                                    Thread.Sleep(50);
                                    WintecIDT700.Beep(100);
                                    MessageBox.Show("�˿���Ч��");
                                }
                                else
                                {
                                    //�����ɹ�
                                    if (czCard.Stat == Model.CzkStat.��ʧ)
                                    {
                                        MessageBox.Show("�˿��ѹ�ʧ");
                                        czCard = null;
                                        return;
                                    }
                                    else if (czCard.Stat == Model.CzkStat.����)
                                    {
                                        MessageBox.Show("�˿��ѽ���");
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


                                MessageBox.Show("ͨѶ�쳣��������ˢ����");


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
        /// ��ȡ�ſ�
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
        /// ����Ƶ��
        /// </summary>
        /// <returns></returns>
        private string readICCard()
        {
            string pwd = string.Empty;
            byte[] pwdbs = new byte[6];

            #region ��ʼ����Ƶ������
            pwd = "FFFFFFFFFFFF";

            for (int i = 0; i < pwdbs.Length; i++)
            {
                pwdbs[i] = Convert.ToByte(pwd.Substring(0, 2), 16);

                pwd = pwd.Substring(2);
            }
            #endregion



            byte block = WintecIDT700.RF_MAKE_BLOCK(1, 0);
            //1������0��
            string cardno = string.Empty;
            byte[] buff = new byte[16];

            cardno = string.Empty;
            try
            {
                #region Ѱ��
                int rfid = WintecIDT700.RF_SearchCard(Convert.ToByte(2), 0);
                #endregion

                if (rfid != -1)
                {
                    #region У������
                    int key = WintecIDT700.RF_Authentication(Convert.ToByte(2), 0, block, pwdbs, 0x60);
                    #endregion

                    if (key == 1)
                    {
                        //����У��ɹ�
                        //��ȡ����
                        WintecIDT700.Beep(100);

                        #region ��ȡ����
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
        /// ��ʾˢ�����
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
        /// ��һҳ
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
        /// ��һҳ
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
        ///��ֵ
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
                MessageBox.Show("����ˢ����");
            }
        }



        /// <summary>
        /// �˿�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            //if (czCard == null)
            //{
            //    MessageBox.Show("����ˢ����");
            //    return;
            //}


            /********ȡ���˿�ģʽ******/
            if (isTH)
            {
                tkPayflow = null;
                payJeTxtBx.Text = "0";
                payJeTxtBx.Focus();
                payJeTxtBx.SelectAll();
                button4.Text = "�˿�";
                label3.Text = "���ѽ�";
                label3.ForeColor = Color.White;
                czCard = null;
                ShowCardNo(czCard);
                isTH = !isTH;
                return;
            }

            /********�����˿�ģʽ******/
            if (!isTH)
            {
                MessageBox.Show("�����˿�ģʽ����������ˮ�ţ�");
                Business.SaleReturnForm thForm = new SaleReturnForm();
                thForm.ShowDialog();
                if (thForm.DialogResult == DialogResult.Cancel)
                {
                    //ȡ���˻�

                    MessageBox.Show("�˻���ȡ����");
                    return;
                }


                isTH = !isTH;


                czCard = new Model.CzCard();//���ˢ���߳�
                ShowCardNo(czCard);

                //payJeTxtBx.Focus();
                //payJeTxtBx.SelectAll();
                button4.Text = "ȡ���˿�";
                label3.Text = "�˿��";
                label3.ForeColor = Color.Red;

                string thNo = thForm.textBox1.Text;
                tkPayflow = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getPayFlow(thNo);
                if (tkPayflow.Tradetype == Model.FlowTradeType.�˻�)
                {
                    MessageBox.Show("����ˮ�����˿");
                    tkPayflow = null;
                    payJeTxtBx.Focus();
                    payJeTxtBx.SelectAll();
                    button4_Click(null, null);
                    return;
                }
                if (tkPayflow.Flow_no == string.Empty || tkPayflow.Flow_no == null)
                {
                    MessageBox.Show("����ˮ�����ڣ�");
                    tkPayflow = null;
                    payJeTxtBx.Focus();
                    payJeTxtBx.SelectAll();
                    button4_Click(null, null);
                    return;
                }
                payJeTxtBx.Text = tkPayflow.Total.ToString("F2");
                czCard = null;
                MessageBox.Show("��ˢ��....");
            }
        }




        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            initCzkcard();//��ʼ����ֵ����ʾ

        }





        //����س���
        private void ExecutePro()
        {
            if (this.cardNoTxtBx.Text == string.Empty)
            {
                this.cardNoTxtBx.Focus();
                return;
            }
            else if (this.cardNoTxtBx.Focused)
            {
                //���뿨�ź�س�
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
                        MessageBox.Show("�����У��ʧ�ܣ�����ϵ����Ա��");
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
                        MessageBox.Show("�˿���Ч��");
                        cardNoTxtBx.Focus();
                        cardNoTxtBx.SelectAll();
                    }
                    else
                    {
                        //�����ɹ�
                        if (card.Stat == Model.CzkStat.��ʧ)
                        {
                            MessageBox.Show("�˿��ѹ�ʧ");
                            return;
                        }
                        else if (card.Stat == Model.CzkStat.����)
                        {
                            MessageBox.Show("�˿��ѽ���");
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
                //�������س�������
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
        /// ��ˢ����¼����Դ
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
        //����
        private void EndBusiness()
        {




            if (isPay == false)
            {
                isPay = true;
                bool payOk = false;

                Model.PayFlow payFlow;//��ʱ������ˮ
                Model.TradeFlow tradeFlow;//��ʱ�����ܱ���ˮ
                string flow_no = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getFlow_no(config.PosNo);
                float payJe = 0;

                #region ���齻����Ч��
                try
                {

                    if (czCard == null)
                    {
                        MessageBox.Show("����ˢ����");
                        isPay = false;
                        return;
                    }


                    payJe = float.Parse(payJeTxtBx.Text);
                    if (payJeTxtBx.Text.IndexOf('.') != -1)
                    {
                        string[] payStr = payJeTxtBx.Text.Split('.');
                        if (payStr[1].Length == 3)
                        {
                            MessageBox.Show("С�����ֻ��������2λ�����������룡");
                            isPay = false;
                            payJeTxtBx.Focus();
                            payJeTxtBx.SelectAll();
                            return;
                        }

                    }
                    if (payJe == 0)
                    {
                        MessageBox.Show("����Ϊ0�����������룡");
                        isPay = false;
                        payJeTxtBx.Focus();
                        payJeTxtBx.SelectAll();
                        isPay = false;
                        return;
                    }

                    if (payJe >= 100000)
                    {
                        MessageBox.Show("�����������������");
                        isPay = false;
                        payJeTxtBx.Focus();
                        payJeTxtBx.SelectAll();
                        return;
                    }
                    if (payJe < 0)
                    {
                        MessageBox.Show("����Ϊ����");
                        isPay = false;
                        payJeTxtBx.Focus();
                        payJeTxtBx.SelectAll();
                        return;
                    }
                    if (isTH)
                    {
                        if (tkPayflow.Cardno != czCard.Hyzh)
                        {
                            MessageBox.Show("����ˮ���ѿ��ż�¼�뵱ǰ���Ų�һ�£�");
                            isPay = false;
                            czCard = null;
                            ShowCardNo(czCard);
                            payJeTxtBx.Focus();
                            payJeTxtBx.SelectAll();
                            return;
                        }
                        if (payJe > tkPayflow.Total)
                        {
                            MessageBox.Show("�˿�����ڽ��׽����������룡");
                            isPay = false;
                            payJeTxtBx.Focus();
                            payJeTxtBx.SelectAll();
                            return;
                        }

                    }

                }
                catch
                {
                    MessageBox.Show("��������Ч�����������룡");
                    isPay = false;
                    payJeTxtBx.Focus();
                    payJeTxtBx.SelectAll();
                    return;
                }


                #endregion

                /*********��ʼ����**************************/
                payFlow = new Model.PayFlow();
                tradeFlow = new Model.TradeFlow();
                short coe = 1;//����ϵ��������Ϊ1���˻�Ϊ-1
                try
                {
                    if (isTH)
                    {
                        //�˻�
                        coe = -1;
                    }

                    if (float.Parse(payJeTxtBx.Text) <= czCard.Total)
                    {
                        #region ��װtradeflow
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
                        tradeFlow.TradeType =isTH? Model.FlowTradeType.�˻�:Model.FlowTradeType.����;
                        tradeFlow.Zkje = 0;
                        tradeFlow.Bcjf = 0;
                        tradeFlow.Scjf = 0;
                        tradeFlow.CustCode = czCard.Hykh;
                        tradeFlow.ShopNo = user.Mdept;
                        //tradeFlow.CustFlag = "";
                        #endregion

                        #region ��װpayflow
                        payFlow.Ckic = config.CzkType;
                        payFlow.Cardno = czCard.Hyzh;
                        payFlow.Hykh = czCard.Hykh;
                        payFlow.Flow_id = 1;
                        payFlow.Flow_no = tradeFlow.Flow_no;
                        payFlow.Operater = tradeFlow.Operater;
                        payFlow.PayAmount = 0;
                        payFlow.Paypmt = Model.FlowPayType.��ֵ��;
                        payFlow.Posno = tradeFlow.Posno;
                        payFlow.Scye = czCard.Total;
                        payFlow.Sdate = tradeFlow.Sdate;
                        payFlow.Squadno = tradeFlow.Squadno;
                        payFlow.Stime = tradeFlow.Stime;
                        payFlow.Total = tradeFlow.Total;
                        payFlow.Tradetype = tradeFlow.TradeType;
                        payFlow.Shopno = tradeFlow.ShopNo;
                        #endregion

                        ShowWaitMsg("���ڽ��н��㣬���Ժ�...");
                        cardNoTxtBx.BackColor = Color.Black;
                        payJeTxtBx.BackColor = Color.Black;
                        Application.DoEvents();
                        #region Զ�̿ۼ���������ˮ
                        string ok = WebService.Pay(tradeFlow, payFlow);
                        #endregion

                        if (ok != "error")
                        {
                            //�ۼ����ɹ�
                            tradeFlow.Flag = (Model.FlowUpLoadFlag)int.Parse(ok);
                            payFlow.Flag = tradeFlow.Flag;

                            #region ����������
                            bool success = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).savePay(tradeFlow, payFlow);
                            #endregion

                            if (!success)
                            {
                                MessageBox.Show("ˢ���ɹ��������������ر�ʧ�ܣ�����ϵ����Ա��");
                            }
                            else
                            {
                                if (config.PrintBill == Model.PrintBillFlag.��ӡ)
                                {
                                    PrintBill(tradeFlow);
                                }
                                //MessageBox.Show("���׳ɹ���");
                                List<Model.PayFlow> payList = new List<Model.PayFlow>();
                                payList.Add(payFlow);
                                payList.AddRange(flowList);
                                flowList = payList;


                                #region ������ˮ��
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
                                    #region �����˻���¼
                                    new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).payflowTh(tkPayflow.Flow_no);
                                    #endregion
                                }

                                #region ���½����б�
                                BindingDataGrid();
                                #endregion

                                #region ���µ׶���Ϣ��ʾ
                                payQtyHjTxtBx.Text = (int.Parse(payQtyHjTxtBx.Text) + 1).ToString();
                                payHjTxtBx.Text = (float.Parse(payHjTxtBx.Text) + tradeFlow.Total).ToString("F2");
                                #endregion

                            }
                            payOk = true;

                        }
                        else
                        {
                            MessageBox.Show("ˢ��ʧ�ܣ������ԣ�");
                        }
                    }
                    else
                    {
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        MessageBox.Show("��������Ѳ��㣡");
                        payOk = false;

                    }
                }
                catch
                {
                    //MessageBox.Show(ex.Message);
                    MessageBox.Show("������������ԣ�");
                    payOk = false;
                }
                finally
                {
                    isPay = false;
                    if (payOk)
                    {
                        isTH = false;
                        tkPayflow = null;
                        initCzkcard();//��ʼ����ֵ����ʾ
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
                    label3.Text = "���ѽ�";

                    button4.Text = "�˿�";
                    

                }
            }
        }

        /// <summary>
        /// ��ʼ����ֵ����ʾ
        /// </summary>
        private void initCzkcard()
        {
            WintecIDT700.Init_msr();
            czCard = null;
            ShowCardNo(czCard);
        }

        //��ӡ
        void PrintBill(Model.TradeFlow tradeFlow)
        {
            string[] printStr = new string[25];
            int index = 0;
            //string[] printStr1 = new string[1];
            //for (int i = 0; i < ((8 - AppConfig.ConfigInfo.CompanyName.Trim().Length) / 2); i++)
            //{
            //    printStr1[0] += "��";
            //}
            //printStr1[0] += AppConfig.ConfigInfo.CompanyName;
            //WintecIDT700.PrintLineStr(printStr1, 1, 4);
            //WintecIDT700.FeedPaper(40);

            printStr[++index] = "         " + config.CompanyName;
            printStr[++index] = "������" + config.CustomerName;
            printStr[++index] = "���ݱ�ţ�" + tradeFlow.Flow_no;
            printStr[++index] = "��ӡʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            printStr[++index] = "������ţ�" + config.PosNo;
            printStr[++index] = "��������������������������������";
            printStr[++index] = "���ţ�" + czCard.Hykh;
            printStr[++index] = string.Empty;
            printStr[++index] = "������" + czCard.CustName;
            printStr[++index] = string.Empty;
            printStr[++index] = "����ԭ��" + czCard.Total.ToString("F2");
            printStr[++index] = string.Empty;
            printStr[++index] = (isTH?"�˿��":"���ѽ�") + tradeFlow.Total.ToString("F2") + "Ԫ";
            printStr[++index] = string.Empty;
            printStr[++index] = "��������" + (czCard.Total - tradeFlow.Total).ToString("F2");
            printStr[++index] = string.Empty;
            printStr[++index] = "������������������������������";
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
            //    printStr[0] += "��";
            //}
            //printStr[0] += AppConfig.ConfigInfo.CompanyName;
            //WintecIDT700.PrintLineStr(printStr, 1, 4);
            //WintecIDT700.FeedPaper(40);

            //printStr[0] = "���ڣ�" + AppConfig.ConfigInfo.CustomerName;
            //PrintOneLine(printStr, false);
            //printStr[0] = "���ݱ�ţ�" + tracenumber + (isSaleReturn == false ? "" : "[��]");
            //PrintOneLine(printStr, false);
            //printStr[0] = "��ӡʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //PrintOneLine(printStr, false);
            //printStr[0] = "Ӫҵ���ڣ�" + DateTime.Now.ToString("yyyy-MM-dd");
            //PrintOneLine(printStr, false);
            //printStr[0] = "������ţ�" + AppConfig.ConfigInfo.PosNo;
            //PrintOneLine(printStr, false);
            //printStr[0] = "��������������������������������";
            //PrintOneLine(printStr, false);
            //printStr[0] = "��� ���    ����     ���� ��� ";
            //PrintOneLine(printStr, false);
            //printStr[0] = "------------------------------";
            //PrintOneLine(printStr, false);
            //int count = 1;
            //foreach (Model.SellDetail SellDetail in saleTempList)
            //{
            //    printStr[0] =
            //        count.ToString().PadRight(4, ' ') +
            //        SellDetail.Productid.PadRight(6, ' ') +
            //        (SellDetail.Productname.Length > 6 ? SellDetail.Productname.Substring(0, 6).PadRight(6, '��') : SellDetail.Productname.PadRight(6, '��')) +
            //        SellDetail.Quantity.PadLeft(3, ' ') +
            //        SellDetail.Amount.PadLeft(6, ' ');
            //    PrintOneLine(printStr, false);
            //    printStr[0] = "------------------------------";
            //    PrintOneLine(printStr, false);
            //    qty += decimal.Parse(SellDetail.Quantity);
            //    amount += decimal.Parse(SellDetail.Amount);
            //    count++;
            //}
            //printStr[0] = "�ϼƣ�                   " + SumAmount.ToString().PadLeft(6, ' ');
            //PrintOneLine(printStr, false);
            //printStr[0] = "������������������������������";
            //PrintOneLine(printStr, true);
            //printStr[0] = "           ��Ƭ��Ϣ           ";
            //PrintOneLine(printStr, false);
            //WintecIDT700.FeedPaper(20);
            //printStr[0] = "��    �ţ�" + CardNumber;
            //PrintOneLine(printStr, false);
            //if (!isSaleReturn)
            //{
            //    printStr[0] = "�� �� �ͣ�" + CardTypename;
            //    PrintOneLine(printStr, false);
            //    printStr[0] = "�Żݽ�" + Youhuiamount;
            //    PrintOneLine(printStr, false);
            //    printStr[0] = "�� �� �ʣ�" + discount * 100 + "��";
            //    PrintOneLine(printStr, false);
            //    printStr[0] = "����ԭ��" + txtOriginal.Text.Trim();
            //    PrintOneLine(printStr, false);
            //    printStr[0] = "��������" + txtBalance.Text.Trim();
            //    PrintOneLine(printStr, false);
            //}
            //printStr[0] = "������������������������������";
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
        //��ӡǰһ�����۵�Ʊ��
        private void PrintPreviousNotes()
        {
            float amount = 0.00f;
            int qty = 0;
            int index = 0;
            string[] printStr = new string[(isSaleReturn == false ? 22 : 19) + PreviousSaleTempList.Count * 2];
            //string[] printStr1 = new string[1];
            //for (int i = 0; i < ((8 - AppConfig.ConfigInfo.CompanyName.Trim().Length) / 2); i++)
            //{
            //    printStr1[0] += "��";
            //}
            //printStr1[0] += AppConfig.ConfigInfo.CompanyName;
            //WintecIDT700.PrintLineStr(printStr1, 1, 4);
            //WintecIDT700.FeedPaper(40);           
            printStr[++index] = "         "+config.CompanyName;
            printStr[++index] = "���ڣ�" + config.CustomerName;
            printStr[++index] = "���ݱ�ţ�" + PreviousTraceNumber + (isSaleReturn == false ? "" : "[��]");
            printStr[++index] = "��ӡʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            printStr[++index] = "������ţ�" + config.PosNo;
            printStr[++index] = "��� ����      ����   ���� ��� ";
            printStr[++index] = "��������������������������������";
            int count = 1;
            foreach (Model.SaleFlow saleFlow in PreviousSaleTempList)
            {
                printStr[++index] =
                                    count.ToString().PadRight(4, ' ') +
                                    (saleFlow.FName.Length > 6 ? saleFlow.FName.Substring(0, 6).PadRight(6, '��') : saleFlow.FName.PadRight(6, '��')) +
                                    saleFlow.Price.ToString("F2").PadRight(6, ' ') +
                                    saleFlow.Qty.ToString().PadLeft(2, ' ') +
                                    saleFlow.Real_total.ToString("F2").PadLeft(3, ' ');
                printStr[++index] = "------------------------------";
                qty += qty;
                amount += saleFlow.Total;
                count++;
            }
  
            printStr[++index] = "------------------------------";
            printStr[++index] = "�ϼƣ�                   " + amount.ToString().PadLeft(6, ' ');
            printStr[++index] = "������������������������������";
            printStr[++index] = "           ��Ƭ��Ϣ           ";
            printStr[++index] = "���ţ�" + PreviousCardNumber;
            if (!isSaleReturn)
            {
              //  printStr[++index] = "�Żݽ�" + PreviousYouhuiamount;
               // printStr[++index] = "�� �� �ʣ�" + float.Parse(Previousdiscount == string.Empty ? "0" : Previousdiscount) * 100 + "%";
                printStr[++index] = "����ԭ��" + PreviousOriginal;
                printStr[++index] = "��������" + PreviousBalance;
            }
            printStr[++index] = "������������������������������";
            printStr[++index] = "������������������������������";
            printStr[++index] = "��Ȩ����@�������  ";
            printStr[++index] = "�绰:053283817927 ";

            WintecIDT700.PrintLineStr(printStr, printStr.Length, 1);
            WintecIDT700.FeedPaper(200);

            //printStr[0] = "���ڣ�" + AppConfig.ConfigInfo.CustomerName;
            //PrintOneLine(printStr, true);
            //printStr[0] = "���ݱ�ţ�" + PreviousTraceNumber + (isSaleReturn == false ? "" : "[��]");
            //PrintOneLine(printStr, true);
            //printStr[0] = "��ӡʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //PrintOneLine(printStr, true);
            //printStr[0] = "Ӫҵ���ڣ�" + DateTime.Now.ToString("yyyy-MM-dd");
            //PrintOneLine(printStr, true);
            //printStr[0] = "������ţ�" + AppConfig.ConfigInfo.PosNo;
            //PrintOneLine(printStr, true);
            //printStr[0] = "��������������������������������";
            //PrintOneLine(printStr, false);
            //printStr[0] = "��� ���    ����     ���� ��� ";
            //PrintOneLine(printStr, false);
            //printStr[0] = "------------------------------";
            //PrintOneLine(printStr, false);
            //int count = 1;
            //foreach (Model.SellDetail SellDetail in PreviousSaleTempList)
            //{
            //    printStr[0] =
            //        count.ToString().PadRight(4, ' ') +
            //        SellDetail.Productid.PadRight(6, ' ') +
            //        (SellDetail.Productname.Length > 6 ? SellDetail.Productname.Substring(0, 6).PadRight(6, '��') : SellDetail.Productname.PadRight(6, '��')) +
            //        SellDetail.Quantity.PadLeft(3, ' ') +
            //        SellDetail.Amount.PadLeft(6, ' ');
            //    PrintOneLine(printStr, false);
            //    printStr[0] = "------------------------------";
            //    PrintOneLine(printStr, false);
            //    qty += decimal.Parse(SellDetail.Quantity);
            //    amount += decimal.Parse(SellDetail.Amount);
            //    count++;
            //}
            //printStr[0] = "�ϼƣ�                   " + PreviousHeji.PadLeft(6, ' ');
            //PrintOneLine(printStr, false);
            //printStr[0] = "������������������������������";
            //PrintOneLine(printStr, true);
            //printStr[0] = "           ��Ƭ��Ϣ           ";
            //PrintOneLine(printStr, false);
            //WintecIDT700.FeedPaper(20);
            //printStr[0] = "��    �ţ�" + PreviousCardNumber;
            //PrintOneLine(printStr, true);
            //if (!isSaleReturn)
            //{
            //    printStr[0] = "�� �� �ͣ�" + CardTypename;
            //    PrintOneLine(printStr, true);
            //    printStr[0] = "�Żݽ�" + Youhuiamount;
            //    PrintOneLine(printStr, true);
            //    printStr[0] = "�� �� �ʣ�" + discount * 100 + "��";
            //    PrintOneLine(printStr, true);
            //    printStr[0] = "����ԭ��" + txtOriginal.Text.Trim();
            //    PrintOneLine(printStr, true);
            //    printStr[0] = "��������" + txtBalance.Text.Trim();
            //    PrintOneLine(printStr, true);
            //}
            //printStr[0] = "������������������������������";
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