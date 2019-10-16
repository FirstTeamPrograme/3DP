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
//using GoogolConfig;

namespace ReadCondition
{
   
    public partial class IO开关 : Form

    {
        public IO开关()
        {
            InitializeComponent();
        }

        private void IO开关_Load(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0,12, 1, 0);
                
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 1, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 2, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 2, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 3, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 3, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 4, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 4, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 5, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 5, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 6, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 6, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 7, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 7, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 8, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 8, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 9, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 9, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 10, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 10, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 11, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 11, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 12, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 1, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 13, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 13, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 14, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 14, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 15, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 15, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.CheckState == CheckState.Checked)
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 16, 0);
                MessageBox.Show("对do输出低电平");
            }
            else
            {
                short sRtn;
                sRtn = mc.GT_SetDoBit(0, 12, 16, 1);
                MessageBox.Show("对do输出高电平");

            }
        }

        //private void switchControl1_Load(object sender, EventArgs e)
        //{
        //    //switchControl1.isSwitch;
        //}

        //private void switchControl1_Click(object sender, EventArgs e)
        //{
        //    if (switchControl1.isSwitch == true)
        //    {
        //        MessageBox.Show("关闭");

        //    }
        //    else
        //    { MessageBox.Show("开启"); }
       

        //}

        //private void checkBox1_Click(object sender, EventArgs e)
        //{

        //}
    }
}
