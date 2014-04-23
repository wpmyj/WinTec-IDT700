using System;

using System.Collections.Generic;
using System.Text;

namespace Action.Sqlite
{
    /// <summary>
    /// sqlite类
    /// </summary>
    public class SqliteBaseAction
    {
        private string connStr;
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnStr
        {
            get { return connStr; }
            set { connStr = value; }
        }


        private SQLiteHelper sqliteEngine;
        /// <summary>
        /// sqlite数据库操作工具
        /// </summary>
        public SQLiteHelper SqliteEngine
        {
            get { return sqliteEngine; }
            set { sqliteEngine = value; }
        }


        public SqliteBaseAction(string connStr)
        {
            this.ConnStr = connStr;
            SqliteEngine = new SQLiteHelper(ConnStr);
        }
    }
}
