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
using PubGlobal;
using Trans;


namespace POS
{
    public partial class FrmMain : BaseClass.FrmBase
    {
        FrmLogon frmLogon = new FrmLogon();
        FrmSale frmSale = new FrmSale();
        FrmQuery frmQuery = new FrmQuery();
        public FrmMain()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            SetFullScreen(true, ref  rectangle);

            ShowWaitMsg("���ڶ�ȡϵͳ����");
            #region ��ȡϵͳ����
            try
            {
                using (StreamReader rd = new StreamReader(PubGlobal.Envionment.VersionFilePath))
                {
                    PubGlobal.Envionment.APPVersion = rd.ReadLine();
                    rd.Close();
                }
            }
            catch (Exception ex)
            {
                PubGlobal.Envionment.APPVersion = "1.0.0.0";//�Ҳ����汾��Ϣ�ļ���Ĭ��Ϊ1.0.0.0��
            }
            try
            {
                using (StreamReader rd = new StreamReader(PubGlobal.Envionment.ConfigFilePath))
                {
                    while (!rd.EndOfStream)
                    {
                        string info = rd.ReadLine();
                        if (string.IsNullOrEmpty(info))
                        {
                            continue;
                        }
                        switch (info.Split('=')[0])
                        {
                            case "PosNO":
                                PubGlobal.SysConfig.PosNO = info.Split('=')[1];
                                break;
                            case "Server":
                                PubGlobal.SysConfig.Server = info.Split('=')[1];
                                break;
                            case "Port":
                                PubGlobal.SysConfig.Port = info.Split('=')[1];
                                break;
                            case "DeptCode":
                                PubGlobal.SysConfig.DeptCode = info.Split('=')[1];
                                break;
                            case "StypeCode":
                                PubGlobal.SysConfig.Stype = info.Split('=')[1];
                                break;
                            case "ShowGoodsMode":
                                PubGlobal.SysConfig.GetGoodsByDept = (info.Split('=')[1] == "1");
                                break;
                            case "PrintCount":
                                try
                                {
                                    PubGlobal.SysConfig.PrintCount = int.Parse(info.Split('=')[1]);
                                }
                                catch
                                {
                                    PubGlobal.SysConfig.PrintCount = 0;
                                }
                                break;
                        }
                    }
                    rd.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ϵͳ����" + ex.Message);
            }
            #endregion
            HideWaitMsg();

            #region ��¼
            if (frmLogon.ShowDialog() == DialogResult.Cancel)
            {
                this.Close();
            }
            #endregion

            InitializeComponent();
            Update();//ϵͳ����
        }

        //���񰴼�
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!this.Enabled)
            {
                e.Handled = true;
                return;
            }
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
                    foreach (ListViewItem lvi in this.listViewMenu.Items)
                    {
                        if (lvi.Tag.ToString() == e.KeyChar.ToString())
                        {
                            lvi.Focused = true;
                            this.listViewMenu.FocusedItem.Selected = true;
                            ExecutePro(lvi.Tag.ToString());
                        }
                    }
                    e.Handled = true;
                    break;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.Enabled)
            {
                e.Handled = true;
                return;
            }
            switch (e.KeyCode)
            {
                //�����ܰ���F1-F4����Ӧ����ײ����ĸ���ť
                case Keys.F1:
                    this.button1_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.Escape:
                case Keys.F5:
                    this.button5_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.F11:
                case Keys.Return:
                    button1_Click(null, null);
                    e.Handled = true;
                    return;
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ȷʵҪע����", "ע����ǰ�û�", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                == DialogResult.Yes)
            {
                if (frmLogon.ShowDialog() == DialogResult.Cancel)
                {
                    this.Close();//�˳�ϵͳ
                }
                else
                {
                    Update();
                    this.listViewMenu.Focus();
                }

            }
        }

        //��������listViewMenu˫���¼�����ť����¼����򿪶�Ӧ�Ĺ��ܽ���
        private void ExecutePro(string opType)
        {
            if (this.listViewMenu.Enabled == false)
                return;
            switch (opType)
            {
                case "1"://��Ʒ����
                    frmSale.ShowDialog();
                    break;

                case "2"://�����ѯ
                    frmQuery.ShowDialog();
                    break;

                case "3"://����
                    Update();
                    break;
            }
        }

        private new  void Update()
        {
            string msg;

            ShowWaitMsg("���ڼ��ϵͳ����");
            #region ϵͳ����
            OnlineDataSync();
            #endregion

            ShowWaitMsg("���ڸ���ϵͳ����");
            #region ��ȡԶ�̲���
            if (!TransModule.GetConfig(out msg))
            {
                MessageBox.Show("����ϵͳ��������" + msg + "\r\n����ϵ����Ա", "����", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            #endregion

            ShowWaitMsg("���ڸ��²�Ʒ��Ϣ");
            #region ��ȡ��Ʒ
            if (!TransModule.GetGoods(out msg))
            {
                MessageBox.Show("���ز�Ʒ��Ϣ����");
            }
            else
            {
                frmSale.RefreshGoodsMenu();
            }
            #endregion

            HideWaitMsg();
        }

        private void FrmMain_Closing(object sender, CancelEventArgs e)
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            SetFullScreen(false, ref  rectangle);
        }

        /// <summary>
        /// ϵͳ����
        /// </summary>
        public void OnlineDataSync()
        {
            int isUpdate = 0;
            string msg;
            try
            {
                isUpdate = TransModule.CheckUpdate(out msg);
                if (isUpdate==1)
                {
                    //������������
                    string path = PubGlobal.Envionment.UpdateFolderPath + PubGlobal.Envionment.UPDATE_EXE_FILE_NAME;
                    if (File.Exists(path))
                    {
                        File.Delete(PubGlobal.Envionment.UpdateAppPath);
                        File.Move(path,PubGlobal.Envionment.UpdateAppPath);
                        File.Delete(path);
                    }
                    //�رձ�����,�������³���
                    //������ɺ󣬹رո��³�������������
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = PubGlobal.Envionment.UpdateAppPath;

                    p.Start();
                    //�رոó���
                    HideWaitMsg();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            HideWaitMsg();
        }
    }
}

