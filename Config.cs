using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gts;
using ReadCondition;
using System.Windows.Forms;

namespace GoogolConfig
{
    class Config
    {
        public static void commandhandler(string command, short error)
        {
            // 如果指令执行返回值为非0，说明指令执行错误，向屏幕输出错误结果
            if (error != 0)
            {
                MessageBox.Show(command + ":" + error);
            }
        }
        //打开运动控制器
        public static short openCard(short p_card)
        {
            short f = mc.GT_Open(p_card, 0, 0);
            if (f == 0)
            {
                f = mc.GT_Reset(p_card);
                if (f == 0)
                { return f; }
                else
                {
                    commandhandler("Card is error!", f);
                    return f;
                }
            }
            else
            {
                commandhandler("Card is error!", f);
                return f;
            }
        }
    }



}
