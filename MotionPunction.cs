using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gts;
using GoogolConfig;
using ReadCondition;
using System.Windows.Forms;

namespace Motion
{
    //public struct TrapParameter
    //{
    //    public double position;
    //    public double sleep;
    //}

    //interface AxiMotion
    //{
    //    void JogMotin(short cardNumber, short AXIS, mc.TJogPrm p_jog, double velocity);
    //    void Trap(bool positive, mc.TTrapPrm p_trap, TrapParameter setTrapParameter, short axi);
    //    void GoHome(short axi, short card);
    //    void GoSafetyPosition(short axi, short card);
    //    void readAxiDAC(short axi, short card);
    //    void readDAC(short sartAdc, short card, short nCount);
    //}



    class MotionPunction
    {
        public struct TrapParameter
        {
            public double position;
            public double sleep;
        }
        //trap运动
        public static void Trap(bool positive, mc.TTrapPrm p_trap, TrapParameter setTrapParameter, short axi)
        {
            int AxiStatus;
            uint pClock;
            short sRtn = mc.GT_GetSts(0, axi, out AxiStatus, 1, out pClock);
            if ((AxiStatus & 0x2) == 0x2 && (AxiStatus & 0x20) == 0x20)
            {
                Config.commandhandler("伺服报警或处于正限位", sRtn);
            }
            else
            {
                mc.TTrapPrm trap = new mc.TTrapPrm();
                //清除各轴的报警和限位
                sRtn = mc.GT_ClrSts(0, axi, 1);
                if (sRtn != 0)
                {
                    Config.commandhandler("清除出错", sRtn);
                }
                // 伺服使能
                sRtn = mc.GT_AxisOn(0, axi);
                if (sRtn != 0)
                {
                    Config.commandhandler("使能出错", sRtn);
                }
                // 位置清零
                sRtn = mc.GT_ZeroPos(0, axi, 1);
                if (sRtn != 0)
                {
                    Config.commandhandler("位置清零出错", sRtn);
                }
                // AXIS轴规划位置清零
                sRtn = mc.GT_SetPrfPos(0, axi, 0);
                if (sRtn != 0)
                {
                    Config.commandhandler("规划出错", sRtn);
                }
                // 将AXIS轴设为点位模式
                sRtn = mc.GT_PrfTrap(0, axi);
                if (sRtn != 0)
                {
                    Config.commandhandler("设置模式出错", sRtn);
                }
                // 读取点位运动参数
                sRtn = mc.GT_GetTrapPrm(0, axi, out trap);
                if (sRtn != 0)
                {
                    Config.commandhandler("读取参数出错", sRtn);
                }
                trap.acc = p_trap.acc;
                trap.dec = p_trap.dec;
                trap.smoothTime = p_trap.smoothTime;
                // 设置点位运动参数
                sRtn = mc.GT_SetTrapPrm(0, axi, ref trap);
                if (sRtn != 0)
                {
                    Config.commandhandler("设置参数出错", sRtn);
                }
                // 设置AXIS轴的目标位置
                int posation = Convert.ToInt32(setTrapParameter.position);
                sRtn = mc.GT_SetPos(0, axi, posation);
                if (sRtn != 0)
                {
                    Config.commandhandler("设置位置出错", sRtn);
                }
                // 设置AXIS轴的目标速度
                double 速度 = Convert.ToDouble(setTrapParameter.sleep);
                sRtn = mc.GT_SetVel(0, axi, 速度);
                if (sRtn != 0)
                {
                    Config.commandhandler("设置速度出错", sRtn);
                }
                // 启动AXIS轴的运动
                sRtn = mc.GT_Update(0, 2 ^ axi);
                if (sRtn != 0)
                {
                    Config.commandhandler("启动运动出错", sRtn);
                }
            }
        }


        public static void JogMotin(short cardNumber, short AXIS, mc.TJogPrm p_jog, double velocity)
        {
            short sRtn;
            mc.TJogPrm jog = new mc.TJogPrm();
            long sts;
            double prfPos, prfVel;

            // 清除各轴的报警和限位
            sRtn = mc.GT_ClrSts(cardNumber, 1, 8);
            Config.commandhandler("GT_ClrSts", sRtn);

            // 伺服使能
            sRtn = mc.GT_AxisOn(cardNumber, AXIS);
            Config.commandhandler("GT_AxisOn", sRtn);

            // 将AXIS轴设为Jog模式
            sRtn = mc.GT_PrfJog(cardNumber, AXIS);
            Config.commandhandler("GT_PrfTrap", sRtn);
            // 读取Jog运动参数
            sRtn = mc.GT_GetJogPrm(cardNumber, AXIS, out jog);
            Config.commandhandler("GT_GetJogPrm", sRtn);
            jog.acc = p_jog.acc;
            jog.dec = p_jog.dec;
            // 设置Jog运动参数
            sRtn = mc.GT_SetJogPrm(cardNumber, AXIS, ref jog);
            Config.commandhandler("GT_SetJogPrm", sRtn);
            // 设置AXIS轴的目标速度
            sRtn = mc.GT_SetVel(cardNumber, AXIS, velocity);
            Config.commandhandler("GT_SetVel", sRtn);
            // 启动AXIS轴的运动
            sRtn = mc.GT_Update(cardNumber, 1 << (AXIS - 1));
            Config.commandhandler("GT_Update", sRtn);
        }

        public static void GoHome(short axi, short card)
        {
            mc.TJogPrm jopPra = new mc.TJogPrm();
            jopPra.acc = 0.25;
            jopPra.dec = 0.25;
            double value = 20;
            AxiCondition.AxiSituation axiSituation = new AxiCondition.AxiSituation();
            JogMotin(0, 1, jopPra, -value);
            do
            { axiSituation = AxiCondition.JustReadCondition(axi, card); }
            while (axiSituation.negativeLimit == false);

            mc.TTrapPrm p_trap = new mc.TTrapPrm();
            p_trap.acc = 0.25;
            p_trap.dec = 0.25;
            p_trap.smoothTime = 25;
            p_trap.velStart = 1;

            TrapParameter setTrapParameter = new TrapParameter();
            setTrapParameter.position = 10000;
            setTrapParameter.sleep = 20;
            Trap(true, p_trap, setTrapParameter, axi);
        }

        //常用于铺粉车
        public static void GoSafetyPosition(short axi, short card)
        {
            AxiCondition.AxiSituation axiSituation = new AxiCondition.AxiSituation();
            axiSituation = AxiCondition.ReadCondition(axi, card);
            if (axiSituation.negativeLimit == true)
            {
                mc.TTrapPrm p_trap = new mc.TTrapPrm();
                p_trap.acc = 0.25;
                p_trap.dec = 0.25;
                p_trap.smoothTime = 25;
                TrapParameter setTrapParameter = new TrapParameter();
                setTrapParameter.position = 5000;
                setTrapParameter.sleep = 20;
                Trap(true, p_trap, setTrapParameter, axi);
            }
            else if (axiSituation.positveLimit == true)
            {
                mc.TTrapPrm p_trap = new mc.TTrapPrm();
                p_trap.acc = 0.25;
                p_trap.dec = 0.25;
                p_trap.smoothTime = 25;
                TrapParameter setTrapParameter = new TrapParameter();
                setTrapParameter.position = -5000;
                setTrapParameter.sleep = 20;
                Trap(true, p_trap, setTrapParameter, axi);
            }
        }

        public static void ReadAxiDAC(short axi, short card)
        {
            short sRtn;
            //电压值
            short sSetValue;
            short sGetValue;
            uint pClock;
            //控制器复位，所有轴都为脉冲
            //计算轴4电压输出值
            sSetValue = (short)32767 * 5 / 10;
            //设置轴4的输出电压
            sRtn = mc.GT_SetDac(card, axi, ref sSetValue, 1);
            //读取轴的输出电压
            sRtn = mc.GT_GetDac(card, axi, out sGetValue, 1, out pClock);

        }

        public static void ReadDAC(short sartAdc, short card, short nCount)
        {
            short sRtn;
            // 电压值
            double dGetVoltageValue;
            // 数字转换值
            short sGetDigitalValue;
            // 读取4个通道的输入电压
            uint pClock;
            sRtn = mc.GT_GetAdc(0, sartAdc, out dGetVoltageValue, nCount, out pClock);
            // 读取4个通道输入电压的数字转换值
            sRtn = mc.GT_GetAdcValue(0, sartAdc, out sGetDigitalValue, nCount, out pClock);
        }

        //通用输出IO高电平
        public static void SetDoHighLevel(short card,short Do)
        {   short sRtn;
            sRtn = mc.GT_SetDoBit(card, 12, Do, 1);
            if (sRtn != 0)
            {
                MessageBox.Show("set Do Level is error！");
            }
        }

        //通用输出IO低电平
        public static void SetDoLowLevel(short card, short Do)
        {
            short sRtn;
            sRtn = mc.GT_SetDoBit(card, 12, Do, 0);
            if (sRtn != 0)
            {
                MessageBox.Show("set Do Level is error！");
            }
        }     
        
    }
}
