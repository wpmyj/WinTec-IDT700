using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransService.Model;
using System.Data.SqlClient;

namespace TransService.DAL
{
    public class GoodsDAL : SQLBaseDAL
    {
        /// <summary>
        /// 读取商品
        /// </summary>
        /// <param Name="Code"></param>
        /// <param Name="goods"></param>
        /// <param Name="msg"></param>
        /// <returns></returns>
        public static bool GetGoodsByDept(string deptNo, out ICollection<MGoods> goods, out string msg)
        {
            SqlDataReader rd = null ;
            try
            {
                if (!DBTool.Select("(  Grpno=@Grpno ) And Flag='1' ",
                    new MGoods() {DeptNo=deptNo }, string.Empty, out rd, out msg))
                {
                    goods = null;
                    return false;
                }
                else
                {
                    goods = ObjTool.BuildObject<MGoods>(rd);
                    rd.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                if (rd!=null && !rd.IsClosed)
                {
                    rd.Close();
                }
                goods = null;
                return false;
            }
        }

        /// <summary>
        /// 读取商品
        /// </summary>
        /// <param Name="stype"></param>
        /// <param Name="goods"></param>
        /// <param Name="msg"></param>
        /// <returns></returns>
        public static bool GetGoodsByStype(string stype, out ICollection<MGoods> goods, out string msg)
        {
            SqlDataReader rd = null ;
            try
            {
                if (!DBTool.Select("(  stype=@stype ) And Flag='1' ",
                    new MGoods() { Stype = stype }, string.Empty, out rd, out msg))
                {
                    goods = null;
                    return false;
                }
                else
                {
                    goods = ObjTool.BuildObject<MGoods>(rd);
                    rd.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                msg = "系统异常：" + ex.Message;
                goods = null;
                if (rd != null && !rd.IsClosed)
                {
                    rd.Close();
                }
                return false;
            }
        }
    }
}
