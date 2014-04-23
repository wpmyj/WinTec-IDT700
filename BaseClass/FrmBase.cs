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
        /// ����ȫ��
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
        /// ��֪ͨ��ʾ
        /// </summary>
        public static bool PNotifyVisible = false;

        /// <summary>
        /// �Զ�����ͬ����ʾ
        /// </summary>
        public static bool PDataSyncVisible = false;

        /// <summary>
        /// ����״̬  true������ false�ѻ���
        /// </summary>
        public static bool OnLine = true;


        
        #region ��ʾϵͳʱ�䡢�Զ�����ͬ����ʾ����֪ͨ��ʾ
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

        #region ��ʾlogo����
        /// <summary>
        /// ��ʾlogo����
        /// </summary>
        /// <param name="_deptName"></param>
        public void ShowDeptName(string _deptName)
        {
            lblDeptName.Text = _deptName;
        }
        #endregion

        #region ��ʾ�ȴ���ʾ����
        static FrmWait waitForm;
        /// <summary>
        /// ��ʾ�ȴ���ʾ����
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        public void ShowWaitMsg(string _msg)
        {
            waitForm.Msg = _msg;
            waitForm.Show();
            Application.DoEvents();
        }
        /// <summary>
        /// �رյȴ���ʾ����
        /// </summary>
        public void HideWaitMsg()
        {
            waitForm.Hide();
            this.Show();
            this.Focus();
        }
        /// <summary>
        /// ��ʾ�ȴ����
        /// </summary>
        public void ShowWaitCursor()
        {
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();
        }
        /// <summary>
        /// ���صȴ����
        /// </summary>
        public void HideWaitCursor()
        { 
            Cursor.Current = Cursors.Default;
            Cursor.Hide();
        }
        #endregion


        #region datagrid��ҳ
        public int currentRowIndex = 0;

        /// <summary>
        /// �Ϸ�ҳ
        /// </summary>
        /// <param name="dataGrid">Ҫ��ҳ��DataGrid</param>
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
        /// �·�ҳ
        /// </summary>
        /// <param name="dataGrid">Ҫ��ҳ��DataGrid</param>
        /// <param name="pageSize">ҳ��С</param>
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


        #region datagrid��һ����һ��
        /// <summary>
        /// ��һ��
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
        /// ��һ��
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