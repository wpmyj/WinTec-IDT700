using System;
using System.Collections.Generic;
using System.Text;
using SqlUtilities.Attributes;
using System.Data;

namespace TransService.Model
{
    [Table(TableName = "Pos_Goods")]
    public class MGoods
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        [Column(ColumnName = "Incode", DbType = DbType.String, PrimaryKey = true)]
        public string Incode
        {
            get;
            set;
        }

        /// <summary>
        /// 条码
        /// </summary>
        [Column(ColumnName = "Barcode", DbType = DbType.String)]
        public string Barcode
        {
            get;
            set;
        }

        /// <summary>
        /// 品名
        /// </summary>
        [Column(ColumnName = "Fname", DbType = DbType.String)]
        public string Fname
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣
        /// </summary>
        [Column(ColumnName = "Disc", DbType = DbType.Int16, Default = 100)]
        public int Disc
        {
            get;
            set;
        }

        /// <summary>
        /// 规格
        /// </summary>
        [Column(ColumnName = "Specs", DbType = DbType.String)]
        public string Spec
        {
            get;
            set;
        }

        /// <summary>
        /// 价格
        /// </summary>
        [Column(ColumnName = "Price", DbType = DbType.Decimal, Default = 0)]
        public decimal Price
        {
            get;
            set;
        }

        /// <summary>
        /// 会员标志
        /// </summary>
        [Column(ColumnName = "Hyflag", DbType = DbType.String)]
        public string HyFlag
        {
            get;
            set;
        }

        /// <summary>
        /// 会员价
        /// </summary>
        [Column(ColumnName = "HyPrc", DbType = DbType.Decimal, Default = 0)]
        public decimal HyPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商
        /// </summary>
        [Column(ColumnName = "Custno", DbType = DbType.String)]
        public string Customer
        {
            get;
            set;
        }

        /// <summary>
        /// 品类
        /// </summary>
        [Column(ColumnName = "stype", DbType = DbType.String)]
        public string Stype
        {
            get;
            set;
        }

        /// <summary>
        /// 促销价
        /// </summary>
        [Column(ColumnName = "PromPrc", DbType = DbType.Decimal, Default = 0)]
        public decimal CxPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 促销状态
        /// </summary>
        [Column(ColumnName = "Status", DbType = DbType.String)]
        public string Status
        {
            get;
            set;
        }

        /// <summary>
        /// 部门
        /// </summary>
        [Column(ColumnName = "Grpno", DbType = DbType.String)]
        public string DeptNo
        {
            get;
            set;
        }
    }
}
