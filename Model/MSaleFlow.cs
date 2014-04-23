using System;
namespace Model
{
    public class MSaleFlow
    {
        /// <summary>
        /// 部门编码
        /// </summary>
        public string DeptCode
        {
            get;
            set;
        }

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
        /// 收款机号
        /// </summary>
        public string PosNo
        {
            get;
            set;
        }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string Incode
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
        /// 单价
        /// </summary>
        public decimal Price
        {
            get;
            set;
        }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty
        {
            get;
            set;
        }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal PreTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Disc
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
        /// 实付
        /// </summary>
        public decimal RealTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 销售日期
        /// </summary>
        public DateTime Sa_date
        {
            get;
            set;
        }

        /// <summary>
        /// 销售时间
        /// </summary>
        public string Sa_time
        {
            get;
            set;
        }

        /// <summary>
        /// 行号
        /// </summary>
        public int RowNo
        {
            get;
            set;
        }

        /// <summary>
        /// 班次号
        /// </summary>
        public string SquadNO
        {
            get;
            set;
        }
    }
}
