using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Action;
using System.Configuration;
using ComClass;
namespace SumPosWebService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
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
                buffer = SerialClass.Serial(czk);
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
                buffer = SerialClass.Serial(czk);
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
            byte[] buffer = null;
            string hyzh = "";
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
                buffer = SerialClass.Serial(czk);
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
        public bool savePay(byte[] saleFlowListBuff, byte[] payFlowListBuff)
        {
            List<Model.SaleFlow> saleFlowList = (List<Model.SaleFlow>)SerialClass.DeSerial(saleFlowListBuff);
            List<Model.PayFlow> payFlowList = (List<Model.PayFlow>)SerialClass.DeSerial(payFlowListBuff);
            //tradeFlow.Sdate = DateTime.Now.ToString("yyyy-MM-dd");
            //payFlow.Sdate = tradeFlow.Sdate;
            bool ok = false;//失败
            try
            {
                //扣款成功 插入流水
                ok = new Action.SqlServer.SqlServerFlowAction(conStr).pay(saleFlowList, payFlowList);
            }
            catch (Exception ex)
            {
                ok = false;
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
        public byte[] syncUser(string grpno)
        {
            byte[] buff;
            List<Model.User> list = new List<Model.User>();
            try
            {
                list.AddRange(new Action.SqlServer.SqlServerUserAction(conStr).GetUserList(grpno));
                buff = SerialClass.Serial(list);
            }
            catch (Exception ex)
            {
                buff = null;
                ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
            }
            return buff;

        }

        /// <summary>
        /// 查询终端参数
        /// </summary>
        /// <param name="posno"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] syncCfg(string posno)
        {
            byte[] buff;
            try
            {
                buff =SerialClass.Serial(new Action.SqlServer.SqlServerConfigAction(conStr).syncCfg(posno));
            }
            catch (Exception ex)
            {
                buff = null;
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
        public bool syncFlow(byte[] saleFlowBuff, byte[] payFlowBuff)
        {
            bool ok = false;
            List<Model.SaleFlow> saleFlowList = (List<Model.SaleFlow>)SerialClass.DeSerial(saleFlowBuff);
            List<Model.PayFlow> payFlowList = (List<Model.PayFlow>)SerialClass.DeSerial(payFlowBuff);
            try
            {
                ok = new Action.SqlServer.SqlServerFlowAction(conStr).saveFlow(saleFlowList, payFlowList);
            }
            catch (Exception ex)
            {
                ok = false;
                ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
            }
            return ok;
        }


        /// <summary>
        /// 同步商品信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public byte[] syncSpxinxi(string grpno)
        {
            byte[] buff;
            List<Model.Spxinxi> list = new List<Model.Spxinxi>();
            try
            {
                list.AddRange(new Action.SqlServer.SqlServerSpxinxiAction(conStr).getSpxinxi(grpno));
                buff = SerialClass.Serial(list);
            }
            catch (Exception ex)
            {
                buff = null;
                ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
            }
            return buff;
        }


        /************************登陆*********************/

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="usercode">用户编码</param>
        /// <param name="pwd">用户密码</param>
        /// <returns></returns>
        [WebMethod]
        public byte[] userLogon(string usercode, string pwd)
        {
            byte[] buff = null;
            Model.User user;
            int i = 0;
            if (pwd != "")
            {
                pwd = Des.EncryStrHex(pwd, "0125" + usercode);
            }
            if (i != -1)
            {
                try
                {
                    user = new Action.SqlServer.SqlServerUserAction(conStr).UserLogon(usercode, pwd);
                    buff = SerialClass.Serial(user);
                }
                catch (Exception ex)
                {
                    buff = null;
                    ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
                }
            }
            return buff;
        }


        ///// <summary>
        ///// 充值
        ///// </summary>
        ///// <returns></returns>
        //[WebMethod]
        //public byte[] czkChzh(byte[] chzhFlowBuff)
        //{
        //    byte[] buff = null;
        //    Model.CzCardChZhFlow czflow;
        //    Model.CzCardChZhRst rst;
        //    try
        //    {
        //        czflow = (Model.CzCardChZhFlow)ZipSerialClass.ZipSerialClass.SerialClass.DeSerial(chzhFlowBuff);
        //        rst = new Action.SqlServer.SqlServerCzCarkAction(conStr).CzCardChZh(czflow);
        //        buff = ZipSerialClass.ZipSerialClass.SerialClass.Serial(rst);
        //    }
        //    catch (Exception ex)
        //    {
        //        buff = null;
        //        ActionWriteLog.writeErrorLog(ex.Source, ex.Message);
        //    }
        //    return buff;
        //}

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
                    Action.ActionWriteLog.writeErrorLog("前台验证失败", "pos机号-" + posNo + ",pos机序号-" + sNo);
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
