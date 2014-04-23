using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace Devices
{
    public class RfidPort
    {
        int on_off = 0;//控制指令[0获取卡号；1读扇区；2写块；3钱包初始化；4充值]
        bool isFinishCycle = false;//是否完成循环

        int WriteSuccess = 0;
        bool isOpen = false;//是否已开天线
        bool isISO = false;//是否已设置模块为ios14443 type A
        bool isOk = false;//是否成功
        int DoMulti = 0;

        byte[] bs_Sys = new byte[8] { 0x02, 0x00, 0x00, 0x04, 0x3a, 0x41, 0x7f, 0x03 };//设置模块
        byte[] bs_Open = new byte[9] { 0x02, 0x00, 0x00, 0x04, 0x05, 0x10, 0x02, 0x0B, 0x03 };//开天线
        byte[] bs_Find = new byte[8] { 0x02, 0x00, 0x00, 0x04, 0x46, 0x52, 0x9C, 0x03 };//寻天线区域内所有卡
        byte[] bs_Prevent = new byte[8] { 0x02, 0x00, 0x00, 0x04, 0x47, 0x04, 0x4F, 0x03 };//防冲突
        byte[] bs_Close = new byte[8] { 0x02, 0x00, 0x00, 0x04, 0x05, 0x00, 0x09, 0x03 };//关闭天线

        List<byte[]> nWriteContent = new List<byte[]>();//16字节要写入的数据,存放位置为1—17,0位存放绝对块号
        byte[] WriteConten = new byte[17];//16字节要写入的数据,存放位置为1—17,0位存放绝对块号
        byte[] KeyContent = new byte[7];//存放1字节绝对块号+6字节密钥，,0位存放绝对块号
        byte keyType = 0x60;//初始化为A密钥验证模式【A密钥为0x60，B密钥为0x61】

        List<byte[]> bs_nGetWritekey = new List<byte[]>();//获取读块指令组

        List<byte> nReadBlock = new List<byte>();//要读取的块号
        List<byte[]> bs_nGetReadkey = new List<byte[]>();//获取读块指令组

        byte[] ReadContent = new byte[16];//存放16字节读取的数据
        List<byte[]> nReadContent = new List<byte[]>();//存放一组读取的数据

        string CardId1 = "";//卡号

        string MonyeCount = "";
        byte[] InitMoney = new byte[5];//存放1字节绝对块号+4字节初始化钱包值【低字节在前】，0位存放绝对块号
        byte[] AddMoney = new byte[5];//存放1字节绝对块号+4字节充值金额【低字节在前】，0位存放绝对块号
        byte[] subMoney = new byte[5];//存放1字节绝对块号+4字节扣款金额【低字节在前】，0位存放绝对块号
        

        SerialPort port = new SerialPort();
        public RfidPort(string nCom)
        {
            //串口设置
            port.BaudRate = 19200;
            port.DataBits = 8;
            port.PortName = nCom;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;

            if (!port.IsOpen)
            {
                port.Open();
            }
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            while (!isISO)//设置模块为ios14443 type A
            {
                port.Write(bs_Sys, 0, bs_Sys.Length);
                Thread.Sleep(100);
            }
            while (!isOpen)//开天线
            {
                port.Write(bs_Open, 0, bs_Open.Length);
                Thread.Sleep(100);
            }
        }

        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(10);
            try
            {
                byte[] receiveData = new byte[port.BytesToRead];
                port.Read(receiveData, 0, port.BytesToRead);
                if (receiveData[4] == 0x03 && receiveData[5] == 0x3a)//设置模块为ios14443 type A
                {
                    isISO = true;
                }
                if (receiveData[4] == 0x03 && receiveData[5] == 0x05)//开天线
                {
                    isOpen = true;
                }
                else if (receiveData[4] == 0x46 && receiveData[5] == 0x00)//寻卡成功，则发送防冲突指令，以取得卡号
                {
                    port.Write(bs_Prevent, 0, bs_Prevent.Length);//发送防冲突指令
                }
                else if (receiveData[4] == 0x47 && receiveData[5] == 0x00)//防冲突成功，选择卡片
                {
                    if(on_off==0)
                    {
                        GetCard(receiveData);//获取卡号
                        isFinishCycle = true;
                    }
                    else if (on_off!=0)
                    {
                        byte[] bs_Choose = DoReceiveData(receiveData);
                        port.Write(bs_Choose, 0, bs_Choose.Length);//发送选卡指令
                    }                    
                }
                else if (receiveData[4] == 0x48 && receiveData[5] == 0x00)//选卡成功成功,验证密钥
                {                   
                    byte[] bs_ReadSection = GetSectionkey();
                    port.Write(bs_ReadSection, 0, bs_ReadSection.Length);//发送验证密匙
                }
                else if (receiveData[5] == 0x4a && receiveData[6] == 0x00)//密钥验证成功，发送读写卡指令
                {
                    if(on_off==1)
                    {
                        if (DoMulti > 0)
                        {
                            DoMulti--;
                            nGetReadkey();
                            byte[] bs_ReadKey = bs_nGetReadkey[0];
                            bs_nGetReadkey.RemoveAt(0);
                            port.Write(bs_ReadKey, 0, bs_ReadKey.Length);//发送读扇区指令
                        }
                        else
                        {
                            byte[] bs_ReadKey = GetReadkey();
                            port.Write(bs_ReadKey, 0, bs_ReadKey.Length);//发送读扇区指令
                        }
                    }  
                    else if(on_off ==2)
                    {
                        if (DoMulti > 0)
                        {
                            DoMulti--;
                            nGetWritekey();
                            byte[] bs_WriteCard = bs_nGetWritekey[0];
                            bs_nGetWritekey.RemoveAt(0);
                            port.Write(bs_WriteCard, 0, bs_WriteCard.Length);//发送写卡指令
                        }
                        else
                        {
                            byte[] bs_WriteCard = GetWritekey();
                            port.Write(bs_WriteCard, 0, bs_WriteCard.Length);//发送写卡指令
                        } 
                    }
                    else if(on_off ==3)
                    {
                        byte[] bs_InitMoney = GetInitWalletkey();
                        port.Write(bs_InitMoney, 0, bs_InitMoney.Length);//发送钱包初始化指令
                    }
                    else if(on_off==4)
                    {
                        byte[] bs_AddMoney = GetAddMoneykey();
                        port.Write(bs_AddMoney, 0, bs_AddMoney.Length);//发送充值指令
                    }
                    else if(on_off==5)
                    {
                        byte[] bs_SubMoney = GetSubMoneykey();
                        port.Write(bs_SubMoney, 0, bs_SubMoney.Length);//发送扣款指令
                    }
                    else if(on_off==6)
                    {
                        byte[] bs_ReadWallet = GetReadWalletkey();
                        port.Write(bs_ReadWallet, 0, bs_ReadWallet.Length);//发送读余额指令
                    }
                }
                else if (receiveData[5] == 0x4c && receiveData[6] == 0x00)
                {
                    isOk = true;//写卡成功
                    WriteSuccess++;
                    if (DoMulti > 0)
                    {
                        DoMulti--;
                        byte[] bs_WriteCard = bs_nGetWritekey[0];
                        bs_nGetWritekey.RemoveAt(0);
                        port.Write(bs_WriteCard, 0, bs_WriteCard.Length);//发送写卡指令
                    }
                    else
                    {
                        isFinishCycle = true;
                    }
                    //MessageBox.Show("写卡成功！");
                }
                else if (receiveData[4] == 0x4b && receiveData[5] == 0x00)
                {
                    GetReadcontent(receiveData);//读卡成功，返回卡数据
                    if (DoMulti > 0)
                    {
                        DoMulti--;
                        byte[] bs_ReadKey = bs_nGetReadkey[0];
                        bs_nGetReadkey.RemoveAt(0);
                        port.Write(bs_ReadKey, 0, bs_ReadKey.Length);//发送读扇区指令
                    }
                    else
                    {
                        isFinishCycle = true;
                    }
                    //MessageBox.Show("读卡成功！");
                }
                else if (receiveData[4] == 0x4e && receiveData[5] == 0x00)
                {
                    isFinishCycle = true;
                    GetReadWallet(receiveData);//获取钱包值
                    //MessageBox.Show("读余额成功！");
                }
                else if (receiveData[5] == 0x4d && receiveData[6] == 0x00)
                {
                    isFinishCycle = true;
                    isOk = true;//钱包初始化成功
                }
                else if (receiveData[5] == 0x50 && receiveData[6] == 0x00)
                {
                    isFinishCycle = true;
                    isOk = true;
                    //MessageBox.Show("充值成功！");
                }
                else if (receiveData[5] == 0x4f && receiveData[6] == 0x00)
                {
                    isFinishCycle = true;
                    isOk = true;
                    //MessageBox.Show("扣款成功！");
                }
                else
                {
                    isFinishCycle = true;
                }
            }
            catch 
            {
                isFinishCycle = true;
                DoMulti = 0;
                bs_nGetReadkey.Clear();
                isOk = false;
                bs_nGetWritekey.Clear();
            }
        }


        public void ClosePort()
        {
            if (port.IsOpen)
            {
                port.Close();
            }
        }
        /// <summary>
        /// 寻卡
        /// </summary>
        /// <returns>返回卡号</returns> 
        public string RF_SearchCard()
        {
            on_off = 0;
            isFinishCycle = false;
            CardId1 = "";
            port.Write(bs_Find, 0, bs_Find.Length);//寻卡
            while(!isFinishCycle)
            {
                Thread.Sleep(50);
            }
            
            return CardId1;
        }

        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="nBlock">块号</param>
        /// <param name="KeyValue">密匙</param>
        /// <param name="Keytype">密匙类型 A密钥：0x60；B密钥：0x61</param>
        /// <returns></returns>
        public byte[] RF_ReadBlock(byte nBlock, byte[] KeyValue, byte Keytype)
        {
            on_off = 1;
            isFinishCycle = false;
            KeyContent[0] = nBlock;//块号
            ReadContent = null;
            for (int i = 0; i < 6; i++)
            {
                KeyContent[i + 1] = KeyValue[i];//获取密匙
            }
            this.keyType = Keytype;//设置密匙类型
            try
            {
                port.Write(bs_Find, 0, bs_Find.Length);//寻卡
            }
            catch
            {
                throw;
            }
            while (!isFinishCycle)
            {
                Thread.Sleep(50);
            }
            return ReadContent;
        }
        /// <summary>
        /// 读多个块
        /// </summary>
        /// <param name="nBlock">块号</param>
        /// <param name="KeyValue">密钥</param>
        /// <param name="Keytype">密匙类型 A密钥：0x60；B密钥：0x61</param>
        /// <param name="SleepTime">线程休眠时间，以400为基数，每多读一块增加50</param>
        /// <returns></returns>
        public List<byte[]> RF_nReadBlock(List<byte> nBlock, byte[] KeyValue, byte Keytype)
        {
            on_off = 1;
            isFinishCycle = false;
            DoMulti = nBlock.Count;
            nReadContent.Clear();
            nReadContent.TrimExcess();
            bs_nGetReadkey.Clear();
            bs_nGetReadkey.TrimExcess();
            ReadContent = null;
            KeyContent[0] = nBlock[0];//块号
            for (int i = 0; i < 6; i++)
            {
                KeyContent[i + 1] = KeyValue[i];//获取密匙
            }
            this.keyType = Keytype;//设置密匙类型
            nReadBlock = nBlock;//获取要读的块号

            port.Write(bs_Find, 0, bs_Find.Length);//寻卡

            while (!isFinishCycle)
            {
                Thread.Sleep(50);
            }

            return nReadContent;
        }
        /// <summary>
        /// 写多个块
        /// </summary>
        /// <param name="KeyValue">密钥</param>
        /// <param name="Keytype">密匙类型 A密钥：0x60；B密钥：0x61</param>
        /// <param name="content">写入卡内容 一字节块号+16字节内容</param>
        /// <param name="SleepTime">线程休眠时间，以400为基数，每多读一块增加100</param>
        /// <returns></returns>
        public int RF_nWriteBlock(byte[] KeyValue, byte Keytype, List<byte[]> contents)
        {
            on_off = 2;
            isFinishCycle = false;
            DoMulti = contents.Count;
            WriteSuccess = 0;
            bs_nGetWritekey.Clear();
            bs_nGetWritekey.TrimExcess();

            KeyContent[0] = contents[0][0];//块号
            for (int i = 0; i < 6; i++)
            {
                KeyContent[i + 1] = KeyValue[i];//获取密匙
            }
            this.keyType = Keytype;//设置密匙类型

            nWriteContent = contents;//获取写入卡的内容

            port.Write(bs_Find, 0, bs_Find.Length);//寻卡
            while (!isFinishCycle)
            {
                Thread.Sleep(50);
            }

            return WriteSuccess;
        }
        /// <summary>
        /// 写卡
        /// </summary>
        /// <param name="nBlock">块号</param>
        /// <param name="KeyValue">密钥</param>
        /// <param name="Keytype">密钥类型 A密钥：0x60；B密钥：0x61</param>
        /// <param name="content">写入的内容</param>
        /// <returns></returns>
        public bool RF_WriteBlock(byte nBlock, byte[] KeyValue, byte Keytype, byte[] content)
        {
            on_off = 2;
            isFinishCycle = false;
            isOk = false;
            KeyContent[0] = nBlock;//块号
            for (int i = 0; i < 6; i++)
            {
                KeyContent[i + 1] = KeyValue[i];//获取密匙
            }
            this.keyType = Keytype;//设置密匙类型
            WriteConten[0] = nBlock;
            for (int i = 0; i < 16; i++)
            {
                WriteConten[i + 1] = content[i];//获取写入卡的内容
            }

            port.Write(bs_Find, 0, bs_Find.Length);//寻卡
            while (!isFinishCycle)
            {
                Thread.Sleep(50);
            }

            return isOk;
        }
        /// <summary>
        /// 初始化钱包
        /// </summary>
        /// <param name="nBlock">块号</param>
        /// <param name="KeyValue">密钥</param>
        /// <param name="Keytype">密钥类型 A密钥：0x60；B密钥：0x61</param>
        /// <param name="initMoney">初始化值</param>
        /// <returns></returns>
        public bool RF_InitPurse(byte nBlock, byte[] KeyValue, byte Keytype,string initMoney)
        {
            on_off = 3;
            isOk = false;
            isFinishCycle = false;
            KeyContent[0] = nBlock;//块号
            for (int i = 0; i < 6; i++)
            {
                KeyContent[i + 1] = KeyValue[i];//获取密匙
            }
            this.keyType = Keytype;//设置密匙类型
            int wallet = int.Parse(initMoney);//钱包初始值
            string X_wallet = wallet.ToString("X").PadLeft(8, '0');
            InitMoney = ConvertWallet(X_wallet);
            port.Write(bs_Find, 0, bs_Find.Length);//寻卡

            while (!isFinishCycle)
            {
                Thread.Sleep(50);
            }

            return isOk;
        }
        /// <summary>
        /// 读钱包
        /// </summary>
        /// <param name="nBlock">块号</param>
        /// <param name="KeyValue">密钥</param>
        /// <param name="Keytype">密钥类型 A密钥：0x60；B密钥：0x61</param>
        /// <returns></returns>
        public string RF_ReadPurse(byte nBlock, byte[] KeyValue, byte Keytype)
        {
            on_off = 6;
            isFinishCycle = false;
            KeyContent[0] = nBlock;//块号
            MonyeCount = "";
            for (int i = 0; i < 6; i++)
            {
                KeyContent[i + 1] = KeyValue[i];//获取密匙
            }
            this.keyType = Keytype;//设置密匙类型

            port.Write(bs_Find, 0, bs_Find.Length);//寻卡

            while (!isFinishCycle)
            {
                Thread.Sleep(50);
            }

            return MonyeCount;
        }
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="nBlock">块号</param>
        /// <param name="KeyValue">密钥</param>
        /// <param name="Keytype">密钥类型 A密钥：0x60；B密钥：0x61</param>
        /// <param name="BackupMoney">充值金额</param>
        /// <returns></returns>
        public bool RF_IncPurse(byte nBlock, byte[] KeyValue, byte Keytype, string IncMoney)
        {
            on_off = 4;
            isFinishCycle = false;
            isOk = false;
            KeyContent[0] = nBlock;//块号
            for (int i = 0; i < 6; i++)
            {
                KeyContent[i + 1] = KeyValue[i];//获取密匙
            }
            this.keyType = Keytype;//设置密匙类型

            int wallet = int.Parse(IncMoney);
            string X_wallet = wallet.ToString("X").PadLeft(8, '0');
            AddMoney = ConvertWallet(X_wallet);

            port.Write(bs_Find, 0, bs_Find.Length);//寻卡

            while (!isFinishCycle)
            {
                Thread.Sleep(50);
            }

            return isOk;
        }
        /// <summary>
        /// 扣款
        /// </summary>
        /// <param name="nBlock">块号</param>
        /// <param name="KeyValue">密钥</param>
        /// <param name="Keytype">密钥类型 A密钥：0x60；B密钥：0x61</param>
        /// <param name="DecMoney">扣款金额</param>
        /// <returns></returns>
        public bool RF_DecPurse(byte nBlock, byte[] KeyValue, byte Keytype, string DecMoney)
        {
            on_off = 5;
            isOk = false;
            isFinishCycle = false;
            KeyContent[0] = nBlock;//块号
            for (int i = 0; i < 6; i++)
            {
                KeyContent[i + 1] = KeyValue[i];//获取密匙
            }
            this.keyType = Keytype;//设置密匙类型

            int wallet = int.Parse(DecMoney);
            string X_wallet = wallet.ToString("X").PadLeft(8, '0');
            subMoney = ConvertWallet(X_wallet);

            port.Write(bs_Find, 0, bs_Find.Length);//寻卡

            while (!isFinishCycle)
            {
                Thread.Sleep(50);
            }

            return isOk;
        }
       
        
#region 生成指令
       
        /// <summary>
        /// 生成扣款指令
        /// </summary>
        /// <returns></returns>
        private byte[]GetSubMoneykey()
        {
            int rootValue = 0x57;//校验字初始值
            List<byte> OrderHead = new List<byte>();
            OrderHead.Add(0x02);
            OrderHead.Add(0x00);
            OrderHead.Add(0x00);
            OrderHead.Add(0x08);
            OrderHead.Add(0x4f);

            subMoney[0] = KeyContent[0];
            byte[] OrderData = SetArrbyte(subMoney);//字节数组处理为数据域
            byte CheckKey = EndCheck(OrderData, rootValue);//生成校验字
            byte[] bs_SubMoneykey = ConvertOrder(OrderHead, OrderData, CheckKey);//生成密匙指令
            return bs_SubMoneykey;
        }
        /// <summary>
        /// 生成充值指令
        /// </summary>
        /// <returns></returns>
        private byte[] GetAddMoneykey()
        {
            int rootValue = 0x58;//校验字初始值
            List<byte> OrderHead = new List<byte>();
            OrderHead.Add(0x02);
            OrderHead.Add(0x00);
            OrderHead.Add(0x00);
            OrderHead.Add(0x08);
            OrderHead.Add(0x50);

            AddMoney[0] = KeyContent[0];
            byte[] OrderData = SetArrbyte(AddMoney);//字节数组处理为数据域
            byte CheckKey = EndCheck(OrderData, rootValue);//生成校验字
            byte[] bs_AddMoneykey = ConvertOrder(OrderHead, OrderData, CheckKey);//生成密匙指令
            return bs_AddMoneykey;
        }
        /// <summary>
        /// 生成选卡指令
        /// </summary>
        /// <param name="receiveData">卡号信息</param>
        /// <returns></returns>
        private byte[] DoReceiveData(byte[] receiveData)
        {
            int rootValue = 0x4F;//校验字初始值
            List<byte> OrderHead = new List<byte>();
            OrderHead.Add(0x02);
            OrderHead.Add(0x00);
            OrderHead.Add(0x00);
            OrderHead.Add(0x07);
            OrderHead.Add(0x48);
            byte[] CardInfo = SetSinglebyte(receiveData, 6, 4);//获取卡号
            byte CheckKey = EndCheck(CardInfo, rootValue);//生成校验字
            byte[] bs_Choose = ConvertOrder(OrderHead, CardInfo, CheckKey);//生成指令
            return bs_Choose;
        }
        /// <summary>
        /// 生成读余额指令
        /// </summary>
        /// <returns></returns>
        private byte[] GetReadWalletkey()
        {
            int rootValue = 0x52;//校验字初始值
            List<byte> OrderHead = new List<byte>();
            OrderHead.Add(0x02);
            OrderHead.Add(0x00);
            OrderHead.Add(0x00);
            OrderHead.Add(0x04);
            OrderHead.Add(0x4e);

            byte[] Datas = new byte[] { KeyContent[0] };
            byte[] OrderData = SetArrbyte(Datas);//字节数组处理为数据域
            byte CheckKey = EndCheck(OrderData, rootValue);//生成校验字
            byte[] bs_ReadKey = ConvertOrder(OrderHead, OrderData, CheckKey);//生成密匙指令
            return bs_ReadKey;
        }
        /// <summary>
        /// 生成钱包初始化指令
        /// </summary>
        /// <returns></returns>
        private byte[] GetInitWalletkey()
        {
            int rootValue = 0x55;//校验字初始值
            List<byte> OrderHead = new List<byte>();
            OrderHead.Add(0x02);
            OrderHead.Add(0x00);
            OrderHead.Add(0x00);
            OrderHead.Add(0x08);
            OrderHead.Add(0x4d);

            InitMoney[0] = KeyContent[0];
            byte[] OrderData = SetArrbyte(InitMoney);//字节数组处理为数据域
            byte CheckKey = EndCheck(OrderData, rootValue);//生成校验字
            byte[] bs_InitWalletkey = ConvertOrder(OrderHead, OrderData, CheckKey);//生成密匙指令
            return bs_InitWalletkey;
        }        
        /// <summary>
        /// 生成密钥指令
        /// </summary>
        /// <returns></returns>
        private byte[] GetSectionkey()
        {
            int rootValue = 0x4a + (Int32)keyType + 0x0b;//校验字初始值
            List<byte> OrderHead = new List<byte>();
            OrderHead.Add(0x02);
            OrderHead.Add(0x00);
            OrderHead.Add(0x00);
            OrderHead.Add(0x0b);
            OrderHead.Add(0x4a);
            OrderHead.Add(keyType);

            byte[] OrderData = SetArrbyte(KeyContent);//字节数组处理为数据域

            byte CheckKey = EndCheck(OrderData, rootValue);//生成校验字
            byte[] bs_ReadSection = ConvertOrder(OrderHead, OrderData, CheckKey);//生成密匙指令
            return bs_ReadSection;
        }
        /// <summary>
        /// 生成写块指令
        /// </summary>
        /// <returns></returns>
        private byte[] GetWritekey()
        {
            int rootValue = 0x60;//校验字初始值

            List<byte> OrderHead = new List<byte>();
            OrderHead.Add(0x02);
            OrderHead.Add(0x00);
            OrderHead.Add(0x00);
            OrderHead.Add(0x14);
            OrderHead.Add(0x4c);

            byte[] OrderData = SetArrbyte(WriteConten);//字节数组处理为数据域
            byte CheckKey = EndCheck(OrderData, rootValue);//生成校验字
            byte[] bs_ReadKey = ConvertOrder(OrderHead, OrderData, CheckKey);//生成密匙指令
            return bs_ReadKey;
        }
       
        /// <summary>
        /// 生成写块指令组
        /// </summary>
        private void nGetWritekey()
        {
            for (int i = 0; i < nWriteContent.Count; i++)
            {
                int rootValue = 0x60;//校验字初始值

                List<byte> OrderHead = new List<byte>();
                OrderHead.Add(0x02);
                OrderHead.Add(0x00);
                OrderHead.Add(0x00);
                OrderHead.Add(0x14);
                OrderHead.Add(0x4c);

                byte[] OrderData = SetArrbyte(nWriteContent[i]);//字节数组处理为数据域
                byte CheckKey = EndCheck(OrderData, rootValue);//生成校验字
                byte[] bs_Writekey = ConvertOrder(OrderHead, OrderData, CheckKey);//生成密匙指令
                bs_nGetWritekey.Add(bs_Writekey);
            }
        }

        /// <summary>
        /// 生成读块指令
        /// </summary>
        /// <returns></returns>
        private byte[] GetReadkey()
        {
            int rootValue = 0x4f;//校验字初始值

            List<byte> OrderHead = new List<byte>();
            OrderHead.Add(0x02);
            OrderHead.Add(0x00);
            OrderHead.Add(0x00);
            OrderHead.Add(0x04);
            OrderHead.Add(0x4b);

            byte[] Datas = new byte[] { KeyContent[0] };
            byte[] OrderData = SetArrbyte(Datas);//字节数组处理为数据域
            byte CheckKey = EndCheck(OrderData, rootValue);//生成校验字
            byte[] bs_ReadKey = ConvertOrder(OrderHead, OrderData, CheckKey);//生成密匙指令
            return bs_ReadKey;
        }

        /// <summary>
        /// 生成读块指令组
        /// </summary>
        private void nGetReadkey()
        {
            for (int i = 0; i < nReadBlock.Count; i++)
            {
                int rootValue = 0x4f;//校验字初始值

                List<byte> OrderHead = new List<byte>();
                OrderHead.Add(0x02);
                OrderHead.Add(0x00);
                OrderHead.Add(0x00);
                OrderHead.Add(0x04);
                OrderHead.Add(0x4b);

                byte[] Datas = new byte[] { nReadBlock[i] };
                byte[] OrderData = SetArrbyte(Datas);//字节数组处理为数据域
                byte CheckKey = EndCheck(OrderData, rootValue);//生成校验字
                byte[] bs_ReadKey = ConvertOrder(OrderHead, OrderData, CheckKey);//生成密匙指令
                bs_nGetReadkey.Add(bs_ReadKey);
            }
        }
#endregion

#region 数据处理
        /// <summary>
        /// 获取余额
        /// </summary>
        /// <param name="receiveData"></param>
        /// <returns></returns>
        private void GetReadWallet(byte[] receiveData)
        {
            byte[] WalletByte = SetSinglebyte(receiveData, 6, 4);
            string[] WalletArr = new string[4];
            for (int i = 0; i < 4; i++)
            {
                WalletArr[i] = WalletByte[i].ToString("X").PadLeft(2, '0');
            }
            string WalletMoney = WalletArr[3] + WalletArr[2] + WalletArr[1] + WalletArr[0];
            WalletMoney = Convert.ToInt64(WalletMoney, 16).ToString();
            MonyeCount = WalletMoney;
        }
        /// <summary>
        /// 获取块内容
        /// </summary>
        /// <param name="receiveData"></param>
        private void GetReadcontent(byte[] receiveData)
        {
            ReadContent = SetSinglebyte(receiveData, 6, 16);
            nReadContent.Add(ReadContent);
        }
        /// <summary>
        /// 获取卡号
        /// </summary>
        /// <param name="receiveData"></param>
        private void GetCard(byte[] receiveData)
        {
            byte[] cardByte = SetSinglebyte(receiveData, 6, 4);
            string[] cardIdArr = new string[4];
            for (int i = 0; i < 4; i++)
            {
                cardIdArr[i] = cardByte[i].ToString("X").PadLeft(2, '0');
            }
            string cardId = cardIdArr[3] + cardIdArr[2] + cardIdArr[1] + cardIdArr[0];
            cardId = Convert.ToInt64(cardId, 16).ToString().PadLeft(10, '0');
            CardId1 = cardId;
        }
        /// <summary>
        /// 生成要保存的16字节数组
        /// </summary>
        /// <param name="content"></param>
        private void ConvertWritecontent(string content)
        {
            WriteConten[0] = 0x00;
            if(content =="")
            {
                for (int i = 1; i < 17; i++)
                {
                    WriteConten[i] = 0x00;
                }
            }
            else
            {
                byte[] cont = Encoding.ASCII.GetBytes(content);
                for (int i = 0; i < cont.Length; i++)
                {
                    WriteConten[i+1] = cont[i];
                }
                if(cont.Length<16)
                {
                    for (int i = cont.Length; i < 16;i++ )
                    {
                        WriteConten[i + 1] = 0x00;
                    }
                }
            }
        }
        /// <summary>
        /// 转换为四位低字节在前的钱包金额数据
        /// </summary>
        /// <param name="MoneyCount">原始钱包金额</param>
        /// <returns></returns>
        private byte[] ConvertWallet(string MoneyCount)
        {
            byte[] WalletMoney=new byte[5];
            for (int i = 0; i < 4; i++)
            {
                string wallet = MoneyCount.Substring(6 - 2 * i, 2);
                WalletMoney[i + 1] = Convert.ToByte(wallet, 16);
            }
            return WalletMoney;
        }
        /// <summary>
        /// 生成校验字
        /// </summary>
        /// <param name="bytes">校验字符数组</param>
        /// <param name="rootValue">校验初始值</param>
        /// <returns></returns>
        private byte EndCheck(byte[] bytes, int rootValue)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x10)
                {
                    rootValue += (Int32)bytes[++i];
                }
                else
                {
                    rootValue += (Int32)bytes[i];
                }
            }
            return (byte)rootValue;
        }
        /// <summary>
        /// 字符串数组转换字节数组
        /// </summary>
        /// <param name="values">待转换的字符串数组</param>
        /// <returns></returns>
        private byte[] StringTobyte(string[] values)
        {
            List<byte> arrBytes = new List<byte>();
            for (int i = 0; i < values.Length; i++)
            {
                byte b_value = Convert.ToByte(values[i], 16);
                arrBytes.Add(b_value);
            }
            byte[] arrByte = new byte[arrBytes.Count];
            for (int i = 0; i < arrBytes.Count; i++)
            {
                arrByte[i] = arrBytes[i];
            }
            return arrByte;
        }
        /// <summary>
        /// 字节处理(字节转命令字)
        /// </summary>
        /// <param name="receiveData">待处理的字节数组</param>
        /// <returns></returns>
        private byte[] SetArrbyte(byte[] receiveData)
        {
            List<byte> arrBytes = new List<byte>();
            for (int i = 0; i < receiveData.Length; i++)
            {
                if (receiveData[i] == 0x02 || receiveData[i] == 0x03 || receiveData[i] == 0x10)
                {
                    arrBytes.Add(0x10);
                    arrBytes.Add(receiveData[i]);
                }
                else
                {
                    arrBytes.Add(receiveData[i]);
                }
            }
            byte[] arrByte = new byte[arrBytes.Count];
            for (int i = 0; i < arrBytes.Count; i++)
            {
                arrByte[i] = arrBytes[i];
            }
            return arrByte;
        }
        /// <summary>
        /// 字节处理(命令字转字节)
        /// </summary>
        /// <param name="Arrbyte">命令字</param>
        /// <param name="byteStart">截取起始位置</param>
        /// <param name="getCount">截取的位数</param>
        /// <returns></returns>
        private byte[] SetSinglebyte(byte[] Arrbyte, int byteStart, int getCount)
        {
            List<byte> Singlebyte = new List<byte>();
            for (int i = 0; i < getCount; i++, byteStart++)
            {
                if (Arrbyte[byteStart] == 0x10)
                {
                    Singlebyte.Add(Arrbyte[++byteStart]);
                }
                else
                {
                    Singlebyte.Add(Arrbyte[byteStart]);
                }
            }
            byte[] arrByte = new byte[Singlebyte.Count];
            for (int i = 0; i < Singlebyte.Count; i++)
            {
                arrByte[i] = Singlebyte[i];
            }
            return arrByte;
        }
        /// <summary>
        /// 生成指令数组
        /// </summary>
        /// <param name="OrderHead">指令头</param>
        /// <param name="OrderData">数据域</param>
        /// <param name="CheckKey">校验字</param>
        /// <returns></returns>
        private byte[] ConvertOrder(List<byte> OrderHead, byte[] OrderData, byte CheckKey)
        {
            for (int i = 0; i < OrderData.Length; i++)
            {
                OrderHead.Add(OrderData[i]);
            }
            if (CheckKey == 0x02 || CheckKey == 0x03||CheckKey == 0x10)
            {
                OrderHead.Add(0x10);
                OrderHead.Add(CheckKey);
            }
            else
            {
                OrderHead.Add(CheckKey);
            }
            OrderHead.Add(0x03);
            byte[] bs_Choose = new byte[OrderHead.Count];
            for (int i = 0; i < OrderHead.Count; i++)
            {
                bs_Choose[i] = OrderHead[i];
            }
            return bs_Choose;
        }
        /// <summary>
        /// 处理ReadContent并显示块内容
        /// </summary>
        private string ByteToString(byte[] ByteReadcontent)
        {
            char[] contents = Encoding.ASCII.GetChars(ByteReadcontent);
            string content = "";
            for (int i = 0; i < 16; i++)
            {
                content += contents[i].ToString();
            }
            return content;
        }

#endregion

    }
}
