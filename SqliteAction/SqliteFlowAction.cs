using System;

using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Collections;
namespace Action.Sqlite
{
    /// <summary>
    /// 流水操作类
    /// </summary>
    public class SqliteFlowAction:SqliteBaseAction
    {
        public SqliteFlowAction(string connStr)
            : base(connStr)
        {

        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="flow_no"></param>
        /// <returns></returns>
        public bool payflowTh(string flow_no)
        {
            bool ok=false;
            #region 组装sql
            string sqlStr = "update pos_PayFlow set tradetype='B' where flow_no='"+flow_no+"'";
            #endregion

            #region 执行
            try
            {
                SqliteEngine.ExecuteNonQuery(sqlStr);
                ok = true;
            }
            catch
            {
                throw;
            }
            #endregion
            return ok;
        }

        /// <summary>
        /// 查询流水
        /// </summary>
        /// <param name="flowNo"></param>
        /// <returns></returns>
        public Model.PayFlow getPayFlow(string flow_no)
        {
            //List<Model.PayFlow> list = new List<Model.PayFlow>();
            #region 组装sql语句
            string sqlStr = "select * from pos_PayFlow where flow_no='" + flow_no + "'";
            #endregion

            #region 执行sql语句
            SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
            #endregion

            #region 组装流水
            Model.PayFlow payflow = new Model.PayFlow();
            while (reader.Read())
            {
                payflow.Flow_no = reader["flow_no"].ToString();
                payflow.Cardno = reader["cardno"].ToString();
                payflow.Hykh = reader["hykh"].ToString();
                payflow.tradetype = reader["tradetype"].ToString();
                payflow.Total = float.Parse(reader["total"].ToString());
                //list.Add(payflow);
            }
            reader.Close();
            #endregion
            return payflow;
        }

        /// <summary>
        /// 查询交易总表
        /// </summary>
        /// <param name="flow_no"></param>
        /// <returns></returns>
        public Model.TradeFlow getTradeFlow(string flow_no)
        {
            #region 组装sql语句
            string sqlStr = "select * from pos_TradeFlow where flow_no='" + flow_no + "'";
            #endregion

            #region 执行sql语句
            SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
            #endregion

            #region 组装流水
            Model.TradeFlow tradeFlow = new Model.TradeFlow();
            while (reader.Read())
            {
                tradeFlow.Total = float.Parse(reader["total"].ToString());
                tradeFlow.tradeType = reader["tradetype"].ToString();
                tradeFlow.CustCode = reader["CustCode"].ToString();
                tradeFlow.Flow_no = reader["flow_no"].ToString();
                //list.Add(payflow);
            }
            reader.Close();
            #endregion
            return tradeFlow;
        }

        /// <summary>
        /// 查询流水
        /// </summary>
        /// <param name="btime">开始时间</param>
        /// <param name="etime">结束时间</param>
        /// <returns></returns>
        public List<Model.PayFlow> getPayFlow(DateTime btime, DateTime etime)
        {
            List<Model.PayFlow> list = new List<Model.PayFlow>();
            #region 组装sql语句
            string sqlStr = "select * from pos_PayFlow where sdate between '" +btime.ToString("yyyy-MM-dd") + "' and '"+etime.ToString("yyyy-MM-dd")+"'";
            #endregion

            #region 执行sql语句
            SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
            #endregion

            #region 组装流水
            while (reader.Read())
            {
                Model.PayFlow payflow = new Model.PayFlow();
                payflow.Flow_no = reader["flow_no"].ToString();
                payflow.Cardno = reader["cardno"].ToString();
                payflow.Hykh = reader["hykh"].ToString();
                payflow.Total = float.Parse(reader["total"].ToString());
                payflow.Sdate = reader["sdate"].ToString();
                payflow.Stime = reader["stime"].ToString();
                list.Add(payflow);
            }
            #endregion
            return list;
        }

        /// <summary>
        /// 保存结算结果
        /// </summary>
        /// <param name="saleFlowList"></param>
        /// <param name="payFlowList"></param>
        /// <returns></returns>
        public bool savePay(Model.TradeFlow tradeFlow,Model.PayFlow payFlow)
        {
            bool ok = false;

                //刷卡成功，生成流水

                ArrayList sqlArrayList = new ArrayList();
                string sqlStr = string.Empty;
                #region 组装sql语句

                #region 插入交易总表流水
                sqlStr = "INSERT INTO pos_TradeFlow ([flow_no],[posno],[sdate],[stime],[operater],[squadno],[tradetype],"
               + " [qty],[total],[zkje],[payje]"
               + ",[Change],[CustCode],[CustFlag],[RightNo],[bcjf],[scjf],[flag]) values("
               + "'" + tradeFlow.Flow_no + "',"
               + "'" + tradeFlow.Posno + "',"
               + "'" + tradeFlow.Sdate + "',"
               + "'" + tradeFlow.Stime + "',"
               + "'" + tradeFlow.Operater + "',"
               + "'" + tradeFlow.Squadno + "',"
               + "'" + tradeFlow.tradeType + "',"
               + tradeFlow.Qty.ToString() + ','
               + tradeFlow.Total.ToString("F2") + ","
               + tradeFlow.Zkje.ToString("F2") + ","
               + tradeFlow.Payje.ToString("F2") + ","
               + tradeFlow.Change.ToString("F2") + ","
               + "'" + tradeFlow.CustCode + "',"
               + "'" + tradeFlow.CustFlag + "',"
               + "'" + tradeFlow.RightNo + "',"
               + tradeFlow.Bcjf + ","
               + tradeFlow.Scjf+","
               +"'"+tradeFlow.flag+"'"
               + ")";
                sqlArrayList.Add(sqlStr);
                #endregion

                #region 插入付款流水
                sqlStr = "insert into pos_PayFlow ([flow_no],[flow_id],[posno],[sdate],[stime],[operater],[squadno],"
                + "[tradetype],[paypmt],[PayAmount],[total] ,[cardno],[hykh],[scye],[ckic],[flag]) values( "
                + "'" + payFlow.Flow_no + "',"
                + payFlow.Flow_id.ToString() + ","
                + "'" + payFlow.Posno + "',"
                + "'" + payFlow.Sdate + "',"
                + "'" + payFlow.Stime + "',"
                + "'" + payFlow.Operater + "',"
                + "'" + payFlow.Squadno + "',"
                + "'" + payFlow.tradetype + "',"
                + "'" + payFlow.paypmt + "',"
                + payFlow.PayAmount.ToString("f2") + ","
                + payFlow.Total.ToString("f2") + ","
                + "'" + payFlow.Cardno + "',"
                +"'"+payFlow.Hykh+"',"
                + payFlow.Scye.ToString("f2") + ","
                + "'" + payFlow.ckic+"',"
                +"'"+payFlow.flag+"'"
                + ")";

                sqlArrayList.Add(sqlStr);

                #endregion

                #region 更新流水号

                string sno = (int.Parse(tradeFlow.Flow_no.Substring(9, 4))+1).ToString();

                int i = 4 - sno.Length;
                for (int a = 1; a <= i; a++)
                {
                    sno = "0" + sno;
                }

                string str = "update Config set flow_no='" + sno + "',LastDate='" + DateTime.Now.ToString("MMddyy") + "'";
                sqlArrayList.Add(str);
                #endregion

                #endregion

                try
                {
                    #region 执行sql语句
                    SqliteEngine.ExecuteNonQueryWithTran(sqlArrayList);
                    ok = true;
                    #endregion
                }
                catch
                {
                    throw;
                }
            return ok;
        }




        /// <summary>
        /// 获取当前流水号
        /// </summary>
        /// <returns></returns>
        public string getFlow_no(string posNo)
        {
            string nowDate = DateTime.Now.ToString("MMddyy");
            string flow_no = string.Empty;
            #region 组装sql语句
            string sqlStr = "select flow_no,LastDate from Config where LastDate='"
                + nowDate + "'";
            #endregion

            #region 执行sql语句
            SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
            #endregion

            #region 读取流水号
            while (reader.Read())
            {
                flow_no = nowDate + posNo + reader["flow_no"].ToString();
            }

            if (flow_no == string.Empty)
            {
                flow_no = nowDate + posNo + "0001";
            }
            return flow_no;
            #endregion
        }

        /// <summary>
        /// 保存流水号
        /// </summary>
        /// <param name="Flow_no"></param>
        /// <returns></returns>
        public bool saveFlow_no(string Flow_no)
        {
            bool ok = false;
            int i=0;
            #region 组装sql
            string sqlstr = "update Config set flow_no='"+Flow_no+"'";            
            #endregion
            try
            {
                i=SqliteEngine.ExecuteNonQuery(sqlstr);
                if (i > 0)
                {
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ok;
        }



        /// <summary>
        /// 查询销售汇总报表
        /// </summary>
        /// <param name="bDate">开始日期</param>
        /// <param name="eDate">结束日期</param>
        /// <returns></returns>
        public List<Model.SaleHzRpt> listSaleHzRpt(string bDate, string eDate)
        {
            List<Model.SaleHzRpt> list = new List<Model.SaleHzRpt>();
            #region 组装sql语句
            string sqlStr = "select code,fName,price,sum(qty) as qty,sum(real_Total) as total from txsSaleFlow "
                + "where sa_date between '"+bDate+"' and '"+eDate+"' " 
                + "group by code,fName,Price ";
            #endregion

            try
            {

                #region 执行sql语句
                SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
                #endregion

                #region 组装SaleHzRpt
                while (reader.Read())
                {
                    Model.SaleHzRpt saleHzRpt = new Model.SaleHzRpt();
                    saleHzRpt.InCode = reader["code"].ToString();
                    saleHzRpt.FName = reader["fName"].ToString();
                    saleHzRpt.Price = float.Parse(reader["Price"].ToString());
                    saleHzRpt.Qty = int.Parse(reader["qty"].ToString());
                    saleHzRpt.Total = float.Parse(reader["total"].ToString());
                    list.Add(saleHzRpt);
                }
                #endregion
            }
            catch
            {
                list.Clear();
                throw;
            }
            return list;
        }






        /// <summary>
        /// 查询未上传过的付款流水
        /// </summary>
        /// <returns></returns>
        public List<Model.PayFlow> getPayFlowNoUpload()
        {
            List<Model.PayFlow> list = new List<Model.PayFlow>();

            #region 组装sql语句
            string sqlStr = "select * from pos_PayFlow where flag=" + ((int)Model.FlowUpLoadFlag.未上传).ToString() ;
            #endregion

            try
            {
                #region 执行sql语句
                SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
                #endregion

                while (reader.Read())
                {

                    #region 组装付款流水
                    Model.PayFlow payFlow = new Model.PayFlow();
                    payFlow.Operater = reader["operater"].ToString();
                    payFlow.Flow_no = reader["serial_no"].ToString();
                    payFlow.Total=float.Parse(reader["total"].ToString());
                    payFlow.Paypmt = (Model.FlowPayType)int.Parse(reader["paypmt"].ToString());
                    payFlow.Sdate = reader["sa_date"].ToString();
                    payFlow.Stime = reader["sa_time"].ToString();
                    payFlow.Cardno = reader["ref_no"].ToString();
                    payFlow.Hykh = reader["hykh"].ToString();
                    payFlow.Squadno = reader["squadno"].ToString();
                    payFlow.Posno = reader["PosNo"].ToString();
                    payFlow.Flow_id = int.Parse(reader["RowNo"].ToString());
                    payFlow.Ckic = (Model.CzCardType)int.Parse(reader["ckic"].ToString());
                    payFlow.Flag = (Model.FlowUpLoadFlag)(int)reader["flag"];
                    #endregion

                    list.Add(payFlow);
                }
            }
            catch 
            {
                list.Clear();
                throw;
            }
            return list;
        }



        /// <summary>
        /// 查询未上传过的交易总表流水
        /// </summary>
        /// <returns></returns>
        public List<Model.TradeFlow> getTradeFlowNoUpload()
        {
            List<Model.TradeFlow> list = new List<Model.TradeFlow>();
            #region 组装sql
            string sqlStr = "select * from pos_TradeFlow where flag="+((int)Model.FlowUpLoadFlag.未上传).ToString();
            #endregion
            try
            {
                #region 执行sql
                SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
                #endregion
                while (reader.Read())
                {
                    Model.TradeFlow tradeFlow = new Model.TradeFlow();
                    tradeFlow.Bcjf = (float)reader["bcjf"];
                    tradeFlow.Change=(float)reader["Change"];
                    tradeFlow.CustCode = reader["CustCode"].ToString();
                    tradeFlow.CustFlag = reader["CustFlag"].ToString();
                    tradeFlow.flag = reader["flag"].ToString();
                    tradeFlow.Flow_no = reader["flow_no"].ToString();
                    tradeFlow.Operater = reader["operater"].ToString();
                    tradeFlow.Payje = (float)reader["payje"];
                    tradeFlow.Posno = reader["posno"].ToString();
                    tradeFlow.Qty = (int)reader["qty"];
                    tradeFlow.RightNo = reader["RightNo"].ToString();
                    tradeFlow.Scjf = (float)reader["scjf"];
                    tradeFlow.Sdate = reader["sdate"].ToString();
                    tradeFlow.Squadno = reader["squadno"].ToString();
                    tradeFlow.Stime = reader["stime"].ToString();
                    tradeFlow.Total = (float)reader["total"];
                    tradeFlow.tradeType = reader["tradetype"].ToString();
                    tradeFlow.Zkje = (float)reader["zkje"];
                    list.Add(tradeFlow);
                }
            }
            catch 
            {
                list.Clear();
                throw;
            }
            return list;
        }


        /// <summary>
        /// 上传完毕后，更新流水flag
        /// </summary>
        /// <returns></returns>
        public bool updateFlowFlag()
        {
            bool ok;
            ArrayList sqlStrList=new ArrayList();

            #region 组装sql语句

            sqlStrList.Add( "update pos_TradeFlow set flag='1'");
            sqlStrList.Add("update pos_PayFlow set flag='1'");

            #endregion

            try
            {
                #region 执行sql语句
                ok = SqliteEngine.ExecuteNonQueryWithTran(sqlStrList);
                #endregion
            }
            catch (Exception ex)
            {
                ok = false;
                throw;
            }
            return ok;
        }


        /// <summary>
        /// 保存充值流水
        /// </summary>
        /// <param name="rst"></param>
        /// <returns></returns>
        public bool saveChZhFlow(Model.CzCardChZhRst rst,Model.User user)
        {
            bool ok=false;
            int i;
            #region 组装sql语句
            string sqlstr = "insert into pos_ChZhFlow(Pzno,OutCardno,scye,cztotal,OperDate,OperCode,OperPep)"
                +"select '"+rst.Pzno+"','"+rst.OutCardno+"',"+rst.Scye+","+rst.Czje+",'"+rst.Sdate+"','"+user.UserCode+"','"+user.UserName+"'";
            #endregion

            try
            {
                i = SqliteEngine.ExecuteNonQuery(sqlstr);
                if (i < 1)
                {
                    throw new Exception("插入失败！");
                }
                else
                {
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ok;
        }

        /// <summary>
        /// 查询充值流水
        /// </summary>
        /// <param name="sdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns></returns>
        public List<Model.CzCardChZhRst> listChZhFlow(DateTime sDate,DateTime eDate)
        {
            List<Model.CzCardChZhRst> list = new List<Model.CzCardChZhRst>();
            #region 组装sql语句
            string sqlStr = "select Pzno,OutCardno,cztotal,OperDate from pos_ChZhFlow "
                + "where OperDate between '" + sDate.ToString("yyyy-MM-dd") + "' and '" + eDate.AddDays(1).ToString("yyyy-MM-dd") + "' ";
            #endregion

            #region 执行sql语句
            SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
            #endregion

            #region 组装流水
            while (reader.Read())
            {
                Model.CzCardChZhRst czflow = new Model.CzCardChZhRst();
                czflow.Pzno=reader["Pzno"].ToString();
                czflow.OutCardno = reader["OutCardno"].ToString();
                czflow.Sdate=reader["OperDate"].ToString();
                czflow.Czje = float.Parse(reader["cztotal"].ToString());
                list.Add(czflow);
            }
            reader.Close();
            #endregion
            return list;
        }
    }
}
