using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using TransService.Model;
using TransService.DAL;
using CodeBetter.Json;
using TransService.COMM;
namespace TransService
{
    /// <summary>
    /// Service 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class TransService : System.Web.Services.WebService
    {
        /// <summary>
        /// 自定义SoapHeader，用于验证权限
        /// </summary>
        public CSoapHeader soapHeader = new CSoapHeader();

        /// <summary>
        /// 验证函数
        /// </summary>
        /// <param Name="soapHeader"></param>
        /// <param Name="msg"></param>
        /// <returns></returns>
        private bool isValid(CSoapHeader soapHeader, out string msg)
        {
            return soapHeader.IsValid(out msg);
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLog(string moduleName, bool success, string msg)
        {
            using (StreamWriter sw = new StreamWriter(@"d:\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "#" + moduleName + "#" + (success ? "success" : "failed") + "#" + msg);
                sw.Close();
            }
        }

        #region 供IDT700调用的方法

        /// <summary>
        /// 连接测试
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string HelloWorld()
        {
            MMessage<string> msg = new MMessage<string>();
            msg.Flag = true;
            msg.Text = "测试通过";
            return Converter.Serialize(msg);
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns></returns>
        [SoapHeader("soapHeader")]
        [WebMethod]
        public string Logon()
        {
            MMessage<MUser> msg = new MMessage<MUser>();
            msg.Flag = isValid(soapHeader, out msg.Text);
            if (msg.Flag)
            {
                msg.Flag = UserDAL.GetUser(soapHeader.UserCode, out msg.Content, out msg.Text);
            }
            if (!msg.Flag)
            {
                WriteLog("Logon", msg.Flag, msg.Text);
            }
            return Converter.Serialize(msg);
        }



        /// <summary>
        /// 读取参数
        /// </summary>
        /// <param Name="PosNo"></param>
        /// <returns></returns>
        [SoapHeader("soapHeader")]
        [WebMethod]
        public string GetConfig()
        {
            MMessage<ICollection<MConfig>> msg = new MMessage<ICollection<MConfig>>();
            msg.Flag = isValid(soapHeader, out msg.Text);
            if (msg.Flag)
            {
                msg.Flag = ConfigDAL.GetConfig(soapHeader.PosNO, out msg.Content, out msg.Text);
            }
            if (!msg.Flag)
            {
                WriteLog("GetConfig", msg.Flag, msg.Text);
            }
            return Converter.Serialize(msg);
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param Name="Code"></param>
        /// <returns></returns>
        [SoapHeader("soapHeader")]
        [WebMethod]
        public string GetGoods()
        {
            MMessage<ICollection<MGoods>> msg = new MMessage<ICollection<MGoods>>();
            msg.Flag = isValid(soapHeader, out msg.Text);
            if (msg.Flag)
            {
                msg.Flag = GoodsDAL.GetGoodsByDept(soapHeader.DeptNO, out msg.Content, out msg.Text);
            }
            if (!msg.Flag)
            {
                WriteLog("GetGoodsByDept", msg.Flag, msg.Text);
            }
            return Converter.Serialize(msg);
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param Name="stype"></param>
        /// <returns></returns>
        [SoapHeader("soapHeader")]
        [WebMethod]
        public string GetGoodsByStype(string stype)
        {
            MMessage<ICollection<MGoods>> msg = new MMessage<ICollection<MGoods>>();
            msg.Flag = isValid(soapHeader, out msg.Text);
            if (msg.Flag)
            {
                msg.Flag = GoodsDAL.GetGoodsByStype(stype, out msg.Content, out msg.Text);
            }
            if (!msg.Flag)
            {
                WriteLog("GetGoodsByStype", msg.Flag, msg.Text);
            }
            return Converter.Serialize(msg);
        }

        /// <summary>
        /// 获取储值卡
        /// </summary>
        /// <param Name="CardNo"></param>
        /// <returns></returns>
        [SoapHeader("soapHeader")]
        [WebMethod]
        public string GetCzCard(string inCardNo)
        {
            MMessage<MCzCard> msg = new MMessage<MCzCard>();
            msg.Flag = isValid(soapHeader, out msg.Text);
            if (msg.Flag)
            {
                msg.Flag = CzkDAL.GetCzCard(inCardNo, out msg.Content, out msg.Text);
            }
            if (!msg.Flag)
            {
                WriteLog("GetCzCard", msg.Flag, msg.Text);
            }
            return Converter.Serialize(msg);
        }

        /// <summary>
        /// 保存流水
        /// </summary>
        /// <param name="flowJson"></param>
        /// <returns></returns>
        [SoapHeader("soapHeader")]
        [WebMethod]
        public string SaveTrade(string saleFlowJson, string payFlowJson)
        {
            MMessage<MCzCard> msg = new MMessage<MCzCard>();
            msg.Flag = isValid(soapHeader, out msg.Text);
            if (msg.Flag)
            {
                MSaleFlow[] saleFlowList = Converter.Deserialize<MSaleFlow[]>(saleFlowJson);
                MPayFlow[] payFlowList = Converter.Deserialize<MPayFlow[]>(payFlowJson);
                msg.Flag = TradeDAL.SaveTrade(saleFlowList, payFlowList, out msg.Content, out msg.Text);
            }
            if (!msg.Flag)
            {
                WriteLog("SaveTrade", msg.Flag, msg.Text);
            }
            return Converter.Serialize(msg);
        }

        /// <summary>
        /// 查询当天流水
        /// </summary>
        /// <returns></returns>
        [SoapHeader("soapHeader")]
        [WebMethod]
        public string QueryTrade()
        {
            MMessage<ICollection<MSaleFlow>> msg = new MMessage<ICollection<MSaleFlow>>();
            msg.Flag = isValid(soapHeader, out msg.Text);
            if (msg.Flag)
            {
                msg.Flag = TradeDAL.QueryTrade(soapHeader.PosNO, out msg.Content, out msg.Text);
            }
            if (!msg.Flag)
            {
                WriteLog("QueryTrade", msg.Flag, msg.Text);
            }
            return Converter.Serialize(msg);
        }
        #endregion

        /// <summary>
        /// 检查系统更新
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string CheckUpdate(string oldVersion, ref List<MDownloadfile> fileList)
        {
            MMessage<string> msg = new MMessage<string>();
            bool update = false;
            string newVersion;
            try
            {
                string _path = @"\Updates";
                string ss = Server.MapPath(_path);
                if (File.Exists(Server.MapPath(_path + @"\POS.exe")))
                {
                    newVersion = FileVersionInfo.GetVersionInfo(Server.MapPath(_path + @"\POS.exe")).FileVersion;
                    //判断版本号
                    string[] oldVers = oldVersion.Split('.');
                    string[] newVers = newVersion.Split('.');
                    for (int i = 0; i < 4; i++)
                    {
                        //逐位比较版本号
                        if (int.Parse(oldVers[i]) == int.Parse(newVers[i]))
                        {
                            //旧版本号等于新版本号
                            update = false;
                            continue;
                        }
                        if (int.Parse(oldVers[i]) > int.Parse(newVers[i]))
                        {
                            //旧版本号大于新版本号
                            throw new Exception("本地版本比服务器版本高");
                        }
                        if(int.Parse(oldVers[i]) < int.Parse(newVers[i]))
                        {
                            //旧版本号小于新版本号
                            update = true;
                            break;
                        }
                    }
                    if (update)
                    {
                        #region 生成配置文件
                        //写配置文件
                        using (StreamWriter wt = new StreamWriter(Server.MapPath(_path + @"\Version.inf"), false))
                        {
                            wt.WriteLine(newVersion);
                            wt.Close();
                        }
                        #endregion

                        DirectoryInfo updateFolder = new DirectoryInfo(Server.MapPath(_path));
                        fileList = new List<MDownloadfile>();
                        foreach (FileInfo file in updateFolder.GetFiles())
                        {
                            fileList.Add(new MDownloadfile(file.Name, ZipClass.ZipFileToBytes(file.FullName)));
                        }
                        msg.Flag = true;
                        //msg.Content = fileList;
                        return Converter.Serialize(msg);
                    }
                    else
                    {
                        msg.Flag = false;
                        msg.Text="已是最新程序";
                        return Converter.Serialize(msg);
                    }
                }
                else
                {
                    msg.Flag=false;
                    msg.Text="更新文件不存在";
                    return Converter.Serialize(msg);
                }
            }
            catch(Exception ex)
            {
                WriteLog("Update",false,ex.Message);
                msg.Flag=false;
                msg.Text=ex.Message;
                msg.Content=null;
                return Converter.Serialize(msg);
            }
        }
    }
}
