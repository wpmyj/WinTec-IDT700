namespace Model
{
    public class MGoods
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public string Incode
        {
            get;
            set;
        }

        /// <summary>
        /// 条码
        /// </summary>
        public string Barcode
        {
            get;
            set;
        }

        /// <summary>
        /// 品名
        /// </summary>
        public string Fname
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣
        /// </summary>
        public int Disc
        {
            get;
            set;
        }

        /// <summary>
        /// 规格
        /// </summary>
        public string Spec
        {
            get;
            set;
        }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price
        {
            get;
            set;
        }

        /// <summary>
        /// 会员标志
        /// </summary>
        public string HyFlag
        {
            get;
            set;
        }

        /// <summary>
        /// 会员价
        /// </summary>
        public decimal HyPrice;

        /// <summary>
        /// 供应商
        /// </summary>
        public string Customer
        {
            get;
            set;
        }

        /// <summary>
        /// 品类
        /// </summary>
        public string Stype
        {
            get;
            set;
        }


        /// <summary>
        /// 促销价
        /// </summary>
        public decimal CxPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            get;
            set;
        }

        /// <summary>
        /// 部门
        /// </summary>
        public string DeptNo
        {
            get;
            set;
        }
    }
}
