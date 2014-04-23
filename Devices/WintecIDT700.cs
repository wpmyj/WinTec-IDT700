using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Devices
{
    public class WintecIDT700
    {
        #region 打印
        /// <summary>
        /// 打印LOGO
        /// </summary>
        public static void PrintLogo()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            System.IO.Stream stream = asm.GetManifestResourceStream(asm.GetName().Name + ".logo.bmp");

            if (stream == null)
                return;

            byte[] bmp = new byte[stream.Length];
            stream.Read(bmp, 0, bmp.Length);

            int width = (int)bmp[18];//get bmp width
            int height = (int)bmp[22];//get bmp height
            int bmpDataPos = (int)bmp[10];//get bmp data start position

            byte[] printData = new byte[bmp.Length - bmpDataPos];

            System.Array.Copy(bmp, bmpDataPos, printData, 0, printData.Length);

            WintecIDT700.PrintBitmap(printData, height, width, 12);

            //WintecIDT700.FeedPaper(50);
        }
        /// <summary>
        /// 打印字符串数组
        /// </summary>
        /// <param name="str">要打印的字符串数组</param>
        /// <param name="linecount">要打印的行数</param>
        /// <param name="mode">模式,1正常；2倍宽；3倍高；4倍宽倍高</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "PrintLineStr")]
        public static extern int PrintLineStr(string[] str, int linecount, int mode);

        /// <summary>
        /// 打印字符串数组(unioncode)
        /// </summary>
        /// <param name="str">要打印的字符串数组</param>
        /// <param name="linecount">要打印的行数</param>
        /// <param name="mode">模式,1正常；2倍宽；3倍高；4倍宽倍高</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "PrintUnicodeStr")]
        public static extern int PrintUnicodeStr(string[] str, int linecount, int mode);

        /// <summary>
        /// 进纸退纸
        /// </summary>
        /// <param name="dots">走纸点数</param>
        /// <returns>0：完成</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "FeedPaper")]
        public static extern int FeedPaper(int dots);

        /// <summary>
        /// 读打印机状态
        /// </summary>
        /// <returns>0 正常 1缺纸 2温度过高</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "ReadPrinterState")]
        public static extern short ReadPrinterState();

        /// <summary>
        /// 打印code39条码
        /// </summary>
        /// <param name="str">要打印的内容</param>
        /// <param name="high">高度</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "prn_BarCode39")]
        public static extern int prn_BarCode39(byte[] str, byte high);

        /// <summary>
        /// 打印upca条码
        /// </summary>
        /// <param name="str">要打印的内容</param>
        /// <param name="high">高度</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "prn_BarCodeUPCA")]
        public static extern int prn_BarCodeUPCA(byte[] str, byte high);

        /// <summary>
        /// 打印EAN条码
        /// </summary>
        /// <param name="str">要打印的内容</param>
        /// <param name="high">高度</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "prn_BarCodeEAN")]
        public static extern int prn_BarCodeEAN(byte[] str, byte high);

        /// <summary>
        /// 打印位图
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="dHeight"></param>
        /// <param name="dWidth"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "PrintBitmap")]
        public static extern int PrintBitmap(byte[] bmp, int dHeight, int dWidth, int pos);

        /// <summary>
        /// 打印位图文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "PrintImageFile")]
        public static extern int PrintImageFile(byte[] filepath, int pos);

        #endregion 打印

        #region flash卡操作
        /// <summary>
        /// 读flash卡
        /// </summary>
        /// <param name="pagAddr">页</param>
        /// <param name="bufAddr">地址</param>
        /// <param name="sndBuf">内容</param>
        /// <param name="len">读取长度</param>
        [DllImport("WintecIDT700.dll", EntryPoint = "RdMemPag")]
        public static extern void RdMemPag(short pagAddr, short bufAddr, byte[] sndBuf, short len);

        /// <summary>
        /// 写卡
        /// </summary>
        /// <param name="pagAddr">页</param>
        /// <param name="bufAddr">地址</param>
        /// <param name="sndBuf">内容</param>
        /// <param name="len">写长度</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "WrMemPag")]
        public static extern byte WrMemPag(short pagAddr, short bufAddr, byte[] sndBuf, short len);

        /// <summary>
        /// 检查有没有卡
        /// </summary>
        /// <returns>1:have card 0:no card</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "Fspi_sw")]
        public static extern byte Fspi_sw();

        #endregion flash卡操作

        #region 逻辑加密卡操作
        /// <summary>
        /// 检查有没有卡
        /// </summary>
        /// <returns>1:have card 0:no card</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "Espi_sw")]
        public static extern byte Espi_sw();

        /// <summary>
        /// 读主存储区
        /// </summary>
        /// <param name="addrFrom"></param>
        /// <param name="buffer"></param>
        /// <param name="ulen"></param>
        [DllImport("WintecIDT700.dll", EntryPoint = "ReadMainMemory")]
        public static extern void ReadMainMemory(byte addrFrom, byte[] buffer, short ulen);

        /// <summary>
        /// 写主存储区
        /// </summary>
        /// <param name="addrFrom"></param>
        /// <param name="buffer"></param>
        /// <param name="ulen"></param>
        [DllImport("WintecIDT700.dll", EntryPoint = "UpdateMainMemory")]
        public static extern void UpdateMainMemory(byte addrFrom, byte[] buffer, short ulen);

        /// <summary>
        /// 读保护存储区
        /// </summary>
        /// <param name="rdBuf"></param>
        [DllImport("WintecIDT700.dll", EntryPoint = "ReadProtectedMem")]
        public static extern void ReadProtectedMem(byte[] rdBuf);

        /// <summary>
        /// 写保护存储区
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="value"></param>
        [DllImport("WintecIDT700.dll", EntryPoint = "WriteProtectedMem")]
        public static extern void WriteProtectedMem(byte addr, byte value);

        /// <summary>
        /// 检查密码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "CompareVerifData")]
        public static extern byte CompareVerifData(byte[] value);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "UpdateVerifData")]
        public static extern byte UpdateVerifData(byte[] oldval, byte newval);

        #endregion 逻辑加密卡操作

        #region RFID卡操作
        /// <summary>
        /// search S50, S70 card。
        /// 寻找Rfid卡。
        /// </summary>
        /// <param name="Com">端口号COM1~COM6：1~6</param>
        /// <param name="address">address=0</param>
        /// <returns></returns>
        [DllImport("IDTDevice.dll", EntryPoint = "RF_SearchCard")]
        public static extern int RF_SearchCard(byte nCom, int address);
        #endregion

        #region 电话模块
        /// <summary>
        /// 检查听筒是否拿起
        /// </summary>
        /// <returns>1:headphones signal 0:no signal</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "CheckSHK")]
        public static extern bool CheckSHK();

        /// <summary>
        /// 是否有按键按下
        /// </summary>
        /// <returns> 1or0</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "CheckDV")]
        public static extern bool CheckDV();

        /// <summary>
        /// 片选语音模块
        /// </summary>
        /// <param name="cRing">1or0</param>
        [DllImport("WintecIDT700.dll", EntryPoint = "TelCS")]
        public static extern void TelCS(char cRing);

        /// <summary>
        /// 振铃响
        /// </summary>
        [DllImport("WintecIDT700.dll", EntryPoint = "TelRing")]
        public static extern void TelRing();

        /// <summary>
        /// 获取拨号按键（拨打电话时）
        /// </summary>
        /// <returns>tel number</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "TelGetDTMF")]
        public static extern char TelGetDTMF();

        #endregion 电话模块

        #region MSR
        [DllImport("WintecIDT700.dll", EntryPoint = "ReadMSR")]
        public static extern int ReadMSR(byte[] buffer, int waittime);

        [DllImport("WintecIDT700.dll", EntryPoint = "MSR_INIT")]
        public static extern int MSR_INIT();//初始化刷卡器

        [DllImport("WintecIDT700.dll", EntryPoint = "CheckMSRIO")]
        public static extern int CheckMSRIO();//检测是否刷卡

        [DllImport("WintecIDT700.dll", EntryPoint = "ReadMSRData")]
        public static extern int ReadMSRData(byte[] buffer);//新读取驱动

        #endregion

        /// <summary>
        /// 蜂鸣器
        /// </summary>
        /// <param name="ms">鸣叫时间</param>
        [DllImport("WintecIDT700.dll", EntryPoint = "Beep")]
        public static extern void Beep(int ms);

        /// <summary>
        /// 状态指示灯
        /// </summary>
        /// <param name="state">1亮 0灭</param>
        [DllImport("WintecIDT700.dll", EntryPoint = "StateLed")]
        public static extern void StateLed(int state);

        /// <summary>
        /// 打开钱箱
        /// </summary>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "DrawerOpen")]
        public static extern int DrawerOpen();

        //gprs模块重置
        [DllImport("WintecIDT700.dll", EntryPoint = "GPRSRESET")]
        private static extern int GPRSRESET(int restime);

        //public static bool GPRSConnect()
        //{
        //    bool successed = RAS.Dial("GPRS", Config.ConfigInfo.Networkuid, Config.ConfigInfo.Networkpwd);
        //    if (!successed)
        //    {
        //        GPRSRESET(4000);
        //        GPRSRESET(1000);
        //        System.Threading.Thread.Sleep(15000);
        //        successed = RAS.Dial("GPRS", Config.ConfigInfo.Networkuid, Config.ConfigInfo.Networkpwd);
        //    }
        //    return successed;
        //}
    }
}
