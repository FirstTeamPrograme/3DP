using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using gts;
using GoogolConfig;
using FormStart;
using Interface_WindowsFormsApp_;
using ReadFile;
using System.IO;
using DrawImage.MianForm;
using System.ComponentModel;

namespace Global.MianForm
{
    class 全局类
    {
        public class 状态
        {
            public string 名称 { get; set; }
            public string 参数 { get; set; }
        }
        public static BindingList<状态> 状态参数 { get; set; }

        public static BindingList<状态> 状态参数1 = new BindingList<状态>()
        {   new 状态() { 名称 = "开始时间:",参数 = "0:0:0"},
            new 状态() { 名称 = "已工作时长:", 参数 = "0:0:0" },
            new 状态() { 名称 = "剩余加工时长:", 参数 = "0:0:0" },
            new 状态() { 名称 = "成型缸:", 参数 = "0" },
            new 状态() { 名称 = "铺粉车:", 参数 = "0" },
            new 状态() { 名称 = "左粉料杠:", 参数 = "0" },
            new 状态() { 名称 = "右粉料缸:", 参数 = "0" },
            new 状态() { 名称 = "风速(m/s):", 参数 = "0" },
            new 状态() { 名称 = "含氧量(%):", 参数 = "0" }
        };

        #region  防止窗体重复打开
        //防止MDI窗体对话框重复打开
        public static bool ShowChildrenForm(string p_ChildrenFormText,Form This)
        {
            int i;     //依次检测当前窗体的子窗体     
            for (i = 0; i < This.MdiChildren.Length; i++)
            {
                if (This.MdiChildren[i].Text == p_ChildrenFormText)     //判断当前子窗体的Text属性值是否与传入的字符串值相同
                {
                    This.MdiChildren[i].Activate();   //如果值相同则表示此子窗体为想要调用的子窗体，激活此子窗体并返回true值
                    return true;
                }
            }
            return false;  //如果没有相同的值则表示要调用的子窗体还没有被打开，返回false值     
        }

        //防止重复打开子对话框
        public static bool SHowOpen(string frmName)
        {
            foreach (Form frm in Application.OpenForms) //遍历已打开窗口
            {
                if (frm.Name == frmName)    //如果此窗口已打开
                {
                    frm.Activate();            //激活当前窗体
                    if (frm.WindowState == FormWindowState.Minimized)   //如果当前窗体已经最小化
                    {
                        frm.WindowState = FormWindowState.Normal;   //还原窗体
                    }
                    return true;
                }
            }
            return false;
        }
        #endregion



    }
}
