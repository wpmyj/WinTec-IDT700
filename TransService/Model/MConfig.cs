using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlUtilities.Attributes;
using System.Data;

namespace TransService.Model
{
    [Table(TableName = "VIDT700Cfg")]
    public class MConfig
    {
        /// <summary>
        /// 收款机号
        /// </summary>
        [Column(ColumnName = "PosNo",DbType=DbType.String)]
        public string PosNo
        {
            get;
            set;
        }

        /// <summary>
        /// 参数名称
        /// </summary>
        [Column(ColumnName = "ItemName", DbType = DbType.String)]
        public string ItemName
        {
            get;
            set;
        }

        /// <summary>
        /// 参数值
        /// </summary>
        [Column(ColumnName = "ItemValue", DbType = DbType.String)]
        public string ItemValue
        {
            get;
            set;
        }
    }
}
