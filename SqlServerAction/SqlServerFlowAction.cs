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
        public bool pay(List<Model.SaleFlow> saleFlowList,List<Model.PayFlow> payFlowList)
        {
            bool ok = false;
            #region 组装sql语句
            ArrayList sqlList = new ArrayList();
            string sqlStr = string.Empty;
            //Model.CzCard card;

            //#region 获取卡片信息
            //card = new Action.SqlServer.SqlServerCzCarkAction(ConStr).getCzCardByZh(payFlow.Ref_no);
            //#endregion

            //#region 获取积分基数
            //sqlStr = "select jfbase from dbo.tCzkMnyType where code=(select mclass from tczkcard where incardno='"+card.Hyzh+"')";
            //SqlDataReader reader=SqlEngine.ExecuteReader(sqlStr);
            //while (reader.Read())
            //{
            //    card.JfBase = float.Parse(reader["jfbase"].ToString());
            //}
            //reader.Close();

            //#endregion



            //if (card.JfBase > 0)
            //{
            //    float bcjf = tradeFlow.Total / card.JfBase;
            //    #region 更新积分
            //    sqlStr = "update tCzkCard set jfgrade=jfgrade+" + bcjf.ToString("F2") + " where incardno='" + card.Hyzh + "'";
            //    sqlList.Add(sqlStr);
            //    #endregion

            //    #region 更新积分流水
            //    sqlStr = "insert into tCzkJfLog(cardno,sdate,scjf,bcjf,bcje,posno,UserCode,UserName,Remark) "
            //         +" values('"+payFlow.Cardno+"','"+tradeFlow.Sdate+"',"+card.JfGrade+","+bcjf+","+tradeFlow.Total+",'"+tradeFlow.Posno+"','"+tradeFlow.Operater+"','"+tradeFlow.Operater+"','终端刷卡积分')";
            //    sqlList.Add(sqlStr);
            //    #endregion
            //}

            //#region 更新储值卡进销存
            //sqlStr = "exec PROC_XsCzkJxc '" + tradeFlow.Sdate + "', '" + payFlow.Cardno + "', '" + card.Hykh + "', 0,0, 0, 0, "+tradeFlow.Total.ToString("F2")+", '" + tradeFlow.Operater + "', '" + tradeFlow.Operater + "', null,null";
            //sqlList.Add(sqlStr);
            //#endregion

            #region 插入商品流水
            foreach (Model.SaleFlow saleFlow in saleFlowList)
            {
                sqlStr = "insert into pos_sales (operater,saler,sgroup,serial_no,PosNo,code,Price,qty,pre_total,disc,total,zdisc,real_total,sa_date,sa_time"
                    + ",vip_no,vip_pmp,RightNo,RightFlag,Squadno,RowNo,hzsn) "
                    + " values("
                    + "'" + saleFlow.Operater + "',"
                    + "'" + saleFlow.Saler + "',"
                    + "'" + saleFlow.Sgroup + "',"
                    + "'" + saleFlow.Serial_no + "',"
                    + "'" + saleFlow.PosNo + "',"
                    + "'" + saleFlow.Code + "',"
                    + saleFlow.Price.ToString("F2") + ","
                    + saleFlow.Qty.ToString() + ","
                    + saleFlow.Pre_total.ToString("F2") + ","
                    + saleFlow.Disc.ToString() + ","
                    + saleFlow.Total.ToString("F2") + ","
                    + saleFlow.Zdisc.ToString() + ","
                    + saleFlow.Real_total.ToString("F2") + ","
                    + "'" + saleFlow.Sa_date + "',"
                    + "'" + saleFlow.Sa_time + "',"
                    + "'" + saleFlow.Vip_no + "',"
                    + "'" + saleFlow.vip_pmp + "',"
                    + "'" + saleFlow.RightNo + "',"
                    + "'" + saleFlow.RightFlag + "',"
                    + "'" + saleFlow.Squadno + "',"
                    + saleFlow.RowNo.ToString() + ","
                    +"'')";

                sqlList.Add(sqlStr);

            }
            #endregion


            #region 插入付款流水、更新储值卡
            foreach (Model.PayFlow payFlow in payFlowList)
            {
                sqlStr = "insert into pos_mulpay (operater,serial_no,total,paypmt,sa_date,sa_time,ref_no,squadno,PosNo,RowNo,hzsn) "
                    + "values("
                    + "'" + payFlow.Operater + "',"
                    + "'" + payFlow.Serial_no + "',"
                    + payFlow.Total.ToString("F2") + ","
                    + "'" + payFlow.paypmt + "',"
                    + "'" + payFlow.Sa_date + "',"
                    + "'" + payFlow.Sa_time + "',"
                    + "'" + payFlow.Ref_no + "',"
                    + "'" + payFlow.Squadno + "',"
                    + "'" + payFlow.PosNo + "',"
                    + payFlow.RowNo.ToString() + ","
                    //+ "'" + ((int)payFlow.Ckic).ToString() + "',"
                    +"'')";

                sqlList.Add(sqlStr);
                if (payFlow.Ref_no != "" && payFlow.Ref_no != null)
                {
                    #region 更新储值卡
                    sqlStr = "update tCzkCard set "
                        + " total=total-(" + payFlow.Total.ToString("F2") + "),"
                        + " xftotal=xftotal+(" + payFlow.Total.ToString("F2") + ")"
                        + " where incardno='" + payFlow.Ref_no + "'";
                    sqlList.Add(sqlStr);
                    #endregion
                }
            }
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
        public bool saveFlow(List<Model.SaleFlow> saleFlowList, List<Model.PayFlow> payFlowList)
        {
            bool ok = false;
            string sqlStr=string.Empty;
            ArrayList sqlList=new ArrayList();
            #region 组装sql语句

            #region 插入商品流水
            foreach (Model.SaleFlow saleFlow in saleFlowList)
            {
                sqlStr = "insert into pos_sales (operater,saler,sgroup,serial_no,PosNo,code,Price,qty,pre_total,disc,total,zdisc,real_total,sa_date,sa_time"
                    + ",vip_no,vip_pmp,RightNo,RightFlag,Squadno,RowNo,hzsn) "
                    + " values("
                    + "'" + saleFlow.Operater + "',"
                    + "'" + saleFlow.Saler + "',"
                    + "'" + saleFlow.Sgroup + "',"
                    + "'" + saleFlow.Serial_no + "',"
                    + "'" + saleFlow.PosNo + "',"
                    + "'" + saleFlow.Code + "',"
                    + saleFlow.Price.ToString("F2") + ","
                    + saleFlow.Qty.ToString() + ","
                    + saleFlow.Pre_total.ToString("F2") + ","
                    + saleFlow.Disc.ToString() + ","
                    + saleFlow.Total.ToString("F2") + ","
                    + saleFlow.Zdisc.ToString() + ","
                    + saleFlow.Real_total.ToString("F2") + ","
                    + "'" + saleFlow.Sa_date + "',"
                    + "'" + saleFlow.Sa_time + "',"
                    + "'" + saleFlow.Vip_no + "',"
                    + "'" + saleFlow.vip_pmp + "',"
                    + "'" + saleFlow.RightNo + "',"
                    + "'" + saleFlow.RightFlag + "',"
                    + "'" + saleFlow.Squadno + "',"
                    + saleFlow.RowNo.ToString() + ","
                    +"'')";

                sqlList.Add(sqlStr);

            }
            #endregion


            #region 插入付款流水
            foreach (Model.PayFlow payFlow in payFlowList)
            {
                sqlStr = "insert into pos_mulpay (operater,serial_no,total,paypmt,sa_date,sa_time,ref_no,squadno,PosNo,RowNo,hzsn) "
                    + "values("
                    + "'" + payFlow.Operater + "',"
                    + "'" + payFlow.Serial_no + "',"
                    + payFlow.Total.ToString("F2") + ","
                    + "'" + ((int)payFlow.Paypmt).ToString() + "',"
                    + "'" + payFlow.Sa_date + "',"
                    + "'" + payFlow.Sa_time + "',"
                    + "'" + payFlow.Ref_no + "',"
                    + "'" + payFlow.Squadno + "',"
                    + "'" + payFlow.PosNo + "',"
                    + payFlow.RowNo.ToString() + ","
                    //+ "'" + ((int)payFlow.Ckic).ToString() + "',"
                    +"'')";

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
