using System;

using System.Collections.Generic;
using System.Text;
using SumPos.SumSerivice;
using ComClass;
namespace SumPos
{
    public  class WebService
    {
        private static Service1 service = new Service1();

        /// <summary>
        /// webservice url
        /// </summary>
        public static string Url
        {
            set
            {
                service.Url = value;
            }
        }

        //public WebService(string Url)
        //{
        //    service.Url = Url;
        //}

        /*****************************储值卡***********************************/
        /// <summary>
        /// 读取储值卡
        /// </summary>
        /// <param name="hyzh">会员帐号</param>
        /// <returns></returns>
        public static Model.CzCard loadCzCardByZh(string hyzh)
        {
            byte[] buff=service.getCzCardByZh(hyzh);

            Model.CzCard czCard =(Model.CzCard)SerialClass.DeSerial(buff);
            return czCard;
        }


        /// <summary>
        /// 读取储值卡
        /// </summary>
        /// <param name="hykh">卡号</param>
        /// <returns></returns>
        public static Model.CzCard loadCzCardByKh(string hykh)
        {

            byte[] buff = service.getCzCardByKh(hykh);

            Model.CzCard czCard = (Model.CzCard)SerialClass.DeSerial(buff);

            return czCard;
        }


        /// <summary>
        /// 读取储值卡
        /// </summary>
        /// <param name="enCryStr">密文</param>
        /// <returns></returns>
        public static Model.CzCard loadCzCardByEnCryStr(string enCryStr)
        {
            byte[] buff = service.getCzCardByEnCryStr(enCryStr);
            Model.CzCard czCard = (Model.CzCard)SerialClass.DeSerial(buff);
            return czCard;
        }

        ///// <summary>
        ///// 充值
        ///// </summary>
        ///// <param name="czFlow"></param>
        ///// <returns></returns>
        //public static Model.CzCardChZhRst czkChZh(Model.CzCardChZhFlow czFlow)
        //{
        //    byte[] buff = ZipSerialClass.ZipSerialClass.SerialClass.Serial(czFlow);
        //    buff = service.czkChzh(buff);
        //    Model.CzCardChZhRst rst = (Model.CzCardChZhRst)ZipSerialClass.ZipSerialClass.SerialClass.DeSerial(buff);
        //    return rst;
        //}

        /***************************结算*****************************************/
        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="tradeFlow">交易总表</param>
        /// <param name="payFlow">付款流水</param>
        /// <returns></returns>
        public static bool Pay(List<Model.SaleFlow> saleFlowList,List<Model.PayFlow> payFlowList)
        {
            bool ok = false;
            #region 流水序列化
            byte[] saleFlowListBuff = SerialClass.Serial(saleFlowList);
            byte[] payFlowListBuff = SerialClass.Serial(payFlowList);
            #endregion

            ok = service.savePay(saleFlowListBuff, payFlowListBuff);

            return ok;
        }


        /****************************同步数据*****************************************/
        /// <summary>
        /// 同步用户
        /// </summary>
        /// <returns></returns>
        public static List<Model.User> syncUser(string posno)
        {
            byte[] buff=service.syncUser(posno);
            List<Model.User> list = (List<Model.User>)SerialClass.DeSerial(buff);
            return list;
        }

        /// <summary>
        /// 同步流水
        /// </summary>
        /// <param name="tradeFlowList"></param>
        /// <param name="payFlowList"></param>
        /// <returns></returns>
        public static bool syncFlow(List<Model.SaleFlow> saleFlowList, List<Model.PayFlow> payFlowList)
        {

            #region 流水序列化
            byte[] saleFlowBuff = SerialClass.Serial(saleFlowList);
            byte[] payFlowBuff = SerialClass.Serial(payFlowList);
            #endregion

            #region 同步流水
            bool ok = service.syncFlow(saleFlowBuff, payFlowBuff);
            #endregion
            return ok;
        }

        /// <summary>
        /// 同步系统参数
        /// </summary>
        /// <param name="posno"></param>
        /// <returns></returns>
        public static Model.Config syncCfg(string posno)
        {
            byte[] buff = service.syncCfg(posno);
            return (Model.Config)(SerialClass.DeSerial(buff));
        }

        /// <summary>
        /// 同步商品信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.Spxinxi> syncSpxinxi(string posNo)
        {
            byte[] buff = service.syncSpxinxi(posNo);
            List<Model.Spxinxi> list = (List<Model.Spxinxi>)SerialClass.DeSerial(buff);
            return list;
        }

        /***************************登陆********************************************/
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static Model.User userLogon(string userCode,string pwd)
        {
            Model.User user;
            byte[] buff = service.userLogon(userCode, pwd);
            user = (Model.User)SerialClass.DeSerial(buff);
            return user;
        }

        ///// <summary>
        ///// 用户注销
        ///// </summary>
        ///// <param name="posno"></param>
        ///// <returns></returns>
        //public static bool userLogoff(string posno)
        //{
        //    bool ok = false;
        //    ok=service.userLogoff(posno);
        //    return ok;
        //}


        /// <summary>
        /// 验证机器登记信息
        /// </summary>
        /// <param name="posNo"></param>
        /// <param name="sNo"></param>
        /// <returns></returns>
        public static bool chkInfo(string posNo, string sNo)
        {
            bool ok = service.ChkRight(posNo, sNo);
            return ok;
        }
    }
}
