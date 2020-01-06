using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gts;
using Motion;

namespace ReadCondition
{
    public partial class 手动操作框 : Form
    {
        public 手动操作框()
        {
            InitializeComponent();
        }

        private void 手动操作框_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages[0].Text = "电机运动";
            tabControl1.TabPages[1].Text = "测试";

            tabControl2.TabPages[0].Text = "定量移动";
            tabControl2.TabPages[1].Text = "定速移动";

            tabPageAxi2.TabPages[0].Text = "定量速动";
            tabPageAxi2.TabPages[1].Text = "定速速动";


            numericUpDown1.Maximum = 30000;
            numericUpDown1.Minimum =1;

            numericUpDown2.Maximum = 30000;
            numericUpDown2.Minimum = 1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mc.TTrapPrm trap=new mc.TTrapPrm();
            trap.acc = 0.25;
            trap.dec = 0.125;
            trap.smoothTime = 25;
            MotionPunction.TrapParameter tp = new MotionPunction.TrapParameter();
            tp.position = Convert.ToInt32(numericUpDown2.Value);
            tp.sleep = Convert.ToInt32(numericUpDown1.Value);
            MotionPunction.Trap(true,trap,tp,1);
            //int AxiStatus;
            //uint pClock;
            //short sRtn1 = mc.GT_GetSts(0,1,out AxiStatus,1,out pClock);
            //if ((AxiStatus&0x2)==0x2&&(AxiStatus&0x20)==0x20)
            //{
            //    MessageBox.Show("伺服报警或处于正限位");
            //}
            //else
            //{
            //    short sRtn;
            //    mc.TTrapPrm trap;
                
            //    //清除各轴的报警和限位
            //    sRtn = mc.GT_ClrSts(0,1, 8);
            //    if (sRtn != 0)
            //    {
            //        MessageBox.Show("清除出错");
            //    }
            //    // 伺服使能
            //    sRtn = mc.GT_AxisOn(0,1);
            //    if (sRtn != 0)
            //    {
            //        MessageBox.Show("使能出错");
            //    }
            //    // 位置清零
            //    sRtn = mc.GT_ZeroPos(0,1,1);
            //    if (sRtn != 0)
            //    {
            //        MessageBox.Show("位置清出错");
            //    }
            //    // AXIS轴规划位置清零
            //    sRtn = mc.GT_SetPrfPos(0,1, 0);
            //    if (sRtn != 0)
            //    {
            //        MessageBox.Show("规划出错");
            //    }
            //    // 将AXIS轴设为点位模式
            //    sRtn = mc.GT_PrfTrap(0,1);
            //    if (sRtn != 0)
            //    {
            //        MessageBox.Show("模式出错");
            //    }
            //    // 读取点位运动参数
            //    sRtn = mc.GT_GetTrapPrm(0,1, out trap);
            //    if (sRtn != 0)
            //    {
            //        MessageBox.Show("读取参数出错");
            //    }
            //    trap.acc = 0.25;
            //    trap.dec = 0.125;
            //    trap.smoothTime = 25;
            //    // 设置点位运动参数
            //    sRtn = mc.GT_SetTrapPrm(0,1, ref trap);
            //    if (sRtn != 0)
            //    {
            //        MessageBox.Show("设置参数出错");
            //    }
            //    // 设置AXIS轴的目标位置
            //    int posation = Convert.ToInt32(numericUpDown2.Value);
            //    sRtn =mc.GT_SetPos(0,1, posation);
            //    if (sRtn != 0)
            //    {
            //        MessageBox.Show("位置出错");
            //    }
            //    // 设置AXIS轴的目标速度
            //    double 速度 = Convert.ToDouble(numericUpDown1.Value);
            //    sRtn = mc.GT_SetVel(0,1, 速度);
            //    if (sRtn != 0)
            //    {
            //        MessageBox.Show("速度出错");
            //    }
            //    // 启动AXIS轴的运动
            //    sRtn = mc.GT_Update(0,1);
            //    if (sRtn != 0)
            //    {
            //        MessageBox.Show("运动出错");
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int AxiStatus;
            uint pClock=0;
            short sRtn1 = mc.GT_GetSts(0, 1, out AxiStatus, 1, out pClock);
            if ((AxiStatus & 0x2) == 0x2 && (AxiStatus & 0x20) == 0x20)
            {
                MessageBox.Show("伺服报警或处于正限位");
            }
            else
            {
                short sRtn;
                mc.TTrapPrm trap;

                //清除各轴的报警和限位
                sRtn = mc.GT_ClrSts(0, 1, 8);
                if (sRtn != 0)
                {
                    MessageBox.Show("清除出错");
                }
                // 伺服使能
                sRtn = mc.GT_AxisOn(0, 1);
                if (sRtn != 0)
                {
                    MessageBox.Show("使能出错");
                }
                // 位置清零
                sRtn = mc.GT_ZeroPos(0, 1, 1);
                if (sRtn != 0)
                {
                    MessageBox.Show("位置清出错");
                }
                // AXIS轴规划位置清零
                sRtn = mc.GT_SetPrfPos(0, 1, 0);
                if (sRtn != 0)
                {
                    MessageBox.Show("规划出错");
                }
                // 将AXIS轴设为点位模式
                sRtn = mc.GT_PrfTrap(0, 1);
                if (sRtn != 0)
                {
                    MessageBox.Show("模式出错");
                }
                // 读取点位运动参数
                sRtn = mc.GT_GetTrapPrm(0, 1, out trap);
                if (sRtn != 0)
                {
                    MessageBox.Show("读取参数出错");
                }
                trap.acc = 0.25;
                trap.dec = 0.125;
                trap.smoothTime = 25;
                // 设置点位运动参数
                sRtn = mc.GT_SetTrapPrm(0, 1, ref trap);
                if (sRtn != 0)
                {
                    MessageBox.Show("设置参数出错");
                }
                // 设置AXIS轴的目标位置
                int posation = Convert.ToInt32(numericUpDown2.Value);
                sRtn = mc.GT_SetPos(0, 1, -posation);
                if (sRtn != 0)
                {
                    MessageBox.Show("位置出错");
                }
                // 设置AXIS轴的目标速度
                double 速度 = Convert.ToDouble(numericUpDown1.Value);
                sRtn = mc.GT_SetVel(0, 1, 速度);
                if (sRtn != 0)
                {
                    MessageBox.Show("速度出错");
                }
                // 启动AXIS轴的运动
                sRtn = mc.GT_Update(0, 1);
                if (sRtn != 0)
                {
                    MessageBox.Show("运动出错");
                }
               // sRtn = mc.GT_AxisOff(0, 1);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            short sRtn = mc.GT_Stop(0,1,1);
            sRtn = mc.GT_AxisOff(0, 1);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            mc.TJogPrm jopPra = new mc.TJogPrm();
            jopPra.acc = 0.25;
            jopPra.dec = 0.25;
            double value = Convert.ToDouble(numericUpDown3.Value);
            MotionPunction.JogMotin(0,1,jopPra,value);
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            mc.GT_Stop(0,1,1);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MotionPunction.GoHome(1,0);
        }

        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            mc.TJogPrm jopPra = new mc.TJogPrm();
            jopPra.acc = 0.25;
            jopPra.dec = 0.25;
            double value = Convert.ToDouble(numericUpDown3.Value);
            MotionPunction.JogMotin(0, 1, jopPra, -value);
            
        }

        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            mc.GT_Stop(0, 1, 1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mc.TJogPrm jopPra = new mc.TJogPrm();
            jopPra.acc = 0.25;
            jopPra.dec = 0.25;
            double value = Convert.ToDouble(numericUpDown1.Value);
            while(true)
            {
                MotionPunction.JogMotin(0, 1, jopPra, value);
                AxiCondition.AxiSituation axiSituation = new AxiCondition.AxiSituation();
                axiSituation = AxiCondition.JustReadCondition(1, 0);
                if (axiSituation.negativeLimit||axiSituation.positveLimit)
                {
                    MotionPunction.GoSafetyPosition(1, 0);
                    break;
                }
            }
        }





    }
}
