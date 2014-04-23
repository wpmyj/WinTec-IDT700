using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransService.Model;
using System.Data.SqlClient;
using TransService.COMM;

namespace TransService.DAL
{
    public class CzkDAL:SQLBaseDAL
    {
        /// <summary>
        /// 读卡
        /// </summary>
        /// <param Name="inCardNo"></param>
        /// <param Name="card"></param>
        /// <param Name="msg"></param>
        /// <returns></returns>
        public static bool GetCzCard(string inCardNo, out MCzCard card, out string msg)
        {
            card=new MCzCard();
            card.InCardno=inCardNo;
            SqlDataReader rd=null;
            try
            {
                if (!DBTool.Select("InCardNo=@InCardNo", card, string.Empty, out rd, out msg))
                {
                    card = null;
                    return false;
                }
                else
                {
                    ICollection<MCzCard> list = ObjTool.BuildObject<MCzCard>(rd);
                    rd.Close();
                    if (list.Count == 0)
                    {
                        card = null;
                        msg = "卡片"+inCardNo+"不存在";
                        return false;
                    }
                    else
                    {
                        card = list.First();
                        if (card.State == "1")
                        {
                            return GetCzkType(card.CardTypeCode,out card.CzkType,out msg);
                        }
                        else
                        {
                            msg = "该卡已禁用或已挂失！";
                            return false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                if (rd != null && !rd.IsClosed)
                {
                    rd.Close();
                }
                card = null;
                return false;
            }
        }

        /// <summary>
        /// 获取储值卡类型
        /// </summary>
        /// <param name="czkTypeCode"></param>
        /// <param name="czkType"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool GetCzkType(string czkTypeCode, out MCzkType czkType, out string msg)
        {
            SqlDataReader rd=null;
            try
            {
                if (!DBTool.Select("Code=@Code", 
                    new MCzkType() { Code = czkTypeCode }, string.Empty, out rd, out msg))
                {
                    czkType = null;
                    return false;
                }
                else
                {
                    ICollection<MCzkType> list = ObjTool.BuildObject<MCzkType>(rd);
                    rd.Close();
                    if (list.Count == 0)
                    {
                        msg = "未找到储值卡类型" + czkTypeCode;
                        czkType = null;
                        return false;
                    }
                    else
                    {
                        czkType = list.First();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (rd != null && !rd.IsClosed)
                {
                    rd.Close();
                }
                msg = "系统异常：" + ex.Message;
                czkType = null;
                return false;
            }
        }

        /// <summary>
        /// 保存储值卡
        /// </summary>
        /// <param name="card"></param>
        /// <param name="tran">事务</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool SaveCzCard(MCzCard card, SqlTransaction tran, out string msg)
        {
            int i;
            if (!DBTool.Update(card,tran, out i, out msg))
            {
                return false;
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                msg = "卡更新失败，未找到卡" + card.OutCardNo;
                return false;
            }
        }
    }
}
