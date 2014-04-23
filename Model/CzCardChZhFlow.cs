using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 充值流水
    /// </summary>
    public class CzCardChZhFlow
    {
        private string outcardNo;
        /// <summary>
        /// 卡号
        /// </summary>
        public string OutcardNo
        {
            get { return outcardNo; }
            set { outcardNo = value; }
        }

        private string incardNo;
        /// <summary>
        /// 内卡号
        /// </summary>
        public string IncardNo
        {
            get { return incardNo; }
            set { incardNo = value; }
        }

        private float bcye;
        /// <summary>
        /// 本次余额
        /// </summary>
        public float Bcye
        {
            get { return bcye; }
            set { bcye = value; }
        }


        private float czje;
        /// <summary>
        /// 充值金额
        /// </summary>
        public float Czje
        {
            get { return czje; }
            set { czje = value; }
        }

        private string userCode;
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string UserCode
        {
            get { return userCode; }
            set { userCode = value; }
        }

        private string userName;
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string posNo;
        /// <summary>
        /// 收款机号
        /// </summary>
        public string PosNo
        {
            get { return posNo; }
            set { posNo = value; }
        }

        private string sdate;
        /// <summary>
        /// 时间
        /// </summary>
        public string Sdate
        {
            get { return sdate; }
            set { sdate = value; }
        }

    }
}
