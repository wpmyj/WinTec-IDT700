namespace Model
{
    public class MMessage<T>
    {
        /// <summary>
        /// 消息标记
        /// </summary>
        public bool Flag
        {
            get;
            set;
        }

        /// <summary>
        /// 消息文本
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public T Content
        {
            get;
            set;
        }
    }
}
