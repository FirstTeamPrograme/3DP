using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gts;
using GoogolConfig;
using FormStart;
using System.IO;
using 另存;

namespace ReadCondition
{
    public partial class mainForm : Form
    {
        //#region 控件大小随窗体大小等比例缩放
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        string str = "";
        //设置Tag属性值
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        //重置各个控件的大小
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
        }

        public mainForm()
        {
            InitializeComponent();
            //窗体大小初始化
            x = this.Width;
            y = this.Height;
            setTag(this);
        }

        private void 实时参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void IO开关ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IO开关 frmIO = new IO开关();
            //frmIO.MdiParent = this;
            frmIO.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            int DeskWidth = Screen.PrimaryScreen.WorkingArea.Width;//获取桌面宽度
            int DeskHeight = Screen.PrimaryScreen.WorkingArea.Height;//获取桌面高度
            this.Width = Convert.ToInt32(DeskWidth * 0.96);//设置窗体宽度
            this.Height = Convert.ToInt32(DeskHeight * 0.96);//设置窗体高度
            this.Left = Convert.ToInt32(DeskWidth * 0.02);
            this.Top = Convert.ToInt32(DeskHeight * 0.02);
           this.BackColor = Color.White;
            //this.StartPosition = FormStartPosition.CenterScreen;
            timer1.Enabled = true;
            //状态栏显示
            this.toolStripStatusLabel1.Text = "当前系统时间：";
            this.toolStripStatusLabel3.Text = "||  工作进程：";
            this.toolStripStatusLabel4.Text = str + "0 %";



           short sRtn = Config.openCard(0);
            if (sRtn!=0)
            {
                MessageBox.Show("控制卡出错");
            }
        }

        private void 手动操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            手动操作框 frm = new 手动操作框();
            //frmIO.MdiParent = this;
            frm.Show();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics ghs = e.Graphics;
            Matrix m = new Matrix();
            ghs.Transform.Translate(pictureBox1.Width/2, pictureBox1.Height / 2);
            //pictureBox1.Tran

            Pen myPen = new Pen(Color.OrangeRed, 1);
            Point p1 = new Point(0, pictureBox1.Height / 2);
            //Point p1 = new Point(0, 0);
            Point p2 = new Point(pictureBox1.Width, pictureBox1.Height / 2);
            ghs.DrawLine(myPen,p1,p2);

            Point p3 = new Point(pictureBox1.Width/2, 0);
            Point p4 = new Point(pictureBox1.Width/2, pictureBox1.Height );
            ghs.DrawLine(myPen, p3, p4);
            myPen.Dispose();

        }

        private void mainForm_SizeChanged(object sender, EventArgs e)
        {
           


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = DateTime.Now.ToString();
        }

        private void TimeBox_TextChanged(object sender, EventArgs e)
        {
            //TimeBox.BackColor = Color.Chocolate;
        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            setControls(newx, newy, this);
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream myStream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                char[] separator = { '\\' };
                string[] splitstrings = new string[30];
                splitstrings = myStream.Name.Split(separator);
                //ShowFile.Items.Add(" 序号 " + "        文件名       " +"       工艺策略       ");

                int n=ShowFile.Items.Count+1;
                ShowFile.Items.Add(" "+n+":"+splitstrings[splitstrings.Length - 1]);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ShowFile.SelectedItems.Count == 0)
            { MessageBox.Show("请选中要删除的项目！"); }
            else
            {
                while (ShowFile.SelectedItems.Count != 0)
                { ShowFile.Items.Remove(ShowFile.SelectedItem); }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            另存文件 lincun = new 另存文件();
            lincun.Show();


        }
    }
}
