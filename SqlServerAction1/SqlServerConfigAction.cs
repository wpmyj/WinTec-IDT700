using System;

using System.Collections.Generic;
using System.Text;

namespace Action.SqlServer
{
    /// <summary>
    /// Config 相关数据库操作
    /// </summary>
    public class SqlServerConfigAction:SqlServerBaseAction
    {
        public SqlServerConfigAction(string connStr)
            : base(connStr)
        {
        }
    }
}
