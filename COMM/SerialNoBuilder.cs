using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace COMM
{
    public class SerialNoBuilder
    {
        /// <summary>
        /// 获取流水号
        /// </summary>
        public static void GetSerialNo()
        {
            try
            {
                using (StreamReader sr = new StreamReader(PubGlobal.Envionment.CurrentPath + @"\SerialNO.dat"))
                {
                    string s = sr.ReadLine();
                    sr.Close();
                    string[] ss = s.Split(':');
                    if (ss[0] != DateTime.Now.ToString("yyyyMMdd"))
                    {
                        PubGlobal.BussinessVar.SerialNo = "0001";
                    }
                    else
                    {
                        PubGlobal.BussinessVar.SerialNo = (int.Parse(ss[1]) + 1).ToString().PadLeft(4, '0');
                    }
                }
            }
            catch
            {
                PubGlobal.BussinessVar.SerialNo= "0001";
            }
        }

        /// <summary>
        /// 提交流水号
        /// </summary>
        public static void CommitSerialNo()
        {
            using (StreamWriter sw = new StreamWriter(PubGlobal.Envionment.CurrentPath + @"\SerialNO.dat", false))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyyMMdd:")+PubGlobal.BussinessVar.SerialNo);
                sw.Close();
            }
        }
    }
}
