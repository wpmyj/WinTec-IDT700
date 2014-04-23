using System;

using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 商品流水
    /// </summary>
    public class SaleFlow
    {
        private string operater;
        /// <summary>
        /// 收款员
        /// </summary>
        public string Operater
        {
            get { return operater; }
            set { operater = value; }
        }


        private string saler;
        /// <summary>
        /// 售货员
        /// </summary>
        public string Saler
        {
            get { return saler; }
            set { saler = value; }
        }


        private string sgroup;
        /// <summary>
        /// 部门
        /// </summary>
        public string Sgroup
        {
            get { return sgroup; }
            set { sgroup = value; }
        }



        private string serial_no;
        /// <summary>
        /// 商品流水
        /// </summary>
        public string Serial_no
        {
            get { return serial_no; }
            set { serial_no = value; }
        }



        private string posNo;
        /// <summary>
        /// 款台号
        /// </summary>
        public string PosNo
        {
            get { return posNo; }
            set { posNo = value; }
        }




        private string code;
        /// <summary>
        /// 商品编码
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
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

        private float price;
        /// <summary>
        /// 售价
        /// </summary>
        public float Price
        {
            get { return price; }
            set { price = value; }
        }




        private float qty;
        /// <summary>
        /// 数量
        /// </summary>
        public float Qty
        {
            get { return qty; }
            set { qty = value; }
        }



        private float pre_total;
        /// <summary>
        /// 应付金额
        /// </summary>
        public float Pre_total
        {
            get { return pre_total; }
            set { pre_total = value; }
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



        private float total;
        /// <summary>
        /// 应付金额
        /// </summary>
        public float Total
        {
            get { return total; }
            set { total = value; }
        }



        private int zdisc;
        /// <summary>
        /// 整单折扣
        /// </summary>
        public int Zdisc
        {
            get { return zdisc; }
            set { zdisc = value; }
        }



        private float real_total;
        /// <summary>
        /// 实付金额
        /// </summary>
        public float Real_total
        {
            get { return real_total; }
            set { real_total = value; }
        }



        private string sa_date;
        /// <summary>
        /// 销售日期
        /// </summary>
        public string Sa_date
        {
            get { return sa_date; }
            set { sa_date = value; }
        }

        private string sa_time;
        /// <summary>
        /// 销售时间
        /// </summary>
        public string Sa_time
        {
            get { return sa_time; }
            set { sa_time = value; }
        }



        private string vip_no;
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string Vip_no
        {
            get { return vip_no; }
            set { vip_no = value; }
        }



        public string vip_pmp;
        /// <summary>
        /// 是否会员销售
        /// true 会员销售，false 普通销售
        /// </summary>
        public bool Vip_pmp
        {
            get
            {
                if (vip_pmp == "02")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        private string rightNo;
        /// <summary>
        /// 权限卡号
        /// </summary>
        public string RightNo
        {
            get { return rightNo; }
            set { rightNo = value; }
        }



        private string rightFlag;
        /// <summary>
        /// 权限标志
        /// </summary>
        public string RightFlag
        {
            get { return rightFlag; }
            set { rightFlag = value; }
        }



        private string squadno;
        /// <summary>
        /// 班次
        /// </summary>
        public string Squadno
        {
            get { return squadno; }
            set { squadno = value; }
        }



        private int rowNo;
        /// <summary>
        /// 该流水在本次销售中的序列号
        /// </summary>
        public int RowNo
        {
            get { return rowNo; }
            set { rowNo = value; }
        }



        public string flag;
        /// <summary>
        /// 是否上传
        /// </summary>
        public FlowUpLoadFlag Flag
        {
            get { return (FlowUpLoadFlag)int.Parse(flag); }
            set { flag = ((int)value).ToString(); }
        }

        public string saleReturnFlag;
        /// <summary>
        /// 
        /// </summary>
        public bool CanReturn
        {
            get
            {
                if (saleReturnFlag == "0")
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
