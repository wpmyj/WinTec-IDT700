using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransService.Model;
using System.Data.SqlClient;
using System.Text;

namespace TransService.DAL
{
    public class TradeDAL : SQLBaseDAL
    {
        /// <summary>
        /// 保存交易
        /// </summary>
        /// <param name="saleFlow"></param>
        /// <param name="payFlow"></param>
        /// <param name="card"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool SaveTrade(MSaleFlow[] saleFlow, MPayFlow[] payFlow, out MCzCard card, out string msg)
        {
            if(saleFlow.Length==0 ||payFlow.Length==0)
            {
                msg = "流水不完整";
                card = null;
                return false;
            }

            //查询储值卡信息
            if (!CzkDAL.GetCzCard(payFlow.First().Ref_no, out card, out msg))
            {
                return false;
            }
            //检测卡余额
            if (card.Total-card.CzkType.KbMoney < payFlow.First().Total)
            {
                msg = "该卡余额不足";
                return false;
            }
            //扣减余额
            card.Total -= payFlow.First().Total;
            card.XfTotal += payFlow.First().Total;
 
            SqlTransaction tran;
            //开启事务
            if (!DBTool.BeginTransaction(out tran, out msg))
            {
                return false;
            }
            //插入商品流水
            if (!SaveSaleFlow(saleFlow, tran, out msg))
            {
                if (DBTool.RollbackTransaction(tran, out msg))
                {
                    msg = "保存商品流水失败";
                    return false;
                }
                else
                {
                    msg = "保存商品流水错误，回滚操作失败";
                    return false;
                }
            }
            //插入付款流水
            if (!SavePayFlow(payFlow, tran, out msg))
            {
                if (DBTool.RollbackTransaction(tran, out msg))
                {
                    msg = "保存付款流水失败";
                    return false;
                }
                else
                {
                    msg = "保存付款流水错误，回滚操作失败";
                    return false;
                }
            }
            //保存储值卡信息
            if (!CzkDAL.SaveCzCard(card, tran, out msg))
            {
                if (DBTool.RollbackTransaction(tran, out msg))
                {
                    msg = "保存储值卡信息失败";
                    return false;
                }
                else
                {
                    msg = "保存储值卡错误，回滚操作失败";
                    return false;
                }
            }
            //提交事务
            if (!DBTool.CommitTransaction(tran, out msg))
            {
                msg = "提交事务失败，已回滚";
                return false;
            }
            //交易成功，查询余额
            if (!CzkDAL.GetCzCard(payFlow.First().Ref_no, out card, out msg))
            {
                msg = "交易成功后确认余额失败";
            }
            return true;
        }

        /// <summary>
        /// 查询交易
        /// </summary>
        /// <param name="DeptNo"></param>
        /// <param name="saleFlow"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool QueryTrade(string PosNo, out ICollection<MSaleFlow> saleFlows, out string msg)
        {
            SqlDataReader rd=null;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select a.sgroup,a.operater,a.serial_no,a.PosNo,a.code,b.Fname ");
            sb.Append(",a.Price,a.qty,a.pre_total,a.disc,a.TOTAL,a.real_total ");
            sb.Append(" ,a.sa_date,a.sa_time,a.Squadno ,a.RowNo");
            sb.Append("  from pos_Sales a left join pos_goods b on a.code=b.incode ");
            sb.AppendFormat(" where PosNo='{0}'",PosNo);
            try
            {
                if (!DBTool.ExecSql(sb.ToString(), out rd, out msg))
                {
                    saleFlows = null;
                    return false;
                }
                else
                {
                    saleFlows = new List<MSaleFlow>();
                    while (rd.Read())
                    {
                        MSaleFlow saleFlow = new MSaleFlow();
                        saleFlow.DeptCode = Convert.ToString(rd["sgroup"]);
                        saleFlow.Disc = Convert.ToInt16(rd["disc"]);
                        saleFlow.Fname = Convert.ToString(rd["Fname"]);
                        saleFlow.Incode = Convert.ToString(rd["code"]);
                        saleFlow.Operater = Convert.ToString(rd["operater"]);
                        saleFlow.PosNo = Convert.ToString(rd["PosNo"]);
                        saleFlow.PreTotal = Convert.ToDecimal(rd["pre_total"]);
                        saleFlow.Price = Convert.ToDecimal(rd["Price"]);
                        saleFlow.Qty = Convert.ToDecimal(rd["qty"]);
                        saleFlow.RealTotal = Convert.ToDecimal(rd["real_total"]);
                        saleFlow.RowNo = Convert.ToInt16(rd["RowNo"]);
                        saleFlow.Sa_date = Convert.ToDateTime(rd["sa_date"]);
                        saleFlow.Sa_time = Convert.ToString(rd["sa_time"]);
                        saleFlow.SerialNo = Convert.ToString(rd["serial_no"]);
                        saleFlow.SquadNO = Convert.ToString(rd["Squadno"]);
                        saleFlow.Total = Convert.ToDecimal(rd["TOTAL"]);
                        saleFlows.Add(saleFlow);
                    }
                    rd.Close();
                    if (saleFlows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        msg = "未查询到流水";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (rd!=null && !rd.IsClosed)
                {
                    rd.Close();
                }
                msg = "系统异常：" + ex.Message;
                saleFlows = null;
                return false;
            }
        }
        /// <summary>
        /// 保存商品流水
        /// </summary>
        /// <param name="saleFlow"></param>
        /// <param name="tran"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static bool SaveSaleFlow(MSaleFlow[] saleFlow, SqlTransaction tran, out string msg)
        {
            int i;
            if (!DBTool.Insert(saleFlow, tran, out i, out msg))
            {
                return false;
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                msg = "插入商品流水失败";
                return false;
            }
        }
        /// <summary>
        /// 保存付款流水
        /// </summary>
        /// <param name="payFlow"></param>
        /// <param name="tran"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static bool SavePayFlow(MPayFlow[] payFlow, SqlTransaction tran, out string msg)
        {
            int i;
            if (!DBTool.Insert(payFlow, tran, out i, out msg))
            {
                return false;
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                msg = "插入付款流水失败";
                return false;
            }
        }
    }
}
