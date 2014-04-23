using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SqlUtilities.Interface;
using SqliteUtilities;
using System.Data.SqlClient;
using System.Configuration;
namespace TransService.DAL
{
    public class SQLBaseDAL
    {

        /// <summary>
        /// 数据库访问工具
        /// </summary>
        protected static ISqlTool DBTool = ToolBuilder.CreateSqlTool();

        /// <summary>
        /// 对象访问工具
        /// </summary>
        protected static IObjectTool ObjTool = ToolBuilder.CreateObjectTool();

        /// <summary>
        /// 数据库状态
        /// </summary>
        public static ConnectionState State
        {
            get
            {
                return DBTool.Connection.State;
            }
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Open(out string msg)
        {
            return Open(ConfigurationManager.AppSettings["DataSource"], ConfigurationManager.AppSettings["InitialCatalog"], ConfigurationManager.AppSettings["UserID"], ConfigurationManager.AppSettings["Password"], out msg);
        }
        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <returns></returns>
        public static bool Open(string ConnectionStr, out string msg)
        {
            return DBTool.Open(ConnectionStr, out msg);
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="DataSource">数据源</param>
        /// <param name="InitialCatalog">数据库</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="Password">密码</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Open(string DataSource, string InitialCatalog, string UserID, string Password, out string msg)
        {
            SqlConnectionStringBuilder b = new SqlConnectionStringBuilder();
            b.DataSource = DataSource;
            b.InitialCatalog = InitialCatalog;
            b.UserID = UserID;
            b.Password = Password;
            return Open(b.ToString(), out msg);
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        /// <returns></returns>
        public static bool Close(out string msg)
        {
            return DBTool.Close(out msg);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam Name="T">泛型</typeparam>
        /// <param Name="obj">对象</param>
        /// <param Name="i">影响行数</param>
        /// <param Name="msg">返回的消息</param>
        /// <returns>是否成功</returns>
        protected static bool Insert<T>(T obj, out int i, out string msg)
        {
            return DBTool.Insert(obj, out i, out msg);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam Name="T">泛型</typeparam>
        /// <param Name="obj">对象</param>
        /// <param Name="i">影响行数</param>
        /// <param Name="msg">返回的消息</param>
        /// <returns>是否成功</returns>
        protected static bool Update<T>(T obj, out int i, out string msg)
        {
            return DBTool.Update(obj, out i, out msg);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam Name="T">泛型</typeparam>
        /// <param Name="obj">对象</param>
        /// <param Name="i">影响的行数</param>
        /// <param Name="msg">返回的消息</param>
        /// <returns>是否成功</returns>
        protected static bool Delete<T>(T obj, out int i, out string msg)
        {
            return DBTool.Delete(obj, out i, out msg);
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <typeparam Name="T"></typeparam>
        /// <param Name="Condition"></param>
        /// <param Name="model"></param>
        /// <param Name="OrderBy"></param>
        /// <param Name="dt"></param>
        /// <param Name="msg"></param>
        /// <returns></returns>
        protected static bool Select<T>(string Condition, T model, string OrderBy, out DataTable dt, out string msg)
        {
            return DBTool.Select(Condition, model, OrderBy, out dt, out msg);
        }

    }
}
