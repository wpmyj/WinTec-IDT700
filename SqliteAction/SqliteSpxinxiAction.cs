using System;

using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SQLite;

namespace Action.Sqlite
{
    /// <summary>
    /// 商品信息数据库相关操作
    /// </summary>
    public class SqliteSpxinxiAction:SqliteBaseAction
    {
        public SqliteSpxinxiAction(string connStr)
            : base(connStr)
        {
        }


        /// <summary>
        /// 保存商品信息
        /// </summary>
        /// <param name="spxinxiList">商品列表</param>
        /// <returns></returns>
        public bool saveSpxinxi(List<Model.Spxinxi> spxinxiList)
        {
            bool ok = false;
            ArrayList sqlStrArray = new ArrayList();
            #region 组装sql语句

            #region 删除原商品信息
            sqlStrArray.Add("delete from tbspxinxi");
            #endregion

            #region 添加商品信息
            foreach (Model.Spxinxi spxinxi in spxinxiList)
            {
                if(spxinxi.FName.Contains("'"))
                {
                    spxinxi.FName.Replace("'", "''");
                }
                string sqlStr = "insert into tbspxinxi "
                    + "(incode,barcode,fName,signChar,disc,unit,specs,price,hyPrc,custno,stype,promPrc,status,grpno,maxDisc,hyFlag,cxFlag,flag) "
                    + "values("
                    + "'"+spxinxi.Incode+"',"
                    + "'" + spxinxi.BarCode + "',"
                    + "'" + spxinxi.FName + "',"
                    + "'" + spxinxi.Signchar + "',"
                    +       spxinxi.Disc.ToString() + ","
                    + "'" + spxinxi.Unit+"',"
                    + "'" + spxinxi.Specs + "',"
                    +       spxinxi.Price.ToString("f2") + ","
                    +       spxinxi.HyPrc.ToString("f2") + ","
                    + "'" + spxinxi.Custno + "',"
                    + "'" + spxinxi.Stype + "',"
                    +       spxinxi.PromPrc.ToString()+","
                    + "'" + spxinxi.status + "',"
                    + "'" + spxinxi.Grpno + "',"
                    +       spxinxi.MaxDisc.ToString() + ","
                    + "'" + spxinxi.hyFlag + "',"
                    + "'" + spxinxi.cxFlag + "',"
                    + "'" + spxinxi.flag + "'"
                    + ")";

                sqlStrArray.Add(sqlStr);
            }
            #endregion

            #endregion

            try
            {
                #region 执行sql语句
                SqliteEngine.ExecuteNonQueryWithTran(sqlStrArray);
                #endregion

                ok = true;
            }
            catch
            {
                ok = false;
                throw;
            }
            return ok;
        }



        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Spxinxi> getSpxinxi()
        {
            List<Model.Spxinxi> spxinxiList = new List<Model.Spxinxi>();
            #region 组装sql语句
            string sqlStr = "select * from tbspxinxi";            
            #endregion
            try
            {

                #region 执行sql语句
                SQLiteDataReader reader=SqliteEngine.ExecuteReader(sqlStr);
                #endregion

                #region 组装商品列表
                while (reader.Read())
                {
                    Model.Spxinxi spxinxiModel = new Model.Spxinxi();
                    spxinxiModel.Incode = reader["Incode"].ToString();
                    spxinxiModel.BarCode = reader["BarCode"].ToString();
                    spxinxiModel.FName = reader["FName"].ToString();
                    spxinxiModel.Signchar = reader["signchar"].ToString();
                    spxinxiModel.Disc = int.Parse(reader["Disc"].ToString());
                    spxinxiModel.Unit = reader["unit"].ToString();
                    spxinxiModel.Specs = reader["Specs"].ToString();
                    
                    spxinxiModel.Price = float.Parse(reader["Price"].ToString());
                    spxinxiModel.HyPrc = float.Parse(reader["HyPrc"].ToString());
                    spxinxiModel.Custno = reader["Custno"].ToString();
                    spxinxiModel.Stype = reader["stype"].ToString();
                    if (reader["PromPrc"] .ToString()!= "")
                    {
                        spxinxiModel.PromPrc = float.Parse(reader["PromPrc"].ToString());
                    }
                    spxinxiModel.status = reader["Status"].ToString();
                    spxinxiModel.Grpno = reader["Grpno"].ToString();
                    spxinxiModel.MaxDisc = int.Parse(reader["MaxDisc"].ToString());
                    spxinxiModel.hyFlag = reader["Hyflag"].ToString();
                    spxinxiModel.cxFlag = reader["CxFlag"].ToString();
                    spxinxiModel.flag = reader["Flag"].ToString();
                    spxinxiList.Add(spxinxiModel);
                }
                #endregion
            }
            catch
            {
                throw;
            }
            return spxinxiList;
        }


        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="incode">商品编码</param>
        /// <returns></returns>
        public Model.Spxinxi getSpxinxi(string str)
        {
            Model.Spxinxi spxinxiModel = new Model.Spxinxi();
            #region 组装sql 语句
            string sqlStr = "select * from tbspxinxi where incode='" + str + "' or barcode='"+str+"' or signchar='"+str+"'";
            #endregion

            #region 执行sql语句
            SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
            #endregion

            #region 组装商品信息
            while (reader.Read())
            {
                spxinxiModel.Incode = reader["Incode"].ToString();
                spxinxiModel.BarCode = reader["BarCode"].ToString();
                spxinxiModel.FName = reader["FName"].ToString();
                spxinxiModel.Signchar = reader["signchar"].ToString();
                spxinxiModel.Disc = int.Parse(reader["Disc"].ToString());
                spxinxiModel.Unit = reader["unit"].ToString();
                spxinxiModel.Specs = reader["Specs"].ToString();
               
                spxinxiModel.Price = float.Parse(reader["Price"].ToString());
                spxinxiModel.HyPrc = float.Parse(reader["HyPrc"].ToString());
                spxinxiModel.Custno = reader["Custno"].ToString();
                spxinxiModel.Stype = reader["stype"].ToString();
                if (reader["PromPrc"].ToString() != "")
                {
                    spxinxiModel.PromPrc = float.Parse(reader["PromPrc"].ToString());
                }
                spxinxiModel.status = reader["Status"].ToString();
                spxinxiModel.Grpno = reader["Grpno"].ToString();
                spxinxiModel.MaxDisc = int.Parse(reader["MaxDisc"].ToString());
                spxinxiModel.hyFlag = reader["Hyflag"].ToString();
                spxinxiModel.cxFlag = reader["CxFlag"].ToString();
                spxinxiModel.flag = reader["Flag"].ToString();
            }
            #endregion

            return spxinxiModel;
        }

    }
}
