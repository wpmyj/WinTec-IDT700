using System;

using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public class Spxinxi
    {
        private string incode;
        /// <summary>
        /// 商品编码
        /// </summary>
        public string Incode
        {
            get { return incode; }
            set { incode = value; }
        }

        private string unit;
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }


        private string barCode;
        /// <summary>
        /// 商品条码
        /// </summary>
        public string BarCode
        {
            get { return barCode; }
            set { barCode = value; }
        }



        private string fName;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string FName
        {
            get { return fName; }
            set { fName = value; }
        }



        private string signchar;
        /// <summary>
        /// 助记符
        /// </summary>
        public string Signchar
        {
            get { return signchar; }
            set { signchar = value; }
        }



        private int disc;
        /// <summary>
        /// 折扣
        /// </summary>
        public int Disc
        {
            get { return disc; }
            set { disc = value; }
        }



        private string specs;
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs
        {
            get { return specs; }
            set { specs = value; }
        }



        private float price;
        /// <summary>
        /// 价格
        /// </summary>
        public float Price
        {
            get { return price; }
            set { price = value; }
        }



        private float hyPrc;
        /// <summary>
        /// 会员价
        /// </summary>
        public float HyPrc
        {
            get { return hyPrc; }
            set { hyPrc = value; }
        }



        private string custno;
        /// <summary>
        /// 供应商号
        /// </summary>
        public string Custno
        {
            get { return custno; }
            set { custno = value; }
        }



        private string stype;
        /// <summary>
        /// 商品类别
        /// </summary>
        public string Stype
        {
            get { return stype; }
            set { stype = value; }
        }




        private float promPrc;
        /// <summary>
        /// 促销价
        /// </summary>
        public float PromPrc
        {
            get { return promPrc; }
            set { promPrc = value; }
        }




        public string status;
        /// <summary>
        /// 商品促销状态
        /// true 促销 false 正常
        /// </summary>
        public bool Status
        {
            get
            {
                if (status == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }




        private string grpno;
        /// <summary>
        /// 部门
        /// </summary>
        public string Grpno
        {
            get { return grpno; }
            set { grpno = value; }
        }



        private int maxDisc;
        /// <summary>
        /// 最大折扣
        /// </summary>
        public int MaxDisc
        {
            get { return maxDisc; }
            set { maxDisc = value; }
        }



        public string hyFlag;
        /// <summary>
        /// 会员商品标志
        /// </summary>
        public bool HyFlag
        {
            get
            {
                if (hyFlag == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        public string cxFlag;
        /// <summary>
        /// 促销标志
        /// </summary>
        public bool CxFlag
        {
            get
            {
                if (cxFlag == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        public string flag;
        /// <summary>
        /// 是否启用
        /// 1 启用 2 不启用
        /// </summary>
        public bool Flag
        {
            get
            {
                if (flag == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        public string isDot;
        /// <summary>
        /// 允许小数
        /// </summary>
        public bool IsDot
        {
            get
            {
                if (isDot == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }




        public string isDisc;
        /// <summary>
        /// 是否折扣
        /// </summary>
        public bool IsDisc
        {
            get
            {
                if (isDisc == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
