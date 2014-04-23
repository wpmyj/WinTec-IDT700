namespace Model
{
    public class MCzkType
    {
        /// <summary>
        /// 类型编码
        /// </summary>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 开卡金额
        /// </summary>
        public decimal KbMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 权限码
        /// </summary>
        public string IsSign
        {
            get;
            set;
        }

    }
}
