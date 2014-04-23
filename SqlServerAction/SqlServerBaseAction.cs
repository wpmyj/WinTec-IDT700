using System;

using System.Collections.Generic;
using System.Text;

namespace Action.SqlServer
{
    public class SqlServerBaseAction
    {
        private string conStr;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConStr
        {
            get { return conStr; }
            set { conStr = value; }
        }




        private SqlHelper sqlEngine;
        /// <summary>
        /// 数据库操作工具
        /// </summary>
        public SqlHelper SqlEngine
        {
            get { return sqlEngine; }
        }



        public SqlServerBaseAction(string connStr)
        {
            ConStr = connStr;
            sqlEngine = new SqlHelper(ConStr);
        }

    }
}
