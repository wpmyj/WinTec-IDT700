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
        /// 查找储值卡
        /// </summary>
        /// <param name="inCardno">储值卡帐号</param>
        /// <returns></returns>
        public Model.CzCard getCzCardByZh(string hyzh)
        {
            Model.CzCard czCard = new Model.CzCard();

            #region 组装sql语句
            string sqlStr = "select * from tCzkCard where hyzh='" + hyzh + "'";
            #endregion

            try
            {
                #region 执行sql语句
                SqlDataReader reader = SqlEngine.ExecuteReader(sqlStr);
                #endregion

                #region 组装储值卡model
                if(reader.Read())
                {
                    czCard.Hyzh = reader["hyzh"].ToString();
                    czCard.Hykh = reader["hykh"].ToString();
                    czCard.CustName = reader["CustName"].ToString();
                    czCard.Psw = reader["Psw"].ToString();
                    czCard.Stat = (Model.CzkStat)int.Parse(reader["Stat"].ToString());
                    czCard.Total = float.Parse(reader["Total"].ToString());
                    czCard.Kkje = float.Parse(reader["kkje"].ToString());
                    //czCard.JfGrade = float.Parse(reader["JfGrade"].ToString());
                }
                #endregion
                reader.Close();
            }
            catch
            {
                throw;
            }
            CzCardVerifyMark(ref czCard);//验证卡金额
            return czCard;
        }


        /// <summary>
        /// 查找储值卡
        /// </summary>
        /// <param name="hykh">储值卡卡号</param>
        /// <returns></returns>
        public Model.CzCard getCzCardByKh(string hykh)
        {
            Model.CzCard czCard = new Model.CzCard();

            #region 组装sql语句
            string sqlStr = "select * from tCzkCard where hykh='" + hykh + "'";
            #endregion

            try
            {
                #region 执行sql语句
                SqlDataReader reader = SqlEngine.ExecuteReader(sqlStr);
                #endregion

                #region 组装储值卡model
                if (reader.Read())
                {
                    czCard.Hyzh = reader["hyzh"].ToString();
                    czCard.Hykh = reader["hykh"].ToString();
                    czCard.CustName = reader["CustName"].ToString();
                    czCard.Psw = reader["Psw"].ToString();
                    czCard.Stat = (Model.CzkStat)int.Parse(reader["Stat"].ToString());
                    czCard.Total = float.Parse(reader["Total"].ToString());
                    czCard.Kkje = float.Parse(reader["kkje"].ToString());

                    //czCard.JfGrade = float.Parse(reader["JfGrade"].ToString());
                }
                #endregion
                reader.Close();
            }
            catch
            {
                throw;
            }

            CzCardVerifyMark(ref czCard);//验证卡金额
            return czCard;
        }

        ///// <summary>
        ///// 刷卡消费
        ///// </summary>
        ///// <param name="czCard">储值卡</param>
        ///// <param name="payFlowList">付款流水</param>
        ///// <param name="saleFlowList">商品流水</param>
        ///// <returns></returns>
        //public bool CardPay(string  hyzh,float total)
        //{
        //    bool ok=false;
        //    ArrayList sqlList = new ArrayList();
        //    #region 更新储值卡
        //    string sqlStr = "update tCzkCard set "
        //        +" total=total-("+total.ToString("F2")+"),"
        //        +" xftotal=xftotal+("+total.ToString("F2")+")"
        //        +"lastxfje="+total+" "
        //        + " where hyzh='"+hyzh+"'";
        //    sqlList.Add(sqlStr);
            
        //    #endregion


        //    try
        //    {
        //        #region 执行sql语句
        //        ok = SqlEngine.ExecuteSqlTran(sqlList);
        //        #endregion
        //    }
        //    catch
        //    {
        //        ok = false;
        //        throw;
        //    }
        //    return ok;
        //}


        /// <summary>
        /// 储值卡充值
        /// </summary>
        /// <param name="czflow"></param>
        /// <returns></returns>
        public Model.CzCardChZhRst CzCardChZh(Model.CzCardChZhFlow czflow)
        {
            Model.CzCardChZhRst rst = new Model.CzCardChZhRst();
            rst.Rst = false;
            bool ok;
            #region 组装sql
            ArrayList sqlList = new ArrayList();
            string sdate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string sqlStr = string.Empty;
            string nextNo = string.Empty;
            string pzNo = string.Empty;
            try
            {
                #region 获取NextNo,生成pzNo
                sqlStr = "select NextNo from xtNextForm where FormType='tCzkCzRpt'";

                SqlDataReader reader = SqlEngine.ExecuteReader(sqlStr);
                while (reader.Read())
                {
                    nextNo = reader["NextNo"].ToString();
                }
                reader.Close();
                pzNo = nextNo;
                while (pzNo.Length < 12)
                {
                    pzNo = "0" + pzNo;
                }
                pzNo = czflow.PosNo + pzNo;
                #endregion

                #region 更新储值卡信息
                sqlStr = "Update tCzkCard"
                + " set total=total+(" + czflow.Czje.ToString("F2") + "),czje=isnull(czje,0)+(" + czflow.Czje.ToString("F2") + ") "
                + " where hyzh='" + czflow.IncardNo + "'";

                sqlList.Add(sqlStr);
                #endregion

                #region 计算校验
                string markstr = new CzkVerifyMark().VerifyMark(czflow.IncardNo,czflow.Bcye+czflow.Czje+czflow.Kkje);
                #endregion

                #region 更新校验
                sqlStr = "update tCzkCardMark set VerifyMark='"+markstr+"' where hykh='"+czflow.OutcardNo+"'";
                sqlList.Add(sqlStr);
                #endregion

                #region 插入tCzkCzRpt
                sqlStr = "insert into tCzkCzRpt$" + sdate.Substring(0, 4) + sdate.Substring(5, 2)
                    + "( hyzh,hykh,scye,czje,flje,pzno,remark,OperType,OperDate,OperCode,OperPep)  "
                    + " select hyzh,hykh," + czflow.Bcye + "," + czflow.Czje + ",0,'" + pzNo + "','" + "门店终端充值" + "','2','" + sdate.Substring(0, 10) + "','" + czflow.UserCode + "','" + czflow.UserName + "'  "
                    + "from tCzkCard where hyzh='" + czflow.IncardNo + "'";

                sqlList.Add(sqlStr);
                #endregion

                #region 更新NextNo
                sqlStr = "update xtNextForm set NextNo='" + (int.Parse(nextNo) + 1).ToString() + "' where FormType='tCzkCzRpt'";
                sqlList.Add(sqlStr);
                #endregion

                #region 更新储值卡进销存
                sqlStr = "exec PROC_XsCzkJxc '" + sdate + "', '" + czflow.IncardNo + "', '" + czflow.OutcardNo + "', 0," + czflow.Czje + ", 0, '" + czflow.UserCode + "', '" + czflow.UserName + "', null,null";
                sqlList.Add(sqlStr);
                #endregion

                #region 更新储值卡日志
                sqlStr = "exec PROC_UpdateCzkCard '" + czflow.IncardNo + "', '" + czflow.UserCode + "', '" + czflow.UserName + "', '02', '充值', null, null";
                sqlList.Add(sqlStr);
                #endregion

                #region 更新用户日志
                sqlStr = "insert into XtUserLog(LogDate,UserCode,UserName,Computer,Operation,LogType)"
                        + " values('" + sdate + "','" + czflow.UserCode + "','" + czflow.UserName + "','" + czflow.PosNo + "','充值卡号：" + czflow.OutcardNo + "','P')";
                sqlList.Add(sqlStr);
                #endregion

                #region 执行
                ok = SqlEngine.ExecuteSqlTran(sqlList);
                #endregion
                
                #region 组装结果
                rst.Pzno = pzNo;
                rst.OutCardno = czflow.OutcardNo;
                rst.Rst = ok;
                rst.Czje = czflow.Czje;
                rst.Sdate = sdate;
                rst.Scye = czflow.Bcye;
                sqlStr = "select total from tczkcard where hyzh='"+czflow.IncardNo+"'";
                reader = SqlEngine.ExecuteReader(sqlStr);
                while (reader.Read())
                {
                    rst.Dqye = float.Parse(reader["total"].ToString());
                }
                #endregion

            #endregion
            }
            catch (Exception ex)
            {
                rst.Rst = false;
                throw ex;
            }
            return rst;
        }


        /// <summary>
        ///  储值卡校验
        /// </summary>
        /// <param name="hyzh"></param>
        /// <returns></returns>
        public void CzCardVerifyMark(ref Model.CzCard card)
        {
            string s1, s2;
            string sqlstr = "";
            SqlDataReader reader;

            #region 计算校验码
            CzkVerifyMark mk = new CzkVerifyMark();
            s1 = mk.VerifyMark(card.Hyzh, card.Total+card.Kkje);
            #endregion

            #region 获取数据库校验码
            sqlstr = "select VerifyMark from tCzkCardMark where hykh='"+card.Hykh+"'";
            try
            {
                reader = SqlEngine.ExecuteReader(sqlstr);
                s2 = "";
                while (reader.Read())
                {
                    s2 = (string)reader["VerifyMark"];
                }
                reader.Close();
            }
            catch
            {
                throw;
            }
            #endregion

            #region 比较验证码

            if (s1.CompareTo(s2) == 0)
            {
                card.CanVerifyMark=true;
            }
            else
            {
                card.CanVerifyMark=false;
            }
            #endregion
        }

        ///// <summary>
        ///// 保存储值卡校验码
        ///// </summary>
        ///// <param name="hyzh"></param>
        ///// <returns></returns>
        //public bool SaveCzCardVerifyMark(ref Model.CzCard card)
        //{
        //    string sqlStr = "";
        //    string s1 = "";

        //    #region 计算校验码
        //    CzkVerifyMark mark = new CzkVerifyMark();
        //    s1 = mark.VerifyMark(card.Hyzh, card.Total+card.Kkje);
        //    #endregion

        //    #region 保存校验码
        //    sqlStr = "update tCzkCardMark set VerifyMark='"+s1+"' where hykh='"+card.Hykh+"'";
        //    int i = 0;
        //    try
        //    {
        //         i = SqlEngine.ExecuteSql(sqlStr);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    #endregion

        //    if (i > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }

}
