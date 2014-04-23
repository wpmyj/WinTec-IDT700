using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace SumPos.Business
{
    public partial class SaleForm : BaseClass.BaseForm
    {
        public Model.Config config;
        public Model.User user;

        float heji = 0;


        //float bkPayJe = 0;//��ǰ��������
        string serial_no = string.Empty;//��ǰ��ˮ��

        List<Model.Spxinxi> spxinxiList;//������Ʒ

        List<Model.SaleFlow> saleFlowList;//��ʱ��Ʒ��ˮ�б�
        List<Model.PayFlow> payFlowList;//��ʱ������ˮ�б�

        //private float nowPayTotal = 0;//�Ѹ����
        //private float total = 0; //�ϼƽ��

        
        //private string inCardno = string.Empty;   //�����
        //private string outCardno = string.Empty;   //�����
        private string ReturnCardNum = string.Empty;//�˵������
        public float discount = 0;//�Ƿ����ؼ۲� 0 �ؼ۲�
        private const int countPerRow = 4;  //listViewһ��Ļÿ�п���ʾ����ĸ���
        private const int countPerColumn = 4;   //listViewһ��Ļÿ�п���ʾ����ĸ���
        private int currentPage = 1;
        public string CardTypename = string.Empty;
        public string  Youhuiamount = string.Empty;





        public SaleForm(Model.Config config)
        {
            ShowWaitMsg("���ڴ򿪣����Ժ�...");
            InitializeComponent();
            this.config = config;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaleForm_Load(object sender, EventArgs e)
        {
            unitTxt.Text = string.Empty;
            specsTxt.Text = string.Empty;
            payFlowList = new List<Model.PayFlow>();
            saleFlowList = new List<Model.SaleFlow>();
            BindingDataGrid();
            #region ��ȡ��Ʒ�б�
            spxinxiList = new Action.Sqlite.SqliteSpxinxiAction(config.SqliteConnStr).getSpxinxi();
            #endregion

            //��ʾ��һҳ16��
            listView1.Clear();
            for (int i = 0; i < countPerRow * countPerColumn; i++)
            {
                if (i <spxinxiList.Count)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = ((Model.Spxinxi)spxinxiList[i]).FName;
                    lvi.Tag = ((Model.Spxinxi)spxinxiList[i]).Incode;
                    lvi.ImageIndex = 0;
                    lvi.ImageIndex = 0;
                    listView1.Items.Add(lvi);
                }
                else
                {
                    break;
                }
            }                   
            Thread.Sleep(500);

            HideWaitMsg();

        }
     



        //���񰴼��¼���
        private void SaleForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    this.button1_Click(null, null);
                    break;
                case Keys.F2:
                    this.button2_Click(null, null);
                    break;
                case Keys.F3:
                    this.button3_Click(null, null);
                    break;
                case Keys.F4:
                    this.button4_Click(null, null);
                    break;
                case Keys.F5:
                    this.button5_Click(null, null);
                    break;
                case Keys.F6:
                    try
                    {
                        if (saleFlowList.Count > 0)
                        {
                            if (saleFlowList.Count > 18)
                                MessageBox.Show("���۵���Ʒ̫�࣬һ�����۵���Ʒ���ܳ���18�֣�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                            else
                                EndBusiness();
                        }
                        else
                        {
                            this.txtcode.Focus();
                        }
                    }
                    catch { }

                              
                    break;
                case Keys.F14:
                    //PrintPreviousNotes();
                    break;
                case Keys.Escape:
                    if (this.txtcode.Text.Length > 0)
                        ClearSet();
                    else
                    {
                        if (this.dataGrid1.VisibleRowCount > 0)
                        {
                            if (MessageBox.Show("��������δ����ȷ���˳���", "�˳�ȷ��",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                this.Close();
                        }
                        else
                            this.Close();
                    }                 
                    e.Handled = true;
                    break;
                case Keys.Up:
                    UpRow(dataGrid1);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    DownRow(dataGrid1, saleFlowList.Count);
                    e.Handled = true;
                    break;
                case Keys.Return:
                    ExecutePro();
                    e.Handled = true;
                    break;
            }
        }




        /// <summary>
        /// ��Ʒ�б�ѡ�и���,d��ѡ��Ʒ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                ShowAddGoods(listView1.FocusedItem.Tag.ToString());
            }
        }


        /// <summary>
        /// ���f1 �����Ʒ�б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            saleFlowList.Clear();
            payFlowList.Clear();
            spxinxiTmp = null;
            heji = 0;
            //if (this.txtcode.Text.Length > 0)
            //{
            //    this.txtPrice.Focus();
            //    this.txtPrice.SelectAll();
            //}
            #region �����Ļ��ʾ
            ClearSet();

            #endregion
            BindingDataGrid();
        }


        /// <summary>
        /// ɾ����Ʒ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (this.dataGrid1.VisibleRowCount == 0)
                //û����Ʒ
                return;
            int row = dataGrid1.CurrentCell.RowNumber;
            heji -= saleFlowList[row].Real_total;
            saleFlowList.RemoveAt(row);
            
            BindingDataGrid();

            if (row > 0)
            {
                //����ղ�ѡ�е���������0
                this.dataGrid1.Select(row - 1);
            }
            else if (dataGrid1.VisibleRowCount > 0)
            {
                this.dataGrid1.Select(0);
            }
        }



        bool isSaleReturn = false;
        string saleNo = "";

        /// <summary>
        /// �˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {          
            if (!isSaleReturn)
            {
                Business.SaleReturnForm saleReturnForm = new SaleReturnForm();

                if (saleReturnForm.ShowDialog() == DialogResult.OK)
                {
                    saleNo = saleReturnForm.SaleNo;

                    #region ��ȡ��Ʒ��ˮ��Ϣ
                    saleFlowList = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getSaleFlow(saleNo);
                    #endregion

                    if (saleFlowList.Count > 0)
                    {
                        if (!saleFlowList[0].CanReturn)
                        {
                            MessageBox.Show("[" + saleNo + "]��ˮ���˻����������ظ��˻���");
                        }
                        else
                        {
                            foreach (Model.SaleFlow saleflow in saleFlowList)
                            {
                                heji += saleflow.Real_total;
                            }
                            BindingDataGrid();
                            this.isSaleReturn = true;
                            this.lblItem.Text = "[�˵�]";
                            this.lblItem.ForeColor = Color.Red;
                            this.button3.Text = "ȡ���˵�";
                        }
                    }
                    else
                    {
                        MessageBox.Show("δ�ҵ�������ˮ��Ϊ[" + saleNo + "]���������ݣ�");
                    }
                }
                saleReturnForm.Dispose();
            }
            else
            {
                saleFlowList.Clear();
                BindingDataGrid();
                ClearSet();
                this.isSaleReturn = false;
                this.lblItem.Text = "[����]";
                this.lblItem.ForeColor = Color.Black;
                this.button3.Text = "�˵�";
            }
                     
        }




        //��һҳ
        private void button4_Click(object sender, EventArgs e)
        {
            
            if (currentPage > 1)
            {
                listView1.Clear();
                currentPage--;
                for (int i = (currentPage - 1) * countPerRow * countPerColumn; i < currentPage * countPerRow * countPerColumn; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = spxinxiList[i].FName;
                    lvi.Tag = spxinxiList[i].Incode;
                    //lvi.ImageIndex = i / countPerRow % 2;
                    lvi.ImageIndex = 0;
                    listView1.Items.Add(lvi);
                }
            }
             
        }




        /// <summary>
        /// �����һҳ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (currentPage * countPerRow * countPerColumn <spxinxiList.Count)
            {
                listView1.Clear();
                for (int i = currentPage * countPerRow * countPerColumn; i < (currentPage + 1) * countPerRow * countPerColumn; i++)
                {
                    if (i < spxinxiList.Count)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = spxinxiList[i].FName;
                        lvi.Tag = spxinxiList[i].Incode;
                        //lvi.ImageIndex = i / countPerRow % 2;
                        lvi.ImageIndex = 0;
                        listView1.Items.Add(lvi);
                    }
                    else
                    {
                        break;
                    }
                }
                currentPage++;
            }
             
        }




        /// <summary>
        /// �����ı���ý���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNumber_GotFocus(object sender, EventArgs e)
        {
            if (this.txtcode.Text == "")
                this.txtcode.Focus();
        }


        /// <summary>
        /// ��ѡ��Ʒ��ǰ�иı�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            this.dataGrid1.Select(this.dataGrid1.CurrentCell.RowNumber);
        }


        private void GetNum(string txt)
        {
            try
            {
                if (this.txtNumber.Text != txt && txt != "0.000" && txt != "")
                {
                    this.txtNumber.Text = txt;
                    this.txtNumber.SelectAll();
                }
                else if (txt == "0.000")
                {
                    if (this.txtNumber.Text != "0.000" && this.txtNumber.Text.IndexOf(".") > 0 &&
                       this.txtNumber.Text.Length - this.txtNumber.Text.IndexOf(".") - 1 == 3)
                    {
                        this.txtNumber.Text = "0.000";
                        this.txtNumber.SelectAll();
                    }
                }

            }
            catch { }
        }




        //����س���
        private void ExecutePro()
        {
            if (this.txtcode.Focused)
            {
                //��Ʒ����
                if (this.txtcode.Text.Length > 0)
                {
                    this.txtNumber.Text = "1";
                    this.txtNumber.Focus();
                    this.txtNumber.SelectAll();
                }
                else if (this.txtcode.Text.Length == 0)
                {
                    this.txtcode.Focus();
                    this.txtcode.SelectAll();
                }
            }
            else if (this.txtPrice.Focused)
            {
                //����
                try
                {
                    float.Parse(this.txtPrice.Text.Trim());
                    this.txtNumber.Focus();
                    this.txtNumber.SelectAll();
                }
                catch
                {
                    this.txtPrice.Focus();
                    this.txtPrice.SelectAll();
                }
            }
            else if (this.txtNumber.Focused)
            {
                //����
                try
                {
                    decimal.Parse(this.txtNumber.Text.Trim());
                    string[] str = this.txtNumber.Text.Trim().Split('.');
                    if (str.Length == 2)
                    {
                        if (str[1].Length > 0)
                        {
                            if (this.txtcode.Text.Length > 0)
                            {
                                
                                AddProduct(this.txtcode.Text.Trim());
                            }
                        }
                    }
                    else if (str.Length == 1)
                    {
                        if (this.txtcode.Text.Length > 0)
                        {
                            AddProduct(this.txtcode.Text.Trim());
                        }
                    }
                    else
                    {
                        this.txtNumber.Focus();
                        this.txtNumber.SelectAll();
                    }
                }
                catch
                {
                    WintecIDT700.Beep(100);
                    System.Threading.Thread.Sleep(50);
                    WintecIDT700.Beep(100);
                    System.Threading.Thread.Sleep(50);
                    WintecIDT700.Beep(100);

                    MessageBox.Show("��������ȷ������");
                    this.txtNumber.Focus();
                    this.txtNumber.SelectAll();
                }
            }
            else
            {
                this.txtcode.Focus();
            }
        }





        //�����Ʒ��Ϣ
        void ClearSet()
        {
            this.txtcode.Text = "";
            this.txtcode.Focus();
            this.lblName.Text = "";
            this.txtPrice.Text = "0.00";
            this.txtNumber.Text = "0";
            this.unitTxt.Text = string.Empty;
            this.specsTxt.Text = string.Empty;

        }




        //��ʾɨ����������Ʒ��Ϣ
        Model.Spxinxi spxinxiTmp;

        void ShowAddGoods(string str)
        {
            spxinxiTmp = new Action.Sqlite.SqliteSpxinxiAction(config.SqliteConnStr).getSpxinxi(str);
            if (spxinxiTmp.Incode != null)
            {
                this.txtcode.Text = spxinxiTmp.Incode;
                this.lblName.Text = spxinxiTmp.FName;//��ʾ��Ʒ���
                this.unitTxt.Text = spxinxiTmp.Unit;
                this.specsTxt.Text = spxinxiTmp.Specs;
                this.txtPrice.Text = spxinxiTmp.Price.ToString("F2");
                this.txtNumber.Text = "1";
                this.txtNumber.Focus();
                this.txtNumber.SelectAll();

            }
            else
            {
                NoThisGoods();
            }
        }



        /// <summary>
        /// ����Ʒ���뵽�����б���
        /// </summary>
        /// <param name="goodid"></param>
        public void AddProduct(string goodid)
        {
            string serialNo = string.Empty;
            try
            {
                if (saleFlowList.Count == 0)
                {
                    //�б���û����Ʒ��ˮ��ȡ�µ���ˮ��
                    #region ��ȡ��ǰ��ˮ��
                    serialNo = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getSerialNo(config.PosNo);
                    if (serialNo == "")
                    {
                        serialNo = DateTime.Now.ToString("MMddyy") + config.PosNo + "0001";
                    }
                    #endregion
                }
                else
                {
                    //�б����Ѿ�����Ʒ��ˮ��ȡ��ǰ��ˮ��
                    serialNo = saleFlowList[0].Serial_no;
                }

                if (decimal.Parse(txtNumber.Text.Trim()) > 0)
                {
                    //��������0
                    if (this.dataGrid1.VisibleRowCount > 0)
                        dataGrid1.UnSelect(dataGrid1.CurrentCell.RowNumber);


                    Model.SaleFlow saleFlow = new Model.SaleFlow();
                    saleFlow.Serial_no = serialNo;
                    saleFlow.Operater = user.UserCode;
                    saleFlow.Code = spxinxiTmp.Incode;
                    saleFlow.FName = spxinxiTmp.FName;
                    saleFlow.Price = float.Parse(this.txtPrice.Text);
                    saleFlow.Qty = float.Parse(float.Parse(txtNumber.Text).ToString("f2"));
                    saleFlow.Total = float.Parse((saleFlow.Price * saleFlow.Qty).ToString("f2"));
                    saleFlow.Pre_total = saleFlow.Total;
                    saleFlow.Real_total = saleFlow.Total;
                    saleFlow.Flag = Model.FlowUpLoadFlag.δ�ϴ�;
                    saleFlow.PosNo = config.PosNo;
                    saleFlow.RowNo = saleFlowList.Count;
                    saleFlow.Zdisc = 100;
                    saleFlow.Sgroup = config.Grpno;
                    //saleFlow.Sa_date = DateTime.Now.ToString("yyyy-MM-dd");
                    //saleFlow.Sa_time = DateTime.Now.ToLongTimeString();
                    saleFlow.Flag = Model.FlowUpLoadFlag.δ�ϴ�;
                    saleFlow.Squadno = "1";
                    saleFlowList.Add(saleFlow);

                    heji += saleFlow.Real_total;

                    BindingDataGrid();

                    //ѡ���޸Ļ���ӵ���
                    this.dataGrid1.Select(saleFlowList.Count-1);
                }
                else
                {
                    this.txtNumber.Focus();
                    this.txtNumber.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("�����Ʒ���������ԣ�");
            }
            finally
            {
                //ȡ����Ʒ�б�����Ʒ��ѡ��״̬
                listView1.FocusedItem.Selected = false;
                spxinxiTmp = null;
                ClearSet();
            }
        }





        /// <summary>
        /// ����ѡ��Ʒ�б�����Դ
        /// </summary>
        private void BindingDataGrid()
        {
            this.dataGrid1.DataSource = null;
            this.dataGridTableStyle1.MappingName = saleFlowList.GetType().Name;
            this.dataGrid1.DataSource = saleFlowList;
        }




        //�޴���Ʒ
        private void NoThisGoods()
        {
            WintecIDT700.Beep(100);
            System.Threading.Thread.Sleep(50);
            WintecIDT700.Beep(100);
            System.Threading.Thread.Sleep(50);
            WintecIDT700.Beep(100);
            ClearSet();
        }






        //����
        private void EndBusiness()
        {
            if (saleFlowList.Count == 0)
            {
                //û����Ʒ
                MessageBox.Show("����ѡ����Ʒ��");
                return;
            }
            EndBusinessForm endForm = new EndBusinessForm(heji, isSaleReturn);
            endForm.Location=new Point(395, 26);
            endForm.ShowDialog();
            if (!endForm.ok)
            {
                endForm.Dispose();
                txtcode.Focus();
                return;
            }
            if (endForm.ok)
            {
                string sdate = DateTime.Now.ToString("yyyy-MM-dd");
                string stime = DateTime.Now.ToString("HH:mm:ss");

                foreach (Model.SaleFlow saleflow in saleFlowList)
                {
                    if (isSaleReturn)
                    {
                        saleflow.Real_total = 0 - saleflow.Real_total;
                        saleflow.Qty = 0 - saleflow.Qty;
                        saleflow.Total = 0 - saleflow.Total;
                        saleflow.Pre_total = 0 - saleflow.Pre_total;
                    }
                    saleflow.Sa_date = sdate;
                    saleflow.Sa_time = stime;
                }

                foreach (Model.PayFlow payflow in endForm.payflowList)
                {
                    payflow.Serial_no = saleFlowList[0].Serial_no;
                    payflow.PosNo = saleFlowList[0].PosNo;
                    payflow.Sa_date = saleFlowList[0].Sa_date;
                    payflow.Sa_time = saleFlowList[0].Sa_time;
                    payflow.Flag = saleFlowList[0].Flag;
                    payflow.Operater = saleFlowList[0].Operater;
                    payflow.Squadno = saleFlowList[0].Squadno;
                    payflow.Sa_date = sdate;
                    payflow.Sa_time = stime;
                }
                payFlowList.AddRange(endForm.payflowList);
                endForm.Dispose();
                try
                {
                    //�ϴ�������


                    ShowWaitMsg("���ڽ��н��㣬���Ժ�...");
                    bool success = false;
                    if (!config.OffLineSale)
                    {
                        #region ���潻��
                        success = WebService.Pay(saleFlowList, payFlowList);
                        #endregion
                    }

                    #region ��������ɹ����޸ı�����ˮ�ϴ���־
                    if (success)
                    {
                        foreach (Model.SaleFlow sflow in saleFlowList)
                        {
                            sflow.Flag = Model.FlowUpLoadFlag.���ϴ�;
                        }
                        foreach (Model.PayFlow pflow in payFlowList)
                        {
                            pflow.Flag = Model.FlowUpLoadFlag.���ϴ�;
                        }
                    }
                    #endregion


                    #region ���汾����ˮ
                    bool bdsuccess = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).savePay(saleFlowList, payFlowList,isSaleReturn);
                    #endregion

                    if (!success && !bdsuccess)
                    {
                        //����������¸���
                        payFlowList.Clear();
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        MessageBox.Show("����ʧ�ܣ������³��ԣ��绹���ܽ��㣬����ϵ����Ա��");
                        return;
                    }
                    if (config.PrintBill == Model.PrintBillFlag.��ӡ)
                    {
                        PrintBill();
                    }
                    saleFlowList.Clear();
                    payFlowList.Clear();
                    heji = 0;
                    ClearSet();
                    if (isSaleReturn)
                    {
                        this.isSaleReturn = false;
                        this.lblItem.Text = "[����]";
                        this.lblItem.ForeColor = Color.Black;
                        this.button3.Text = "�˵�";
                    }
                       
                }



                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    MessageBox.Show("������������ԣ�");
                    payFlowList.Clear();
                }
                finally
                {
                    BindingDataGrid();
                    HideWaitMsg();
                }
            }
        }




        //��ӡ
        void PrintBill()
        {
            float amount = 0.00f;
            int qty = 0;
            //��¼�˴����ۣ������ٴδ�ӡ
            /*
            PreviousSaleTempList.Clear(); 
            PreviousTraceNumber = saleFlowList[0].Serial_no;
            PreviousCardNumber = czCard.Outcardno;
            PreviousHeji = total.ToString();
            PreviousOriginal = remainToalTxtbox.Text.Trim();
            PreviousBalance = czkTotalTxt.Text.Trim();
            PreviousCardtypename = CardTypename;
            Previousdiscount = discount.ToString();
            PreviousYouhuiamount = Youhuiamount.ToString();

            foreach (Model.SaleFlow selldetail in saleFlowList)
            {
                PreviousSaleTempList.Add(selldetail);
            }
                        */
            int index = 0;
            
            string[] printStr = new string[ saleFlowList.Count*2+payFlowList.Count*2+15];
            //string[] printStr1 = new string[1];
            //for (int i = 0; i < ((8 - AppConfig.ConfigInfo.CompanyName.Trim().Length) / 2); i++)
            //{
            //    printStr1[0] += "��";
            //}
            //printStr1[0] += AppConfig.ConfigInfo.CompanyName;
            //WintecIDT700.PrintLineStr(printStr1, 1, 4);
            //WintecIDT700.FeedPaper(40);
            //û��32��Ӣ���ַ� 16������

            printStr[++index] = feedStr(config.CompanyName);
            printStr[++index] = feedStr(config.DeptName);
            printStr[++index] = "���ݱ�ţ�" + saleFlowList[0].Serial_no + (isSaleReturn == false ? "" : "[��]");
            printStr[++index] = "��ӡʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            printStr[++index] = "������ţ�" + config.PosNo;
            printStr[++index] = "��� ����      ����   ���� ��� ";
            printStr[++index] = "��������������������������������";
            int count = 1;
            foreach (Model.SaleFlow saleFlow in saleFlowList)
            {
                printStr[++index] =
                    count.ToString().PadRight(4, ' ') +
                    (saleFlow.FName.Length > 6 ? saleFlow.FName.Substring(0, 6).PadRight(7, ' ') : saleFlow.FName.PadRight(7, ' ')) +
                    saleFlow.Price.ToString("F2") +"  "+
                    saleFlow.Qty.ToString()+ "  "+" "+
                    saleFlow.Real_total.ToString("F2");
                printStr[++index] = "------------------------------";
                qty += qty;
                amount += saleFlow.Total;
                count++;
            }
            printStr[++index] = "------------------------------";
             printStr[++index]="�ϼƣ�                   " + amount.ToString("F2");
           // printStr[++index] = "������������������������������";
            //printStr[++index] = "           ��Ƭ��Ϣ           ";
            //printStr[++index] = "����    ������      ���";
            //foreach (Model.PayFlow payflow in payFlowList)
          //  {
            //    printStr[++index] = payflow.Ref_no+" "+payflow.Total.ToString("F2")+"   "+payflow.Scye;
           // }
            //if (!isSaleReturn)
            //{
               // printStr[++index] = "�Żݽ�" + Youhuiamount;
               // printStr[++index] = "�� �� �ʣ�" + discount * 100 + "%";
               // printStr[++index] = "����ԭ��" + remainToalTxtbox.Text.Trim();               
           // }
           // printStr[++index] = "��������" + czkTotalTxt.Text.Trim();
            printStr[++index] = "������������������������������";
            if (config.Str1 != string.Empty)
            {
                printStr[++index] = feedStr(config.Str1);
                printStr[++index] = string.Empty;
            }
            if (config.Str2 != string.Empty)
            {
                printStr[++index] = feedStr(config.Str2);
                printStr[++index] = string.Empty;
            }
            if (config.Str3 != string.Empty)
            {
                printStr[++index] = feedStr(config.Str3);
            }
            WintecIDT700.PrintLineStr(printStr, printStr.Length, 1);
            WintecIDT700.FeedPaper(200);

         
        }

        /// <summary>
        /// ����ӡ�ַ���
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string feedStr(string str)
        {
            int lc = 25 - str.Length;
            str = str.PadLeft(str.Length + lc / 2, ' ');
            str = str.PadRight(str.Length + lc / 2, ' ');

            return str;
        }

        private string PreviousTraceNumber = string.Empty;
        private string PreviousCardNumber = string.Empty;
        private string PreviousHeji = string.Empty;
        private string PreviousOriginal = string.Empty;
        private string PreviousBalance = string.Empty;
        private string PreviousCardtypename = string.Empty;
        private string Previousdiscount = string.Empty;
        private string PreviousYouhuiamount = string.Empty;

        List<Model.SaleFlow> PreviousSaleTempList = new List<Model.SaleFlow>();

      

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

            this.button1.Click -= new System.EventHandler(this.button1_Click);
            this.button2.Click -= new System.EventHandler(this.button2_Click);
            this.button3.Click -= new System.EventHandler(this.button3_Click);
            this.button4.Click -= new System.EventHandler(this.button4_Click);
            this.button5.Click -= new System.EventHandler(this.button5_Click);
            this.txtNumber.GotFocus -= new System.EventHandler(this.txtNumber_GotFocus);
            this.dataGrid1.CurrentCellChanged -= new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            this.listView1.SelectedIndexChanged -= new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.Load -= new System.EventHandler(this.SaleForm_Load);
            this.Closed -= new System.EventHandler(this.SaleForm_Closed);
            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.SaleForm_KeyDown);
        }

    }
}