using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interface_WindowsFormsApp_
{
    public partial class Zn : Form
    {
        //矩形框坐标
        Rectangle DrawRect = new Rectangle(100, 100, 100, 100);
        //鼠标按下时坐标
        private Point MouseDownP = new Point();
        //放大倍数
        private int zoom = 1;
        //鼠标移动矩形框次数，如果移动过矩形框则不从中心放大，以移动后的位置放大缩小，缩小为原大小，缩放数为0时，重置此数


        




        void DrawImg()//缩放重绘
        {

                Graphics g = Graphics.FromImage(bt);

                g.FillRectangle(new SolidBrush(Color.WhiteSmoke), 0, 0, 2000, 2000);
                Pen p = new Pen(Color.Black, 1);//定义了一个黑色,宽度为1的画笔
                Pen p01 = new Pen(Color.White, 1);
                Rectangle rect = new Rectangle(0, 0, 1220, 720);
                g.DrawRectangle(p, rect);

                SolidBrush b1 = new SolidBrush(Color.Blue);
                SolidBrush b2 = new SolidBrush(Color.White);

                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                rect.Height = 100 * 5 * zoom;
                rect.Width = 100 * 5 * zoom;
                rect.Location = new Point((1200 / 2 + 20 - 50 * 5 - MouseDownP.X) * zoom + MouseDownP.X, (700 / 2 + 20 - 50 * 5 - MouseDownP.Y) * zoom + MouseDownP.Y);
                g.DrawRectangle(p, rect);

                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                for (int i = -50; i <= 50; i = i + 10)
                {
                    if (i % 10 == 0)
                    {
                        g.DrawLine(p, new Point((1200 / 2 + 20 + i * 5 - MouseDownP.X) * zoom + MouseDownP.X, (700 / 2 + 20 - 50 * 5 - MouseDownP.Y) * zoom + MouseDownP.Y), new Point((1200 / 2 + 20 + i * 5 - MouseDownP.X) * zoom + MouseDownP.X, (700 / 2 + 20 + 50 * 5 - MouseDownP.Y) * zoom + MouseDownP.Y));
                        g.DrawLine(p, new Point((1200 / 2 + 20 - 50 * 5 - MouseDownP.X) * zoom + MouseDownP.X, (700 / 2 + 20 + i * 5 - MouseDownP.Y) * zoom + MouseDownP.Y), new Point((1200 / 2 + 20 + 50 * 5 - MouseDownP.X) * zoom + MouseDownP.X, (700 / 2 + 20 + i * 5 - MouseDownP.Y) * zoom + MouseDownP.Y));
                    }
                }
                p.DashPattern = new float[] { 3, 3 };//设置短划线和空白部分的数组  5为虚线长度，1为虚线间距
                g.DrawLine(p, new Point(0, (700 / 2 + 20 + 0 * 5 - MouseDownP.Y) * zoom + MouseDownP.Y), new Point(1220, (700 / 2 + 20 + 0 * 5 - MouseDownP.Y) * zoom + MouseDownP.Y));
                g.DrawLine(p, new Point((1200 / 2 + 20 + 0 * 5 - MouseDownP.X) * zoom + MouseDownP.X, 0), new Point((1200 / 2 + 20 + 0 * 5 - MouseDownP.X) * zoom + MouseDownP.X, 720));


            //displayGraphics.DrawImage(I, ClientRectangle);
            //this.CreateGraphics().DrawImage(bt, new Point(0, 0));
        }
        void DrawImgBG()//绘制背景
        {
            using (Graphics g = Graphics.FromImage(bt))
            {
                Pen p = new Pen(Color.Black, 1);//定义了一个黑色,宽度为1的画笔
                Pen p01 = new Pen(Color.White, 1);
                Rectangle rect = new Rectangle(0, 0, 1220, 720);
                g.DrawRectangle(p, rect);
                Rectangle rect01 = new Rectangle(0, 0, 20, 720);
                Rectangle rect02 = new Rectangle(0, 0, 1220, 20);
                SolidBrush b1 = new SolidBrush(Color.Blue);
                SolidBrush b2 = new SolidBrush(Color.White);
                g.FillRectangle(b1, rect01);
                g.FillRectangle(b1, rect02);
                //this.CreateGraphics().DrawImage(bt, new Point(0, 0));

            }
        }
        void DrawImgCoord()//绘制坐标系
        {
            using (Graphics g = Graphics.FromImage(bt))
            {

                string[] zuobiao_p = new string[31] {  "0", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100", "110", "120","130","140","150","160","170","180","190","200","210","220","230","240", "250", "260", "270", "280", "290", "300" };
                string[] zuobiao_n = new string[31] { "0", "-10", "-20", "-30", "-40", "-50", "-60", "-70", "-80", "-90", "-100", "-110", "-120", "-130", "-140", "-150", "-160", "-170", "-180", "-190", "-200", "-210", "-220", "-230", "-240", "-250", "-260", "-270", "-280", "-290", "-300" };

                Pen p = new Pen(Color.Black, 1);//定义了一个黑色,宽度为1的画笔
                Pen p01 = new Pen(Color.White, 1);
                Rectangle rect = new Rectangle(0, 0, 1220, 720);
                g.DrawRectangle(p, rect);

                SolidBrush b1 = new SolidBrush(Color.Blue);
                SolidBrush b2 = new SolidBrush(Color.White);

                //(1200 / 2 + 20 + 0 * 5 - MouseDownP.X) * zoom + MouseDownP.X
               // (700 / 2 + 20 + 0 * 5 - MouseDownP.Y) * zoom + MouseDownP.Y

                for(int i0= (1200 / 2 + 20 + 0 * 5 - MouseDownP.X) *zoom + MouseDownP.X,i=i0; i<=2000;i=i+5 * zoom)//X轴正坐标
                {
                    if((i-i0)%(50*zoom)==0)
                    {
                        g.DrawLine(p01, i, 15, i, 20);
                        g.DrawString(zuobiao_p[(i - i0) / (50 * zoom)], new Font("宋体", 10), b2, new Point(i, 0));
                    }
                    else if((i - i0) % (5 * zoom) == 0)
                    {
                        g.DrawLine(p01, i, 18, i, 20);
                    }
                }
                for (int i0 = (1200 / 2 + 20 + 0 * 5 - MouseDownP.X) * zoom + MouseDownP.X, i = i0; i >= 20; i = i - 5 * zoom)//X轴负坐标
                {
                    if ((i - i0) % (50 * zoom) == 0)
                    {
                        g.DrawLine(p01, i, 15, i, 20);
                        g.DrawString(zuobiao_n[(i0 - i) / (50 * zoom)], new Font("宋体", 10), b2, new Point(i, 0));
                    }
                    else if ((i - i0) % (5 * zoom) == 0)
                    {
                        g.DrawLine(p01, i, 18, i, 20);
                    }
                }
                for (int i0 = (700 / 2 + 20 + 0 * 5 - MouseDownP.Y) * zoom + MouseDownP.Y, i = i0; i <= 2000; i = i + 5 * zoom)//Y轴正坐标
                {
                    if ((i - i0) % (50 * zoom) == 0)
                    {
                        StringFormat format1 = new StringFormat();
                        //指定字符串的水平对齐方式
                        format1.Alignment = StringAlignment.Far;
                        //表示字符串的垂直对齐方式
                        format1.LineAlignment = StringAlignment.Center;
                        //旋转角度和平移
                        System.Drawing.Drawing2D.Matrix mtxRotate = g.Transform;

                        mtxRotate.RotateAt(90, new PointF(5,10));
                        g.Transform = mtxRotate;
                        if((i - i0) / (50 * zoom)>=0&& (i - i0) / (50 * zoom) <= 30)
                             g.DrawString(zuobiao_p[(i - i0) / (50 * zoom)], new Font("宋体", 10), b2, new Point(i, 0));
                        g.ResetTransform();
                        
                        g.DrawLine(p01, 15,i, 20, i);
                       
                    }
                    else if ((i - i0) % (5 * zoom) == 0)
                    {
                        g.DrawLine(p01, 18, i, 20, i);
                    }
                }
                for (int i0 = (700 / 2 + 20 + 0 * 5 - MouseDownP.Y) * zoom + MouseDownP.Y, i = i0; i >= 20; i = i - 5 * zoom)//Y轴负坐标
                {
                    if ((i - i0) % (50 * zoom) == 0)
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


                        g.DrawString(zuobiao_n[(i0 - i) / (50 * zoom)], new Font("宋体", 10), b2, new Point(i, 0));
                        g.ResetTransform();

                        g.DrawLine(p01, 15, i, 20, i);
                    }
                    else if ((i - i0) % (5 * zoom) == 0)
                    {
                        g.DrawLine(p01, 18,i, 20, i);
                    }
                }

               
            }
        }
        private void FirstPaintPicture(double xpoint, double ypoint, string TiffPath)
        {
        }

        private void ScalePaintPicture(double xpoint, double ypoint, string TiffPath)
        {
        }

        public Zn()//主程序
        {
            InitializeComponent();
           
            firstpoint = new Point(0, 0);
            secondpoint = new Point(0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.Width = 1220;
            this.Height = 720;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void Zn_Paint(object sender, PaintEventArgs e)//初始界面绘图
        {
            Graphics g =this.CreateGraphics();//创建画板,这里的画板是由Form提供的.　　
            string[] zuobiaox = new string[31] { "-120", "-110", "-100", "-90", "-80", "-70", "-60", "-50", "-40", "-30", "-20", "-10", "0", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100", "110", "120", "250", "260", "270", "280", "290", "300" };
            string[] zuobiaoy = new string[31] { "-70", "-60", "-50", "-40", "-30", "-20", "-10", "0", "10", "20", "30", "40", "50", "60", "70", "80", "90", "170", "180", "190", "200", "210", "220", "230", "240", "250", "260", "270", "280", "290", "300" };
            Pen p = new Pen(Color.Black, 1);//定义了一个黑色,宽度为1的画笔
            Pen p01 = new Pen(Color.White, 1);
            Rectangle rect = new Rectangle(0, 0, 1220, 720);
            g.DrawRectangle(p, rect);
            Rectangle rect01 = new Rectangle(0, 0, 20, 720);
            Rectangle rect02 = new Rectangle(0, 0, 1220, 20);
            SolidBrush b1 = new SolidBrush(Color.Blue);
            SolidBrush b2 = new SolidBrush(Color.White);
            g.FillRectangle(b1, rect01);
            g.FillRectangle(b1, rect02);



            for (int i = 0; i <= 1220; i = i + 5)
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
            for (int i = 0; i <= 720; i = i + 5)
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
            g.DrawLine(p, new Point(1200 / 2 + 20, 20), new Point(1200 / 2 + 20, 720));
            g.DrawLine(p, new Point(20, 700 / 2 + 20), new Point(1220, 700 / 2 + 20));
            g.TranslateTransform(1200 / 2 + 20, 700 / 2 + 20);

            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            rect.Height = 100 * (int)5;
            rect.Width = 100 * (int)5;
            rect.Location = new Point(-50 * (int)5, -50 * (int)5);
            g.DrawRectangle(p, rect);

            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            for (int i = -50; i <= 50; i = i + 10)
            {
                if (i % 10 == 0)
                {
                    g.DrawLine(p, new Point(i * 5, -50 * (int)5), new Point(i * (int)5, 50 * (int)5));
                    g.DrawLine(p, new Point(-50 * 5, i * (int)5), new Point(50 * (int)5, i * (int)5));
                }
            }
            
            //Picture_Drawing();
        }
        


        private float img_x = 620, img_y = 20+700/2, MouseDown_x, MouseDown_y, MouseUp_x = 0, MouseUp_y = 0, MouseWheel_x = 0, MouseWheel_y = 0, img_gx = 0, img_gy = 0;

        private void Zn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDown_x = e.X;
                MouseDown_y = e.Y;

                //if (e.X >= img_x && e.Y >= img_y && e.X <= img_x + img_w && e.Y <= img_y+img_h/*this.ClientRectangle.Contains(PointToClient(Cursor.Position))*/)
                //{
                //    if(select_SFlag ==1)
                //        select_SFlag = 0;
                //    else
                //        select_SFlag = 1;
                //}
                
            }
        }

        private int img_w, img_h, select_Flag = 0, select_SFlag=0, pzoom = 0;
        Bitmap bt = new Bitmap(2000, 2000);
        Image img = Image.FromFile(@"D:\a1_green.png");
        Rectangle img_r = new Rectangle();
        
        
        private void Picture_Drawing()
        {
            Graphics g = Graphics.FromImage(bt);
            DrawImg();
            
            //原图
            /*img_x = (img_x - MouseWheel_x) * pzoom + MouseWheel_x;
            img_y = (img_y - MouseWheel_y) * pzoom + MouseWheel_y;*/
            img_w = img.Width;
            img_h = img.Height;

            //img_x = (img_x - MouseWheel_x) * pzoom;
            //img_y = (img_y - MouseWheel_y) * pzoom;
            //g.DrawImage(img, img_x, img_y);
            if (select_Flag == 0 )
            {
                g.TranslateTransform(-img_x * zoom, -img_y * zoom);
                g.ScaleTransform((float)100.0 * (float)zoom / (float)100.0, (float)100.0 * (float)zoom / (float)100.0);
                g.TranslateTransform(img_x / zoom, img_y / zoom);
                g.DrawImage(img, img_x - ((MouseWheel_x - img_x) - (MouseWheel_x - img_x) / zoom), img_y - ((MouseWheel_y - img_y) - (MouseWheel_y - img_y) / zoom));
            }
            else
            {
                Brush br = new SolidBrush(Color.FromArgb(50, Color.Blue));
                if (pzoom != 0)
                {

                    g.TranslateTransform(-img_x * pzoom, -img_y * pzoom);
                    //g.ScaleTransform((float)100.0 * (float)pzoom / (float)100.0, (float)100.0 * (float)pzoom / (float)100.0);
                    g.ScaleTransform(pzoom, pzoom);
                    //g.TranslateTransform(-img_x / 2, -img_x / 2);
                    //g.TranslateTransform((-img_x - MouseWheel_x) / pzoom, (-img_y - MouseWheel_y) / pzoom);
                    g.TranslateTransform(img_x / pzoom, img_y / pzoom);
                    //g.TranslateTransform(img_gx, img_gy);


                    //g.DrawImage(img, img_x - MouseWheel_x / pzoom, img_y - MouseWheel_y / pzoom);
                    //g.DrawImage(img, img_x, img_y );
                    g.DrawImage(img, img_x - ((MouseWheel_x - img_x) - (MouseWheel_x - img_x) / pzoom), img_y - ((MouseWheel_y - img_y) - (MouseWheel_y - img_y) / pzoom));
                    Rectangle rb = new Rectangle((int)img_x - (((int)MouseWheel_x - (int)img_x) - ((int)MouseWheel_x - (int)img_x) / pzoom),
                   (int)img_y - (((int)MouseWheel_y - (int)img_y) - ((int)MouseWheel_y - (int)img_y) / pzoom), img_w, img_h);
                    g.FillRectangle(br, rb);

                }
                else
                {
                    g.DrawImage(img, img_x, img_y);
                    Rectangle rb = new Rectangle((int)img_x ,(int)img_y, img_w, img_h);
                    g.FillRectangle(br, rb);
                }
            }
            

            
            img_r.X = (int)img_x;
            img_r.Y = (int)img_y;
            img_r.Width = (int)img_w;
            img_r.Height = (int)img_h;

            DrawImgBG();
            DrawImgCoord();
            //this.CreateGraphics().DrawImage(bt, new Point(0, 0));
        }
        private void Zn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDown_x = e.X;
                MouseDown_y = e.Y;
                firstpoint.X = e.X;
                firstpoint.Y = e.Y;

                //if (this.ClientRectangle.Contains(PointToClient(Cursor.Position))/*e.X >= img_x && e.Y >= img_y && e.X <= img_x + img_w && e.Y <= img_y + img_h*/)
                if (e.X >= img_x && e.Y >= img_y && e.X <= img_x + img_w && e.Y <= img_y + img_h)
                {
                    //select_Flag = 1;
                }
                else
                {
                    select_Flag = 0;

                }
            }
        }

        private void Zn_MouseMove(object sender, MouseEventArgs e)
        {
            if (select_Flag == 1 && e.Button == MouseButtons.Left)
            {
                img_x = img_x + (e.X - MouseDown_x);
                img_y = img_y + (e.Y - MouseDown_y);
                MouseDown_x = e.X;
                MouseDown_y = e.Y;
                //Picture_Drawing();
                sr_Flag = 1;
                Select_rectangle();

            }
            if (e.Button == MouseButtons.Left)
            {
                secondpoint.X = e.X;
                secondpoint.Y = e.Y;
                if (select_Flag == 0)
                {
                    Select_rectangle();
                }

            }

            //if(select_Flag == 0)


        }
        
        Point firstpoint;
        Point secondpoint;
        private void Zn_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUp_x = e.X;
            MouseUp_y = e.Y;
            
            if(select_Flag ==0)
            {
                Select_rectangle();
            }
            if ( img_x>= firstpoint.X &&  img_y>= firstpoint.Y &&  img_x + img_w<= secondpoint.X &&  img_y + img_h<= secondpoint.Y )
            {
                select_Flag = 1;
                
            }
            else
            {
                select_Flag = 0;
                
            }
            sr_Flag = 1;
            Select_rectangle();
        }
        static int sr_Flag=0;
        private void Select_rectangle()
        {
            if(sr_Flag ==0)
            {
                Graphics gra = Graphics.FromImage(bt);
               // Graphics gra = this.CreateGraphics();
                Pen drawPen = new Pen(Color.DarkSlateBlue, (float)0.5);
                drawPen.DashPattern = new float[] { 3, 3 };

                Picture_Drawing();
                SolidBrush b1 = new SolidBrush(Color.FromArgb(50, Color.Green));
                
                Rectangle r = new Rectangle(firstpoint.X, firstpoint.Y, (secondpoint.X - firstpoint.X), secondpoint.Y - firstpoint.Y);

                gra.DrawRectangle(drawPen, r);
                gra.FillRectangle(b1, r);
                this.CreateGraphics().DrawImage(bt, new Point(0, 0));

            }
            else
            {
                Graphics gra = this.CreateGraphics();
                //Graphics gra = this.CreateGraphics();
                Pen drawPen = new Pen(Color.DarkSlateBlue, (float)0.5);
                drawPen.DashPattern = new float[] { 3, 3 };
                Picture_Drawing();
                sr_Flag = 0;
                this.CreateGraphics().DrawImage(bt, new Point(0, 0));
            }

        }




        private void Form1_MouseWheel(object sender, MouseEventArgs e)//滚轮滚动事件
        {
            MouseDownP.X = e.X;
            MouseDownP.Y = e.Y;
            MouseWheel_x = e.X;
            MouseWheel_y = e.Y;
            
            if (select_Flag != 1)
            {
                if (e.Delta > 0)
                {
                    if (zoom <= 50)
                        zoom++;
                    if (zoom > 20 && zoom <= 100)
                        zoom = zoom + 20;
                }

                else
                {
                    if (zoom > 0)
                        zoom--;

                    if (zoom == 0) //放大倍数＝0,不放大，鼠标拖动标记归0      
                        zoom = 1;

                }



                //Picture_Drawing();
                sr_Flag = 1;
                Select_rectangle();
            }
            else
            {

                if (e.Delta > 0)
                {

                    pzoom++;
                    //OperateClass.maxMin(picBox, img, pzooom);

                }
                else if (e.Delta < 0)
                {
                    pzoom--;
                    //OperateClass.maxMin(picBox, img_ori, zoomtime);
                }

                if (pzoom == 0)
                    pzoom = 1;

                //Picture_Drawing();
                sr_Flag = 1;
                Select_rectangle();
            }
        }
    }

    
}
