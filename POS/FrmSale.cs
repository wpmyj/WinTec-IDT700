using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Linq;
using PubGlobal;
using devices;
using Devices;
using Model;

namespace POS
{
    public partial class FrmSale : BaseClass.FrmBase
    {
        #region �Ӵ���
        /// <summary>
        /// ��������
        /// </summary>
        FrmInputQty frmInputQty = new FrmInputQty();

        /// <summary>
        /// �ϼƴ���
        /// </summary>
        FrmSettle frmSettle= new FrmSettle();
        #endregion

        #region ��ʼ�� 
        public FrmSale()
        {
            InitializeComponent();
            mSaleFlowBindingSource.DataSource = PubGlobal.BussinessVar.saleFlowList;
        }
        #endregion

        #region ˢ����Ʒ�б�
        public void RefreshGoodsMenu()
        {
            lvGoods.Items.Clear();//����б�
            if (PubGlobal.BussinessVar.goodsList == null)
            {
                return;
            }
            foreach (MGoods goods in PubGlobal.BussinessVar.goodsList)
            {
                ListViewItem item = new ListViewItem(goods.Fname);
                item.ImageIndex = 6;
                item.Tag = goods;
                item.ForeColor = Color.Blue;
                item.BackColor = Color.White;
                lvGoods.Items.Add(item);
            }
        }
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        private void AllClear()
        {
            PubGlobal.BussinessVar.saleFlowList.Clear();
            PubGlobal.BussinessVar.card = null;
            PubGlobal.BussinessVar.isReturn = false;
            PubGlobal.BussinessVar.payFlowList.Clear();

            mSaleFlowBindingSource.ResetBindings(false);
        }


        #region ����button�¼�
        /// <summary>
        /// ���񰴼��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                case Keys.F10:
                case Keys.F6:
                    //�ϼ�
                    if (PubGlobal.BussinessVar.saleFlowList.Count == 0)
                    {
                        break;
                    }
                    if (frmSettle.ShowDialog() == DialogResult.OK)
                    {
                        //���׳ɹ�
                        //�����Ϣ
                        AllClear();
                    }
                    //Application.DoEvents();
                    break;
                case Keys.Escape:
                    this.DialogResult = DialogResult.OK;
                    if (PubGlobal.BussinessVar.saleFlowList.Count == 0)
                    {
                        button5_Click(null, null);//�˳�
                    }
                    else
                    {
                        button3_Click(null, null);//����
                    }
                    break;
                case Keys.Up:
                    UpRow(dgSaleFlow);//��һ��
                    break;
                case Keys.Down://��һ��
                    DownRow(dgSaleFlow,PubGlobal.BussinessVar.saleFlowList.Count);
                    break;
                //case Keys.Return:
                //    //ExecutePro();//����س���
                //    e.Handled = true;
                //    break;
            }
            e.Handled = true;
        }
       

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int index = dgSaleFlow.CurrentRowIndex;
            if (index < 0)
            {
                return;
            }
            if (frmInputQty.ShowDialog() == DialogResult.OK)
            {
                MSaleFlow saleFlow = PubGlobal.BussinessVar.saleFlowList[index];

                saleFlow.Qty=frmInputQty.Qty*(PubGlobal.BussinessVar.isReturn?(-1):1);
                saleFlow.PreTotal = saleFlow.Price * saleFlow.Qty;
                saleFlow.RealTotal = saleFlow.Price * saleFlow.Qty;
                saleFlow.Total = saleFlow.Price * saleFlow.Qty;

                mSaleFlowBindingSource.ResetBindings(false);
                SelectSaleFlow(index);
            }
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (mSaleFlowBindingSource.Current == null)
            {
                return;
            }
            MSaleFlow saleFlow = mSaleFlowBindingSource.Current as MSaleFlow;
            if (MessageBox.Show("ȷ��ɾ����" + saleFlow.Fname + "����", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                PubGlobal.BussinessVar.saleFlowList.Remove(saleFlow);
                //��дRowNo
                for (int i = 0; i < PubGlobal.BussinessVar.saleFlowList.Count; i++)
                {
                    PubGlobal.BussinessVar.saleFlowList[i].RowNo = i + 1;
                }
                mSaleFlowBindingSource.ResetBindings(false);
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (PubGlobal.BussinessVar.saleFlowList.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("ɾ��������Ʒ��", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                AllClear();
            }
        }

        /// <summary>
        /// �˻�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (PubGlobal.BussinessVar.saleFlowList.Count == 0)
            {
                PubGlobal.BussinessVar.isReturn = !PubGlobal.BussinessVar.isReturn;
                button4.Text = PubGlobal.BussinessVar.isReturn ? "ȡ���˻�" : "�˻�";
            }
        }
        
        /// <summary>
        /// �˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (PubGlobal.BussinessVar.saleFlowList.Count > 0)
            {
                MessageBox.Show("��������",string.Empty,MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region ����

        //�޴���Ʒ
        private void NoThisGoods()
        {
            WintecIDT700.Beep(100);
            System.Threading.Thread.Sleep(50);
            WintecIDT700.Beep(100);
            System.Threading.Thread.Sleep(50);
            WintecIDT700.Beep(100);
            //ClearSet();
        }

        private void dgSaleFlow_CurrentCellChanged(object sender, EventArgs e)
        {
            SelectSaleFlow(dgSaleFlow.CurrentCell.RowNumber);
        }
        #endregion  

        #region ���
        /// <summary>
        /// �����Ʒ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvGoods_ItemActivate(object sender, EventArgs e)
        {
            MGoods goods = lvGoods.FocusedItem.Tag as MGoods;
            MSaleFlow saleFlow;
            int index = PubGlobal.BussinessVar.saleFlowList.FindIndex(a => a.Incode == goods.Incode);
            if (index >= 0)
            {
                saleFlow = PubGlobal.BussinessVar.saleFlowList[index];
                saleFlow.Qty += PubGlobal.BussinessVar.isReturn?(-1):1;
                saleFlow.PreTotal =saleFlow.Price*saleFlow.Qty;
                saleFlow.RealTotal = saleFlow.Price * saleFlow.Qty;
                saleFlow.Total = saleFlow.Price * saleFlow.Qty;
            }
            else
            {
                saleFlow = new MSaleFlow();
                saleFlow.Incode = goods.Incode;
                saleFlow.DeptCode = PubGlobal.SysConfig.DeptCode;
                saleFlow.Disc = 100;
                saleFlow.Fname = goods.Fname;
                saleFlow.Operater = PubGlobal.SysConfig.User.UserCode;
                saleFlow.PosNo = PubGlobal.SysConfig.PosNO;
                saleFlow.Price = goods.Price;
                saleFlow.Qty = PubGlobal.BussinessVar.isReturn?(-1):1;
                saleFlow.PreTotal = goods.Price*saleFlow.Qty;
                saleFlow.RealTotal = goods.Price*saleFlow.Qty;
                saleFlow.RowNo = PubGlobal.BussinessVar.saleFlowList.Count + 1;
                saleFlow.SquadNO = "1";
                saleFlow.Total = goods.Price*saleFlow.Qty;
                PubGlobal.BussinessVar.saleFlowList.Add(saleFlow);
                index = PubGlobal.BussinessVar.saleFlowList.Count - 1;
            }
            mSaleFlowBindingSource.ResetBindings(false);
            SelectSaleFlow(index);
        }


        /// <summary>
        /// ѡȡָ����
        /// </summary>
        /// <param name="rowNo"></param>
        private void SelectSaleFlow(int rowNo)
        {
            if (rowNo < PubGlobal.BussinessVar.saleFlowList.Count && rowNo >= 0)
            {
                dgSaleFlow.UnSelect(dgSaleFlow.CurrentRowIndex);
                dgSaleFlow.Select(rowNo);
                dgSaleFlow.CurrentRowIndex = rowNo;
            }
        }
        #endregion
    }
}