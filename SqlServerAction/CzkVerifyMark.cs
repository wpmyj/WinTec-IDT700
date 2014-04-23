using System;
using System.Collections.Generic;
using System.Text;

    public class CzkVerifyMark
    {
        public string VerifyMark(string cardno, float total)
        {
            Des d = new Des();
            string s1, s2, s3;
            string fillChar = Encoding.Default.GetString(new byte[] { Convert.ToByte(48) });
            s1=cardno+UFillRightString(total.ToString(),8,fillChar);
            s2 = UFillRightString(cardno, 8, fillChar);
            s3 = UFillRightString(GetNextNumber(cardno), 8, fillChar);
            return d.EncryStrHex(d.EncryStrHex(s1,s2),s3);
        }

        public string UFillRightString(string s,int l,string fillChar)
        {
            return UFillString(s,l,fillChar,"R");
        }

        public string UFillString(string s,int l,string fillChar,string fillMode)
        {
            string fillString="";
            int i;

            if (fillMode.ToUpper() != "R"&&fillMode.ToUpper()!="L")
            {
                return s;
            }
            if (s.Length >= l)
            {
                return s.Substring(0, l);
            }
            for (i = 0; i < l - s.Length; i++)
            {
                fillString = fillString + fillChar;
            }
            if (fillMode.ToUpper()== "R")
            {
                return s + fillString;
            }
            else
            {
                return fillString + s;
            }
        }



        public string GetNextNumber(string szNumber)
        {
            int nCarry, i;
            byte[] Result = Encoding.Default.GetBytes(szNumber);

            nCarry = 1;
            i = szNumber.Length;

            while (nCarry == 1 && i > 0)
            {
                if (Result[i - 1] != Convert.ToByte('9'))
                {
                    Result[i - 1] = Convert.ToByte((int)Result[i - 1] + 1);
                    nCarry = 0;
                }
                else
                {
                    Result[i - 1] = Convert.ToByte('0');
                    i = i - 1;
                }
            }

            return Encoding.Default.GetString(Result);

        }
    }
