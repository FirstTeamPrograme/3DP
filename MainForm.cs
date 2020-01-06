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
using Interface_WindowsFormsApp_;
using ReadFile;
using System.IO;
using DrawImage.MianForm;
using Global.MianForm;
using 电机参数框.perForm;
using 激光参数框.perForm;
using 填充参数对话框.perForm;


namespace ReadCondition
{
    public partial class mainForm : Form
    {
        #region 放大缩小
        public static float PanelX;//= 0;
        // public float PX = panel1.Width;
        public static float PanelY;//= panel1.Width;
        //#region 控件大小随窗体大小等比例缩放
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        string str = "";
        //遍历panel控件
        private void GetPannelControl(Panel panelCtr)
        {
            foreach (Control con in panelCtr.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    SetTag(con);
                }
            }
        }

        //设置Tag属性值
        private void SetTag(Control cons)
        {   //遍历所有控件内的所有控件
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    SetTag(con);
                }
            }
        }
        //重置各个控件的大小
        private void SetControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx) - 1;//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy) - 1;//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx) - 1;//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy) - 1;//顶边距
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy - 1;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);

                    if (con.Controls.Count > 0)
                    {
                        SetControls(newx, newy, con);
                    }
                }
            }
        }
        #endregion
        public mainForm()
        {
            InitializeComponent();
            //窗体大小初始化
            x = this.Width;
            y = this.Height;
            SetTag(this);
            //主对话框的界面设计
            int DeskWidth = Screen.PrimaryScreen.WorkingArea.Width;//获取桌面宽度
            int DeskHeight = Screen.PrimaryScreen.WorkingArea.Height;//获取桌面高度
            this.Width = Convert.ToInt32(DeskWidth * 0.96);//设置窗体宽度
            this.Height = Convert.ToInt32(DeskHeight * 0.96);//设置窗体高度
            this.Left = Convert.ToInt32(DeskWidth * 0.02);
            this.Top = Convert.ToInt32(DeskHeight * 0.02);

            this.Resize += new EventHandler(mainForm_Resize);
            //界面个部分设置
            groupBox1.Width = Convert.ToInt32(0.2 * this.Width);
            groupBox2.Width = Convert.ToInt32(0.2 * this.Width);
            panel1.Width = Convert.ToInt32(0.5 * this.Width);
            groupBox1.Left = Convert.ToInt32(0.02 * this.Width); ;
            dataGridView2.Left = 1;
            dataGridView2.Width = groupBox1.Width - 2;

            groupBox4.Left = Convert.ToInt32(0.02 * this.Width);
            groupBox4.Width = groupBox1.Width;
            panel1.Left = groupBox1.Width + Convert.ToInt32(0.04 * this.Width);

            groupBox2.Left = groupBox1.Width + panel1.Width + Convert.ToInt32(0.06 * this.Width);
            treeView1.Left = 1;
            treeView1.Width = groupBox2.Width - 2;

            groupBox3.Left = groupBox1.Width + panel1.Width + Convert.ToInt32(0.06 * this.Width);
            groupBox3.Width = groupBox2.Width;
            dataGridView1.Left = 1;
            dataGridView1.Width = groupBox3.Width - 2;




        }

        private void 实时参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void IO开关ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IO开关 frmIO = new IO开关();
            //frmIO.MdiParent = this;
            if (!全局类.SHowOpen(frmIO.Name))
            { frmIO.Show(); }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            全局类.状态参数 = 全局类.状态参数1;
            dataGridView2.GridColor = Color.Blue;
            dataGridView2.DataSource = 全局类.状态参数;

            //有多少变量就需要设置多少个列宽
            int wh = dataGridView2.Width - dataGridView2.RowHeadersWidth;
            dataGridView2.Columns[0].Width = wh / 2;//设置列宽
            dataGridView2.Columns[1].Width = wh / 2 - 4;

            timer1.Enabled = true;
            //状态栏显示
            this.toolStripStatusLabel1.Text = "当前系统时间：";
            this.toolStripStatusLabel3.Text = "||  工作进程：";
            this.toolStripStatusLabel4.Text = str + "0 %";


            //验证固高控制卡
            short sRtn = Config.openCard(0);
            if (sRtn != 0)
            {
                MessageBox.Show("控制卡出错");
            }
        }

        private void 手动操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            手动操作框 frm = new 手动操作框();
            //frmIO.MdiParent = this;
            if (!全局类.SHowOpen(frm.Name))
            { frm.Show(); }
        }

        private void mainForm_SizeChanged(object sender, EventArgs e)
        {



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = DateTime.Now.ToString();

            for (int i = 0; i < 全局类.状态参数.Count; i++)
            {
                if (全局类.状态参数[i].名称 == "开始时间:")
                {
                    string array = DateTime.Now.ToString();
                    string[] str = array.Split(' ');
                    全局类.状态参数[i].参数 = str[1];
                    dataGridView2.Refresh();
                }

            }
        }

        private void TimeBox_TextChanged(object sender, EventArgs e)
        {
            //TimeBox.BackColor = Color.Chocolate;
        }
        //屏幕修改
        private void mainForm_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            //SetControls(newx, newy, this);

        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        static List<string> CliMessage(STL.CLI cli)
        {
            List<string> strList = new List<string>();
            string 总层数 = "总层数：" + cli.LayerNumber;
            strList.Add(总层数);
            string 层厚 = "层厚:" + cli.LayerThickness;
            strList.Add(层厚);
            string 起始层 = "起始层:" + cli.StartLayer;
            strList.Add(起始层);
            string 类型 = "类型:" + cli.PartType;
            strList.Add(类型);
            return strList;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle Rect = new Rectangle(0, 0, panel1.Width, panel1.Height);
            DImage.初始化画图区(Rect, e);
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(*.cli)|*.cli";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in openFileDialog1.FileNames)
                {
                    if (!File.Exists(s))
                    { MessageBox.Show("文件不存在"); }
                    else
                    {
                        string pathName = System.IO.Path.GetDirectoryName(s);
                        string fileName = System.IO.Path.GetFileNameWithoutExtension(s);
                        //  textBox1.Text = fileName;
                        TreeNode tn = treeView1.Nodes.Add(fileName);
                        FileStream mystream = new FileStream(s, FileMode.Open, FileAccess.Read);
                        STL.CLI cli = new STL.CLI();
                        cli = STL.ReadCLI(s);
                        List<string> strList = CliMessage(cli);
                        for (int i = 0; i < strList.Count; i++)
                        {
                            TreeNode tn1 = new TreeNode(strList[i]);
                            tn.Nodes.Add(tn1);
                        }
                        //STL.STLFile.Add(cli);
                    }
                }
                return;

            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            { treeView1.Nodes.Remove(treeView1.SelectedNode); }
            else
            {
                MessageBox.Show("未选中要删除的内容！");
            }
        }
        //datagridview的控件重绘
        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X + 2,
                  e.RowBounds.Location.Y,
                  dataGridView2.RowHeadersWidth - 4,
                  e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridView2.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dataGridView2.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(*.cli)|*.cli";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in openFileDialog1.FileNames)
                {
                    if (!File.Exists(s))
                    { MessageBox.Show("文件不存在"); }
                    else
                    {
                        string pathName = System.IO.Path.GetDirectoryName(s);
                        string fileName = System.IO.Path.GetFileNameWithoutExtension(s);
                        //  textBox1.Text = fileName;
                        TreeNode tn = treeView1.Nodes.Add(fileName);
                        FileStream mystream = new FileStream(s, FileMode.Open, FileAccess.Read);
                        STL.CLI cli = new STL.CLI();
                        cli = STL.ReadCLI(s);
                        List<string> strList = CliMessage(cli);
                        for (int i = 0; i < strList.Count; i++)
                        {
                            TreeNode tn1 = new TreeNode(strList[i]);
                            tn.Nodes.Add(tn1);
                        }
                        //STL.STLFile.Add(cli);
                    }
                }
            }
        }

        private void 运动参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            电机参数对话框 frm = new 电机参数对话框();
            if (!全局类.SHowOpen(frm.Name))
            { frm.Show(); }
        }

        private void 激光参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            激光参数 frm = new 激光参数();
            if (!全局类.SHowOpen(frm.Name))
            { frm.Show(); }
        }

        private void 填充参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            填充参数 frm = new 填充参数();
            if (!全局类.SHowOpen(frm.Name))
            {
                frm.Show();
            }
        }
    }
}
