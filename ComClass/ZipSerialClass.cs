using System;
using System.Collections.Generic;
using System.Text;
using CompactFormatter;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ComClass
{
    /// <summary>
    /// 序列化压缩工具类
    /// </summary>
    #region 序列化
    /// <summary>
    /// 序列化与反序列化实例工具类
    /// </summary>
    public  static class SerialClass
    {
        /// <summary>
        /// 序列化实例
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <param name="obj">实例</param>
        /// <returns></returns>
        public static byte[] Serial(object obj)
        {
            //定义序列化工具实例
            CompactFormatterPlus ser = new CompactFormatterPlus();
            //定义流
            MemoryStream ms = new MemoryStream();
            //定义结果字节数组
            byte[] res;
            //序列化开始
            ser.Serialize(ms, obj);
            //读取序列化结果
            res = ms.ToArray();
            ms.Close();
            //压缩
            res = ZipClass.Compress(res);
            return res;
        }
        /// <summary>
        /// 将字节流反序列化，返回一个obj
        /// </summary>
        ///<param name="buff">字节流</param>
        /// <returns></returns>
        public static object DeSerial(byte[] buff)
        {
            //定义序列化工具实例
            CompactFormatterPlus ser = new CompactFormatterPlus();
            //解压缩
            buff = ZipClass.DeCompress(buff);
            //定义流
            MemoryStream ms = new MemoryStream(buff);
            object obj = ser.Deserialize(ms);
            return obj;
        }
    }
    #endregion

    #region 压缩

    /// <summary>
    /// 压缩工具类
    /// </summary>
    public static class ZipClass
    {
        /// <summary>
        /// 压缩
        /// </summary>
        ///<param name="buff">压缩目标字节数组</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] Compress(byte[] bytesSource)
        {
            MemoryStream mMemory = new MemoryStream();
            Deflater mDeflater = new Deflater(Deflater.DEFAULT_COMPRESSION);
            DeflaterOutputStream mStream = new DeflaterOutputStream(mMemory, mDeflater, 131072);
            mStream.Write(bytesSource, 0, bytesSource.Length);
            mStream.Close();
            byte[] byteDest = mMemory.ToArray();
            return byteDest;
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static byte[] DeCompress(byte[] bytesSource)
        {
            // byte[] bytesSource = Convert.FromBase64String(strSource);
            InflaterInputStream mStream = new InflaterInputStream(new MemoryStream(bytesSource));
            MemoryStream mMemory = new MemoryStream();
            Int32 mSize = 4096;
            byte[] mWriteData = new byte[4096];
            while (true)
            {
                mSize = mStream.Read(mWriteData, 0, mWriteData.Length);
                if (mSize > 0)
                    mMemory.Write(mWriteData, 0, mSize);
                else
                    break;
            }
            mStream.Close();
            byte[] byteDest = mMemory.ToArray();
            return byteDest;
        }
    }
    #endregion
    
}
