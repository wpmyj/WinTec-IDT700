using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlUtilities.Attributes;
using System.Data;

namespace TransService.Model
{
    [Table(TableName="pos_sales")]
    public class MSaleFlow
    {
        /// <summary>
        /// 部门编码
        /// </summary>
        [Column(ColumnName = "sgroup", DbType = DbType.String)]
        public string DeptCode
        {
            get;
            set;
        }

        /// <summary>
        /// 操作员
        /// </summary>
        [Column(ColumnName = "operater", DbType = DbType.String)]
        public string Operater
        {
            get;
            set;
        }

        /// <summary>
        /// 流水号
        /// </summary>
        [Column(ColumnName = "serial_no", DbType = DbType.String)]
        public string SerialNo
        {
            get;
            set;
        }

        /// <summary>
        /// 收款机号
        /// </summary>
        [Column(ColumnName = "PosNo", DbType = DbType.String)]
        public string PosNo
        {
            get;
            set;
        }

        /// <summary>
        /// 商品编码
        [Column(ColumnName = "code", DbType = DbType.String)]
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
        [Column(ColumnName = "Price", DbType = DbType.Decimal, Default = 0)]
        public decimal Price
        {
            get;
            set;
        }

        /// <summary>
        /// 数量
        /// </summary>
        [Column(ColumnName = "qty", DbType = DbType.Decimal, Default = 0)]
        public decimal Qty
        {
            get;
            set;
        }

        /// <summary>
        /// 原价
        /// </summary>
        [Column(ColumnName = "pre_total", DbType = DbType.Decimal, Default = 0)]
        public decimal PreTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣
        /// </summary>
        [Column(ColumnName = "disc", DbType = DbType.Decimal, Default = 0)]
        public decimal Disc
        {
            get;
            set;
        }

        /// <summary>
        /// 总金额
        /// </summary>
        [Column(ColumnName = "TOTAL", DbType = DbType.Decimal, Default = 0)]
        public decimal Total
        {
            get;
            set;
        }

        /// <summary>
        /// 实付
        /// </summary>
        [Column(ColumnName = "real_total", DbType = DbType.Decimal, Default = 0)]
        public decimal RealTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 销售日期
        /// </summary>
        [Column(ColumnName = "sa_date", DbType = DbType.DateTime)]
        public DateTime Sa_date
        {
            get;
            set;
        }

        /// <summary>
        /// 销售时间
        /// </summary>
        [Column(ColumnName = "sa_time", DbType = DbType.String)]
        public string Sa_time
        {
            get;
            set;
        }

        /// <summary>
        /// 行号
        /// </summary>
        [Column(ColumnName = "RowNo", DbType = DbType.Int16, Default = 0)]
        public int RowNo
        {
            get;
            set;
        }

        /// <summary>
        /// 班次号
        /// </summary>
        [Column(ColumnName = "Squadno", DbType = DbType.String, Default = "1")]
        public string SquadNO
        {
            get;
            set;
        }

    }
}
