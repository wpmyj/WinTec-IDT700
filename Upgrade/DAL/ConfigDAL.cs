using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Upgrade.DAL
{
    class ConfigDAL
    {
        public static Upgrade.Model.ConfigModel GetConfigInfo()
        {
            Upgrade.Model.ConfigModel configmodel = new Upgrade.Model.ConfigModel();

            string sqlStr = "Select customerid,customername,linkman,phone,companyname,posno,softwareversion,serverip,historydatakeeptime,pwd,epw,remark1,remark2,remark3 From config";
            SQLiteDataReader dr = null;
            try
            {
                dr = SQLiteHelper.ExecuteReader(Config.ConnectionString, sqlStr);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                //恢复数据库文件
                System.IO.File.Copy(Config.CurrentPath + @"\data.bak",
                    Config.CurrentPath + @"\data.xml", true);
                dr = SQLiteHelper.ExecuteReader(Config.ConnectionString, sqlStr);
            }
            while (dr.Read())
            {
                configmodel.CustomerId = dr["CustomerID"] == null ? string.Empty : dr["CustomerId"].ToString();
                configmodel.CustomerName = dr["CustomerName"] == null ? string.Empty : dr["CustomerName"].ToString();
                configmodel.LinkMan = dr["LinkMan"] == null ? string.Empty : dr["LinkMan"].ToString();
                configmodel.Phone = dr["Phone"] == null ? string.Empty : dr["Phone"].ToString();
                configmodel.CompanyName = dr["CompanyName"] == null ? string.Empty : dr["CompanyName"].ToString();
                configmodel.PosNo = dr["PosNo"] == null ? string.Empty : dr["PosNo"].ToString();
                configmodel.SoftWareVersion = dr["SoftWareVersion"] == null ? string.Empty : dr["SoftWareVersion"].ToString();
                configmodel.ServerIP = dr["ServerIP"] == null ? string.Empty : dr["ServerIP"].ToString();
                configmodel.HistoryDataKeepTime = dr["HistoryDataKeepTime"] == null ? string.Empty : dr["HistoryDataKeepTime"].ToString();
                configmodel.Pwd = dr["Pwd"] == null ? string.Empty : dr["Pwd"].ToString();
                configmodel.EPW = dr["EPW"] == null ? string.Empty : dr["EPW"].ToString();
                configmodel.Remark1 = dr["Remark1"] == null ? string.Empty : dr["Remark1"].ToString();
                configmodel.Remark2 = dr["Remark2"] == null ? string.Empty : dr["Remark2"].ToString();
                configmodel.Remark3 = dr["Remark3"] == null ? string.Empty : dr["Remark3"].ToString();
            }
            dr.Close();

            return configmodel;
        }

        public static bool Update(Upgrade.Model.ConfigModel model)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(" Update config ");
            strb.Append(" Set ");
            strb.Append("customerid             = '" + model.CustomerId + "',");
            strb.Append("customername           = '" + model.CustomerName + "',");
            strb.Append("linkman                = '" + model.LinkMan + "',");
            strb.Append("phone                  = '" + model.Phone + "',");
            strb.Append("companyname            = '" + model.CompanyName + "',");
            strb.Append("posno                  = '" + model.PosNo + "',");
            strb.Append("softwareversion        = '" + model.SoftWareVersion + "',");
            strb.Append("serverip               = '" + model.ServerIP + "',");
            strb.Append("historydatakeeptime    = '" + model.HistoryDataKeepTime + "',");
            strb.Append("pwd                    = '" + model.Pwd + "',");
            strb.Append("EPW                    = '" + model.EPW + "',");
            strb.Append("Remark1                = '" + model.Remark1 + "',");
            strb.Append("Remark2                = '" + model.Remark2 + "',");
            strb.Append("Remark3                = '" + model.Remark3 + "'");
            string strSql = strb.ToString();

            int intCount = SQLiteHelper.ExecuteNonQuery(Config.ConnectionString, strSql);
            if (intCount > 0)
                return true;
            else
                return false;
        }
    }
}

