using System;

using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 付款流水
    /// </summary>
    public class PayFlow
    {
        private string operater;
        /// <summary>
        /// 收款员
        /// </summary>
        public string Operater
        {
            get { return operater; }
            set { operater = value; }
        }



        private string serial_no;
        /// <summary>
        /// 流水号
        /// </summary>
        public string Serial_no
        {
            get { return serial_no; }
            set { serial_no = value; }
        }



        private float total;
        /// <summary>
        /// 付款金额
        /// </summary>
        public float Total
        {
            get { return total; }
            set { total = value; }
        }



        public string paypmt;
        /// <summary>
        /// 付款方式
        /// </summary>
        public PayType Paypmt
        {
            get { return (PayType)int.Parse(paypmt); }
            set { paypmt = ((int)value).ToString();
            if (paypmt.Length == 1)
            {
                paypmt = "0" + paypmt;
            }
            }
        }




        private string sa_date;
        /// <summary>
        /// 消费日期
        /// </summary>
        public string Sa_date
        {
            get { return sa_date; }
            set { sa_date = value; }
        }



        private string sa_time;
        /// <summary>
        /// 消费时间
        /// </summary>
        public string Sa_time
        {
            get { return sa_time; }
            set { sa_time = value; }
        }



        private string ref_no;
        /// <summary>
        /// 储值卡号
        /// </summary>
        public string Ref_no
        {
            get { return ref_no; }
            set { ref_no = value; }
        }



        private string squadno;
        /// <summary>
        /// 班次号
        /// </summary>
        public string Squadno
        {
            get { return squadno; }
            set { squadno = value; }
        }



        private string posNo;
        /// <summary>
        /// 款台号
        /// </summary>
        public string PosNo
        {
            get { return posNo; }
            set { posNo = value; }
        }



        private int rowNo;
        /// <summary>
        /// 流水编号
        /// </summary>
        public int RowNo
        {
            get { return rowNo; }
            set { rowNo = value; }
        }



        public  string ckic;
        /// <summary>
        /// 是否Ic卡
        /// </summary>
        public CzCardType Ckic
        {
            get { return (CzCardType)int.Parse(ckic); }
            set { ckic = ((int)value).ToString(); }
        }




        public string flag;
        /// <summary>
        /// 是否上传
        /// </summary>
        public FlowUpLoadFlag Flag
        {
            get { return (FlowUpLoadFlag)int.Parse(flag); }
            set { flag = ((int)value).ToString(); }
        }


        private float scye;
        /// <summary>
        /// 上次余额
        /// </summary>
        public float Scye
        {
            get { return scye; }
            set { scye = value; }
        }
    }
}
