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


        //float bkPayJe = 0;//当前卡付款金额
        string serial_no = string.Empty;//当前流水号

        List<Model.Spxinxi> spxinxiList;//常用商品

        List<Model.SaleFlow> saleFlowList;//临时商品流水列表
        List<Model.PayFlow> payFlowList;//临时付款流水列表

        //private float nowPayTotal = 0;//已付金额
        //private float total = 0; //合计金额

        
        //private string inCardno = string.Empty;   //卡编号
        //private string outCardno = string.Empty;   //卡面号
        private string ReturnCardNum = string.Empty;//退单卡编号
        public float discount = 0;//是否是特价菜 0 特价菜
        private const int countPerRow = 4;  //listView一屏幕每行可显示的项的个数
        private const int countPerColumn = 4;   //listView一屏幕每列可显示的项的个数
        private int currentPage = 1;
        public string CardTypename = string.Empty;
        public string  Youhuiamount = string.Empty;





        public SaleForm(Model.Config config)
        {
            ShowWaitMsg("正在打开，请稍候...");
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
            #region 读取商品列表
            spxinxiList = new Action.Sqlite.SqliteSpxinxiAction(config.SqliteConnStr).getSpxinxi();
            #endregion

            //显示第一页16个
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
     



        //捕获按键事件。
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
                                MessageBox.Show("销售的商品太多，一次销售的商品不能超过18种！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
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
                            if (MessageBox.Show("尚有数据未处理，确认退出吗？", "退出确认",
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
        /// 商品列表选中更改,d点选商品
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
        /// 点击f1 清空商品列表
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
            #region 清空屏幕显示
            ClearSet();

            #endregion
            BindingDataGrid();
        }


        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (this.dataGrid1.VisibleRowCount == 0)
                //没有商品
                return;
            int row = dataGrid1.CurrentCell.RowNumber;
            heji -= saleFlowList[row].Real_total;
            saleFlowList.RemoveAt(row);
            
            BindingDataGrid();

            if (row > 0)
            {
                //如果刚才选中的列数大于0
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
        /// 退单
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

                    #region 获取商品流水信息
                    saleFlowList = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getSaleFlow(saleNo);
                    #endregion

                    if (saleFlowList.Count > 0)
                    {
                        if (!saleFlowList[0].CanReturn)
                        {
                            MessageBox.Show("[" + saleNo + "]流水已退货！不允许重复退货！");
                        }
                        else
                        {
                            foreach (Model.SaleFlow saleflow in saleFlowList)
                            {
                                heji += saleflow.Real_total;
                            }
                            BindingDataGrid();
                            this.isSaleReturn = true;
                            this.lblItem.Text = "[退单]";
                            this.lblItem.ForeColor = Color.Red;
                            this.button3.Text = "取消退单";
                        }
                    }
                    else
                    {
                        MessageBox.Show("未找到销售流水号为[" + saleNo + "]的销售数据！");
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
                this.lblItem.Text = "[销售]";
                this.lblItem.ForeColor = Color.Black;
                this.button3.Text = "退单";
            }
                     
        }




        //上一页
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
        /// 点击下一页按钮
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
        /// 数量文本获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNumber_GotFocus(object sender, EventArgs e)
        {
            if (this.txtcode.Text == "")
                this.txtcode.Focus();
        }


        /// <summary>
        /// 已选商品当前列改变
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




        //处理回车键
        private void ExecutePro()
        {
            if (this.txtcode.Focused)
            {
                //商品编码
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
                //单价
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
                //数量
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

                    MessageBox.Show("请输入正确数量！");
                    this.txtNumber.Focus();
                    this.txtNumber.SelectAll();
                }
            }
            else
            {
                this.txtcode.Focus();
            }
        }





        //清空商品信息
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




        //显示扫描或输入的商品信息
        Model.Spxinxi spxinxiTmp;

        void ShowAddGoods(string str)
        {
            spxinxiTmp = new Action.Sqlite.SqliteSpxinxiAction(config.SqliteConnStr).getSpxinxi(str);
            if (spxinxiTmp.Incode != null)
            {
                this.txtcode.Text = spxinxiTmp.Incode;
                this.lblName.Text = spxinxiTmp.FName;//显示商品简称
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
        /// 将商品加入到销售列表中
        /// </summary>
        /// <param name="goodid"></param>
        public void AddProduct(string goodid)
        {
            string serialNo = string.Empty;
            try
            {
                if (saleFlowList.Count == 0)
                {
                    //列表中没有商品流水，取新的流水号
                    #region 获取当前流水号
                    serialNo = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getSerialNo(config.PosNo);
                    if (serialNo == "")
                    {
                        serialNo = DateTime.Now.ToString("MMddyy") + config.PosNo + "0001";
                    }
                    #endregion
                }
                else
                {
                    //列表中已经有商品流水，取当前流水号
                    serialNo = saleFlowList[0].Serial_no;
                }

                if (decimal.Parse(txtNumber.Text.Trim()) > 0)
                {
                    //数量大于0
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
                    saleFlow.Flag = Model.FlowUpLoadFlag.未上传;
                    saleFlow.PosNo = config.PosNo;
                    saleFlow.RowNo = saleFlowList.Count;
                    saleFlow.Zdisc = 100;
                    saleFlow.Sgroup = config.Grpno;
                    //saleFlow.Sa_date = DateTime.Now.ToString("yyyy-MM-dd");
                    //saleFlow.Sa_time = DateTime.Now.ToLongTimeString();
                    saleFlow.Flag = Model.FlowUpLoadFlag.未上传;
                    saleFlow.Squadno = "1";
                    saleFlowList.Add(saleFlow);

                    heji += saleFlow.Real_total;

                    BindingDataGrid();

                    //选中修改或添加的行
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
                MessageBox.Show("添加商品出错，请重试！");
            }
            finally
            {
                //取消商品列表中商品的选中状态
                listView1.FocusedItem.Selected = false;
                spxinxiTmp = null;
                ClearSet();
            }
        }





        /// <summary>
        /// 绑定已选商品列表数据源
        /// </summary>
        private void BindingDataGrid()
        {
            this.dataGrid1.DataSource = null;
            this.dataGridTableStyle1.MappingName = saleFlowList.GetType().Name;
            this.dataGrid1.DataSource = saleFlowList;
        }




        //无此商品
        private void NoThisGoods()
        {
            WintecIDT700.Beep(100);
            System.Threading.Thread.Sleep(50);
            WintecIDT700.Beep(100);
            System.Threading.Thread.Sleep(50);
            WintecIDT700.Beep(100);
            ClearSet();
        }






        //结算
        private void EndBusiness()
        {
            if (saleFlowList.Count == 0)
            {
                //没有商品
                MessageBox.Show("请先选择商品！");
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
                    //上传服务器


                    ShowWaitMsg("正在进行结算，请稍侯...");
                    bool success = false;
                    if (!config.OffLineSale)
                    {
                        #region 保存交易
                        success = WebService.Pay(saleFlowList, payFlowList);
                        #endregion
                    }

                    #region 联机保存成功，修改本地流水上传标志
                    if (success)
                    {
                        foreach (Model.SaleFlow sflow in saleFlowList)
                        {
                            sflow.Flag = Model.FlowUpLoadFlag.已上传;
                        }
                        foreach (Model.PayFlow pflow in payFlowList)
                        {
                            pflow.Flag = Model.FlowUpLoadFlag.已上传;
                        }
                    }
                    #endregion


                    #region 保存本地流水
                    bool bdsuccess = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).savePay(saleFlowList, payFlowList,isSaleReturn);
                    #endregion

                    if (!success && !bdsuccess)
                    {
                        //结算出错，重新付款
                        payFlowList.Clear();
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        MessageBox.Show("交易失败！请重新尝试！如还不能结算，请联系管理员！");
                        return;
                    }
                    if (config.PrintBill == Model.PrintBillFlag.打印)
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
                        this.lblItem.Text = "[销售]";
                        this.lblItem.ForeColor = Color.Black;
                        this.button3.Text = "退单";
                    }
                       
                }



                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    MessageBox.Show("结算出错，请重试！");
                    payFlowList.Clear();
                }
                finally
                {
                    BindingDataGrid();
                    HideWaitMsg();
                }
            }
        }




        //打印
        void PrintBill()
        {
            float amount = 0.00f;
            int qty = 0;
            //记录此次销售，便于再次打印
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
            //    printStr1[0] += "　";
            //}
            //printStr1[0] += AppConfig.ConfigInfo.CompanyName;
            //WintecIDT700.PrintLineStr(printStr1, 1, 4);
            //WintecIDT700.FeedPaper(40);
            //没行32个英文字符 16个汉字

            printStr[++index] = feedStr(config.CompanyName);
            printStr[++index] = feedStr(config.DeptName);
            printStr[++index] = "单据编号：" + saleFlowList[0].Serial_no + (isSaleReturn == false ? "" : "[退]");
            printStr[++index] = "打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            printStr[++index] = "机器编号：" + config.PosNo;
            printStr[++index] = "序号 名称      单价   数量 金额 ";
            printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
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
             printStr[++index]="合计：                   " + amount.ToString("F2");
           // printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
            //printStr[++index] = "           卡片信息           ";
            //printStr[++index] = "卡号    付款金额      余额";
            //foreach (Model.PayFlow payflow in payFlowList)
          //  {
            //    printStr[++index] = payflow.Ref_no+" "+payflow.Total.ToString("F2")+"   "+payflow.Scye;
           // }
            //if (!isSaleReturn)
            //{
               // printStr[++index] = "优惠金额：" + Youhuiamount;
               // printStr[++index] = "折 扣 率：" + discount * 100 + "%";
               // printStr[++index] = "卡内原余额：" + remainToalTxtbox.Text.Trim();               
           // }
           // printStr[++index] = "卡内现余额：" + czkTotalTxt.Text.Trim();
            printStr[++index] = "＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝";
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
        /// 填充打印字符串
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