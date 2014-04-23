using System;

using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Collections;

namespace Action.Sqlite
{
    /// <summary>
    /// 系统参数数据库操作类
    /// </summary>
    public class SqliteConfigAction:SqliteBaseAction
    {
        public SqliteConfigAction(string connStr)
            :base(connStr)
        {

        }


        public Model.Config getConfig()
        {
            Model.Config config = new Model.Config();

            #region 组装sql 语句
            string sqlStr = "select * from Config";
            #endregion
            try
            {
                SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
                #region 组装config model
                while (reader.Read())
                {
                    config.ServerAdd = reader["ServerAdd"].ToString();
                    config.PosNo = reader["PosNo"].ToString();
                    config.CzkType = (Model.CzCardType)int.Parse(reader["CzCardType"].ToString());
                    config.CompanyName = reader["CompanyName"].ToString();
                    config.CustomerName = reader["CustomerName"].ToString();
                    config.printBill = reader["printBill"].ToString();
                    config.readCardMode = reader["ReadCardMode"].ToString();
                    config.Str1 = reader["str1"].ToString();
                    config.Str2 = reader["str2"].ToString();
                    config.Str3 = reader["str3"].ToString();
                }
                #endregion
            }
            catch
            {
                throw;
            }
            return config;
        }






        /// <summary>
        /// 系统参数初始化
        /// </summary>
        /// <returns></returns>
        public bool sysReSet()
        {
            ArrayList sqlList = new ArrayList();
            bool ok = false;
            #region 组装sql
            sqlList.Add("update Config set Pwd='',Uid='sa',ServerAdd='http://192.168.0.1/',CompanyName='美食广场',CustomerName='档口1',PosNo='001',flow_no=null,lastdate=null,ReadCardMode='0'");
            sqlList.Add("delete from pos_PayFlow");
            sqlList.Add("delete from pos_TradeFlow");
            //sqlList.Add("delete from txsSaleFlow");
            #endregion

            try
            {
                #region 执行sql
                ok = SqliteEngine.ExecuteNonQueryWithTran(sqlList);
                #endregion
            }
            catch
            {
                ok = false;
                throw;
            }
            return ok;
        }




        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool saveConfig(Model.Config config)
        {
            bool ok = false;

            #region 组装sql语句
            string sqlStr = "update Config set "
                + "ServerAdd='"+config.ServerAdd+"',"
                + "CompanyName='"+config.CompanyName+"',"
                + "CustomerName='"+config.CustomerName+"',"
                + "PosNo='"+config.PosNo+"',"
                +"printBill='"+config.printBill+"',"
                + "CzCardType='"+((int)config.CzkType).ToString()+"',"
                + "ReadCardMode='"+config.readCardMode+"',"
                +"str1='"+config.Str1+"',"
                +"str2='"+config.Str2+"',"
                +"str3='"+config.Str3+"'";

            #endregion

            try
            {
                #region 执行sql 语句
                int ret = SqliteEngine.ExecuteNonQuery(sqlStr);
                #endregion

                if (ret == 1)
                {
                    ok = true;
                }
            }
            catch
            {
                ok = false;
                throw;
            }
            return ok;
        }
    }
}
