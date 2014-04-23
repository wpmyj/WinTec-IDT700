using System;

using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

namespace Action.SqlServer
{
    /// <summary>
    /// 流水操作类
    /// </summary>
    public class SqlServerFlowAction:SqlServerBaseAction
    {
        public SqlServerFlowAction(string connStr)
            : base(connStr)
        {
        }


        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="payFlow"></param>
        /// <returns></returns>
        public bool pay(Model.TradeFlow tradeFlow,Model.PayFlow payFlow)
        {
            bool ok = false;
            #region 组装sql语句
            ArrayList sqlList = new ArrayList();
            string sqlStr = string.Empty;
            Model.CzCard card;

            #region 获取卡片信息
            card = new Action.SqlServer.SqlServerCzCarkAction(ConStr).getCzCardByZh(payFlow.Cardno);
            #endregion

            #region 获取积分基数
            sqlStr = "select jfbase from dbo.tCzkMnyType where code=(select mclass from tczkcard where incardno='"+card.Hyzh+"')";
            SqlDataReader reader=SqlEngine.ExecuteReader(sqlStr);
            while (reader.Read())
            {
                card.JfBase = float.Parse(reader["jfbase"].ToString());
            }
            reader.Close();

            #endregion

            #region 更新储值卡
            sqlStr = "update tCzkCard set "
                + " total=total-(" + tradeFlow.Total.ToString("F2") + "),"
                + " xftotal=xftotal+(" + tradeFlow.Total.ToString("F2") + ")"
                + " where incardno='" + payFlow.Cardno + "'";
            sqlList.Add(sqlStr);
            #endregion 

            if (card.JfBase > 0)
            {
                float bcjf = tradeFlow.Total / card.JfBase;
                #region 更新积分
                sqlStr = "update tCzkCard set jfgrade=jfgrade+" + bcjf.ToString("F2") + " where incardno='" + card.Hyzh + "'";
                sqlList.Add(sqlStr);
                #endregion

                #region 更新积分流水
                sqlStr = "insert into tCzkJfLog(cardno,sdate,scjf,bcjf,bcje,posno,UserCode,UserName,Remark) "
                     +" values('"+payFlow.Cardno+"','"+tradeFlow.Sdate+"',"+card.JfGrade+","+bcjf+","+tradeFlow.Total+",'"+tradeFlow.Posno+"','"+tradeFlow.Operater+"','"+tradeFlow.Operater+"','终端刷卡积分')";
                sqlList.Add(sqlStr);
                #endregion
            }

            #region 更新储值卡进销存
            sqlStr = "exec PROC_XsCzkJxc '" + tradeFlow.Sdate + "', '" + payFlow.Cardno + "', '" + card.Hykh + "', 0,0, 0, 0, "+tradeFlow.Total.ToString("F2")+", '" + tradeFlow.Operater + "', '" + tradeFlow.Operater + "', null,null";
            sqlList.Add(sqlStr);
            #endregion

            #region tradeflow
            sqlStr = "INSERT INTO tXsTradeFlow ([flow_no],[djlx],[posno],[sdate],[operater],[squadno],[tradetype],"
            +" [total],[zkje],[payje]"
            + ",[Change],[CustCode],[bcjf],[scjf]) values("
            + "'" + tradeFlow.Flow_no+"',"
            +"'1',"
            + "'" + tradeFlow.Posno + "',"
            + "'" + tradeFlow.Sdate+" "+tradeFlow.Stime+ "',"
            + "'" + tradeFlow.Operater + "',"
            + "'" + tradeFlow.Squadno + "',"
            + "'" + tradeFlow.tradeType + "',"
            + tradeFlow.Total.ToString("F2") + ","
            + tradeFlow.Zkje.ToString("F2") + ","
            + tradeFlow.Payje.ToString("F2") + ","
            + tradeFlow.Change.ToString("F2") + ","
            + "'" + tradeFlow.CustCode + "',"
            + tradeFlow.Bcjf + ','
            + tradeFlow.Scjf 
            + ")";

            sqlList.Add(sqlStr);
            #endregion

            #region payflow
            sqlStr = "insert into tXsPayFlow ([flow_no],[flow_id],[posno],[sdate],[operater],[squadno],"
            + "[tradetype],[paypmt],[Pay_Amount],[total] ,[cardno],[scye]) values( "

            + "'" + payFlow.Flow_no + "',"
            + payFlow.Flow_id.ToString()+","
            + "'" + payFlow.Posno + "',"
            + "'" + payFlow.Sdate +" "+payFlow.Stime+ "',"
            + "'" + payFlow.Operater + "',"
            + "'" + payFlow.Squadno + "',"
            + "'" + payFlow.tradetype + "',"
            + "'" + payFlow.paypmt + "',"
            + payFlow.PayAmount.ToString("f2") + ","
            + payFlow.Total.ToString("f2") + ","
            + "'" + payFlow.Cardno + "',"
            + payFlow.Scye.ToString("f2") 
            +")";
            sqlList.Add(sqlStr);
            #endregion


            #endregion


            try
            {
                #region 执行sql
                ok = SqlEngine.ExecuteSqlTran(sqlList);
                #endregion
            }
            catch (Exception ex)
            {
                ok = false;
                throw ex;
            }
            return ok;

        }



        /// <summary>
        /// 保存流水
        /// </summary>
        /// <param name="tradeFlowList">交易总表</param>
        /// <param name="payFlowList">付款流水</param>
        /// <returns></returns>
        public bool saveFlow(List<Model.TradeFlow> tradeFlowList, List<Model.PayFlow> payFlowList)
        {
            bool ok = false;
            string sqlStr=string.Empty;
            ArrayList sqlList=new ArrayList();
            #region 组装sql语句
            #region 交易总流水

            foreach (Model.TradeFlow tradeFlow in tradeFlowList)
            {
                sqlStr = "INSERT INTO tXsTradeFlow ([flow_no],[djlx],[posno],[sdate],[operater],[squadno],[tradetype],"
            + " [total],[zkje],[payje]"
            + ",[Change],[CustCode],[bcjf],[scjf]) values("
            + "'" + tradeFlow.Flow_no + "',"
            + "'1',"
            + "'" + tradeFlow.Posno + "',"
            + "'" + tradeFlow.Sdate + " " + tradeFlow.Stime + "',"
            + "'" + tradeFlow.Operater + "',"
            + "'" + tradeFlow.Squadno + "',"
            + "'" + tradeFlow.tradeType + "',"
            + tradeFlow.Total.ToString("F2") + ","
            + tradeFlow.Zkje.ToString("F2") + ","
            + tradeFlow.Payje.ToString("F2") + ","
            + tradeFlow.Change.ToString("F2") + ","
            + "'" + tradeFlow.CustCode + "',"
            + tradeFlow.Bcjf + ','
            + tradeFlow.Scjf
            + ")";
             sqlList.Add(sqlStr);
            }

            #endregion

            #region 付款流水

            foreach (Model.PayFlow payFlow in payFlowList)
            {
                sqlStr = "insert into tXsPayFlow ([flow_no],[flow_id],[posno],[sdate],[operater],[squadno],"
            + "[tradetype],[paypmt],[Pay_Amount],[total] ,[cardno],[scye]) values( "

            + "'" + payFlow.Flow_no + "',"
            + payFlow.Flow_id.ToString() + ","
            + "'" + payFlow.Posno + "',"
            + "'" + payFlow.Sdate + " " + payFlow.Stime + "',"
            + "'" + payFlow.Operater + "',"
            + "'" + payFlow.Squadno + "',"
            + "'" + payFlow.tradetype + "',"
            + "'" + payFlow.paypmt + "',"
            + payFlow.PayAmount.ToString("f2") + ","
            + payFlow.Total.ToString("f2") + ","
            + "'" + payFlow.Cardno + "',"
            + payFlow.Scye.ToString("f2")
            + ")";
                sqlList.Add(sqlStr);
            }

            #endregion

            #endregion

            try
            {
                #region 执行sql
                ok = SqlEngine.ExecuteSqlTran(sqlList);
                #endregion
            }
            catch(Exception ex)
            {
                ok = false;
                throw ex;
            }
            return ok;
        }
    }
}
