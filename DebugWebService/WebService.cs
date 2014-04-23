using System;

using System.Collections.Generic;
using System.Text;
using DebugWebService.localhost;
using ZipSerialClass;

namespace DebugWebService
{
    public  class WebService
    {
       // private  SumPosWebService service = new SumPosWebService();
        private WebReference.SumPosWebService service = new DebugWebService.WebReference.SumPosWebService();
        public WebService(string url)
        {
            service.Url = url;
        }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public  string Url
        {
            get
            {
                return service.Url;
            }
         set
            {
                service.Url = value;
            }
        }


        /*****************************储值卡***********************************/
        /// <summary>
        /// 读取储值卡
        /// </summary>
        /// <param name="hyzh">会员帐号</param>
        /// <returns></returns>
        public  Model.CzCard loadCzCardByZh(string hyzh)
        {
            byte[] buff=service.getCzCardByZh(hyzh);

            Model.CzCard czCard =(Model.CzCard)ZipSerialClass.ZipSerialClass.SerialClass.DeSerial(buff);

            return czCard;
        }

        public  Model.CzCard loadCzkCardByJm(string hyzhjm)
        {
            byte[] buff=service.getCzCardByEnCryStr(hyzhjm);

            Model.CzCard czCard =(Model.CzCard)ZipSerialClass.ZipSerialClass.SerialClass.DeSerial(buff);

            return czCard;
        }
        /// <summary>
        /// 读取储值卡
        /// </summary>
        /// <param name="hykh">卡号</param>
        /// <returns></returns>
        public  Model.CzCard loadCzCardByKh(string hykh)
        {
            byte[] buff = service.getCzCardByKh(hykh);

            Model.CzCard czCard = (Model.CzCard)ZipSerialClass.ZipSerialClass.SerialClass.DeSerial(buff);

            return czCard;
        }

        /***************************结算*****************************************/
        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="tradeFlow">交易总表</param>
        /// <param name="payFlow">付款流水</param>
        /// <returns></returns>
        public  string Pay(Model.TradeFlow tradeFlow,Model.PayFlow payFlow)
        {
            string ok =string.Empty;//0 刷卡成功 流水上传失败 1 刷卡成功，流水上传成功
            #region 流水序列化
            byte[] tradFlowBuff = ZipSerialClass.ZipSerialClass.SerialClass.Serial(tradeFlow);
            byte[] payFlowBuff = ZipSerialClass.ZipSerialClass.SerialClass.Serial(payFlow);
            #endregion

            ok = service.savePay(tradFlowBuff, payFlowBuff);

            return ok;
        }
        /****************************同步数据*****************************************/
        /// <summary>
        /// 同步用户
        /// </summary>
        /// <returns></returns>
        public List<Model.User> syncUser()
        {
            byte[] buff=service.syncUser();
            List<Model.User> list = (List<Model.User>)ZipSerialClass.ZipSerialClass.SerialClass.DeSerial(buff);
            return list;
        }

        /// <summary>
        /// 同步流水
        /// </summary>
        /// <param name="tradeFlowList"></param>
        /// <param name="payFlowList"></param>
        /// <returns></returns>
        public bool syncFlow(List<Model.TradeFlow> tradeFlowList, List<Model.PayFlow> payFlowList)
        {
            #region 流水序列化
            byte[] tradeFlowBuff = ZipSerialClass.ZipSerialClass.SerialClass.Serial(tradeFlowList);
            byte[] payFlowBuff = ZipSerialClass.ZipSerialClass.SerialClass.Serial(payFlowList);
            #endregion

            #region 同步流水
            bool ok = service.syncFlow(tradeFlowBuff, payFlowBuff);
            #endregion
            return ok;
        }
    }
}
