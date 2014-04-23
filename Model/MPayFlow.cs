using System;
namespace Model
{
    public class MPayFlow
    {
        /// <summary>
        /// 操作员
        /// </summary>
        public string Operater
        {
            get;
            set;
        }

        /// <summary>
        /// 流水号
        /// </summary>
        public string SerialNo
        {
            get;
            set;
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Total
        {
            get;
            set;
        }

        /// <summary>
        /// 付款方式
        /// </summary>
        public string Paypmt
        {
            get;
            set;
        }

        /// <summary>
        /// 付款日期
        /// </summary>
        public DateTime Sa_date
        {
            get;
            set;
        }

        /// <summary>
        /// 付款时间
        /// </summary>
        public string Sa_time
        {
            get;
            set;
        }

        /// <summary>
        /// 卡号
        /// </summary>
        public string Ref_no
        {
            get;
            set;
        }

        /// <summary>
        /// 班次号
        /// </summary>
        public string Squadno
        {
            get;
            set;
        }

        /// <summary>
        /// 收款机号
        /// </summary>
        public string PosNO
        {
            get;
            set;
        }

        /// <summary>
        /// 行号
        /// </summary>
        public int RowNO
        {
            get;
            set;
        }
    }
        
}
