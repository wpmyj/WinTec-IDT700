using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace ComClass
{
    public class DllPubFile
    {
        /*************************************************************/
        /// <summary>
        /// 1
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CzkEncryStr(string str)
        {
            int tNewsjm;
            string s;
            string s1;
            string s2;
            string result;
            int key;
            int len = str.Length;
            s = (100 + len).ToString().Substring(1, 2) + str;
            while (s.Length < 16)
            {
                s = s + new Random().Next(9).ToString();
            }
            tNewsjm = new Random().Next(9);
            s1 = s.Substring(0, 12);
            s2 = s.Substring(12, 4) + tNewsjm.ToString();
            key = int.Parse(s.Substring(12, 4)) + tNewsjm;
            byte[] resultb = EncryptStr(s1, key);
            result = Convert.ToBase64String(resultb);
            result = EncodeCardString(result) + s2;

            return result;
        }


        public static string CzkDecryStr(string str)
        {
            int len = str.Length;
            int tNewsjm = int.Parse(str.Substring(len - 1, 1));
            int key = int.Parse(str.Substring(len - 5, 4)) + tNewsjm;
            string s = str.Substring(0, len - 5);
            string result;
            // S:=DecryptStr(MimeDecodeString(DecodeCardString(S)),Key)+Copy(Str,Len-4,5);

            result = DecodeCardString(s);
            byte[] resultb = Convert.FromBase64String(result);
            result = Encoding.Default.GetString(resultb, 0, resultb.Length);
            byte[] bs = DecryptStr(result, key);
            result = Encoding.Default.GetString(bs, 0, bs.Length);
            s = result + str.Substring(len - 5, 5);

            len = int.Parse(s.Substring(0, 2));
            result = s.Substring(2, len);

            return result;
        }
        /*************************************************************/

        /// <summary>
        /// 2
        /// </summary>
        /// <param name="InputString"></param>
        /// <returns></returns>
        public static string EncodeCardString(string InputString)
        {
            string str = "";
            int i;
            byte[] bytes = Encoding.Default.GetBytes(InputString);
            for (i = 0; i < InputString.Length; i++)
            {
                string tmpstr = int.Parse(bytes[i].ToString()).ToString("X");
                while (tmpstr.Length < 2)
                {
                    tmpstr = "0" + tmpstr;
                }
                str = str + tmpstr;
            }
            char[] s = str.ToCharArray();
            for (i = 0; i < str.Length; i++)
            {
                switch (s[i])
                {
                    case 'A':
                        s[i] = '=';
                        break;
                    case 'B':
                        s[i] = '>';
                        break;
                    case 'C':
                        s[i] = s[i - 1];
                        s[i - 1] = '=';
                        break;
                    case 'D':
                        s[i] = s[i - 1];
                        s[i - 1] = '>';
                        break;
                    case 'E':
                        s[i] = s[i - 1];
                        s[i - 1] = '9';
                        break;
                    case 'F':
                        s[i] = s[i - 1];
                        s[i - 1] = '8';
                        break;

                }


            }

            return new string(s);
        }

        public static string DecodeCardString(string InputString)
        {
            int i;
            char[] str;
            string result = "";
            if ((InputString.Length % 2) != 0)
            {
                result = "";
                return result; ;
            }
            str = InputString.ToCharArray();
            i = 0;
            while (i < str.Length)
            {
                switch (str[i])
                {
                    case '=':
                        if (i % 2 == 0)
                        {
                            str[i] = str[i + 1];
                            str[i + 1] = 'C';
                            i = i + 1;
                        }
                        else
                        {
                            str[i] = 'A';
                        }
                        break;
                    case '>':
                        if (i % 2 == 0)
                        {
                            str[i] = str[i + 1];
                            str[i + 1] = 'D';
                            i = i + 1;
                        }
                        else
                        {
                            str[i] = 'B';
                        }
                        break;
                    case '9':
                        if (i % 2 == 0)
                        {
                            str[i] = str[i + 1];
                            str[i + 1] = 'E';
                            i = i + 1;
                        }
                        break;
                    case '8':
                        if (i % 2 == 0)
                        {
                            str[i] = str[i + 1];
                            str[i + 1] = 'F';
                            i = i + 1;
                        }
                        break;
                }
                i = i + 1;
            }

            InputString = new string(str);

            for (i = 0; i < str.Length / 2; i++)
            {
                int num = Convert.ToInt32(InputString[(i * 2)].ToString() + InputString[(i * 2 + 1)].ToString(), 16);

                result = result + Convert.ToChar(num);
            }

            return result;
        }

        /*************************************************************/
        /// <summary>
        ///3 加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] EncryptStr(string str, int key)
        {
            int i;
            byte[] result = new byte[str.Length];
            byte[] s = Encoding.Default.GetBytes(str);

            for (i = 0; i < str.Length; i++)
            {

                int l = s[i] ^ (key >> 8);


                result[i] = Convert.ToByte(l);
                key = result[i] + key;
            }
            return result;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] DecryptStr(string str, int key)
        {
            int i;
            byte[] result = new byte[str.Length];
            byte[] s = Encoding.Default.GetBytes(str);

            for (i = 0; i < str.Length; i++)
            {

                int l = s[i] ^ (key >> 8);


                result[i] = Convert.ToByte(l);
                key = s[i] + key;

            }
            return result;
        }

        /***************************************************************/

        public static string EncryStrHex(string str, int key)
        {
            string strResult, TempResult, Temp;
            int i;
            try
            {
                byte[] bs = EncryptStr(str, key);
                TempResult = Encoding.Default.GetString(bs, 0, bs.Length);
                strResult = "";
                for (i = 0; i < TempResult.Length; i++)
                {
                    Temp = ((int)(Encoding.Default.GetBytes(TempResult[i].ToString()))[0]).ToString("X");
                    if (Temp.Length == 1)
                    {
                        Temp = '0' + Temp;
                    }
                    strResult = strResult + Temp;
                }
            }
            catch (Exception ex)
            {
                strResult = "";
            }
            return strResult;
        }

        public static string DecryStrHex(string strHex, int key)
        {
            string str, temp, result;
            int i;
            try
            {
                str = "";
                for (i = 0; i < strHex.Length / 2; i++)
                {
                    temp = strHex.Substring(i * 2, 2);
                    byte b = Convert.ToByte(HexToInt(temp));
                    str = str + Encoding.Default.GetString(new byte[] { b }, 0, 1);
                }
                byte[] bs = DecryptStr(str, key);
                result = Encoding.Default.GetString(bs, 0, bs.Length);
            }
            catch
            {
                result = "";
            }
            return result;
        }
        public static int HexToInt(string hex)
        {
            int i, res;
            char ch;
            res = 0;
            for (i = 0; i < hex.Length; i++)
            {
                ch = hex[i];
                if (ch >= '0' && ch <= '9')
                {
                    res = res * 16 + (int)(Encoding.Default.GetBytes(ch.ToString())[0]) - (int)(Encoding.Default.GetBytes("0")[0]);
                }
                else if (ch >= 'A' && ch <= 'F')
                {
                    res = res * 16 + (int)(Encoding.Default.GetBytes(ch.ToString())[0]) - (int)(Encoding.Default.GetBytes("A")[0]) + 10;
                }
                else if (ch >= 'a' && ch <= 'f')
                {
                    res = res * 16 + (int)(Encoding.Default.GetBytes(ch.ToString())[0]) - (int)(Encoding.Default.GetBytes("a")[0]);
                }
                else
                {
                    res = 0;
                }
            }
            return res;
        }
    }
}