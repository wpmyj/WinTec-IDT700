using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using COMM;


namespace BaseClass
{
    public partial class FrmBase : Form
    {
        public FrmBase()
        {
            InitializeComponent();
            
            timer.Enabled = true;
            if (waitForm == null)
            {
                waitForm = new FrmWait("");
            }
        }

        /// <summary>
        /// 设置全屏
        /// </summary>
        /// <param name="fullscreen"></param>
        /// <param name="rectOld"></param>
        /// <returns></returns>
        public static bool SetFullScreen(bool fullscreen, ref Rectangle rectOld)
        {
            int Hwnd = 0;
            Hwnd = Win32.FindWindow("HHTaskBar", null);
            if (Hwnd == 0) return false;
            if (fullscreen)
            {
                Win32.ShowWindow((IntPtr)Hwnd, Win32.SW_HIDE);
                Rectangle rectFull = Screen.PrimaryScreen.Bounds;
                Win32.SystemParametersInfo(Win32.SPI_GETWORKAREA, 0, ref rectOld, Win32.SPIF_UPDATEINIFILE);//get
                Win32.SystemParametersInfo(Win32.SPI_SETWORKAREA, 0, ref rectFull, Win32.SPIF_UPDATEINIFILE);//set
            }
            else
            {
                Win32.ShowWindow((IntPtr)Hwnd, Win32.SW_SHOW);
                Win32.SystemParametersInfo(Win32.SPI_SETWORKAREA, 0, ref rectOld, Win32.SPIF_UPDATEINIFILE);
            }

            return true;
        }

        private void BaseForm_Closing(object sender, CancelEventArgs e)
        {
            this.timer.Enabled = false;
        }

        public static string Emp = "";
        /// <summary>
        /// 新通知提示
        /// </summary>
        public static bool PNotifyVisible = false;

        /// <summary>
        /// 自动数据同步提示
        /// </summary>
        public static bool PDataSyncVisible = false;

        /// <summary>
        /// 联机状态  true联机中 false脱机中
        /// </summary>
        public static bool OnLine = true;


        
        #region 显示系统时间、自动数据同步提示、新通知提示
        private void timer_Tick(object sender, EventArgs e)
        {
            this.labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            this.labelEmp.Text = Emp;
            if (PNotifyVisible)
                this.picNotify.Visible = !this.picNotify.Visible;
            else
                this.picNotify.Visible = false;
            if (PDataSyncVisible)
                this.picDataSync.Visible = !this.picDataSync.Visible;
            else
                this.picDataSync.Visible = false;
        }
        #endregion

        #region 显示logo标题
        /// <summary>
        /// 显示logo标题
        /// </summary>
        /// <param name="_deptName"></param>
        public void ShowDeptName(string _deptName)
        {
            lblDeptName.Text = _deptName;
        }
        #endregion

        #region 显示等待提示窗口
        static FrmWait waitForm;
        /// <summary>
        /// 显示等待提示窗口
        /// </summary>
        /// <param name="msg">提示信息</param>
        public void ShowWaitMsg(string _msg)
        {
            waitForm.Msg = _msg;
            waitForm.Show();
            Application.DoEvents();
        }
        /// <summary>
        /// 关闭等待提示窗口
        /// </summary>
        public void HideWaitMsg()
        {
            waitForm.Hide();
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
        public void DownRow(DataGrid dataGrid, int totalRows)
        {
            if (dataGrid.CurrentRowIndex < totalRows - 1)
            {
                dataGrid.UnSelect(dataGrid.CurrentRowIndex);
                dataGrid.CurrentRowIndex = dataGrid.CurrentRowIndex + 1;
                dataGrid.Select(dataGrid.CurrentRowIndex);
            }
            else if (totalRows > 0)
            {
                dataGrid.CurrentRowIndex = totalRows - 1;
                dataGrid.Select(dataGrid.CurrentRowIndex);
            }
            currentRowIndex = dataGrid.CurrentRowIndex;
        }
        #endregion

        private void FrmBase_Activated(object sender, EventArgs e)
        {
            this.labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }


    }
}