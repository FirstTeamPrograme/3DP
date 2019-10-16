using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormStart
{
    public partial class Login : Form
    {
       // public static int login=-1; 
        public Login()
        {
            InitializeComponent();
        }

        private void FormStart_Load(object sender, EventArgs e)
        {
            //string Patch = Application.StartupPath.Substring(0, Application.StartupPath.Substring(0,
            //    Application.StartupPath.LastIndexOf("\\")).LastIndexOf("\\"));
            //Patch += @"\Picture\Login.png";
            //Image Ming = Image.FromFile(Patch, true);
            //imageList1.Images.Add(Ming);
            //pictureBox1.Image = 
            pictureBox1.BackgroundImage= imageList1.Images[0];
            //this.BackgroundImage= imageList1.Images[0];
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.BackColor = Color.GreenYellow;
            button2.BackColor = Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "aaa" && textBox2.Text == "aaa")
            {  //验证用户名密码成功
                //login = 1;
                this.DialogResult = DialogResult.OK;    //返回一个登录成功的对话框状态
                this.Close();    //关闭登录窗口
            }
            else
            {
                MessageBox.Show("登录信息填写不正确！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;    //返回一个登录成功的对话框状态
            this.Close();    //关闭登录窗口
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            { button1.Focus(); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.01;
        }
    }
}
