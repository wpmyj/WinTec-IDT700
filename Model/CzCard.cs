using System;

using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 储值卡
    /// </summary>
    public class CzCard
    {
        private string hykh;
        /// <summary>
        /// 储值卡号
        /// </summary>
        public string Hykh
        {
            get { return hykh; }
            set { hykh = value; }
        }



        private string hyzh;
        /// <summary>
        /// 储值卡帐号
        /// </summary>
        public string Hyzh
        {
            get { return hyzh; }
            set { hyzh = value; }
        }



        private string custName;
        /// <summary>
        /// 姓名
        /// </summary>
        public string CustName
        {
            get { return custName; }
            set { custName = value; }
        }



        private float total;
        /// <summary>
        /// 余额
        /// </summary>
        public float Total
        {
            get { return total; }
            set { total = value; }
        }

        private float kkje;
        /// <summary>
        /// 空卡金额
        /// </summary>
        public float Kkje
        {
            get { return kkje; }
            set { kkje = value; }
        }

        private string psw;
        /// <summary>
        /// 密码
        /// </summary>
        public string Psw
        {
            get { return psw; }
            set { psw = value; }
        }



        private float lastxfje;
        /// <summary>
        /// 上次消费金额
        /// </summary>
        public float Lastxfje
        {
            get { return lastxfje; }
            set { lastxfje = value; }
        }


        private string stat;
        /// <summary>
        /// 储值卡状态
        /// </summary>
        public CzkStat Stat
        {
            get { return (CzkStat)int.Parse(stat); }
            set { stat = ((int)value).ToString(); }
        }

        private float jfBase;
        /// <summary>
        /// 积分基数
        /// </summary>
        public float JfBase
        {
            get { return jfBase; }
            set { jfBase = value; }
        }

        private float jfGrade;
        /// <summary>
        /// 当前
        /// </summary>
        public float JfGrade
        {
            get { return jfGrade; }
            set { jfGrade = value; }
        }

        private bool canVerifyMark;
        /// <summary>
        /// 验证通过
        /// </summary>
        public bool CanVerifyMark
        {
            get { return canVerifyMark; }
            set { canVerifyMark = value; }
        }
    }
}
