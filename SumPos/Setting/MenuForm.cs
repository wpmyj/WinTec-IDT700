using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SumPos.Setting
{
    public partial class MenuForm : BaseClass.BaseForm
    {
        public Model.Config config;
        public Model.User user;

        public MenuForm()
        {
            InitializeComponent();
        }

        //ѡ�е�һ���˵���
        private void MenuForm_Load(object sender, EventArgs e)
        {
            this.listViewMenu.Items[0].Focused = true;
            this.listViewMenu.Items[0].Selected = true;
        }


        /// <summary>
        /// ���񰴼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuForm_KeyDown(object sender, KeyEventArgs e)
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
                    break;
                case Keys.Escape:
                case Keys.F4:
                    this.button4_Click(null, null);
                    break;
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                    foreach (ListViewItem lvi in this.listViewMenu.Items)
                    {
                        if (lvi.Tag.ToString() == ((char)e.KeyValue).ToString())
                        {
                            lvi.Focused = true;
                            this.listViewMenu.FocusedItem.Selected = true;
                            ExecutePro(lvi.Tag.ToString());
                        }
                    }
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
            this.Close();
        }

        //��������listViewMenu˫���¼�����ť����¼����򿪶�Ӧ�Ĺ��ܽ���
        private void ExecutePro(string opType)
        {
            switch (opType)
            {
                /*
                case "1":

                ProductInfo productInfo = new ProductInfo();
                productInfo.ShowDialog();
                productInfo.Dispose();
                break;

                ShopInfoForm shopinfo = new ShopInfoForm();
                shopinfo.ShowDialog();
                shopinfo.Dispose();
                break;
                */
                case "1":
                    SystemParamSetForm systemParamSetForm = new SystemParamSetForm();
                    systemParamSetForm.config = config;
                    systemParamSetForm.ShowDialog();

                    systemParamSetForm.Dispose();
                    break;
                //case "3":

                case "0":
                    ExitForm exitForm = new ExitForm();
                    DialogResult ret = exitForm.ShowDialog();
                    if (ret == DialogResult.OK && exitForm.pwd == user.Mpassword)
                    {
                        ResetForm resetForm = new ResetForm();
                        resetForm.config = config;
                        resetForm.ShowDialog();
                        resetForm.Dispose();
                    }
                    break;
            }
        }

        private void MenuForm_Closed(object sender, EventArgs e)
        {
            this.button1.Click -= new System.EventHandler(this.button1_Click);
            this.button4.Click -= new System.EventHandler(this.button4_Click);
            this.listViewMenu.ItemActivate -= new System.EventHandler(this.listViewMenu_ItemActivate);
            this.Load -= new System.EventHandler(this.MenuForm_Load);
            this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.MenuForm_KeyDown);
        }

    }
}

