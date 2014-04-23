using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using ZipSerialClass;
using Action;
using System.Configuration;

namespace SumPosWebService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class SumPosWebService : System.Web.Services.WebService
    {
        string conStr = ConfigurationManager.ConnectionStrings["DatabaseStr"].ConnectionString;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /*****************储值卡***********************/
        /// <summary>
        /// 获取储值卡
        /// </summary>
        /// <param name="hyzh">帐号</param>
        /// <returns></returns>
        [WebMethod]
        public byte[] getCzCardByZh(string hyzh)
        {
            byte[] buffer;
            try
            {
                Model.CzCard czk = new Action.SqlServer.SqlServerCzCarkAction(conStr).getCzCardByZh(hyzh);
                buffer = ZipSerialClass.ZipSerialClass.SerialClass.Serial(czk);
            }
            catch (Exception ex)
            {
                buffer = null;
                Action.ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
            }
            return buffer;
        }


        /// <summary>
        /// 获取储值卡
        /// </summary>
        /// <param name="hykh">卡号</param>
        /// <returns></returns>
        [WebMethod]
        public byte[] getCzCardByKh(string hykh)
        {
            byte[] buffer;
            try
            {
                Model.CzCard czk = new Action.SqlServer.SqlServerCzCarkAction(conStr).getCzCardByKh(hykh);
                buffer = ZipSerialClass.ZipSerialClass.SerialClass.Serial(czk);
            }
            catch (Exception ex)
            {
                buffer = null;
                Action.ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
            }
            return buffer;
        }

        /// <summary>
        /// 获取储值卡
        /// </summary>
        /// <param name="enCryStr">秘文</param>
        /// <returns></returns>
        [WebMethod]
        public byte[] getCzCardByEnCryStr(string enCryStr)
        {
            byte[] buffer=null;
            string hyzh="";
            Model.CzCard czk = new Model.CzCard();
            #region 解析账号
            if (enCryStr.StartsWith(";"))
            {
                enCryStr = enCryStr.Substring(1, enCryStr.Length - 2);
            }
            hyzh = DllPubFile.CzkDecryStr(enCryStr);
            #endregion
            //if (i != -1)
           // {
                try
                {
                    czk = new Action.SqlServer.SqlServerCzCarkAction(conStr).getCzCardByZh(hyzh);
                    buffer = ZipSerialClass.ZipSerialClass.SerialClass.Serial(czk);
                }
                catch (Exception ex)
                {
                    Action.ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
                }
         //   }
            return buffer;
        }


        /******************结算*************************/
        /// <summary>
        /// 保存结算
        /// </summary>
        /// <param name="payflow"></param>
        /// <returns></returns>
        [WebMethod]
        public string savePay(byte[] tradeFlowBuff,byte[] payFlowBuff)
        {
            Model.TradeFlow tradeFlow =(Model.TradeFlow) ZipSerialClass.ZipSerialClass.SerialClass.DeSerial(tradeFlowBuff);
            Model.PayFlow payFlow = (Model.PayFlow)ZipSerialClass.ZipSerialClass.SerialClass.DeSerial(payFlowBuff);
            tradeFlow.Sdate = DateTime.Now.ToString("yyyy-MM-dd");
            payFlow.Sdate = tradeFlow.Sdate;
            string ok = "error";//失败
            try
            {
                    //扣款成功 插入流水
                    if (new Action.SqlServer.SqlServerFlowAction(conStr).pay(tradeFlow, payFlow))
                    {
                        ok = "1";
                    }
                    else
                    {
                        ok = "0";
                    }
            }
            catch (Exception ex)
            {
                ok = "error";
                ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
            }
            return ok;
        }


        /*********************数据同步****************************/
        /// <summary>
        /// 同步用户
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public byte[] syncUser()
        {
            byte[] buff;
            List<Model.User> list = new List<Model.User>();
            try
            {
                list.AddRange( new Action.SqlServer.SqlServerUserAction(conStr).GetUserList());
                buff = ZipSerialClass.ZipSerialClass.SerialClass.Serial(list);
            }
            catch (Exception ex)
            {
                buff=null;
                ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
            }
            return buff;

        }


        /// <summary>
        /// 同步流水
        /// </summary>
        /// <param name="tradeFlowBuff"></param>
        /// <param name="payFlowBuff"></param>
        /// <returns></returns>
        [WebMethod]
        public bool syncFlow(byte[] tradeFlowBuff, byte[] payFlowBuff)
        {
            bool ok = false;
            List<Model.TradeFlow> tradeFlowList=(List<Model.TradeFlow>)ZipSerialClass.ZipSerialClass.SerialClass.DeSerial(tradeFlowBuff);
            List<Model.PayFlow> payFlowList=(List<Model.PayFlow>)ZipSerialClass.ZipSerialClass.SerialClass.DeSerial(payFlowBuff);
            try
            {
                ok = new Action.SqlServer.SqlServerFlowAction(conStr).saveFlow(tradeFlowList, payFlowList);
            }
            catch (Exception ex)
            {
                ok = false;
                ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
            }
            return ok;   
        }


        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="usercode">用户编码</param>
        /// <param name="pwd">用户密码</param>
        /// <returns></returns>
        [WebMethod]
        public byte[] userLogon(string usercode, string pwd,string posNo)
        {
            byte[] buff=null;
            Model.User user;

                try
                {
                    user = new Action.SqlServer.SqlServerUserAction(conStr).UserLogon(usercode, pwd,posNo);
                    buff = ZipSerialClass.ZipSerialClass.SerialClass.Serial(user);
                }
                catch (Exception ex)
                {
                    buff = null;
                    ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
                }
            
            return buff;
        }


        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="posNo"></param>
        /// <returns></returns>
        [WebMethod]
        public bool userLogoff( string posNo)
        {
            bool ok = false;
            try
            {
                ok = new Action.SqlServer.SqlServerUserAction(conStr).UserLogoff( posNo);
            }
            catch (Exception ex)
            {
                ok = false;
                ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
            }
            return ok;
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public byte[] czkChzh(byte[] chzhFlowBuff)
        {
            byte[] buff = null;
            Model.CzCardChZhFlow czflow;
            Model.CzCardChZhRst rst;
            try
            {
                czflow = (Model.CzCardChZhFlow)ZipSerialClass.ZipSerialClass.SerialClass.DeSerial(chzhFlowBuff);
                rst = new Action.SqlServer.SqlServerCzCarkAction(conStr).CzCardChZh(czflow);
                buff = ZipSerialClass.ZipSerialClass.SerialClass.Serial(rst);
            }
            catch (Exception ex)
            {
                buff = null;
                ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
            }
            return buff;
        }

        /// <summary>
        /// 检查站点是否注册
        /// </summary>
        /// <param name="posNo"></param>
        /// <param name="sNo"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ChkRight(string posNo, string sNo)
        {
            bool ok = false;
            try
            {
                ok = new Action.SqlServer.SqlServerConfigAction(conStr).ChkRight(posNo, sNo);
                if (ok == false)
                {
                    Action.ActionWriteLog.writeErrorLog("前台登记验证","pos机号-"+posNo+",pos机序号-"+sNo);
                }
            }
            catch (Exception ex)
            {

                Action.ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
            }
            return ok;
        }
    }
}
