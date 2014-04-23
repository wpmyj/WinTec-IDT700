using System;

using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Action.SqlServer
{
    /// <summary>
    /// 储值卡相关操作
    /// </summary>
    public class SqlServerCzCarkAction:SqlServerBaseAction
    {
        public SqlServerCzCarkAction(string connStr)
            : base(connStr)
        {
        }


        /// <summary>
        /// 查找指定储值卡
        /// </summary>
        /// <param name="inCardno">储值卡内卡号</param>
        /// <returns></returns>
        public Model.CzCard getCzCard(string inCardno)
        {
            Model.CzCard czCard = new Model.CzCard();

            #region 组装sql语句
            string sqlStr = "select * from tCzkCard where InCardno='" + inCardno + "'";
            #endregion

            try
            {
                #region 执行sql语句
                SqlDataReader reader = SqlEngine.ExecuteReader(sqlStr);
                #endregion

                #region 组装储值卡model
                while (reader.Read())
                {
                    czCard.Incardno = reader["InCardno"].ToString();
                    czCard.Outcardno = reader["OutCardno"].ToString();
                    czCard.Psw = reader["Psw"].ToString();
                    string a = reader["Stat"].ToString();
                    czCard.Stat = (Model.CzkStat)int.Parse(reader["Stat"].ToString());
                    czCard.Total = float.Parse(reader["Total"].ToString());
                    czCard.CzkType = (Model.CzCardType)int.Parse(reader["ckic"].ToString());
                }
                #endregion
            }
            catch
            {
                throw;
            }
            return czCard;
        }



        /// <summary>
        /// 刷卡消费
        /// </summary>
        /// <param name="czCard">储值卡</param>
        /// <param name="payFlowList">付款流水</param>
        /// <param name="saleFlowList">商品流水</param>
        /// <returns></returns>
        public bool CardPay(string  inCardNo,float total)
        {
            bool ok=false;

            #region 组装sql语句
            string sqlStr = "update tCzkCard set "
                +" total=total-("+total.ToString("F2")+"),"
                +" XfTotal=XfTotal+("+total.ToString("F2")+")"
                + " where inCardno='"+inCardNo+"'";
            #endregion

            try
            {
                #region 执行sql语句
                int ret = SqlEngine.ExecuteSql(sqlStr);
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


        /// <summary>
        /// 刷卡消费
        /// </summary>
        /// <param name="payFlowList">付款流水</param>
        /// <returns></returns>
        public bool CardPay(List<Model.PayFlow> payFlowList)
        {
            bool ok = false;
            ArrayList sqlArrayList = new ArrayList();
            #region 组装sql
            foreach (Model.PayFlow payflow in payFlowList)
            {
                string sqlStr = "update tCzkCard set "
               + " total=total-(" + payflow.Total.ToString("F2") + "),"
               + " XfTotal=XfTotal+(" + payflow.Total.ToString("F2") + ")"
               + " where inCardno='" + payflow.Ref_no + "'";
                sqlArrayList.Add(sqlStr);
            }
            #endregion

            #region 执行sql
            try
            {
                ok=SqlEngine.ExecuteSqlTran(sqlArrayList);
            }
            catch
            {
                ok = false;
            }
            #endregion

            return ok;
        }
    }
}
