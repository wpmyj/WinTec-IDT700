using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
namespace Action
{
    /// <summary>
    /// 系统错误日志登记类
    /// </summary>
    public class ActionWriteLog
    {
        /// <summary>
        /// 登记系统错误日志
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="module">模块</param>
        /// <param name="info">信息</param>
        public static void writeErrorLog(string module, string info)
        {
            #region 获取xtErrorLog.xml
            XmlDocument log = new XmlDocument();
            string path = System.Web.HttpContext.Current.Server.MapPath("/")+System.Web.HttpContext.Current.Server.HtmlEncode(System.Web.HttpContext.Current.Request.ApplicationPath)+"/xtErrorLog.xml";
            log.Load(path);
            #endregion

            #region 组装错误信息
            XmlElement eventNode = log.CreateElement("event");
            XmlElement infoNode = log.CreateElement("info");
            eventNode.SetAttribute("happenTime",DateTime.Now.ToString());
            eventNode.SetAttribute("module", module);
            infoNode.SetAttribute("value", info);
            eventNode.AppendChild(infoNode);
            log.SelectSingleNode("events").AppendChild(eventNode);
            #endregion

            #region 保存xtErrorLog.xml
            log.Save(path);
            #endregion
        }


        /// <summary>
        /// 登记系统日志
        /// </summary>
        /// <param name="module"></param>
        /// <param name="info"></param>
        public static void writeWorkLog(string module,string info)
        {
            #region 获取xtErrorLog.xml
            XmlDocument log = new XmlDocument();
            string path = System.Web.HttpContext.Current.Server.MapPath("/") + "xtErrorLog.xml";
            log.Load(path);
            #endregion

            #region 组装错误信息
            XmlElement eventNode = log.CreateElement("event");
            XmlElement infoNode = log.CreateElement("info");
            eventNode.SetAttribute("happenTime", DateTime.Now.ToString());
            eventNode.SetAttribute("module", module);
            infoNode.SetAttribute("value", info);
            eventNode.AppendChild(infoNode);
            log.SelectSingleNode("events").AppendChild(eventNode);
            #endregion

            #region 保存xtErrorLog.xml
            log.Save(path);
            #endregion
        }
    }
}