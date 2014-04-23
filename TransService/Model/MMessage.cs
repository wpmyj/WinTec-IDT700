using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransService.Model
{
    public class MMessage<T>
    {
        /// <summary>
        /// 消息标记
        /// </summary>
        public bool Flag;

        /// <summary>
        /// 消息文本
        /// </summary>
        public string Text;

        /// <summary>
        /// 消息内容
        /// </summary>
        public T Content;
    }
}
