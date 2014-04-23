using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlUtilities.Attributes;
using System.Data;

namespace TransService.Model
{
    [Table(TableName = "tCzkCard")]
    public class MCzCard
    {
        /// <summary>
        /// 内卡号
        /// </summary>
        [Column(ColumnName = "InCardNo", DbType = DbType.String, PrimaryKey = true)]
        public string InCardno
        {
            get;
            set;
        }

        /// <summary>
        /// 外卡号
        /// </summary>
        [Column(ColumnName = "OutCardNo", DbType = DbType.String)]
        public string OutCardNo
        {
            get;
            set;
        }

        /// <summary>
        /// 余额
        /// </summary>
        [Column(ColumnName = "Total", DbType = DbType.Decimal, Default = 0)]
        public decimal Total
        {
            get;
            set;
        }

        /// <summary>
        /// 消费总金额
        /// </summary>
        [Column(ColumnName = "XfTotal", DbType = DbType.Decimal, Default = 0)]
        public decimal XfTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        [Column(ColumnName = "Stat", DbType = DbType.String)]
        public string State
        {
            get;
            set;
        }

        /// <summary>
        /// 储值卡类型编码
        /// </summary>
        [Column(ColumnName = "CzkType", DbType = DbType.String)]
        public string CardTypeCode
        {
            get;
            set;
        }

        /// <summary>
        /// 储值卡类型
        /// </summary>
        public MCzkType CzkType;
    }
}
