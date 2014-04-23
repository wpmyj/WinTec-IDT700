using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace BaseClass
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
            this.labelTime.Text = DateTime.Now.ToString("HH:mm");
            if (waitForm == null)
            {
                waitForm = new WaitForm("");
            }
        }

        private void BaseForm_Closing(object sender, CancelEventArgs e)
        {
            this.timer.Enabled = false;
        }

        #region 显示系统时间
        /// <summary>
        /// 显示时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            this.labelTime.Text = DateTime.Now.ToString("HH:mm");
        }
        #endregion


        #region 显示等待提示窗口
        static WaitForm waitForm;
        /// <summary>
        /// 显示等待提示窗口
        /// </summary>
        /// <param name="msg">提示信息</param>
        public void ShowWaitMsg(string _msg)
        {
            waitForm.Msg = _msg;
            waitForm.Show();
            this.Enabled = false;
            Application.DoEvents();
        }



        /// <summary>
        /// 关闭等待提示窗口
        /// </summary>
        public void HideWaitMsg()
        {
            waitForm.Hide();
            this.Enabled = true;
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
            this.Enabled = false;
        }



        /// <summary>
        /// 隐藏等待光标
        /// </summary>
        public void HideWaitCursor()
        { 
            Cursor.Current = Cursors.Default;
            Cursor.Hide();
            this.Enabled = true;
        }
        #endregion

        #region datagrid翻页
        public int currentRowIndex = 0;
        /// <summary>
        /// 上翻页
        /// </summary>
        /// <param name="dataGrid">要翻页的DataGrid</param>
        public void UpPage(DataGrid dataGrid)
        {
            if (dataGrid.CurrentCell.RowNumber >0)
            {
                dataGrid.UnSelect(dataGrid.CurrentCell.RowNumber);
                if (currentRowIndex - dataGrid.VisibleRowCount > 0)
                    currentRowIndex = currentRowIndex - dataGrid.VisibleRowCount;
                else
                    currentRowIndex = 0;
                dataGrid.Select(currentRowIndex);
            }
        }

        /// <summary>
        /// 下翻页
        /// </summary>
        /// <param name="dataGrid">要翻页的DataGrid</param>
        /// <param name="pageSize">页大小</param>
        public void DownPage(DataGrid dataGrid, int totalRows)
        {
            if (dataGrid.CurrentCell.RowNumber < totalRows - 1)
            {
                dataGrid.UnSelect(dataGrid.CurrentCell.RowNumber);
                if (currentRowIndex + dataGrid.VisibleRowCount > totalRows - 1)
                    currentRowIndex = totalRows - 1;
                else
                    currentRowIndex = currentRowIndex + dataGrid.VisibleRowCount;
                dataGrid.Select(currentRowIndex);
            }
        }
        #endregion


        #region datagrid上一条下一条
        /// <summary>
        /// 上一条
        /// </summary>
        /// <param name="dataGrid"></param>
        public void UpRow(DataGrid dataGrid)
        {
            if (dataGrid.CurrentCell.RowNumber > 0)
            {
                dataGrid.UnSelect(dataGrid.CurrentCell.RowNumber);
                dataGrid.Select(dataGrid.CurrentCell.RowNumber-1);
            }
            else if (dataGrid.VisibleRowCount > 0)
            {
                dataGrid.Select(0);
            }
            currentRowIndex = dataGrid.CurrentRowIndex;
        }
        /// <summary>
        /// 下一条
        /// </summary>
        /// <param name="dataGrid"></param>
        public void DownRow(DataGrid dataGrid, int totalRows)
        {
            if (dataGrid.CurrentCell.RowNumber < totalRows - 1)
            {
                dataGrid.UnSelect(dataGrid.CurrentCell.RowNumber);
                dataGrid.Select(dataGrid.CurrentCell.RowNumber+1);
            }
            else if (totalRows > 0)
            {
                dataGrid.Select(totalRows - 1);
            }
        }
        #endregion

    }

}