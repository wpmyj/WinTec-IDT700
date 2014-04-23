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
        public static string Emp = "";
        /// <summary>
        /// 新促销活动提示
        /// </summary>
        public static bool P1Visible = false;
        /// <summary>
        /// 调拨申请、确认提示
        /// </summary>
        public static bool P2Visible = false;
        /// <summary>
        /// 订单审核通过提示
        /// </summary>
        public static bool P3Visible = false;
        /// <summary>
        /// 自动数据同步进行中
        /// </summary>
        public static bool P4Visible = false;
        /// <summary>
        /// 新通知提示
        /// </summary>
        public static bool P5Visible = false;
        private void timer_Tick(object sender, EventArgs e)
        {
            this.labelTime.Text = DateTime.Now.ToString("HH:mm");
            this.labelEmp.Text = Emp;
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
            if (dataGrid.CurrentRowIndex >0)
            {
                dataGrid.UnSelect(dataGrid.CurrentRowIndex);
                if (currentRowIndex - dataGrid.VisibleRowCount > 0)
                    dataGrid.CurrentRowIndex = currentRowIndex - dataGrid.VisibleRowCount;
                else
                    dataGrid.CurrentRowIndex = 0;
                dataGrid.Select(dataGrid.CurrentRowIndex);
            }
            
            currentRowIndex = dataGrid.CurrentRowIndex;
        }

        /// <summary>
        /// 下翻页
        /// </summary>
        /// <param name="dataGrid">要翻页的DataGrid</param>
        /// <param name="pageSize">页大小</param>
        public void DownPage(DataGrid dataGrid, int totalRows)
        {
            if (dataGrid.CurrentRowIndex < totalRows - 1)
            {
                dataGrid.UnSelect(dataGrid.CurrentRowIndex);
                if (currentRowIndex + dataGrid.VisibleRowCount > totalRows - 1)
                    dataGrid.CurrentRowIndex = totalRows - 1;
                else
                    dataGrid.CurrentRowIndex = currentRowIndex + dataGrid.VisibleRowCount;
                dataGrid.Select(dataGrid.CurrentRowIndex);
            }
            currentRowIndex = dataGrid.CurrentRowIndex;
        }
        #endregion


        #region datagrid上一条下一条
        /// <summary>
        /// 上一条
        /// </summary>
        /// <param name="dataGrid"></param>
        public void UpRow(DataGrid dataGrid)
        {
            if (dataGrid.CurrentRowIndex > 0)
            {
                dataGrid.UnSelect(dataGrid.CurrentRowIndex);
                dataGrid.CurrentRowIndex = dataGrid.CurrentRowIndex - 1;
                dataGrid.Select(dataGrid.CurrentRowIndex);
            }
            else if (dataGrid.VisibleRowCount > 0)
            {
                dataGrid.CurrentRowIndex = 0;
                dataGrid.Select(0);
            }
            currentRowIndex = dataGrid.CurrentRowIndex;
        }
        /// <summary>
        /// 下一条
        /// </summary>
        /// <param name="dataGrid"></param>
        public void DownRow(DataGrid dataGrid)
        {
            try
            {
                if (dataGrid.CurrentRowIndex >=0)
                {
                    dataGrid.UnSelect(dataGrid.CurrentRowIndex);
                    dataGrid.CurrentRowIndex = dataGrid.CurrentRowIndex + 1;
                    dataGrid.Select(dataGrid.CurrentRowIndex);

                }
                else if (dataGrid.VisibleRowCount > 0)
                {
                    dataGrid.CurrentRowIndex = 0;
                    dataGrid.Select(0);
                    currentRowIndex = 0;
                }

            }
            catch
            {
                dataGrid.CurrentRowIndex = 0;
                dataGrid.Select(dataGrid.CurrentRowIndex);
            }
            finally
            {
                currentRowIndex = dataGrid.CurrentRowIndex;
            }

        }
        #endregion

    }
}