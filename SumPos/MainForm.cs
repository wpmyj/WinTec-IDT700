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

        public MainForm()
        {
            InitializeComponent();
        }

        //ѡ�е�һ���˵���
        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.Enabled = false;
            #region ͬ��
            /*
            Thread dataSyncThread = new Thread(new ThreadStart(DataSyncStart));
            dataSyncThread.Priority = ThreadPriority.Lowest;
            dataSyncThread.IsBackground = false;
             */
            if (MessageBox.Show("�Ƿ�ͬ�����ݣ���", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {

                DataSyncStart();
            }
            #endregion

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

        private void button4_Click(object sender, EventArgs e)
        {
            if(new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).checkFlowUpload())
            {
                if (MessageBox.Show("��δ�ϴ��������ݣ��Ƿ������ϴ�����",null,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    DataSyncStart();
                }
                
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
                    ////ϵͳ����
                    //Setting.MenuForm settingMenuForm = new SumPos.Setting.MenuForm();
                    //settingMenuForm.config = config;
                    //settingMenuForm.user = user;
                    //settingMenuForm.ShowDialog();
                    //settingMenuForm.Dispose();
                    //break;
                case "#":
                    //����ͬ��
                    DataSyncStart();
                    break;

            }
        }

        bool isDataSyncing = false;

        /// <summary>
        /// ��������ͬ��
        /// </summary>
        private void DataSyncStart()
        {

                Application.DoEvents();

                bool ok = false;//�����Ƿ�ɹ���־
                Cursor.Current = Cursors.WaitCursor;
                Cursor.Show();

                try
                {
                    #region ��֤�ն���Ч��
                    ShowWaitMsg("������֤�ն���Ч��....");
                    ok=WebService.chkInfo(config.PosNo,config.PosId);
                    if (!ok)
                    {
                        MessageBox.Show("ͬ��ʧ�ܣ����ն�δע�ᣡ����ϵ����Ա��");

                        isDataSyncing = false;
                        this.Show();
                        Cursor.Current = Cursors.Default;
                        Cursor.Hide();
                        HideWaitMsg();
                        this.listViewMenu.Focus();

                        return;
                    }
                    #endregion

                    #region ����ϵͳ����
                    ShowWaitMsg("����ͬ��ϵͳ����....");
                    Model.Config cfgtmp = WebService.syncCfg(config.PosNo);
                    config.Grpno = cfgtmp.Grpno;
                    config.CompanyName = cfgtmp.CompanyName;
                    config.DeptName = cfgtmp.DeptName;
                    new Action.Sqlite.SqliteConfigAction(config.SqliteConnStr).saveConfig(config);
                    #endregion



                    #region ��ȡ�û��б�
                    ShowWaitMsg("����ͬ���û��б�....");
                    List<Model.User> userList = WebService.syncUser(config.Grpno);
                    #endregion

                    if (userList.Count > 0)
                    {
                        #region �����û��б�
                        ok = new Action.Sqlite.SqliteUserAction(config.SqliteConnStr).saveUser(userList);
                        if (!ok)
                        {
                            MessageBox.Show("�û��б����ʧ�ܣ�");
                        }
                        #endregion
                    }
                    #region ��ȡ��Ʒ�б�
                    ShowWaitMsg("����ͬ����Ʒ�б�...");
                    List<Model.Spxinxi> spxinxiList = WebService.syncSpxinxi(config.Grpno);
                    #endregion

                    if (spxinxiList.Count > 0)
                    {
                        #region ������Ʒ�б�
                        ok = new Action.Sqlite.SqliteSpxinxiAction(config.SqliteConnStr).saveSpxinxi(spxinxiList);
                        if (!ok)
                        {
                            MessageBox.Show("��Ʒ��Ϣͬ��ʧ�ܣ�");
                        }
                        #endregion
                    }


                    ShowWaitMsg("�����ϴ���������....");
                    #region ��ȡδ�ϴ���������
                    List<Model.SaleFlow> upLoadSaleFlow = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getSaleFlowNoUpLoad();
                    List<Model.PayFlow> upLoadPayFlow = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).getPayFlowNoUpload();
                    #endregion

                    if (upLoadSaleFlow.Count > 0 || upLoadPayFlow.Count > 0)
                    {
                        #region �ϴ���������
                        ok = WebService.syncFlow(upLoadSaleFlow, upLoadPayFlow);
                        #endregion


                        if (ok)
                        {

                            #region ���±����ϴ���־
                            ok = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).updateFlowFlag();
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("���������ϴ�ʧ�ܣ�");
                        }
                    }

                    #region ɾ����������
                    ShowWaitMsg("����ɾ����������...");
                    ok = new Action.Sqlite.SqliteFlowAction(config.SqliteConnStr).clearFlowFrom(DateTime.Now.AddDays(0 - config.DataSaveDays).ToString("yyyy-MM-dd"));
                    #endregion

                    if (!ok)
                    {
                        MessageBox.Show("ɾ����������ʧ�ܣ�");
                    }
                    this.Enabled = true;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("����ͬ��ʧ�ܣ�");
                }
                finally
                {

                    isDataSyncing = false;
                    this.Show();
                    Cursor.Current = Cursors.Default;
                    Cursor.Hide();
                    HideWaitMsg();
                    this.listViewMenu.Focus();
                }
            //this.listViewMenu.Items[0].Focused = true;
            //this.listViewMenu.Items[0].Selected = true;
        }
    }
}

