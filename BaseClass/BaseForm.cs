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
        public static string Emp = "";
        /// <summary>
        /// �´������ʾ
        /// </summary>
        public static bool P1Visible = false;
        /// <summary>
        /// �������롢ȷ����ʾ
        /// </summary>
        public static bool P2Visible = false;
        /// <summary>
        /// �������ͨ����ʾ
        /// </summary>
        public static bool P3Visible = false;
        /// <summary>
        /// �Զ�����ͬ��������
        /// </summary>
        public static bool P4Visible = false;
        /// <summary>
        /// ��֪ͨ��ʾ
        /// </summary>
        public static bool P5Visible = false;
        private void timer_Tick(object sender, EventArgs e)
        {
            this.labelTime.Text = DateTime.Now.ToString("HH:mm");
            this.labelEmp.Text = Emp;
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