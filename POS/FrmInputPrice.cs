using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace POS
{
    public partial class FrmInputPrice : Form
    {
        public FrmInputPrice()
        {
            InitializeComponent();
            this.Width = 300;
            this.Height = 100;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }

        /// <summary>
        /// 操作方式
        /// </summary>
        public string paypmt
        {
            set
            {
                label1.Text = value;
            }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string danwei
        {
            set
            {
                label2.Text = value;
            }
        }
        /// <summary>
        /// 初始金额
        /// </summary>
        public string initNum
        {
            set
            {
                txtPrice.Text = value;
            }
        }

        public decimal inputNum;//输入数据

        private void txtPrice_GotFocus(object sender, EventArgs e)
        {
            txtPrice.SelectAll();
        }

        /// <summary>
        /// 按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputPriceBx_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            switch (e.KeyData)
            {
                case Keys.Enter:
                    DialogResult = DialogResult.OK;
                    this.Close();
                    break;
                case Keys.Escape:
                    //取消
                    DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;
            }
        }

        private void InputPriceBx_Load(object sender, EventArgs e)
        {
            txtPrice.Focus();
            txtPrice.SelectAll();
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputPriceBx_Closing(object sender, CancelEventArgs e)
        {                   
            //确定
            try
            {
                inputNum = decimal.Parse(txtPrice.Text);
                if (label1.Text == "折扣" && txtPrice.Text.Contains("."))
                {
                    //折扣中含有小数
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("请输入正确"+label1.Text+"！");
                e.Cancel = true;
            }
        }

    }
}