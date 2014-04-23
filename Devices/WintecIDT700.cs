using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Devices
{
    public class WintecIDT700
    {
        #region ��ӡ
        /// <summary>
        /// ��ӡLOGO
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
        /// ��ӡ�ַ�������
        /// </summary>
        /// <param name="str">Ҫ��ӡ���ַ�������</param>
        /// <param name="linecount">Ҫ��ӡ������</param>
        /// <param name="mode">ģʽ,1������2����3���ߣ�4������</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "PrintLineStr")]
        public static extern int PrintLineStr(string[] str, int linecount, int mode);

        /// <summary>
        /// ��ӡ�ַ�������(unioncode)
        /// </summary>
        /// <param name="str">Ҫ��ӡ���ַ�������</param>
        /// <param name="linecount">Ҫ��ӡ������</param>
        /// <param name="mode">ģʽ,1������2����3���ߣ�4������</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "PrintUnicodeStr")]
        public static extern int PrintUnicodeStr(string[] str, int linecount, int mode);

        /// <summary>
        /// ��ֽ��ֽ
        /// </summary>
        /// <param name="dots">��ֽ����</param>
        /// <returns>0�����</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "FeedPaper")]
        public static extern int FeedPaper(int dots);

        /// <summary>
        /// ����ӡ��״̬
        /// </summary>
        /// <returns>0 ���� 1ȱֽ 2�¶ȹ���</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "ReadPrinterState")]
        public static extern short ReadPrinterState();

        /// <summary>
        /// ��ӡcode39����
        /// </summary>
        /// <param name="str">Ҫ��ӡ������</param>
        /// <param name="high">�߶�</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "prn_BarCode39")]
        public static extern int prn_BarCode39(byte[] str, byte high);

        /// <summary>
        /// ��ӡupca����
        /// </summary>
        /// <param name="str">Ҫ��ӡ������</param>
        /// <param name="high">�߶�</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "prn_BarCodeUPCA")]
        public static extern int prn_BarCodeUPCA(byte[] str, byte high);

        /// <summary>
        /// ��ӡEAN����
        /// </summary>
        /// <param name="str">Ҫ��ӡ������</param>
        /// <param name="high">�߶�</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "prn_BarCodeEAN")]
        public static extern int prn_BarCodeEAN(byte[] str, byte high);

        /// <summary>
        /// ��ӡλͼ
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="dHeight"></param>
        /// <param name="dWidth"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "PrintBitmap")]
        public static extern int PrintBitmap(byte[] bmp, int dHeight, int dWidth, int pos);

        /// <summary>
        /// ��ӡλͼ�ļ�
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "PrintImageFile")]
        public static extern int PrintImageFile(byte[] filepath, int pos);

        #endregion ��ӡ

        #region flash������
        /// <summary>
        /// ��flash��
        /// </summary>
        /// <param name="pagAddr">ҳ</param>
        /// <param name="bufAddr">��ַ</param>
        /// <param name="sndBuf">����</param>
        /// <param name="len">��ȡ����</param>
        [DllImport("WintecIDT700.dll", EntryPoint = "RdMemPag")]
        public static extern void RdMemPag(short pagAddr, short bufAddr, byte[] sndBuf, short len);

        /// <summary>
        /// д��
        /// </summary>
        /// <param name="pagAddr">ҳ</param>
        /// <param name="bufAddr">��ַ</param>
        /// <param name="sndBuf">����</param>
        /// <param name="len">д����</param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "WrMemPag")]
        public static extern byte WrMemPag(short pagAddr, short bufAddr, byte[] sndBuf, short len);

        /// <summary>
        /// �����û�п�
        /// </summary>
        /// <returns>1:have card 0:no card</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "Fspi_sw")]
        public static extern byte Fspi_sw();

        #endregion flash������

        #region �߼����ܿ�����
        /// <summary>
        /// �����û�п�
        /// </summary>
        /// <returns>1:have card 0:no card</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "Espi_sw")]
        public static extern byte Espi_sw();

        /// <summary>
        /// �����洢��
        /// </summary>
        /// <param name="addrFrom"></param>
        /// <param name="buffer"></param>
        /// <param name="ulen"></param>
        [DllImport("WintecIDT700.dll", EntryPoint = "ReadMainMemory")]
        public static extern void ReadMainMemory(byte addrFrom, byte[] buffer, short ulen);

        /// <summary>
        /// д���洢��
        /// </summary>
        /// <param name="addrFrom"></param>
        /// <param name="buffer"></param>
        /// <param name="ulen"></param>
        [DllImport("WintecIDT700.dll", EntryPoint = "UpdateMainMemory")]
        public static extern void UpdateMainMemory(byte addrFrom, byte[] buffer, short ulen);

        /// <summary>
        /// �������洢��
        /// </summary>
        /// <param name="rdBuf"></param>
        [DllImport("WintecIDT700.dll", EntryPoint = "ReadProtectedMem")]
        public static extern void ReadProtectedMem(byte[] rdBuf);

        /// <summary>
        /// д�����洢��
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="value"></param>
        [DllImport("WintecIDT700.dll", EntryPoint = "WriteProtectedMem")]
        public static extern void WriteProtectedMem(byte addr, byte value);

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "CompareVerifData")]
        public static extern byte CompareVerifData(byte[] value);

        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "UpdateVerifData")]
        public static extern byte UpdateVerifData(byte[] oldval, byte newval);

        #endregion �߼����ܿ�����

        #region RFID������
        /// <summary>
        /// search S50, S70 card��
        /// Ѱ��Rfid����
        /// </summary>
        /// <param name="Com">�˿ں�COM1~COM6��1~6</param>
        /// <param name="address">address=0</param>
        /// <returns></returns>
        [DllImport("IDTDevice.dll", EntryPoint = "RF_SearchCard")]
        public static extern int RF_SearchCard(byte nCom, int address);
        #endregion

        #region �绰ģ��
        /// <summary>
        /// �����Ͳ�Ƿ�����
        /// </summary>
        /// <returns>1:headphones signal 0:no signal</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "CheckSHK")]
        public static extern bool CheckSHK();

        /// <summary>
        /// �Ƿ��а�������
        /// </summary>
        /// <returns> 1or0</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "CheckDV")]
        public static extern bool CheckDV();

        /// <summary>
        /// Ƭѡ����ģ��
        /// </summary>
        /// <param name="cRing">1or0</param>
        [DllImport("WintecIDT700.dll", EntryPoint = "TelCS")]
        public static extern void TelCS(char cRing);

        /// <summary>
        /// ������
        /// </summary>
        [DllImport("WintecIDT700.dll", EntryPoint = "TelRing")]
        public static extern void TelRing();

        /// <summary>
        /// ��ȡ���Ű���������绰ʱ��
        /// </summary>
        /// <returns>tel number</returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "TelGetDTMF")]
        public static extern char TelGetDTMF();

        #endregion �绰ģ��

        #region MSR
        [DllImport("WintecIDT700.dll", EntryPoint = "ReadMSR")]
        public static extern int ReadMSR(byte[] buffer, int waittime);

        [DllImport("WintecIDT700.dll", EntryPoint = "MSR_INIT")]
        public static extern int MSR_INIT();//��ʼ��ˢ����

        [DllImport("WintecIDT700.dll", EntryPoint = "CheckMSRIO")]
        public static extern int CheckMSRIO();//����Ƿ�ˢ��

        [DllImport("WintecIDT700.dll", EntryPoint = "ReadMSRData")]
        public static extern int ReadMSRData(byte[] buffer);//�¶�ȡ����

        #endregion

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="ms">����ʱ��</param>
        [DllImport("WintecIDT700.dll", EntryPoint = "Beep")]
        public static extern void Beep(int ms);

        /// <summary>
        /// ״ָ̬ʾ��
        /// </summary>
        /// <param name="state">1�� 0��</param>
        [DllImport("WintecIDT700.dll", EntryPoint = "StateLed")]
        public static extern void StateLed(int state);

        /// <summary>
        /// ��Ǯ��
        /// </summary>
        /// <returns></returns>
        [DllImport("WintecIDT700.dll", EntryPoint = "DrawerOpen")]
        public static extern int DrawerOpen();

        //gprsģ������
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
