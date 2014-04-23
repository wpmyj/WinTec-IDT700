using System;
using System.Collections.Generic;
using System.Text;

namespace TransService.Model
{
    /// <summary>
    /// 文件对象
    /// </summary>
    public class MDownloadfile
    {
        public MDownloadfile() { }
        public MDownloadfile(string fn, byte[] fc)
        {
            FileName = fn;
            FileContent = fc;
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            set;
            get;
        }
        /// <summary>
        /// 文件字节
        /// </summary>
        public byte[] FileContent
        {
            set;
            get;
        }
    }
}
