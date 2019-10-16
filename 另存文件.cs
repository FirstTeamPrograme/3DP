using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 另存
{
    public partial class 另存文件 : Form
    {
        public 另存文件()
        {
            InitializeComponent();
        }

        private void 另存文件_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBDialog = new FolderBrowserDialog();
            if (FBDialog.ShowDialog()==DialogResult.OK)
            {
                string strPath = FBDialog.SelectedPath;
                if (strPath.EndsWith("\\"))
                {
                    textBox1.Text = strPath;
                }
                else
                { textBox1.Text = strPath + "\\"; }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text + textBox2.Text;
            DirectoryInfo DInfo = new DirectoryInfo(str);
            DInfo.Create();

            if (DInfo.Exists)
            {
                MessageBox.Show("文件存在");

            }
        }
    }
}
