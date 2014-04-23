using System.Collections.Generic;
using Model;

namespace PubGlobal
{
    /// <summary>
    /// 业务变量
    /// </summary>
    public class BussinessVar
    {
        /// <summary>
        /// 商品列表
        /// </summary>
        public static  MGoods[] goodsList;

        /// <summary>
        /// 当前储值卡
        /// </summary>
        public static MCzCard card;
        
        /// <summary>
        /// 当前流水号
        /// </summary>
        public static string SerialNo
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为退货
        /// </summary>
        public static bool isReturn;

        /// <summary>
        /// 商品流水
        /// </summary>
        public static List<MSaleFlow> saleFlowList=new List<MSaleFlow>();

        /// <summary>
        /// 合计
        /// </summary>
        public static decimal Total;

        /// <summary>
        /// 付款流水
        /// </summary>
        public static List<MPayFlow> payFlowList=new List<MPayFlow>();

        /// <summary>
        /// 报表流水数据源
        /// </summary>
        public static List<MSaleFlow> QuerySaleFlows = new List<MSaleFlow>();
    }
}
