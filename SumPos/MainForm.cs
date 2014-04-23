using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Data.SqlClient;

namespace SumPos
{
    public partial class MainForm : BaseClass.BaseForm
    {
        /// <summary>
        /// ��ǰ��½�û�
        /// </summary>
        public Model.User user;

        public Model.Config config;

        private WebService ser;

        public MainForm()
        {
            InitializeComponent();
        }

        //ѡ�е�һ���˵���
        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.Enabled = false;

           // #region ͬ������
            /*
            Thread dataSyncThread = new Thread(new ThreadStart(DataSyncStart));
            dataSyncThread.Priority = ThreadPriority.Lowest;
            dataSyncThread.IsBackground = false;
             */
            //DataSyncStart();
            //#endregion

        }

        //���񰴼�
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '#':
                case '.':
                    foreach (ListViewItem lvi in this.listViewMenu.Items)
                    {
                        if (lvi.Tag.ToString() == e.KeyChar.ToString())
                        {
                            lvi.Focused = true;
                            this.listViewMenu.FocusedItem.Selected = true;
                            ExecutePro(lvi.Tag.ToString());
                        }
                    }
                    break;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //�����ܰ���F1-F4����Ӧ����ײ����ĸ���ť
                case Keys.F1:
                    this.button1_Click(null, null);
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                        System.Threading.Thread.Sleep(50);
                        WintecIDT700.Beep(100);
                    break;
                case Keys.Escape:
                case Keys.F4:
                    this.button4_Click(null, null);
                    break;
            }
        }

        //����listViewMenu˫���¼�
        private void listViewMenu_ItemActivate(object sender, EventArgs e)
        {
            if (this.listViewMenu.FocusedItem != null)
                ExecutePro(this.listViewMenu.FocusedItem.Tag.ToString());
        }

        //��ť����¼�
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listViewMenu.FocusedItem != null)
            {
                this.listViewMenu.FocusedItem.Selected = true;
                ExecutePro(this.listViewMenu.FocusedItem.Tag.ToString());
            }
            this.listViewMenu.Focus();
        }


        /// <summary>
        /// �û�ע��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                WebService.userLogoff(config.PosNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Owner.Show();
            //this.Dispose();
            this.Close();
        }

        //��������listViewMenu˫���¼�����ť����¼����򿪶�Ӧ�Ĺ��ܽ���
        private void ExecutePro(string opType)
        {
            if (this.listViewMenu.Enabled == false)
                return;
            switch (opType)
            {
                case "1":
                    //����
                    Business.SaleForm saleForm = new Business.SaleForm(config);
                    saleForm.config = config;
                    saleForm.user = user;
                    saleForm.ShowDialog();
                    saleForm.Dispose();
                    break;
                case "2":
                    //����ͳ��
                    Business.SaleQueryForm saleQueryForm = new SumPos.Business.SaleQueryForm();
                    saleQueryForm.config = config;
                    saleQueryForm.ShowDialog();
                    saleQueryForm.Dispose();
                    break;
                case "3":
                    //��ֵͳ��
                    Business.ChongZhiQueryForm chzhQueryForm = new SumPos.Business.ChongZhiQueryForm();
                    chzhQueryForm.config = config;
                    chzhQueryForm.ShowDialog();
                    chzhQueryForm.Dispose();
                    break;

                case "4":
                    //ϵͳ����
                    Setting.MenuForm settingMenuForm = new SumPos.Setting.MenuForm();
                    settingMenuForm.config = config;
                    settingMenuForm.user = user;
                    settingMenuForm.ShowDialog();
                    settingMenuForm.Dispose();
                    break;

                case "#":
                    //����ͬ��
                    //DataSyncStart();
                    break;

            }
        }

        bool isDataSyncing = false;

        /// <summary>
        /// ��������ͬ��
        /// </summary>
        private void DataSyncStart()
        {
            //if (!isDataSyncing)
            //{

            //    isDataSyncing = true;
            //    bool ok=false;//�����Ƿ�ɹ���־
            //    ShowWaitMsg("����ͬ������,���Ժ�....");
            //    Cursor.Current = Cursors.WaitCursor;
            //    Cursor.Show();
            //    Application.DoEvents();
            //    try
            //    {
            //        //#region ��ȡ�û��б�
            //       // List<Model.User> userList = ser.syncUser();
            //       // #endregion

            //       // if (userList.Count > 0)
            //      //  {
            //      //      #region �����û��б�
            //            //ok = new Action.Sqlite.SqliteUserAction(config.SqliteConnStr).saveUser(userList);
            //            //if (!ok)
            //            //{
            //            //    MessageBox.Show("�û��б����ʧ��");
            //            //}
            //            //#endregion
            //     //   }
                   


            //        #region ��ȡδ�ϴ���������
            //        List<Model.TradeFlow> upLoadTradeFlow = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getTradeFlowNoUpload();
            //        List<Model.PayFlow> upLoadPayFlow = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getPayFlowNoUpload();
            //        #endregion

            //        if (upLoadTradeFlow.Count > 0 || upLoadPayFlow.Count > 0)
            //        {
            //            #region �ϴ���������
            //            ok = ser.syncFlow(upLoadTradeFlow,upLoadPayFlow);
            //            #endregion


            //            if (ok)
            //            {
            //                #region ���±����ϴ���־
            //                ok = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).updateFlowFlag();
            //                #endregion
            //            }
            //        }

            //        if (ok)
            //        {
            //            //MessageBox.Show("����ͬ���ɹ���");
            //        }
            //        else
            //        {
            //            MessageBox.Show("����ͬ��ʧ�ܣ�");
            //        }
            //        this.Enabled = true;
            //    }
            //    catch(Exception ex)
            //    {
            //        MessageBox.Show("����ͬ��ʧ�ܣ�");
            //    }
            //    finally
            //    {

            //        isDataSyncing = false;
            //        this.Show();
            //        Cursor.Current = Cursors.Default;
            //        Cursor.Hide();
            //        HideWaitMsg();
            //        this.listViewMenu.Focus();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("���ڽ�������ͬ�������Ժ��ٽ��б�����...");
            //}
        }

    }
}

