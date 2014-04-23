using System;
using System.Collections.Generic;
using System.Text;
namespace devices
{
    public class ICCard
    {
        public ICCard(string comPort)
        {
            CardReader = new Devices.RfidPort(comPort);
        }
        private Devices.RfidPort CardReader;

        /// <summary>
        /// 关闭端口
        /// </summary>
        public void ClosePort()
        {
            CardReader.ClosePort();
        }
        /// <summary>
        /// 寻卡
        /// </summary>
        /// <returns></returns>
        public bool SearchIC()
        {
            return CardReader.RF_SearchCard()!="-1";
        }
        /// <summary>
        /// 读取块
        /// </summary>
        /// <param name="sq">扇区</param>
        /// <param name="kh">块号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public string ReadIC(string sq, string kh, string pwd)
        {
            byte block = MakeBlockByte(int.Parse(sq), int.Parse(kh));
            byte keyType = 0x60;
            byte[] pwds = MakePwdBytes(pwd);
            byte[] readContent;
            try
            {
                readContent = CardReader.RF_ReadBlock(block, pwds, keyType);
            }
            catch
            {
                throw;
            }
            return ReadStrFromBytes(readContent);
        }

        /// <summary>
        /// 写卡
        /// </summary>
        /// <param name="sq">扇区</param>
        /// <param name="kh">块号</param>
        /// <param name="pwd">密码</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public bool WriteIC(string sq, string kh, string pwd, string str)
        {
            byte block = MakeBlockByte(int.Parse(sq), int.Parse(kh));
            byte keyType = 0x60;
            byte[] pwds = MakePwdBytes(pwd);
            byte[] content = MakeContentBytes(str);
            return CardReader.RF_WriteBlock(block, pwds, keyType, content);
        }


        /// <summary>
        /// 构建密码字节数组
        /// </summary>
        /// <param name="pwds">密码字符串</param>
        /// <returns></returns>
        private byte[] MakePwdBytes(string pwdstr)
        {
            byte[] pwds = new byte[6];
            for (int i = 0; i < pwds.Length; i++)
            {
                if (pwdstr != string.Empty)
                {
                    pwds[i] = Convert.ToByte(pwdstr.Substring(0, 2), 16);

                    pwdstr = pwdstr.Substring(2);
                }
                else
                {
                    pwds[i] = Convert.ToByte("FF", 16);
                }
            }
            return pwds;
        }

        /// <summary>
        /// 构建内容字节数组
        /// </summary>
        /// <param name="str">内容字符串</param>
        /// <returns></returns>
        private byte[] MakeContentBytes(string str)
        {
            byte[] content = new byte[16];

            for (int i = 0; i < 16; i++)
            {
                if (str != string.Empty)
                {
                    int a = Convert.ToInt16(str.Substring(0, 2), 16);
                    content[i] = Convert.ToByte(a);
                    str = str.Substring(2);
                }
                else
                {
                    content[i] = Convert.ToByte(0);
                }
            }
            return content;
        }


        /// <summary>
        /// 构建块号
        /// </summary>
        /// <param name="Sector"></param>
        /// <param name="Block"></param>
        /// <returns></returns>
        private byte MakeBlockByte(int Sector, int Block)
        {
            if (Sector < 32)
            {
                return Convert.ToByte(((Sector) << 2) | Block);
            }
            else
            {
                return Convert.ToByte(128 + ((((Sector) - 32) << 4) | (Block)));
            }
        }


        /// <summary>
        /// 从字节数组中读取16进制字符串
        /// </summary>
        /// <param name="content">字节数组</param>
        /// <returns></returns>
        private string ReadStrFromBytes(byte[] content)
        {
            string readStr = string.Empty;
            foreach (byte b in content)
            {
                readStr += Convert.ToString(b, 16).PadLeft(2, '0');
            }
            return readStr;
        }


    }
}