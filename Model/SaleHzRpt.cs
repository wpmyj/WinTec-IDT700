using System;

using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 销售汇总model
    /// </summary>
    public class SaleHzRpt
    {
        private string inCode;
        /// <summary>
        /// 商品编码
        /// </summary>
        public string InCode
        {
            get { return inCode; }
            set { inCode = value; }
        }



        private string fName;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string FName
        {
            get { return fName; }
            set { fName = value; }
        }



        private float price;
        /// <summary>
        /// 商品价格
        /// </summary>
        public float Price
        {
            get { return price; }
            set { price = value; }
        }



        private int qty;
        /// <summary>
        /// 商品数量
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
    }
}
