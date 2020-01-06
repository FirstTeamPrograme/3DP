using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gts;
using GoogolConfig;

namespace ReadCondition
{
    class AxiCondition
    {
        //读取状态
        public struct AxiSituation
        {
            public bool servoAlarm;            //伺服报警
            public bool flagError;             //跟随误差越限标记
            public bool positveLimit;          //正限位
            public bool negativeLimit;         //负限位
            public bool smoothStop;            //平滑停止
            public bool abruptStop;            //急停
            public bool ServoOn;               //伺服开
            public bool profileMotion;         //规划器运动
        }

        public struct AxiMotionParameter
        {
            public double m_profilePosition;               // 规划位置
            public double m_profileVelocity;               // 规划速度
            public double m_ProfileAcc;                    // 规划加速度
            public int m_profileMotionMode;                // 运动模式
            public string m_motinModeName;                 // 运动模式，字符串变量
        }
        public AxiMotionParameter ReadAxiMotiveParameter(short axi, short card, uint pClock)
        {
            AxiMotionParameter axiParameter;
            // 读取运动数据

            // printf("规划位置 %8.2f\n", dPrfPos);
            short sRtn = mc.GT_GetPrfPos(axi, axi, out axiParameter.m_profilePosition, 1, out pClock);
            Config.commandhandler("GT_GetPrfPos", sRtn);
            //printf("规划速度 %8.2f\n", dPrfVel);
            sRtn = mc.GT_GetPrfVel(axi, axi, out axiParameter.m_profileVelocity, 1, out pClock);
            Config.commandhandler("GT_GetPrfVel", sRtn);
            //printf("规划加速度 %8.2f\n", dPrfAcc);
            sRtn = mc.GT_GetPrfAcc(axi, axi, out axiParameter.m_ProfileAcc, 1, out pClock);
            Config.commandhandler("GT_GetPrfAcc", sRtn);

            // 读取运动模式
            sRtn = mc.GT_GetPrfMode(axi, axi, out axiParameter.m_profileMotionMode, 1, out pClock);
            Config.commandhandler("GT_GetPrfMode", sRtn);
            // 清空字符串
            axiParameter.m_motinModeName = null;
            //memset(chPrfMode, '\0', 20);
            switch (axiParameter.m_profileMotionMode)
            {
                case 0:
                    axiParameter.m_motinModeName = "Trap";
                    Config.commandhandler("Trap", 0);
                    break;
                case 1:
                    axiParameter.m_motinModeName = "Jog";
                    Config.commandhandler("Jog", 1);
                    break;
                case 2:
                    axiParameter.m_motinModeName = "PT";
                    Config.commandhandler("PT", 2);
                    break;
                case 3:
                    axiParameter.m_motinModeName = "Gear";
                    Config.commandhandler("Gear", 3);
                    break;
                case 4:
                    axiParameter.m_motinModeName = "Follow";
                    Config.commandhandler("Follow", 4);
                    break;
                case 5:
                    axiParameter.m_motinModeName = "Interpolation";
                    Config.commandhandler("Interpolation", 5);
                    break;
                case 6:
                    axiParameter.m_motinModeName = "PVT";
                    Config.commandhandler("PVT", 6);
                    break;
                default:
                    break;
            }
            return axiParameter;
        }

        public static AxiSituation ReadCondition(short axi, short card)
        {
            short bFlagAlarm = 0;           // 伺服报警标志
            short bFlagMError = 0;          // 跟随误差越限标志
            short bFlagPosLimit = 0;        // 正限位触发标志
            short bFlagNegLimit = 0;        // 负限位触发标志
            short bFlagSmoothStop = 0;      // 平滑停止标志
            short bFlagAbruptStop = 0;      // 急停标志
            short bFlagServoOn = 0;         // 伺服使能标志
            short bFlagMotion = 0;          // 规划器运动标志
            short sRtn;                     // 指令返回值变量
            int lAxisStatus;                // 轴状态
            uint pClock;                    //读取时钟

            AxiSituation axiCondition;
            // 读取轴状态
            sRtn = mc.GT_GetSts(card, axi, out lAxisStatus, 1, out pClock);
            Config.commandhandler("GT_GetSts", sRtn);
            // 伺服报警标志
            if ((lAxisStatus & 0x2) == 2)//
            {
                axiCondition.servoAlarm = true;
                bFlagAlarm = 1;
                Config.commandhandler("伺服报警\n", bFlagAlarm);
            }
            else
            {
                bFlagAlarm = 0;
                axiCondition.servoAlarm = false;
                Config.commandhandler("伺服正常\n", bFlagAlarm);
            }
            // 跟随误差越限标志
            if ((lAxisStatus & 0x10) == 0x10)
            {
                bFlagMError = 1;
                axiCondition.flagError = true;
                Config.commandhandler("运动出错\n", bFlagMError);
            }
            else
            {
                axiCondition.flagError = false;
                bFlagMError = 0;
                Config.commandhandler("运动正常\n", bFlagMError);
            }
            // 正向限位
            if ((lAxisStatus & 0x20) == 0x20)
            {
                axiCondition.positveLimit = true;
                bFlagPosLimit = 1;
                Config.commandhandler("正限位触发\n", bFlagPosLimit);
            }
            else
            {
                axiCondition.positveLimit = false;
                bFlagPosLimit = 0;
                Config.commandhandler("正限位未触发\n", bFlagPosLimit);
            }
            // 负向限位
            if ((lAxisStatus & 0x40) == 0x40)
            {
                axiCondition.negativeLimit = true;
                bFlagNegLimit = 1;
                Config.commandhandler("负限位触发\n", 1);
            }
            else
            {
                axiCondition.negativeLimit = false;
                //bFlagNegLimit = FALSE;
                Config.commandhandler("负限位未触发\n", 1);
            }
            // 平滑停止
            if ((lAxisStatus & 0x80) == 0x80)
            {
                axiCondition.smoothStop = true;

                bFlagSmoothStop = 1;
                Config.commandhandler("平滑停止触发\n", 1);
            }
            else
            {
                axiCondition.smoothStop = false;
                bFlagSmoothStop = 0;
                Config.commandhandler("平滑停止未触发\n", 0);
            }
            // 急停标志
            if ((lAxisStatus & 0x100) == 0x100)
            {
                axiCondition.abruptStop = true;
                bFlagAbruptStop = 1;
                Config.commandhandler("急停触发\n", 1);
            }
            else
            {
                axiCondition.abruptStop = false;
                bFlagAbruptStop = 0;
                Config.commandhandler("急停未触发\n", 0);
            }
            // 伺服使能标志
            if ((lAxisStatus & 0x200) == 0x200)
            {
                axiCondition.ServoOn = true;
                bFlagServoOn = 1;
                Config.commandhandler("伺服使能\n", 1);
            }
            else
            {
                axiCondition.ServoOn = false;
                bFlagServoOn = 0;
                Config.commandhandler("伺服关闭\n", 0);
            }
            // 规划器正在运动标志
            if ((lAxisStatus & 0x400) == 0x400)
            {
                axiCondition.profileMotion = true;
                bFlagMotion = 1;
                Config.commandhandler("规划器正在运动\n", 1);
            }
            else
            {
                axiCondition.profileMotion = false;
                bFlagMotion = 0;
                Config.commandhandler("规划器已停止\n", 0);
            }
            return axiCondition;
        }

        public static AxiSituation JustReadCondition(short axi, short card)
        {
            short bFlagAlarm = 0;           // 伺服报警标志
            short bFlagMError = 0;          // 跟随误差越限标志
            short bFlagPosLimit = 0;        // 正限位触发标志
            short bFlagNegLimit = 0;        // 负限位触发标志
            short bFlagSmoothStop = 0;      // 平滑停止标志
            short bFlagAbruptStop = 0;      // 急停标志
            short bFlagServoOn = 0;         // 伺服使能标志
            short bFlagMotion = 0;          // 规划器运动标志
            short sRtn;                     // 指令返回值变量
            int lAxisStatus;                // 轴状态
            uint pClock;                    //读取时钟

            AxiSituation axiCondition;
            // 读取轴状态
            sRtn = mc.GT_GetSts(card, axi, out lAxisStatus, 1, out pClock);
            // 伺服报警标志
            if ((lAxisStatus & 0x2) == 2)//
            {
                axiCondition.servoAlarm = true;
                bFlagAlarm = 1;
            }
            else
            {
                bFlagAlarm = 0;
                axiCondition.servoAlarm = false;
            }
            // 跟随误差越限标志
            if ((lAxisStatus & 0x10) == 0x10)
            {
                bFlagMError = 1;
                axiCondition.flagError = true;
               
            }
            else
            {
                axiCondition.flagError = false;
                bFlagMError = 0;
                //Config.commandhandler("运动正常\n", bFlagMError);
            }
            // 正向限位
            if ((lAxisStatus & 0x20) == 0x20)
            {
                axiCondition.positveLimit = true;
                bFlagPosLimit = 1;
               // Config.commandhandler("正限位触发\n", bFlagPosLimit);
            }
            else
            {
                axiCondition.positveLimit = false;
                bFlagPosLimit = 0;
               // Config.commandhandler("正限位未触发\n", bFlagPosLimit);
            }
            // 负向限位
            if ((lAxisStatus & 0x40) == 0x40)
            {
                axiCondition.negativeLimit = true;
                bFlagNegLimit = 1;
                Config.commandhandler("负限位触发\n", 1);
            }
            else
            {
                axiCondition.negativeLimit = false;
                //bFlagNegLimit = FALSE;
               // Config.commandhandler("负限位未触发\n", 1);
            }
            // 平滑停止
            if ((lAxisStatus & 0x80) == 0x80)
            {
                axiCondition.smoothStop = true;

                bFlagSmoothStop = 1;
                //Config.commandhandler("平滑停止触发\n", 1);
            }
            else
            {
                axiCondition.smoothStop = false;
                bFlagSmoothStop = 0;
               // Config.commandhandler("平滑停止未触发\n", 0);
            }
            // 急停标志
            if ((lAxisStatus & 0x100) == 0x100)
            {
                axiCondition.abruptStop = true;
                bFlagAbruptStop = 1;
               // Config.commandhandler("急停触发\n", 1);
            }
            else
            {
                axiCondition.abruptStop = false;
                bFlagAbruptStop = 0;
               // Config.commandhandler("急停未触发\n", 0);
            }
            // 伺服使能标志
            if ((lAxisStatus & 0x200) == 0x200)
            {
                axiCondition.ServoOn = true;
                bFlagServoOn = 1;
                //Config.commandhandler("伺服使能\n", 1);
            }
            else
            {
                axiCondition.ServoOn = false;
                bFlagServoOn = 0;
               // Config.commandhandler("伺服关闭\n", 0);
            }
            // 规划器正在运动标志
            if ((lAxisStatus & 0x400) == 0x400)
            {
                axiCondition.profileMotion = true;
                bFlagMotion = 1;
               // Config.commandhandler("规划器正在运动\n", 1);
            }
            else
            {
                axiCondition.profileMotion = false;
                bFlagMotion = 0;
               // Config.commandhandler("规划器已停止\n", 0);
            }
            return axiCondition;
        }


    }
}
