using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace COMM
{
    public class CommMethod
    {
        #region  MD5加密字符串
        /// <summary>
        /// MD5加密字符串
        /// </summary>
        /// <param name="strPwd">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string strPwd)
        {
            byte[] result = Encoding.Default.GetBytes(strPwd);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }
        #endregion

        //#region 硬键盘输入英文字母
        ///// <summary>
        ///// 硬键盘输入英文字母
        ///// </summary>
        ///// <param name="txtcode"></param>
        ///// <param name="timer1"></param>
        ///// <param name="e"></param>
        ///// <param name="lblInputMethod"></param>
        //public static void InputConvert(TextBox txtcode, Timer timer1, KeyPressEventArgs e, Label lblInputMethod)
        //{
        //    timer1.Enabled = false;
        //    string txtSelected = txtcode.SelectedText;
        //    string inputMethod = lblInputMethod.Text;
        //    switch (e.KeyChar)
        //    {
        //        case '0':
        //        case '1':
        //            txtcode.Text += e.KeyChar.ToString();
        //            txtcode.Select(txtcode.Text.Length, 0);
        //            e.Handled = true;
        //            break;
        //        case '2':
        //        case '3':
        //        case '4':
        //        case '5':
        //        case '6':
        //            switch (inputMethod)
        //            {
        //                case "123":
        //                    txtcode.Text += e.KeyChar.ToString();
        //                    txtcode.Select(txtcode.Text.Length, 0);
        //                    break;
        //                case "abc":
        //                    if (txtSelected == "")
        //                        txtcode.Text += e.KeyChar.ToString();
        //                    else
        //                    {
        //                        if(txtSelected==e.KeyChar.ToString())
        //                            txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1)
        //                                    + ((char)(((int)e.KeyChar - 50) * 3 + 97)).ToString();
        //                        else if (txtSelected == ((char)(((int)e.KeyChar - 50) * 3 + 97)).ToString())
        //                            txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1)
        //                                    + ((char)(((int)e.KeyChar - 50) * 3 + 98)).ToString();
        //                        else if (txtSelected == ((char)(((int)e.KeyChar - 50) * 3 + 98)).ToString())
        //                            txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1)
        //                                    + ((char)(((int)e.KeyChar - 50) * 3 + 99)).ToString();
        //                        else if (txtSelected == ((char)(((int)e.KeyChar - 50) * 3 + 99)).ToString())
        //                            txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1)
        //                                    + e.KeyChar.ToString();
        //                        else
        //                            txtcode.Text += e.KeyChar.ToString();
        //                    }
        //                    txtcode.Select(txtcode.Text.Length - 1, 1);
        //                    timer1.Enabled = true;
        //                    break;
        //                case "ABC":
        //                    if (txtcode.SelectedText == "")
        //                        txtcode.Text += e.KeyChar.ToString();
        //                    else
        //                    {
        //                        if(txtSelected==e.KeyChar.ToString())
        //                            txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1)
        //                                    + ((char)(((int)e.KeyChar - 50) * 3 + 65)).ToString();
        //                        else if (txtSelected == ((char)(((int)e.KeyChar - 50) * 3 + 65)).ToString())
        //                            txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1)
        //                                    + ((char)(((int)e.KeyChar - 50) * 3 + 66)).ToString();
        //                        else if (txtSelected == ((char)(((int)e.KeyChar - 50) * 3 + 66)).ToString())
        //                            txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1)
        //                                    + ((char)(((int)e.KeyChar - 50) * 3 + 67)).ToString();
        //                        else if (txtSelected == ((char)(((int)e.KeyChar - 50) * 3 + 67)).ToString())
        //                            txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1)
        //                                    + e.KeyChar.ToString();
        //                        else
        //                            txtcode.Text += e.KeyChar.ToString();
        //                    }
        //                    txtcode.Select(txtcode.Text.Length - 1, 1);
        //                    timer1.Enabled = true;
        //                    break;
        //            }
        //            e.Handled = true;
        //            break;
        //        case '7':
        //            switch (inputMethod)
        //            {
        //                case "123":
        //                    txtcode.Text += e.KeyChar.ToString();
        //                    txtcode.Select(txtcode.Text.Length, 0);
        //                    break;
        //                case "abc":
        //                    if (txtSelected == "")
        //                        txtcode.Text += "7";
        //                    else
        //                    {
        //                        switch (txtSelected)
        //                        {
        //                            case "7":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "p";
        //                                break;
        //                            case "p":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "q";
        //                                break;
        //                            case "q":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "r";
        //                                break;
        //                            case "r":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "s";
        //                                break;
        //                            case "s":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "7";
        //                                break;
        //                            default:
        //                                txtcode.Text += "7";
        //                                break;
        //                        }
        //                    }
        //                    txtcode.Select(txtcode.Text.Length - 1, 1);
        //                    timer1.Enabled = true;
        //                    break;
        //                case "ABC":
        //                    if (txtcode.SelectedText == "")
        //                        txtcode.Text += "7";
        //                    else
        //                    {
        //                        switch (txtSelected)
        //                        {
        //                            case "7":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "P";
        //                                break;
        //                            case "P":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "Q";
        //                                break;
        //                            case "Q":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "R";
        //                                break;
        //                            case "R":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "S";
        //                                break;
        //                            case "S":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "7";
        //                                break;
        //                            default:
        //                                txtcode.Text += "7";
        //                                break;
        //                        }
        //                    }
        //                    txtcode.Select(txtcode.Text.Length - 1, 1);
        //                    timer1.Enabled = true;
        //                    break;
        //            }
        //            e.Handled = true;
        //            break;
        //        case '8':
        //            switch (inputMethod)
        //            {
        //                case "123":
        //                    txtcode.Text += e.KeyChar.ToString();
        //                    txtcode.Select(txtcode.Text.Length, 0);
        //                    break;
        //                case "abc":
        //                    if (txtSelected == "")
        //                        txtcode.Text += "8";
        //                    else
        //                    {
        //                        switch (txtSelected)
        //                        {
        //                            case "8":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "t";
        //                                break;
        //                            case "t":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "u";
        //                                break;
        //                            case "u":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "v";
        //                                break;
        //                            case "v":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "8";
        //                                break;
        //                            default:
        //                                txtcode.Text += "8";
        //                                break;
        //                        }
        //                    }
        //                    txtcode.Select(txtcode.Text.Length - 1, 1);
        //                    timer1.Enabled = true;
        //                    break;
        //                case "ABC":
        //                    if (txtcode.SelectedText == "")
        //                        txtcode.Text += "8";
        //                    else
        //                    {
        //                        switch (txtSelected)
        //                        {
        //                            case "8":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "T";
        //                                break;
        //                            case "T":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "U";
        //                                break;
        //                            case "U":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "V";
        //                                break;
        //                            case "V":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "8";
        //                                break;
        //                            default:
        //                                txtcode.Text += "8";
        //                                break;
        //                        }
        //                    }
        //                    txtcode.Select(txtcode.Text.Length - 1, 1);
        //                    timer1.Enabled = true;
        //                    break;
        //            }
        //            e.Handled = true;
        //            break;
        //        case '9':
        //            switch (inputMethod)
        //            {
        //                case "123":
        //                    txtcode.Text += e.KeyChar.ToString();
        //                    txtcode.Select(txtcode.Text.Length, 0);
        //                    break;
        //                case "abc":
        //                    if (txtSelected == "")
        //                        txtcode.Text += "9";
        //                    else
        //                    {
        //                        switch (txtSelected)
        //                        {
        //                            case "9":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "w";
        //                                break;
        //                            case "w":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "x";
        //                                break;
        //                            case "x":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "y";
        //                                break;
        //                            case "y":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "z";
        //                                break;
        //                            case "z":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "9";
        //                                break;
        //                            default:
        //                                txtcode.Text += "9";
        //                                break;
        //                        }
        //                    }
        //                    txtcode.Select(txtcode.Text.Length - 1, 1);
        //                    timer1.Enabled = true;
        //                    break;
        //                case "ABC":
        //                    if (txtcode.SelectedText == "")
        //                        txtcode.Text += "9";
        //                    else
        //                    {
        //                        switch (txtSelected)
        //                        {
        //                            case "9":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "W";
        //                                break;
        //                            case "W":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "X";
        //                                break;
        //                            case "X":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "Y";
        //                                break;
        //                            case "Y":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "Z";
        //                                break;
        //                            case "Z":
        //                                txtcode.Text = txtcode.Text.Substring(0, txtcode.Text.Length - 1) + "9";
        //                                break;
        //                            default:
        //                                txtcode.Text += "9";
        //                                break;
        //                        }
        //                    }
        //                    txtcode.Select(txtcode.Text.Length - 1, 1);
        //                    timer1.Enabled = true;
        //                    break;
        //            }
        //            e.Handled = true;
        //            break;
        //        case '#':
        //            switch (inputMethod)
        //            {
        //                case "123":
        //                    lblInputMethod.Text = "ABC";
        //                    break;
        //                //case "123":
        //                //    lblInputMethod.Text = "abc";
        //                //    break;
        //                //case "abc":
        //                //    lblInputMethod.Text = "ABC";
        //                //    break;
        //                case "ABC":
        //                    lblInputMethod.Text = "123";
        //                    break;
        //            }
        //            e.Handled = true;
        //            break;
        //    }
        //}
        //#endregion

        #region 隐藏/显示输入法软键盘
        [DllImport("coredll.dll")]
        private static extern uint SipShowIM(long flags);
        [DllImport("coredll.dll", SetLastError = true)]
        private static extern IntPtr ImmGetContext(IntPtr hWnd);
        [System.Runtime.InteropServices.DllImport("coredll.dll")]
        private static extern bool ImmSetOpenStatus(IntPtr hIMC, bool fOpen);
        [System.Runtime.InteropServices.DllImport("coredll.dll")]
        private static extern IntPtr ImmReleaseContext(IntPtr hWnd, IntPtr context);
        /// <summary>
        /// 隐藏/显示输入法软键盘
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="show"></param>
        public static void ShowHideImm(IntPtr hWnd, bool show)
        {
            if (show)
            {
                SipShowIM(1);
                IntPtr himc = ImmGetContext(hWnd);
                bool ret = ImmSetOpenStatus(himc, true);
            }
            else
            {
                SipShowIM(0);
                IntPtr himc = ImmGetContext(hWnd);
                bool ret = ImmSetOpenStatus(himc, false);
                ImmReleaseContext(hWnd, himc);
            }
        }
        #endregion

        #region 取字符串拼音首字母
        /// <summary>
        /// 取字符串拼音首字母
        /// </summary>
        /// <param name="IndexTxt"></param>
        /// <returns></returns>
        public static String GetIndexCode(String IndexTxt)
        {
            String _Temp = null;
            for (int i = 0; i < IndexTxt.Length; i++)
                _Temp = _Temp + GetOneIndex(IndexTxt.Substring(i, 1));
            return _Temp;
        }


        //得到单个字符的首字母 
        private static String GetOneIndex(String OneIndexTxt)
        {
            if (Convert.ToChar(OneIndexTxt) >= 0 && Convert.ToChar(OneIndexTxt) < 256)
                return OneIndexTxt;
            else
            {
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] unicodeBytes = Encoding.Unicode.GetBytes(OneIndexTxt);
                byte[] gb2312Bytes = Encoding.Convert(Encoding.Unicode, gb2312, unicodeBytes);
                return GetX(Convert.ToInt32(
                String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[0]) - 160)
                + String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[1]) - 160)
                ));
            }

        }


        //根据区位得到首字母 
        private static String GetX(int GBCode)
        {
            if (GBCode >= 1601 && GBCode < 1637) return "A";
            if (GBCode >= 1637 && GBCode < 1833) return "B";
            if (GBCode >= 1833 && GBCode < 2078) return "C";
            if (GBCode >= 2078 && GBCode < 2274) return "D";
            if (GBCode >= 2274 && GBCode < 2302) return "E";
            if (GBCode >= 2302 && GBCode < 2433) return "F";
            if (GBCode >= 2433 && GBCode < 2594) return "G";
            if (GBCode >= 2594 && GBCode < 2787) return "H";
            if (GBCode >= 2787 && GBCode < 3106) return "J";
            if (GBCode >= 3106 && GBCode < 3212) return "K";
            if (GBCode >= 3212 && GBCode < 3472) return "L";
            if (GBCode >= 3472 && GBCode < 3635) return "M";
            if (GBCode >= 3635 && GBCode < 3722) return "N";
            if (GBCode >= 3722 && GBCode < 3730) return "O";
            if (GBCode >= 3730 && GBCode < 3858) return "P";
            if (GBCode >= 3858 && GBCode < 4027) return "Q";
            if (GBCode >= 4027 && GBCode < 4086) return "R";
            if (GBCode >= 4086 && GBCode < 4390) return "S";
            if (GBCode >= 4390 && GBCode < 4558) return "T";
            if (GBCode >= 4558 && GBCode < 4684) return "W";
            if (GBCode >= 4684 && GBCode < 4925) return "X";
            if (GBCode >= 4925 && GBCode < 5249) return "Y";
            if (GBCode >= 5249 && GBCode <= 5589) return "Z";
            if (GBCode >= 5601 && GBCode <= 8794)
            {
                String CodeData = "cjwgnspgcenegypbtwxzdxykygtpjnmjqmbsgzscyjsyyfpggbzgydywjkgaljswkbjqhyjwpdzlsgmr"
                + "ybywwccgznkydgttngjeyekzydcjnmcylqlypyqbqrpzslwbdgkjfyxjwcltbncxjjjjcxdtqsqzycdxxhgckbphffss"
                + "pybgmxjbbyglbhlssmzmpjhsojnghdzcdklgjhsgqzhxqgkezzwymcscjnyetxadzpmdssmzjjqjyzcjjfwqjbdzbjgd"
                + "nzcbwhgxhqkmwfbpbqdtjjzkqhylcgxfptyjyyzpsjlfchmqshgmmxsxjpkdcmbbqbefsjwhwwgckpylqbgldlcctnma"
                + "eddksjngkcsgxlhzaybdbtsdkdylhgymylcxpycjndqjwxqxfyyfjlejbzrwccqhqcsbzkymgplbmcrqcflnymyqmsqt"
                + "rbcjthztqfrxchxmcjcjlxqgjmshzkbswxemdlckfsydsglycjjssjnqbjctyhbftdcyjdgwyghqfrxwckqkxebpdjpx"
                + "jqsrmebwgjlbjslyysmdxlclqkxlhtjrjjmbjhxhwywcbhtrxxglhjhfbmgykldyxzpplggpmtcbbajjzyljtyanjgbj"
                + "flqgdzyqcaxbkclecjsznslyzhlxlzcghbxzhznytdsbcjkdlzayffydlabbgqszkggldndnyskjshdlxxbcghxyggdj"
                + "mmzngmmccgwzszxsjbznmlzdthcqydbdllscddnlkjyhjsycjlkohqasdhnhcsgaehdaashtcplcpqybsdmpjlpcjaql"
                + "cdhjjasprchngjnlhlyyqyhwzpnccgwwmzffjqqqqxxaclbhkdjxdgmmydjxzllsygxgkjrywzwyclzmcsjzldbndcfc"
                + "xyhlschycjqppqagmnyxpfrkssbjlyxyjjglnscmhcwwmnzjjlhmhchsyppttxrycsxbyhcsmxjsxnbwgpxxtaybgajc"
                + "xlypdccwqocwkccsbnhcpdyznbcyytyckskybsqkkytqqxfcwchcwkelcqbsqyjqcclmthsywhmktlkjlychwheqjhtj"
                + "hppqpqscfymmcmgbmhglgsllysdllljpchmjhwljcyhzjxhdxjlhxrswlwzjcbxmhzqxsdzpmgfcsglsdymjshxpjxom"
                + "yqknmyblrthbcftpmgyxlchlhlzylxgsssscclsldclepbhshxyyfhbmgdfycnjqwlqhjjcywjztejjdhfblqxtqkwhd"
                + "chqxagtlxljxmsljhdzkzjecxjcjnmbbjcsfywkbjzghysdcpqyrsljpclpwxsdwejbjcbcnaytmgmbapclyqbclzxcb"
                + "nmsggfnzjjbzsfqyndxhpcqkzczwalsbccjxpozgwkybsgxfcfcdkhjbstlqfsgdslqwzkxtmhsbgzhjcrglyjbpmljs"
                + "xlcjqqhzmjczydjwbmjklddpmjegxyhylxhlqyqhkycwcjmyhxnatjhyccxzpcqlbzwwwtwbqcmlbmynjcccxbbsnzzl"
                + "jpljxyztzlgcldcklyrzzgqtgjhhgjljaxfgfjzslcfdqzlclgjdjcsnclljpjqdcclcjxmyzftsxgcgsbrzxjqqcczh"
                + "gyjdjqqlzxjyldlbcyamcstylbdjbyregklzdzhldszchznwczcllwjqjjjkdgjcolbbzppglghtgzcygezmycnqcycy"
                + "hbhgxkamtxyxnbskyzzgjzlqjdfcjxdygjqjjpmgwgjjjpkjsbgbmmcjssclpqpdxcdyykypcjddyygywchjrtgcnyql"
                + "dkljczzgzccjgdyksgpzmdlcphnjafyzdjcnmwescsglbtzcgmsdllyxqsxsbljsbbsgghfjlwpmzjnlyywdqshzxtyy"
                + "whmcyhywdbxbtlmswyyfsbjcbdxxlhjhfpsxzqhfzmqcztqcxzxrdkdjhnnyzqqfnqdmmgnydxmjgdhcdycbffallztd"
                + "ltfkmxqzdngeqdbdczjdxbzgsqqddjcmbkxffxmkdmcsychzcmljdjynhprsjmkmpcklgdbqtfzswtfgglyplljzhgjj"
                + "gypzltcsmcnbtjbhfkdhbyzgkpbbymtdlsxsbnpdkleycjnycdykzddhqgsdzsctarlltkzlgecllkjljjaqnbdggghf"
                + "jtzqjsecshalqfmmgjnlyjbbtmlycxdcjpldlpcqdhsycbzsckbzmsljflhrbjsnbrgjhxpdgdjybzgdlgcsezgxlblg"
                + "yxtwmabchecmwyjyzlljjshlgndjlslygkdzpzxjyyzlpcxszfgwyydlyhcljscmbjhblyjlycblydpdqysxktbytdkd"
                + "xjypcnrjmfdjgklccjbctbjddbblblcdqrppxjcglzcshltoljnmdddlngkaqakgjgyhheznmshrphqqjchgmfprxcjg"
                + "dychghlyrzqlcngjnzsqdkqjymszswlcfqjqxgbggxmdjwlmcrnfkkfsyyljbmqammmycctbshcptxxzzsmphfshmclm"
                    + "ldjfyqxsdyjdjjzzhqpdszglssjbckbxyqzjsgpsxjzqznqtbdkwxjkhhgflbcsmdldgdzdblzkycqnncsybzbfglzzx"
                + "swmsccmqnjqsbdqsjtxxmbldxcclzshzcxrqjgjylxzfjphymzqqydfqjjlcznzjcdgzygcdxmzysctlkphtxhtlbjxj"
                + "lxscdqccbbqjfqzfsltjbtkqbsxjjljchczdbzjdczjccprnlqcgpfczlclcxzdmxmphgsgzgszzqjxlwtjpfsyaslcj"
                + "btckwcwmytcsjjljcqlwzmalbxyfbpnlschtgjwejjxxglljstgshjqlzfkcgnndszfdeqfhbsaqdgylbxmmygszldyd"
                + "jmjjrgbjgkgdhgkblgkbdmbylxwcxyttybkmrjjzxqjbhlmhmjjzmqasldcyxyqdlqcafywyxqhz";
                String _gbcode = GBCode.ToString();
                int pos = (Convert.ToInt16(_gbcode.Substring(0, 2)) - 56) * 94 + Convert.ToInt16(_gbcode.Substring(_gbcode.Length - 2, 2));
                return CodeData.Substring(pos - 1, 1);
            }
            return " ";
        }
        #endregion


    }
}
