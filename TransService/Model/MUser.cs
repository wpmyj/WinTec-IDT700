using System;
using System.Collections.Generic;
using System.Text;
using SqlUtilities.Attributes;
using System.Data;
namespace TransService.Model
{
    [Table(TableName="XtMuser")]
    public class MUser
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Column(ColumnName = "UserName", DbType = DbType.String)]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 用户编码
        /// </summary>
        [Column(ColumnName = "UserCode", DbType = DbType.String, PrimaryKey = true)]
        public string UserCode
        {
            get;
            set;
        }

        /// <summary>
        /// 密码
        /// </summary>
        [Column(ColumnName = "mpassword", DbType = DbType.String)]
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// 有效性
        /// </summary>
        [Column(ColumnName = "validflag", DbType = DbType.String)]
        public string ValidFlag
        {
            get;
            set;
        }

    }
}
