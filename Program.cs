using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormStart;
//using ReadCondition;
namespace ReadCondition
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new mainForm());
           // Application.Run(new Login());
            Login fr = new Login();
            fr.ShowDialog();
            if (fr.DialogResult == DialogResult.OK)
            {
               
                Application.Run(new mainForm());
                fr.Close();
            }
            else if (fr.DialogResult == DialogResult.No)
            {
                Application.Exit();
                //fr.Close();
            }
            else { return; }
        }
    }
}
