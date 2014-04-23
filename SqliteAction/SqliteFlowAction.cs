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
        /// 获取商品流水
        /// </summary>
        /// <param name="serialNo">流水号</param>
        /// <returns></returns>
        public List<Model.SaleFlow> getSaleFlow(string serialNo)
        {
            List<Model.SaleFlow> list = new List<Model.SaleFlow>();

            #region 组装sql语句
            string sqlStr = "select * from txsSaleFlow where  serial_no='" + serialNo + "'";
            #endregion
            try
            {
                #region 执行存储过程
                SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
                #endregion

                #region 组装商品流水
                while (reader.Read())
                {
                    Model.SaleFlow saleFlow = new Model.SaleFlow();
                    saleFlow.Operater = reader["operater"].ToString();
                    saleFlow.Saler = reader["saler"].ToString();
                    saleFlow.Sgroup = reader["sgroup"].ToString();
                    saleFlow.Serial_no = reader["serial_no"].ToString();
                    saleFlow.PosNo = reader["PosNo"].ToString();
                    saleFlow.Code = reader["code"].ToString();
                    saleFlow.FName = reader["fName"].ToString();
                    saleFlow.Price = float.Parse(reader["Price"].ToString());
                    saleFlow.Qty = int.Parse(reader["qty"].ToString());
                    saleFlow.Pre_total = float.Parse(reader["pre_total"].ToString());
                    saleFlow.Disc = int.Parse(reader["disc"].ToString());
                    saleFlow.Total = float.Parse(reader["total"].ToString());
                    saleFlow.Zdisc = int.Parse(reader["zdisc"].ToString());
                    saleFlow.Real_total = float.Parse(reader["real_total"].ToString());
                    saleFlow.Sa_date = reader["sa_date"].ToString();
                    saleFlow.Sa_time = reader["sa_time"].ToString();
                    saleFlow.Vip_no = reader["vip_no"].ToString();
                    saleFlow.vip_pmp = reader["vip_pmp"].ToString();
                    saleFlow.RightNo = reader["RightNo"].ToString();
                    saleFlow.RightFlag = reader["RightFlag"].ToString();
                    saleFlow.Squadno = reader["Squadno"].ToString();
                    saleFlow.flag=reader["flag"].ToString();
                    saleFlow.RowNo = int.Parse(reader["RowNo"].ToString());
                    saleFlow.saleReturnFlag = reader["isSaleReturn"].ToString();
                    list.Add(saleFlow);
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
        /// 保存结算结果
        /// </summary>
        /// <param name="saleFlowList"></param>
        /// <param name="payFlowList"></param>
        /// <returns></returns>
        public bool savePay(List<Model.SaleFlow> saleFlowList, List<Model.PayFlow> payFlowList,bool isSaleReturn)
        {
            bool ok = false;

                ArrayList sqlArrayList = new ArrayList();

                #region 组装sql语句

                #region 插入商品流水
                
                foreach (Model.SaleFlow saleFlow in saleFlowList)
                {
                    string sqlStr = "insert into txsSaleFlow "
                        + "(operater,saler,sgroup,serial_no,PosNo,code,fName,Price,"
                        + "qty,pre_total,disc,total,zdisc,real_total,sa_date,sa_time,"
                        + "vip_no,vip_pmp,RightNo,RightFlag,Squadno,RowNo,Flag,isSaleReturn) values("
                        + "'" + saleFlow.Operater + "',"
                        + "'" + saleFlow.Saler + "',"
                        + "'" + saleFlow.Sgroup + "',"
                        + "'" + saleFlow.Serial_no + "',"
                        + "'" + saleFlow.PosNo + "',"
                        + "'" + saleFlow.Code + "',"
                        + "'" + saleFlow.FName + "',"
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
                        + "'" + saleFlow.flag + "',"
                        +"'0')";

                    sqlArrayList.Add(sqlStr);
                }
                #endregion

                #region 插入付款流水
                
                foreach (Model.PayFlow payFlow in payFlowList)
                {
                    string sqlStr = "insert into txspayflow "
                        + "(operater,serial_no,total,paypmt,sa_date,sa_time,ref_no,squadno,PosNo,RowNo,ckic,flag) values("
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
                        + "'" + payFlow.ckic + "',"
                        + "'" + payFlow.flag + "')";

                    sqlArrayList.Add(sqlStr);
                }

                #endregion

                #region 更新退货状态
                if (isSaleReturn)
                {
                    string sqlStr = "update txsSaleFlow set isSaleReturn='1' where serial_no='"+saleFlowList[0].Serial_no+"'";
                    sqlArrayList.Add(sqlStr);
                }

                #endregion

                #region 更新流水号

                string sno = (int.Parse(saleFlowList[0].Serial_no.Substring(9, 4))+1).ToString().PadLeft(4,'0');

                string str = "update Config set serial_no='" + sno + "',LastDate='" + DateTime.Now.ToString("MMddyy") + "'";
                sqlArrayList.Add(str);
                #endregion

                #endregion

                try
                {
                    #region 执行sql语句
                    ok=SqliteEngine.ExecuteNonQueryWithTran(sqlArrayList);
                    #endregion
                }
                catch
                {
                    throw;
                    ok = false;
                }
            return ok;
        }

        /// <summary>
        /// 获取当前流水号
        /// </summary>
        /// <returns></returns>
        public string getSerialNo(string posNo)
        {
            string nowDate = DateTime.Now.ToString("MMddyy");
            string serial_no = "";
            #region 组装sql语句
            string sqlStr = "select Serial_no,LastDate from Config where LastDate='"
                + nowDate + "'";
            #endregion

            #region 执行sql语句
            SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
            #endregion

            #region 读取流水号
            while (reader.Read())
            {
                serial_no = nowDate + posNo + reader["Serial_no"].ToString().PadLeft(4,'0');
            }

            return serial_no;
            #endregion
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
        /// 查询未上传过的商品流水
        /// </summary>
        /// <returns></returns>
        public List<Model.SaleFlow> getSaleFlowNoUpLoad()
        {
            List<Model.SaleFlow> list = new List<Model.SaleFlow>();

            #region 组装sql语句
            string sqlStr = "select * from txsSaleFlow where flag='"+((int)Model.FlowUpLoadFlag.未上传).ToString()+"'";
            #endregion

            try
            {
                #region 执行sql语句
                SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlStr);
                #endregion

                while (reader.Read())
                {
                    #region 组装流水
                    Model.SaleFlow saleFlow = new Model.SaleFlow();
                    saleFlow.Operater = reader["operater"].ToString();
                    saleFlow.Saler = reader["saler"].ToString();
                    saleFlow.Sgroup = reader["sgroup"].ToString();
                    saleFlow.Serial_no = reader["serial_no"].ToString();
                    saleFlow.PosNo = reader["PosNo"].ToString();
                    saleFlow.Code = reader["code"].ToString();
                    saleFlow.FName = reader["fName"].ToString();
                    saleFlow.Price = float.Parse(reader["Price"].ToString());
                    saleFlow.Qty = int.Parse(reader["qty"].ToString());
                    saleFlow.Pre_total = float.Parse(reader["pre_total"].ToString());
                    saleFlow.Disc = int.Parse(reader["disc"].ToString());
                    saleFlow.Total = float.Parse(reader["total"].ToString());
                    saleFlow.Zdisc = int.Parse(reader["zdisc"].ToString());
                    saleFlow.Real_total = float.Parse(reader["real_total"].ToString());
                    saleFlow.Sa_date = reader["sa_date"].ToString();
                    saleFlow.Sa_time = reader["sa_time"].ToString();
                    saleFlow.Vip_no = reader["vip_no"].ToString();
                    saleFlow.vip_pmp = reader["vip_pmp"].ToString();
                    saleFlow.RightNo = reader["RightNo"].ToString();
                    saleFlow.RightFlag = reader["RightFlag"].ToString();
                    saleFlow.Squadno = reader["Squadno"].ToString();
                    saleFlow.RowNo = int.Parse(reader["RowNo"].ToString());
                    #endregion
                    list.Add(saleFlow);
                }
            }
            catch (Exception ex)
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
            string sqlStr = "select * from txspayflow where flag='" + ((int)Model.FlowUpLoadFlag.未上传).ToString() + "'";
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
                    payFlow.Serial_no = reader["serial_no"].ToString();
                    payFlow.Total=float.Parse(reader["total"].ToString());
                    payFlow.paypmt =reader["paypmt"].ToString();
                    payFlow.Sa_date = reader["sa_date"].ToString();
                    payFlow.Sa_time = reader["sa_time"].ToString();
                    payFlow.Ref_no = reader["ref_no"].ToString();
                    payFlow.Squadno = reader["squadno"].ToString();
                    payFlow.PosNo = reader["PosNo"].ToString();
                    payFlow.RowNo = int.Parse(reader["RowNo"].ToString());
                    payFlow.ckic = reader["ckic"].ToString();
                    #endregion

                    list.Add(payFlow);
                }
            }
            catch (Exception ex)
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

            sqlStrList.Add( "update txsSaleFlow set flag='1'");
            sqlStrList.Add("update txspayflow set flag='1'");

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
        /// 查询是否有未上传商品
        /// </summary>
        /// <returns></returns>
        public bool checkFlowUpload()
        {
            bool ok = false;
            string sqlstr = "select * from txspayflow where flag ='0'";
            try
            {
                SQLiteDataReader reader = SqliteEngine.ExecuteReader(sqlstr);
                if (reader.Read())
                {
                    ok = true;
                }
                reader.Close();
            }
            catch
            {
                throw;
            }
            return ok;
        }


        /// <summary>
        /// 清除指定日期前流水
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool clearFlowFrom(string date)
        {
            bool ok = false;
            ArrayList sqlList = new ArrayList();
            string sqlStr = "delete from txspayflow where sa_date <'"+date+"' and flag='0'";
            sqlList.Add(sqlStr);
            sqlStr = "delete from txsSaleFlow where sa_date<'"+date+"' and flag='0'";
            sqlList.Add(sqlStr);

            try
            {
                ok = SqliteEngine.ExecuteNonQueryWithTran(sqlList);
            }
            catch
            {
                ok = false;
            }

            return ok; ;
        }
    }
}
