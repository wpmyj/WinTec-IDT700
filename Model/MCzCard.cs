namespace Model
{
    public class MCzCard
    {
        /// <summary>
        /// 内卡号
        /// </summary>
        public string InCardno
        {
            get;
            set;
        }

        /// <summary>
        /// 外卡号
        /// </summary>
        public string OutCardNo
        {
            get;
            set;
        }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Total
        {
            get;
            set;
        }

        /// <summary>
        /// 消费总金额
        /// </summary>
        public decimal XfTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string State
        {
            get;
            set;
        }

        /// <summary>
        /// 储值卡类型编码
        /// </summary>
        public string CardTypeCode
        {
            get;
            set;
        }

        /// <summary>
        /// 储值卡类型
        /// </summary>
        public MCzkType CzkType
        {
            get;
            set;
        }
    }
}
