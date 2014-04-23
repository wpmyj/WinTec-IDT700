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
        private string flow_no;
        /// <summary>
        /// 流水号
        /// </summary>
        public string Flow_no
        {
            get { return flow_no; }
            set { flow_no = value; }
        }



        private int flow_id;
        /// <summary>
        /// 序号
        /// </summary>
        public int Flow_id
        {
            get { return flow_id; }
            set { flow_id = value; }
        }


        private string posno;
        /// <summary>
        /// 款台号
        /// </summary>
        public string Posno
        {
            get { return posno; }
            set { posno = value; }
        }



        private DateTime sdate;
        /// <summary>
        /// 收款日期
        /// </summary>
        public string Sdate
        {
            get { return sdate.ToString("yyyy-MM-dd"); }
            set { sdate = DateTime.Parse(value); }
        }



        private string stime;
        /// <summary>
        /// 收款时间
        /// </summary>
        public string Stime
        {
            get { return stime; }
            set { stime = value; }
        }



        private string operater;
        /// <summary>
        /// 收款员
        /// </summary>
        public string Operater
        {
            get { return operater; }
            set { operater = value; }
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



        public string tradetype;
        /// <summary>
        /// 交易模式
        /// A 销售 B 退货
        /// </summary>
        public FlowTradeType Tradetype
        {
            set
            {
                if (value == FlowTradeType.销售)
                {
                    tradetype = "A";
                }
                else
                {
                    tradetype = "B";
                }
            }
            get
            {
                if (tradetype == "A")
                {
                    return FlowTradeType.销售;
                }
                else
                {
                    return FlowTradeType.退货;
                }
            }
        }


        public  string paypmt;
        /// <summary>
        /// 付款方式
        /// </summary>
        public FlowPayType Paypmt
        {
            get { return (FlowPayType)(int.Parse(paypmt)); }
            set { paypmt = ((int)value).ToString(); }
        }



        private float payAmount;
        /// <summary>
        /// 外币金额
        /// </summary>
        public float PayAmount
        {
            get { return payAmount; }
            set { payAmount = value; }
        }



        private float total;
        /// <summary>
        /// 总金额
        /// </summary>
        public float Total
        {
            get { return total; }
            set { total = value; }
        }



        private string cardno;
        /// <summary>
        /// 储值卡帐号
        /// </summary>
        public string Cardno
        {
            get { return cardno; }
            set { cardno = value; }
        }

        private string hykh;
        /// <summary>
        /// 储值卡卡号
        /// </summary>
        public string Hykh
        {
            get { return hykh; }
            set { hykh = value; }
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


        public string ckic;
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
            get
            {
                if (flag == "1")
                {
                    return FlowUpLoadFlag.已上传;
                }
                else
                {
                    return FlowUpLoadFlag.未上传;
                }
            }
            set
            {
                if (value == FlowUpLoadFlag.已上传)
                {
                    flag = "1";
                }
                else
                {
                    flag = "0";
                }
            }
        }

    }
}
