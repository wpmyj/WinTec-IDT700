using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 充值结果
    /// </summary>
    public class CzCardChZhRst
    {
        private string sdate;
        /// <summary>
        /// 充值时间
        /// </summary>
        public string Sdate
        {
            get { return sdate; }
            set { sdate = value; }
        }

        private bool rst;
        /// <summary>
        /// 充值结果
        /// </summary>
        public bool Rst
        {
            get { return rst; }
            set { rst = value; }
        }

        private float czje;
        /// <summary>
        /// 本次充值金额
        /// </summary>
        public float Czje
        {
            get { return czje; }
            set { czje = value; }
        }

        private float scye;
        /// <summary>
        /// 充值前余额
        /// </summary>
        public float Scye
        {
            get { return scye; }
            set { scye = value; }
        }

        private float dqye;
        /// <summary>
        /// 当前余额
        /// </summary>
        public float Dqye
        {
            get { return dqye; }
            set { dqye = value; }
        }

        private string outCardno;
        /// <summary>
        /// 卡号
        /// </summary>
        public string OutCardno
        {
            get { return outCardno; }
            set { outCardno = value; }
        }

        private string pzno;
        /// <summary>
        /// 充值流水号
        /// </summary>
        public string Pzno
        {
            get { return pzno; }
            set { pzno = value; }
        }
    }
}
