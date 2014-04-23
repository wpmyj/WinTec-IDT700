using System;

using System.Collections.Generic;
using System.Text;
using System.Collections;

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
        /// 上传流水
        /// </summary>
        /// <param name="saleFlowList">商品流水</param>
        /// <param name="payFlowList">付款流水</param>
        /// <returns></returns>
        public bool upLoadFlow(List<Model.SaleFlow> saleFlowList, List<Model.PayFlow> payFlowList)
        {
            bool ok;
            ArrayList sqlStrList = new ArrayList();
            string sqlStr=string.Empty;
            #region 组装sql语句

            #region 插入商品流水
            foreach (Model.SaleFlow saleFlow in saleFlowList)
            {
                sqlStr = "insert into pos_sales (operater,saler,sgroup,serial_no,PosNo,code,Price,qty,pre_total,disc,total,zdisc,real_total,sa_date,sa_time"
                    + ",vip_no,vip_pmp,RightNo,RightFlag,Squadno,RowNo) "
                    + " values("
                    + "'"+saleFlow.Operater+"',"
                    + "'" + saleFlow.Saler + "',"
                    + "'" + saleFlow.Sgroup + "',"
                    + "'" + saleFlow.Serial_no + "',"
                    + "'" + saleFlow.PosNo + "',"
                    + "'" + saleFlow.Code + "',"
                    + saleFlow.Price.ToString("F2")+","
                    + saleFlow.Qty.ToString()+","
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
                    + saleFlow.RowNo.ToString() + ")";

                sqlStrList.Add(sqlStr);

            }
            #endregion

            #region 插入付款流水
            foreach (Model.PayFlow payFlow in payFlowList)
            {
                sqlStr = "insert into pos_mulpay (operater,serial_no,total,paypmt,sa_date,sa_time,ref_no,squadno,PosNo,RowNo,ckic) "
                    + "values("
                    + "'"+payFlow.Operater+"',"
                    + "'" + payFlow.Serial_no + "',"
                    + payFlow.Total.ToString("F2")+","
                    + "'" + ((int)payFlow.Paypmt).ToString() + "',"
                    + "'" + payFlow.Sa_date + "',"
                    + "'" + payFlow.Sa_time + "',"
                    + "'" + payFlow.Ref_no + "',"
                    + "'" + payFlow.Squadno + "',"
                    + "'" + payFlow.PosNo + "',"
                    + payFlow.RowNo.ToString()+","
                    + "'" + ((int)payFlow.Ckic).ToString() + "')";

                sqlStrList.Add(sqlStr);
            }
            #endregion

            #endregion

            try
            {
                #region 执行sql语句
                ok = SqlEngine.ExecuteSqlTran(sqlStrList);
                #endregion

                ok = true;
            }
            catch(Exception ex)
            {
                ok = false;
                throw;
            }
            return ok;
        }
    }
}
