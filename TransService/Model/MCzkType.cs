using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlUtilities.Attributes;
using System.Data;

namespace TransService.Model
{
    [Table(TableName="tCzkType")]
    public class MCzkType
    {
        /// <summary>
        /// 类型编码
        /// </summary>
        [Column(ColumnName = "Code", DbType = DbType.String, PrimaryKey = true)]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Column(ColumnName = "Name", DbType = DbType.String)]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 开卡金额
        /// </summary>
        [Column(ColumnName = "KbMoney", DbType = DbType.Decimal, Default = 0)]
        public decimal KbMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 权限码
        /// </summary>
        [Column(ColumnName = "isSign", DbType = DbType.String)]
        public string IsSign
        {
            get;
            set;
        }

    }
}
