using System;

using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 储值卡类型
    /// </summary>
    public enum CzCardType
    {
        磁卡=0,
        射频卡=1
    }

    /// <summary>
    /// 储值卡状态
    /// </summary>
    public enum CzkStat
    {
        启用=1,
        禁用=0,
        挂失=2
    }

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserMClass
    {
        超级用户=0,
        管理员=1,
        收款员=2,
        售货员=3
    }


    /// <summary>
    /// 网络类型
    /// </summary>
    public enum NetWork { GRPS, ADSL, MODEM, Ethernet }


    /// <summary>
    /// 付款方式
    /// </summary>
    public enum PayType
    {
        现金=1,
        银行卡=11,
        储值卡=12,
    }



    /// <summary>
    /// 流水上传标志
    /// </summary>
    public enum FlowUpLoadFlag
    {
        已上传=1,
        未上传=0
    }


    /// <summary>
    /// 打印小票
    /// </summary>
    public enum PrintBillFlag
    {
        不打印=0,
        打印=1
    }



    /// <summary>
    /// 刷卡数量
    /// </summary>
    public enum MulCardFlag
    {
        单卡=0,
        多卡=1
    }


    /// <summary>
    /// 允许手动输入卡号
    /// </summary>
    public enum ReadCardByHand
    {
        禁止手动 = 0,
        允许手动 = 1
    }
}
