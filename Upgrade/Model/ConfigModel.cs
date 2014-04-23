using System;
using System.Collections.Generic;
using System.Text;

namespace Upgrade.Model
{
    /// <summary>
    /// Config实体类
    /// </summary>
    public class ConfigModel
    {
        public ConfigModel()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private string _customerid;
        /// <summary>
        /// 客户编号
        /// </summary>
        public string CustomerId
        {
            get { return _customerid; }
            set { _customerid = value; }
        }

        private string _customername;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get { return _customername; }
            set { _customername = value; }
        }

        private string _linkman;
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan
        {
            get { return _linkman; }
            set { _linkman = value; }
        }

        private string _phone;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private string _companyname;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get { return _companyname; }
            set { _companyname = value; }
        }

        private string _posno;
        /// <summary>
        /// POS机设备号
        /// </summary>
        public string PosNo
        {
            get { return _posno; }
            set { _posno = value; }
        }

        private string _softwareversion;
        /// <summary>
        /// 终端软件版本
        /// </summary>
        public string SoftWareVersion
        {
            get { return _softwareversion; }
            set { _softwareversion = value; }
        }

        private string _serverip;
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerIP
        {
            get { return _serverip; }
            set { _serverip = value; }
        }
        
        private string _historydatakeeptime;
        /// <summary>
        /// 历史数据保留时间
        /// </summary>
        public string HistoryDataKeepTime
        {
            get { return _historydatakeeptime; }
            set { _historydatakeeptime = value; }
        }
        
        private string _pwd;
        /// <summary>
        /// 终端登录密码
        /// </summary>
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        private string _epw;
        /// <summary>
        /// 系统退出密码
        /// </summary>
        public string EPW
        {
            get { return _epw; }
            set { _epw = value; }
        }

        private string _remark1;
        /// <summary>
        /// 备注1
        /// </summary>
        public string Remark1
        {
            get { return _remark1; }
            set { _remark1 = value; }
        }

        private string _remark2;
        /// <summary>
        /// 备注2
        /// </summary>
        public string Remark2
        {
            get { return _remark2; }
            set { _remark2 = value; }
        }

        private string _remark3;
        /// <summary>
        /// 备注3
        /// </summary>
        public string Remark3
        {
            get { return _remark3; }
            set { _remark3 = value; }
        }
    }
}
