using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Trans.TService;
using COMM;
using Model;
using CodeBetter.Json;
using System.IO;

namespace Trans
{
    public class TransModule
    {
        /// <summary>
        /// Webservice对象
        /// </summary>
        static TService.TransService TransClass;

        public static CSoapHeader soapHeader
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化webservice 对象
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="UserCode"></param>
        /// <param name="Password"></param>
        /// <param name="DeptNo"></param>
        public static void Init(string Url, string UserCode, string Password, string DeptNo)
        {
            if (TransClass == null)
            {
                TransClass = new Trans.TService.TransService();
                TransClass.CSoapHeaderValue = new CSoapHeader();
            }
            TransClass.Url = Url;
            TransClass.CSoapHeaderValue.UserCode = UserCode;
            TransClass.CSoapHeaderValue.Password = Des.EncryStrHex(Password, "0125" + UserCode);
            TransClass.CSoapHeaderValue.DeptNO = DeptNo;
            TransClass.CSoapHeaderValue.PosNO = PubGlobal.SysConfig.PosNO;
        }

        /// <summary>
        /// 格式化json字符串
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string FormatJson(string json)
        {
            return json.Replace("null", "\"null\"");
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <returns></returns>
        public static bool HelloWorld(string Server,string Port,out string msg)
        {
            try
            {
                TransService TestClass=new TransService();
                TestClass.Url=@"http://" + Server + ":" + Port + @"/TransService.asmx";
                string json = TestClass.HelloWorld();
                MMessage<string> mmsg = Converter.Deserialize<MMessage<string>>(json);
                msg = mmsg.Text;
                return true;
            }
            catch (Exception ex)
            {
                msg = "连接错误：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public static bool Logon(out string msg)
        {
            try
            {
                string json = TransClass.Logon();
                MMessage<MUser> mmsg = Converter.Deserialize<MMessage<MUser>>(json);
                if (mmsg.Flag)
                {
                    PubGlobal.SysConfig.User = mmsg.Content ;
                    msg = "登陆成功";
                    return true;
                }
                else
                {
                    PubGlobal.SysConfig.User = null;
                    msg = mmsg.Text;
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 读取参数
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool GetConfig(out string msg)
        {
            try
            {
                string json = TransClass.GetConfig();
                MMessage<MConfig[]> mmsg = Converter.Deserialize<MMessage<MConfig[]>>(
                   json );
                if (mmsg.Flag)
                {
                    foreach (MConfig config in mmsg.Content)
                    {
                        switch (config.ItemName)
                        {
                            case "RECEIPT HEAD1":
                                PubGlobal.SysConfig.PrnHeader1 = config.ItemValue;
                                break;
                            case "RECEIPT HEAD2":
                                PubGlobal.SysConfig.PrnHeader2 = config.ItemValue;
                                break;
                            case "RECEIPT HEAD3":
                                PubGlobal.SysConfig.PrnHeader3 = config.ItemValue;
                                break;
                            case "RECEIPT TAIL1":
                                PubGlobal.SysConfig.PrnFooter1 = config.ItemValue;
                                break;
                            case "RECEIPT TAIL2":
                                PubGlobal.SysConfig.PrnFooter2 = config.ItemValue;
                                break;
                            case "RECEIPT TAIL3":
                                PubGlobal.SysConfig.PrnFooter3 = config.ItemValue;
                                break;
                            case "ICNEWMM":
                                PubGlobal.SysConfig.CzkPassword = Des.DecryStrHex(config.ItemValue, "0125");
                                break;
                            case "IcLen":
                                PubGlobal.SysConfig.CzkLen = int.Parse(config.ItemValue);
                                break;
                            default:
                                break;

                        }
                    }
                    msg = "读取参数完毕";
                    return true;
                }
                else
                {
                    msg = mmsg.Text;
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 通过部门获取商品列表
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool GetGoods(out string msg)
        {
            try
            {
                string json;
                if (PubGlobal.SysConfig.GetGoodsByDept)
                {
                    json = TransClass.GetGoods();
                }
                else
                {
                    json = TransClass.GetGoodsByStype(PubGlobal.SysConfig.Stype);
                }
                MMessage<MGoods[]> mmsg = Converter.Deserialize<MMessage<MGoods[]>>(json);
                if (mmsg.Flag)
                {
                    PubGlobal.BussinessVar.goodsList = mmsg.Content;
                    msg = "读取商品列表成功";
                    return true;
                }
                else
                {
                    PubGlobal.BussinessVar.goodsList = null;
                    msg = mmsg.Text;
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 读取储值卡
        /// </summary>
        /// <param name="InCardNo"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool GetCzCard(string inCardNo, out string msg)
        {
            try
            {
                MMessage<MCzCard> mmsg = Converter.Deserialize<MMessage<MCzCard>>(
                    TransClass.GetCzCard(inCardNo));
                if (mmsg.Flag)
                {
                    PubGlobal.BussinessVar.card = mmsg.Content;
                    msg = "储值卡获取成功";
                    return true;
                }
                else
                {
                    PubGlobal.BussinessVar.card = null;
                    msg = mmsg.Text;
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                PubGlobal.BussinessVar.card = null;
                return false;
            }
        }

        /// <summary>
        /// 保存交易
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool SaveTrade(out string msg)
        {
            if (PubGlobal.BussinessVar.saleFlowList == null || PubGlobal.BussinessVar.payFlowList == null ||
                PubGlobal.BussinessVar.saleFlowList.Count == 0 || PubGlobal.BussinessVar.payFlowList.Count == 0)
            {
                msg = "流水不完整";
                return false;
            }

            try
            {
                MMessage<MCzCard> mmsg = Converter.Deserialize<MMessage<MCzCard>>(
                    TransClass.SaveTrade(Converter.Serialize(PubGlobal.BussinessVar.saleFlowList),
                    Converter.Serialize(PubGlobal.BussinessVar.payFlowList)));
                if (mmsg.Flag)
                {
                    PubGlobal.BussinessVar.card = mmsg.Content;
                    msg = "交易成功";
                    return true;
                }
                else
                {
                    msg = mmsg.Text;
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 查询当日交易
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool QueryTrade(out decimal total,out string msg)
        {
            try
            {
                string json = TransClass.QueryTrade();
                MMessage<MSaleFlow[]> mmsg = Converter.Deserialize<MMessage<MSaleFlow[]>>(json);
                if (mmsg.Flag)
                {
                    PubGlobal.BussinessVar.QuerySaleFlows.Clear();
                    PubGlobal.BussinessVar.QuerySaleFlows.AddRange(mmsg.Content);
                    msg = mmsg.Text;
                    total = 0;
                    foreach (MSaleFlow saleFlow in mmsg.Content)
                    {
                        total += saleFlow.Total;
                    }
                    return true;
                }
                else
                {
                    msg = mmsg.Text;
                    total = 0;
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                total = 0;
                return false;
            }
        }

        /// <summary>
        /// 查询是否有更新
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>0 不需要更新 1 需要更新 -1 异常</returns>
        public static int CheckUpdate(out string msg)
        {
            MDownloadfile[] updateFileList=null;
            string json=TransClass.CheckUpdate(PubGlobal.Envionment.APPVersion,ref updateFileList);
            MMessage<string> mmsg=Converter.Deserialize<MMessage<string>>(json);
            if (mmsg.Flag && updateFileList != null && updateFileList.Length > 0)
            {
                string path = PubGlobal.Envionment.UpdateFolderPath;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                foreach (MDownloadfile file in updateFileList)
                {
                    try
                    {
                        ZipClass.UnzipBytesToFile(file.FileContent, path+@"\" + file.FileName);
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        return -1;
                    }
                }
                msg = string.Empty;
                return 1;
            }
            else
            {
                msg = "不需要更新";
                return 0;
            }
        }
    }
}
