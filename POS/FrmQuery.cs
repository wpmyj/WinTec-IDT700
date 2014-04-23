using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BaseClass;
using Trans;

namespace POS
{
    public partial class FrmQuery : FrmBase
    {
        public FrmQuery()
        {
            InitializeComponent();
            mSaleFlowBindingSource.DataSource = PubGlobal.BussinessVar.QuerySaleFlows;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            PubGlobal.BussinessVar.QuerySaleFlows.Clear();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            decimal total;
            string msg;
            if (!TransModule.QueryTrade(out total, out msg))
            {
                MessageBox.Show(msg);
            }
            else
            {
                lblTotal.Text = total.ToString("F2");
                mSaleFlowBindingSource.ResetBindings(false);
            }
        }

        private void FrmQuery_Load(object sender, EventArgs e)
        {
            lblSaDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            button1_Click(null, null);
        }

        private void FrmQuery_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    UpRow(dgQuerySaleFlow);
                    break;
                case Keys.Down:
                    DownRow(dgQuerySaleFlow, PubGlobal.BussinessVar.QuerySaleFlows.Count);
                    break;
            }
        }

        private void dgQuerySaleFlow_CurrentCellChanged(object sender, EventArgs e)
        {
            SelectSaleFlow(dgQuerySaleFlow.CurrentCell.RowNumber);
        }
        /// <summary>
        /// 选取指定列
        /// </summary>
        /// <param name="rowNo"></param>
        private void SelectSaleFlow(int rowNo)
        {
            if (rowNo < PubGlobal.BussinessVar.QuerySaleFlows.Count && rowNo >= 0)
            {
                dgQuerySaleFlow.UnSelect(dgQuerySaleFlow.CurrentRowIndex);
                dgQuerySaleFlow.Select(rowNo);
                dgQuerySaleFlow.CurrentRowIndex = rowNo;
            }
        }
    }
}