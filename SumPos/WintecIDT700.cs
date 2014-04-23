using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace SumPos
{
    public class WintecIDT700
    {
        #region 打印
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

        [DllImport("WintecIDT700.dll", EntryPoint = "CheckMSRIO")]
        public static extern int CheckMSRIO();//检测是否刷卡

        [DllImport("WintecIDT700.dll", EntryPoint = "ReadMSRData")]
        public static extern int ReadMSRData(byte[] buffer);//新读取数据

        [DllImport("WintecIDT700.dll", EntryPoint = "OpenMSR")]
        public static extern int OpenMSR();
        [DllImport("WintecIDT700.dll", EntryPoint = "CloseMSR")]
        public static extern int CloseMSR();


        [DllImport("WintecIDT700.dll", EntryPoint = "Init_msr")]
        public static extern int Init_msr();//初始化刷卡器
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
        public static extern int GPRSRESET(int restime);

        public static bool GPRSConnect()
        {
            bool successed = false;
            //for (int i = 0; i < 3; i++)
            //{
            //    successed = RAS.Dial(AppConfig.ConfigInfo.Network, AppConfig.ConfigInfo.uid, AppConfig.ConfigInfo.pwd);
            //    if (!successed)
            //    {
            //        GPRSRESET(3000);
            //        System.Threading.Thread.Sleep(5000);
            //    }
            //    else
            //        break;
            //}
            return successed;
        }
        #region 射频卡
        /// <summary>
        /// 打开串口。
        /// </summary>
        /// <param name="Com">端口号COM1~COM6：1~6</param>
        /// <param name="BaudRate">波特率</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "OpenComm")]
        public static extern IntPtr OpenComm(int nCom, int BaudRate);

        /// <summary>
        /// 寻找Rfid卡。
        /// </summary>
        /// <param name="hCom">端口句柄</param>
        /// <param name="Com">端口号COM1~COM6：1~6</param>
        /// <param name="address">address=0</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "RF_SearchCard")]
        public static extern int RF_SearchCard(IntPtr hCom, byte nCom, int address);

        /// <summary>
        /// 关闭串口。
        /// </summary>
        /// <param name="hCom">端口句柄</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "CloseComm")]
        public static extern bool CloseComm(IntPtr hCom);

        /// <summary>
        /// 串口发送数据。
        /// </summary>
        /// <param name="nCom">端口号COM1~COM6：1~6</param>
        /// <param name="address">address=0</param>
        /// <param name="nCmd">0x15 命令符</param>
        /// <param name="aBuf">byte aBuf[4];aBuf[0] = 0x03 数据域</param>
        /// <param name="nDatelen">1数据于大小</param>
        /// <param name="?">null  返回值</param>
        /// <param name="nAckLen">0 返回字节数</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "RF_SendCmd")]
        public static extern int RF_SendCmd(byte nCom, int address, byte nCmd, Byte[] aBuf, byte nDatelen, Byte[] pAck, byte nAckLen);
        /// <summary>
        /// search S50, S70 card。
        /// 寻找Rfid卡。
        /// </summary>
        /// <param name="Com">端口号COM1~COM6：1~6</param>
        /// <param name="address">address=0</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "RF_SearchCard")]
        public static extern int RF_SearchCard(byte nCom, int address);

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="nCom">端口号COM1~COM6：1~6</param>
        /// <param name="nAddress">0</param>
        /// <param name="nBlock">块号RF_MAKE_BLOCK(0,0)  0</param>
        /// <param name="Password">null</param>
        /// <param name="cPwdFlags"> 密码A 0x60，密码B 0x61</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "RF_Authentication")]
        public static extern int RF_Authentication(byte nCom, int nAddress, byte nBlock, byte[] Password, byte cPwdFlags);

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="nCom"></param>
        /// <param name="adress"></param>
        /// <param name="nBlock"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "RF_ReadBlock")]
        public static extern int RF_ReadBlock(byte nCom, int adress, byte nBlock, byte[] buffer);


        public static byte RF_MAKE_BLOCK(int Sector, int Block)
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
        #endregion
    }
}
