using System;

using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Action.SqlServer
{
    /// <summary>
    /// 商品信息相关数据库操作
    /// </summary>
    public class SqlServerSpxinxiAction:SqlServerBaseAction
    {
        public SqlServerSpxinxiAction(string connStr)
            : base(connStr)
        {
        }


        /// <summary>
        /// 获得商品信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Spxinxi> getSpxinxi()
        {
            List<Model.Spxinxi> spxinxiList = new List<Model.Spxinxi>();

            #region 组装sql语句
            string sqlStr = "select * from Pos_Goods";
            #endregion

            try
            {
                SqlDataReader reader = SqlEngine.ExecuteReader(sqlStr);

                #region 组装商品列表
                while (reader.Read())
                {
                    Model.Spxinxi spxinxiModel = new Model.Spxinxi();
                    spxinxiModel.Incode = reader["Incode"].ToString();
                    spxinxiModel.BarCode = reader["BarCode"].ToString();
                    spxinxiModel.FName = reader["Fname"].ToString();
                    spxinxiModel.Signchar = reader["signchar"].ToString();
                    spxinxiModel.Disc = int.Parse(reader["Disc"].ToString());
                    if (reader["Specs"] .ToString()!="")
                    {
                        spxinxiModel.Specs = reader["Specs"].ToString();
                    }
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
                    spxinxiModel.isDot = reader["isDot"].ToString();
                    spxinxiModel.isDisc = reader["isDisc"].ToString();

                    spxinxiList.Add(spxinxiModel);
                }
                #endregion
            }
            catch
            {
                spxinxiList.Clear();
                throw;
            }
           return spxinxiList;
        }
    }
}
