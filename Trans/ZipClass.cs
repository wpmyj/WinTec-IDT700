using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip;


    public class ZipClass
    {
        private static int buffSize = 2048;//指定压缩块缓存的大小，一般为2048的倍数

        /// <summary>
        /// BZIP2压缩数据
        /// </summary>
        /// <param name="input">原始未压缩数据</param>
        /// <returns>压缩后的byte[]数据</returns>
        public static byte[] BZipCompress(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("null input");
            }
            try
            {
                //int buffSize = 2048;//指定压缩块的大小，一般为2048的倍数
                using (MemoryStream outmsStrm = new MemoryStream())
                {
                    using (MemoryStream inmsStrm = new MemoryStream(input))
                    {
                        BZip2.Compress(inmsStrm, outmsStrm, buffSize);
                    }
                    return outmsStrm.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 解压缩BZIP2数据
        /// </summary>
        /// <param name="input">被BZIP2压缩过的byte[]数据</param>
        /// <returns>解压后的byte[]数据</returns>
        public static byte[] BZipDeCompress(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("null input");
            }
            try
            {
                using (MemoryStream outmsStrm = new MemoryStream())
                {
                    using (MemoryStream inmsStrm = new MemoryStream(input))
                    {
                        BZip2.Decompress(inmsStrm, outmsStrm);
                    }
                    return outmsStrm.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// 压缩Deflater数据
        /// </summary>
        /// <param name="input">待压缩byte[]数据</param>
        /// <returns>返回压缩后的byte[]</returns>
        public static byte[] DeflaterCompress(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("null input");
            }
            try
            {
                Deflater mDeflater = new Deflater(Deflater.BEST_COMPRESSION);
                //int buffSize = 2048;//131072 buff size
                using (MemoryStream outmsStrm = new MemoryStream())
                {
                    using (DeflaterOutputStream mStream = new DeflaterOutputStream(outmsStrm, mDeflater, buffSize))
                    {
                        mStream.Write(input, 0, input.Length);
                    }
                    return outmsStrm.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 解压缩Deflater数据
        /// </summary>
        /// <param name="input">压缩过的byte[]数据</param>
        /// <returns>解压后的byte[]数据</returns>
        public static byte[] DeflaterDeCompress(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("null input");
            }
            try
            {
                Int32 mSize;
                //int buffSize = 2048;
                byte[] buff = new byte[buffSize];
                using (MemoryStream outmsStrm = new MemoryStream())
                {
                    using (InflaterInputStream mStream = new InflaterInputStream(new MemoryStream(input)))
                    {
                        while (true)
                        {
                            mSize = mStream.Read(buff, 0, buff.Length);
                            if (mSize > 0)
                            {
                                outmsStrm.Write(buff, 0, mSize);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    return outmsStrm.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// GZIP压缩
        /// </summary>
        /// <param name="input">未压缩的数据</param>
        /// <returns>GZIP压缩后的数据</returns>
        public static byte[] GZipCompress(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("null input");
            }
            try
            {
                using (MemoryStream outmsStrm = new MemoryStream())
                {
                    using (GZipOutputStream gzip = new GZipOutputStream(outmsStrm))
                    {
                        gzip.Write(input, 0, input.Length);
                    }
                    return outmsStrm.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// GZIP解压缩
        /// </summary>
        /// <param name="input">压缩过的数据</param>
        /// <returns>解压后的数据</returns>
        public static byte[] GZipDeCompress(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("null input");
            }
            try
            {
                using (MemoryStream outmsStrm = new MemoryStream())
                {
                    using (GZipInputStream gzip = new GZipInputStream(new MemoryStream(input)))
                    {
                        Int32 mSize;
                        //int buffSize = 2048;
                        byte[] buff = new byte[buffSize];
                        while (true)
                        {
                            mSize = gzip.Read(buff, 0, buffSize);
                            if (mSize > 0)
                            {
                                outmsStrm.Write(buff, 0, mSize);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    return outmsStrm.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// ZIP压缩数据
        /// </summary>
        /// <param name="input">待压缩的数据</param>
        /// <returns>ZIP压缩后的数据</returns>
        public static byte[] ZipCompress(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("null input");
            }
            try
            {
                using (MemoryStream outmsStrm = new MemoryStream())
                {
                    using (ZipOutputStream zipStrm = new ZipOutputStream(outmsStrm))
                    {
                        ZipEntry zn = new ZipEntry("znName");
                        zipStrm.PutNextEntry(zn);
                        zipStrm.Write(input, 0, input.Length);
                    }
                    return outmsStrm.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// ZIP解压缩数据
        /// </summary>
        /// <param name="input">压缩过的数据</param>
        /// <returns>解压后的数据</returns>
        public static byte[] ZipDeCompress(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("null input");
            }
            try
            {
                using (MemoryStream outmsStrm = new MemoryStream())
                {
                    using (ZipInputStream zipStrm = new ZipInputStream(new MemoryStream(input)))
                    {
                        Int32 mSize;
                        //int buffSize = 2048;
                        byte[] buff = new byte[buffSize];
                        ZipEntry zn = new ZipEntry("znName");
                        while ((zn = zipStrm.GetNextEntry()) != null)
                        {
                            while (true)
                            {
                                mSize = zipStrm.Read(buff, 0, buffSize);
                                if (mSize > 0)
                                {
                                    outmsStrm.Write(buff, 0, mSize);
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }
                    return outmsStrm.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //将文件压缩成二进制流
        public static byte[] ZipFileToBytes(string fileToZip)
        {
            System.IO.FileStream fs = new System.IO.FileStream(fileToZip, System.IO.FileMode.Open);
            byte[] bytesToZip = new Byte[fs.Length];
            fs.Read(bytesToZip, 0, (int)fs.Length);
            fs.Close();
            //return DeflaterCompress(bytesToZip);
            return GZipCompress(bytesToZip);
            //return ZipCompress(bytesToZip);
            //return BZipCompress(bytesToZip);
        }

        //将二进制流解压到文件
        public static void UnzipBytesToFile(byte[] zipedBytes, string fileName)
        {
            System.IO.FileStream streamWriter = File.Create(fileName);
            //byte[] b = DeflaterDeCompress(zipedBytes);
            byte[] b = GZipDeCompress(zipedBytes);
            //byte[] b = ZipDeCompress(zipedBytes);
            //byte[] b = BZipDeCompress(zipedBytes);
            streamWriter.Write(b, 0, b.Length);
            streamWriter.Close();
        }
    }


