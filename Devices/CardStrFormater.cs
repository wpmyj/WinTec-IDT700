using System;
using System.Collections.Generic;
using System.Text;
namespace Devices {
    public class CardStrFormater
    {
        /// <summary>
        /// 格式化卡号
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public static string FormateCardNo(string cardno)
        {
            int cardno_int = int.Parse(cardno);
            string str = Convert.ToString(cardno_int, 16).PadLeft(5, '0');
            str = "208" + str;
            str = HxL(str);
            return str + "0121203ff3030300020002000";
        }

        /// <summary>
        /// 读取卡号
        /// </summary>
        /// <param name="formateStr"></param>
        /// <returns></returns>
        public static string GetCardNo(string formateStr, int len)
        {
            string str = formateStr.Substring(0, 8);
            str = HxL(str).Substring(3, 5);

            #region 16进制转10进制
            str = Convert.ToInt32(str, 16).ToString().PadLeft(len, '0');
            #endregion
            return str;
        }

        /// <summary>
        /// 8位字符串高地位转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string HxL(string str)
        {
            #region 高地位转换
            string str2 = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                str2 = str2 + str.Substring(6 - 2 * i, 2);
            }
            #endregion

            return str2;
        }
        /// <summary>
        /// 格式化金额字符串
        /// </summary>
        /// <param name="mny"></param>
        /// <returns></returns>
        public static string FormateMoney(decimal mny)
        {
            string mnystr = mny.ToString();
            string[] mnystrs = mnystr.Split('.');
            if (mnystrs.Length == 2)
            {
                mnystr = mnystrs[0] + mnystrs[1].PadRight(2, '0');
            }
            else
            {
                mnystr = mnystrs[0] + "00";
            }
            Int64 i = Int64.Parse(mnystr);
            string str16 = HxL(Convert.ToString(i, 16).PadLeft(8, '0'));
            string str16f = HxL(Convert.ToString(~i, 16).PadLeft(8, '0'));
            return str16 + str16f + str16 + "05FA05FA";
        }

        /// <summary>
        /// 获取金额
        /// </summary>
        /// <param name="formateStr"></param>
        /// <returns></returns>
        public static decimal GetMoney(string formateStr)
        {
            string mnyStr = HxL(formateStr.Substring(0, 8));
            string mny100 = Convert.ToInt64(mnyStr, 16).ToString();
            return decimal.Parse(mny100.Substring(0, mny100.Length - 2) + "." + mny100.Substring(mny100.Length - 2, 2));
        }
    }
}
