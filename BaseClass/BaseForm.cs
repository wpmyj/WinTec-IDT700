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

        #region ��ʾϵͳʱ��
        /// <summary>
        /// ��ʾʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            this.labelTime.Text = DateTime.Now.ToString("HH:mm");
        }
        #endregion


        #region ��ʾ�ȴ���ʾ����
        static WaitForm waitForm;
        /// <summary>
        /// ��ʾ�ȴ���ʾ����
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        public void ShowWaitMsg(string _msg)
        {
            waitForm.Msg = _msg;
            waitForm.Show();
            this.Enabled = false;
            Application.DoEvents();
        }



        /// <summary>
        /// �رյȴ���ʾ����
        /// </summary>
        public void HideWaitMsg()
        {
            waitForm.Hide();
            this.Enabled = true;
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
            this.Enabled = false;
        }



        /// <summary>
        /// ���صȴ����
        /// </summary>
        public void HideWaitCursor()
        { 
            Cursor.Current = Cursors.Default;
            Cursor.Hide();
            this.Enabled = true;
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
        /// �·�ҳ
        /// </summary>
        /// <param name="dataGrid">Ҫ��ҳ��DataGrid</param>
        /// <param name="pageSize">ҳ��С</param>
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


        #region datagrid��һ����һ��
        /// <summary>
        /// ��һ��
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
        /// ��һ��
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