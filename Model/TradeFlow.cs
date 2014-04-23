using System;

using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 交易总表
    /// </summary>
    public class TradeFlow
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


        private string posno;
        /// <summary>
        /// 款台号
        /// </summary>
        public string Posno
        {
            get { return posno; }
            set { posno = value; }
        }


        private string sdate;
        /// <summary>
        /// 交易日期
        /// </summary>
        public string Sdate
        {
            get { return sdate; }
            set { sdate = value; }
        }



        private string stime;
        /// <summary>
        /// 交易时间
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



        public string tradeType;
        /// <summary>
        /// 交易模式
        /// </summary>
        public FlowTradeType TradeType
        {
            get
            {
                if (tradeType == "A")
                {
                    return FlowTradeType.销售;
                }
                else
                {
                    return FlowTradeType.退货;
                }
            }
            set
            {
                if (value == FlowTradeType.销售)
                {
                    tradeType = "A";
                }
                else
                {
                    tradeType = "B";
                }
            }
        }



        private int qty;
        /// <summary>
        /// 数量
        /// </summary>
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
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


        private float zkje;
        /// <summary>
        /// 折扣金额
        /// </summary>
        public float Zkje
        {
            get { return zkje; }
            set { zkje = value; }
        }



        private float payje;
        /// <summary>
        /// 付款金额
        /// </summary>
        public float Payje
        {
            get { return payje; }
            set { payje = value; }
        }



        private float change;
        /// <summary>
        /// 找零
        /// </summary>
        public float Change
        {
            get { return change; }
            set { change = value; }
        }



        private string custCode;
        /// <summary>
        /// 客户编码
        /// </summary>
        public string CustCode
        {
            get { return custCode; }
            set { custCode = value; }
        }



        private string custFlag;
        /// <summary>
        /// 客户标志
        /// </summary>
        public string CustFlag
        {
            get { return custFlag; }
            set { custFlag = value; }
        }



        private string rightNo;
        /// <summary>
        /// 权限编号
        /// </summary>
        public string RightNo
        {
            get { return rightNo; }
            set { rightNo = value; }
        }


        private float bcjf;
        /// <summary>
        /// 本次积分
        /// </summary>
        public float Bcjf
        {
            get { return bcjf; }
            set { bcjf = value; }
        }



        private float scjf;
        /// <summary>
        /// 上次积分
        /// </summary>
        public float Scjf
        {
            get { return scjf; }
            set { scjf = value; }
        }
       



        public string flag;
        /// <summary>
        /// 是否上传
        /// </summary>
        public FlowUpLoadFlag Flag
        {
            get
            {
                if (flag == "0")
                {
                    return FlowUpLoadFlag.未上传;
                }
                else
                {
                    return FlowUpLoadFlag.已上传;
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
