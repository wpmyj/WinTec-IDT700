using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlUtilities.Attributes;
using System.Data;

namespace TransService.Model
{
    [Table(TableName="pos_mulpay")]
    public class MPayFlow
    {
        /// <summary>
        /// 操作员
        /// </summary>
        [Column(ColumnName = "Operater", DbType = DbType.String)]
        public string Operater
        {
            get;
            set;
        }

        /// <summary>
        /// 流水号
        /// </summary>
        [Column(ColumnName = "serial_no", DbType = DbType.String, IsNull = false)]
        public string SerialNo
        {
            get;
            set;
        }

        /// <summary>
        /// 总金额
        /// </summary>
        [Column(ColumnName = "total", DbType = DbType.Decimal, Default = 0)]
        public decimal Total
        {
            get;
            set;
        }

        /// <summary>
        /// 付款方式
        /// </summary>
        [Column(ColumnName = "paypmt", DbType = DbType.String)]
        public string Paypmt
        {
            get;
            set;
        }

        /// <summary>
        /// 付款日期
        /// </summary>
        [Column(ColumnName = "sa_date", DbType = DbType.DateTime)]
        public DateTime Sa_date
        {
            get;
            set;
        }

        /// <summary>
        /// 付款时间
        /// </summary>
        [Column(ColumnName = "sa_time", DbType = DbType.String)]
        public string Sa_time
        {
            get;
            set;
        }

        /// <summary>
        /// 卡号
        /// </summary>
        [Column(ColumnName = "ref_no", DbType = DbType.String)]
        public string Ref_no
        {
            get;
            set;
        }

        /// <summary>
        /// 班次号
        /// </summary>
        [Column(ColumnName = "squadno", DbType = DbType.String, Default = "1")]
        public string Squadno
        {
            get;
            set;
        }

        /// <summary>
        /// 收款机号
        /// </summary>
        [Column(ColumnName = "PosNo", DbType = DbType.String)]
        public string PosNO
        {
            get;
            set;
        }

        /// <summary>
        /// 行号
        /// </summary>
        [Column(ColumnName = "RowNo", DbType = DbType.Int16, Default = 0)]
        public int RowNO
        {
            get;
            set;
        }

    }
}
