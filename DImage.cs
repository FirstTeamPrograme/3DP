using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DrawImage.MianForm
{
   public class DImage
    {
        void DrawImg(Rectangle Rect,int zoom,Rectangle WorkPlace,PointF MouseDownP)//缩放重绘
        {
            Bitmap bt = new Bitmap(Rect.Width, Rect.Height);
            Graphics g = Graphics.FromImage(bt);
            
            //覆盖绘图区域
            g.FillRectangle(new SolidBrush(Color.WhiteSmoke),Rect);
            //画笔
            Pen p = new Pen(Color.Black, 1);//定义了一个黑色,宽度为1的画笔
            Pen p01 = new Pen(Color.White, 1);//定义一个白色画笔
            //画工作区的矩形
            g.DrawRectangle(p, WorkPlace);
            //画刷
            SolidBrush b1 = new SolidBrush(Color.Blue);
            SolidBrush b2 = new SolidBrush(Color.White);
            //实线风格
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            //
            WorkPlace.Height = WorkPlace.Height * 5 * zoom;
            WorkPlace.Width = WorkPlace.Width * 5 * zoom;
            WorkPlace.Location = new Point(  Convert.ToInt32((1200 / 2 + 20 - 50 * 5 - MouseDownP.X) * zoom + MouseDownP.X), 
                                              Convert.ToInt32((700 / 2 + 20 - 50 * 5 - MouseDownP.Y) * zoom + MouseDownP.Y));

            g.DrawRectangle(p, WorkPlace);

            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            for (int i = -50; i <= 50; i = i + 10)
            {
                if (i % 10 == 0)
                {
                    Point P1 = new Point(Convert.ToInt32((1200 / 2 + 20 + i * 5 - MouseDownP.X) * zoom + MouseDownP.X),
                                         Convert.ToInt32((700 / 2 + 20 - 50 * 5 - MouseDownP.Y) * zoom + MouseDownP.Y));
                    Point P2 = new Point(Convert.ToInt32((1200 / 2 + 20 - 50 * 5 - MouseDownP.X) * zoom + MouseDownP.X),
                        Convert.ToInt32((700 / 2 + 20 + i * 5 - MouseDownP.Y) * zoom + MouseDownP.Y));
                    g.DrawLine(p, P1, P2);
                    Point P3 = new Point(Convert.ToInt32((1200 / 2 + 20 - 50 * 5 - MouseDownP.X) * zoom + MouseDownP.X),
                                        Convert.ToInt32((700 / 2 + 20 + i * 5 - MouseDownP.Y) * zoom + MouseDownP.Y));
                    Point P4 = new Point(Convert.ToInt32((1200 / 2 + 20 + 50 * 5 - MouseDownP.X) * zoom + MouseDownP.X),
                        Convert.ToInt32((700 / 2 + 20 + i * 5 - MouseDownP.Y) * zoom + MouseDownP.Y));

                    g.DrawLine(p, P3, P4);
                }
        
            }
            p.DashPattern = new float[] { 3, 3 };//设置短划线和空白部分的数组  5为虚线长度，1为虚线间距
            g.DrawLine(p, new Point(0,    Convert.ToInt32((700 / 2 + 20 + 0 * 5 - MouseDownP.Y) * zoom + MouseDownP.Y)), 
                          new Point(1220,   Convert.ToInt32((700 / 2 + 20 + 0 * 5 - MouseDownP.Y) * zoom + MouseDownP.Y)));
            g.DrawLine(p, new Point(Convert.ToInt32((1200 / 2 + 20 + 0 * 5 - MouseDownP.X) * zoom + MouseDownP.X),   0), 
                          new Point(Convert.ToInt32((1200 / 2 + 20 + 0 * 5 - MouseDownP.X) * zoom + MouseDownP.X), 720));

        }

        public static void 初始化画图区(Rectangle Rect, PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;//创建画板,这里的画板是由Form提供的.　　
            string[] zuobiaox = new string[31] { "-120", "-110", "-100", "-90", "-80", "-70", "-60", "-50", "-40", "-30", "-20", "-10", "0", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100", "110", "120", "250", "260", "270", "280", "290", "300" };
            string[] zuobiaoy = new string[31] { "-70", "-60", "-50", "-40", "-30", "-20", "-10", "0", "10", "20", "30", "40", "50", "60", "70", "80", "90", "170", "180", "190", "200", "210", "220", "230", "240", "250", "260", "270", "280", "290", "300" };
            Pen p = new Pen(Color.BlanchedAlmond, 1);//定义了一个黑色,宽度为1的画笔
            Pen p01 = new Pen(Color.White, 1);
           // Rectangle rect = new Rectangle(0, 0, 1220, 720);
            g.DrawRectangle(p, Rect);
            Rectangle rect01 = new Rectangle(0, 0, 20, Rect.Height);
            Rectangle rect02 = new Rectangle(0, 0, Rect.Width, 20);
            SolidBrush b1 = new SolidBrush(Color.CornflowerBlue);
            SolidBrush b2 = new SolidBrush(Color.White);
            g.FillRectangle(b1, rect01);
            g.FillRectangle(b1, rect02);

            for (int i = 0; i <= Rect.Width; i = i + 5)
            {
                if ((i - 20) % 50 == 0)
                {
                    g.DrawLine(p01, i, 15, i, 20);
                    g.DrawString(zuobiaox[(i - 20) / 50], new Font("宋体", 10), b2, new Point(i - 10, 0));

                }
                else if (i % 5 == 0 && i >= 20)
                {
                    g.DrawLine(p01, i, 18, i, 20);
                }
            }
            for (int i = 0; i <= Rect.Height; i = i + 5)
            {
                if ((i - 20) % 50 == 0)
                {

                    StringFormat format1 = new StringFormat();
                    //指定字符串的水平对齐方式
                    format1.Alignment = StringAlignment.Far;
                    //表示字符串的垂直对齐方式
                    format1.LineAlignment = StringAlignment.Center;
                    //旋转角度和平移
                    System.Drawing.Drawing2D.Matrix mtxRotate = g.Transform;

                    mtxRotate.RotateAt(90, new PointF(5, 10));
                    g.Transform = mtxRotate;

                    g.DrawString(zuobiaoy[(i - 20) / 50], new Font("宋体", 10), b2, new Point(i - 10, 0));

                    g.ResetTransform();
                    g.DrawLine(p01, 15, i, 20, i);
                }
                else if (i % 5 == 0 && i >= 20)
                {
                    g.DrawLine(p01, 18, i, 20, i);
                }
            }

            p.DashPattern = new float[] { 3, 3 };//设置短划线和空白部分的数组  5为虚线长度，1为虚线间距
            //中心线
            g.DrawLine(p, new Point((Rect.Width-20) / 2 + 20, 20), new Point((Rect.Width - 20) / 2 + 20, Rect.Height));
            g.DrawLine(p, new Point(20, (Rect.Height - 20) / 2 + 20), new Point(Rect.Width, (Rect.Height - 20) / 2 + 20));
            //改变坐标原点
            g.TranslateTransform((Rect.Width - 20) / 2 + 20, (Rect.Height - 20) / 2 + 20);

        }
    }
}
