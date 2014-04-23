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
        private string incardno;
        /// <summary>
        /// 内卡号
        /// </summary>
        public string Incardno
        {
            get { return incardno; }
            set { incardno = value; }
        }



        private string outcardno;
        /// <summary>
        /// 外卡号
        /// </summary>
        public string Outcardno
        {
            get { return outcardno; }
            set { outcardno = value; }
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



        public string stat;
        /// <summary>
        /// 状态
        /// </summary>
        public CzkStat Stat
        {
            get { return (CzkStat)int.Parse(stat); }
            set { stat = ((int)value).ToString(); }
        }



        private string czkType;
        /// <summary>
        /// 卡类型
        /// </summary>
        public CzCardType CzkType
        {
            get { return (CzCardType)int.Parse(czkType); }
            set { czkType = ((int)value).ToString(); }
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

        private bool canVerifyMark;
        /// <summary>
        /// 卡金额校验
        /// </summary>
        public bool CanVerifyMark
        {
            get { return canVerifyMark; }
            set { canVerifyMark = value; }
        }
    }
}
