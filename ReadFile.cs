using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using SetPoint;

namespace ReadFile
{
    class STL
    {
        public class ThreeDimension
        {
            public double x, y, z;
        }

        public class CLI
        {
            public int LayerNumber;//总层数
            public string FileStyle;//文件风格二进制或ASCII
            public double Units;//单位
            public int Version;//版本号
            public double LayerThickness;//层厚
            public List<ThreeDimension> Dimension = new List<ThreeDimension>();//实体最小外接立方体的对角点（x，y，z）
            public List<Layer> LayerLine = new List<Layer>();//层数据链表
            public int StartLayer;//起始层数
            public string PartType;//零件类型
        }
        public class Layer
        {
            public double z;//每层的高度
            public List<Line> LineList = new List<Line>();//层线集合
        }

        public class Line
        {
            public int id, n, dir;//id模型，n:munber of points,dir:direction of counter
            public List<MyPoint> Polygon = new List<MyPoint>();//counter

        }
        public static List<CLI> STLFile;
        public static CLI ReadCLI(string FilePath)
        {
            FileStream myStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            CLI Cli = new CLI();
            BinaryReader reader = new BinaryReader(myStream);
            string str = "";
            string End = "$$HEADEREND";

            while (true)
            {
                str = "";
                while (reader.PeekChar() != -1)
                {
                    if (str == End)
                        break;

                    char ch = reader.ReadChar();
                    if (ch != '\n')
                    {
                        str += ch;
                    }
                    else { break; }

                }
                string strName = "";
                strName += str[0]; strName += str[1];
                strName += str[2]; strName += str[3];
                strName += str[4]; strName += str[5];
                strName += str[6];
                if (str == "$$HEADEREND")
                {
                    break;
                }
                else if (strName == "$$VERSI")
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == '/')
                        {
                            string unt = "";
                            for (int j = i + 1; j < str.Length; j++)
                            {
                                unt += str[j];
                            }
                            Cli.Version = Convert.ToInt16(unt);
                            break;
                        }
                    }
                }
                else if (str == "$$BINARY")
                {
                    Cli.FileStyle = "Binary";
                }
                else if (str == "$$ASCII")
                { Cli.FileStyle = "ASCII"; }

                else if (strName == "$$UNITS")
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == '/')
                        {
                            string unt = "";
                            for (int j = i + 1; j < str.Length; j++)
                            {
                                unt += str[j];
                            }
                            Cli.Units = Convert.ToDouble(unt);
                            break;
                        }
                    }
                }
                else if (strName == "$$DIMEN")
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == '/')
                        {
                            int f = 0, d = 2;
                            string unt = "";

                            while (i < str.Length - 1)
                            {
                                ThreeDimension p = new ThreeDimension();
                                for (int j = i + 1; j < str.Length; j++)
                                {
                                    if (str[j] != ',')
                                    {
                                        unt += str[j];
                                        if (j == str.Length - 1)
                                        {
                                            i = j;
                                            p.z = Convert.ToDouble(unt);
                                            f++;
                                            d = 3;
                                            break;
                                        }
                                    }
                                    else if (f % 3 == 0)
                                    {
                                        i = j;
                                        p.x = Convert.ToDouble(unt);
                                        unt = "";
                                        f++;
                                    }
                                    else if (f % 3 == 1)
                                    {
                                        i = j;
                                        p.y = Convert.ToDouble(unt);
                                        unt = "";
                                        f++;
                                    }
                                    else if (f % 3 == 2)
                                    {
                                        i = j;
                                        p.z = Convert.ToDouble(unt);
                                        f++;
                                        unt = "";
                                        d = 3;
                                        break;
                                    }
                                }
                                if (f % 3 == 0 && d % 3 == 0)
                                {
                                    Cli.Dimension.Add(p);
                                }

                            }
                            break;
                        }
                    }
                }

                else if (strName == "$$LAYER")
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == '/')
                        {
                            string unt = "";
                            for (int j = i + 1; j < str.Length; j++)
                            {
                                unt += str[j];
                            }
                            Cli.LayerNumber = Convert.ToInt32(unt);
                            break;
                        }
                    }
                }
            }
            long Pos = myStream.Position;
            while (myStream.Position < myStream.Length)
            {
                Layer alayer = ReadOneLayer(myStream.Position, myStream, Cli.Units);
                Cli.LayerLine.Add(alayer);
            }
            myStream.Close();
            if (Cli.LayerLine.Count() > 1)
            { Cli.LayerThickness = Cli.LayerLine[1].z - Cli.LayerLine[0].z; }
            else
            { Cli.LayerThickness = Cli.LayerLine[0].z; }

            DealCLI(Cli);
            SetCliPartType(Cli);
            return Cli;
        }

        public static void SetCliPartType(CLI cli)
        {
            for (int i=0;i<cli.LayerNumber;i++)
            {
                for (int j=0;j< cli.LayerLine[i].LineList.Count;j++)
                {
                    if (cli.LayerLine[i].LineList[j].dir==2)
                    { cli.PartType = "Brace";
                        return ;
                    }
                }
            }
            cli.PartType = "Substance";
        }

        public static void DealCLI(CLI Cli)
        {
            for (int c = 0; c < Cli.LayerNumber; c++)
            {
                for (int L = 0; L < Cli.LayerLine[c].LineList.Count; L++)
                {
                    if (Cli.LayerLine[c].LineList[L].dir != 2)
                    {
                        Line line = Cli.LayerLine[c].LineList[L];
                        DealSurplus(line);
                        Cli.LayerLine[c].LineList[L] = line;
                    }
                }
            }
        }

        public static Layer ReadOneLayer(long position, FileStream myStream, double Unint)
        {
            Layer layer = new Layer();
            long poa = position;
            BinaryReader reader = new BinaryReader(myStream);
            myStream.Seek(position, 0);
            int ch1 = reader.ReadInt16();
            if (ch1 == 128)
            {
                ch1 = reader.ReadInt16();
                layer.z = ch1 * Unint;
                poa += 4;
            }
            else
            {
                return layer;
            }

            while (poa < myStream.Length)
            {
                ch1 = reader.ReadInt16();
                Line line = new Line();
                if (ch1 == 129)
                {
                    ch1 = reader.ReadInt16();
                    line.id = ch1;
                    ch1 = reader.ReadInt16();
                    line.dir = ch1;
                    ch1 = reader.ReadInt16();
                    line.n = ch1;
                    List<MyPoint> Oneline = new List<MyPoint>();
                    for (int i = 0; i < line.n; i++)
                    {
                        MyPoint Lp = new MyPoint();
                        Lp.x = reader.ReadInt16() * Unint;
                        Lp.y = reader.ReadInt16() * Unint;
                        Oneline.Add(Lp);
                    }
                    line.Polygon = Oneline;
                    layer.LineList.Add(line);
                    poa = myStream.Position;
                }
                else if (ch1 == 128)
                {
                    position = myStream.Position - 2;
                    myStream.Seek(position, 0);
                    break;
                }
            }
            return layer;
        }

        public static void DealSurplus(Line Ln)
        {
            bool Fg = true;
            while (Fg)
            {
                Fg = false;
                int n = Ln.Polygon.Count;
                for (int i = 0; i < n; i++)
                {
                    if (SamePoint(Ln.Polygon[i], Ln.Polygon[(i + 1) % n]) && n > 1)
                    {
                        DeletePoint(Ln, i);
                        Fg = true;
                        break;
                    }
                    else if (TreeOneLine(Ln.Polygon[i], Ln.Polygon[(i + 1) % n], Ln.Polygon[(i + 2) % n]) && n > 2)
                    {
                        DeletePoint(Ln, (i + 1) % n);
                        Fg = true;
                        break;
                    }
                    else { }
                }
            }
            Ln.n = Ln.Polygon.Count;
        }

        public static bool TreeOneLine(MyPoint one, MyPoint Two, MyPoint Three)
        {
            MyPoint OneTwo = new MyPoint();
            OneTwo.x = Two.x - one.x;
            OneTwo.y = Two.y - one.y;
            double ZTwo = Math.Sqrt(OneTwo.y * OneTwo.x + OneTwo.y * OneTwo.y);

            MyPoint OneThree = new MyPoint();
            OneThree.x = Three.x - one.x;
            OneThree.y = Three.y - one.y;

            double ZThree = Math.Sqrt(OneThree.x * OneThree.x + OneThree.y * OneThree.y);
            double Cha = (OneTwo.x * OneThree.y - OneTwo.y * OneThree.x) / (ZTwo * ZThree);

            if (Math.Abs(Cha) < 0.01)
            { return true; }
            else
            { return false; }
        }

        public static bool SamePoint(MyPoint p, MyPoint p1)
        {
            double x = p1.x - p.x;
            double y = p1.y - p.y;
            double L = Math.Sqrt(x * x + y * y);

            if (L <= 0.01)
            {

                return true;
            }
            else { return false; }
        }

        public static void DeletePoint(Line line, int i)
        {
            int n = line.Polygon.Count;
            List<MyPoint> Poly = new List<MyPoint>();
            var LINE = line.Polygon;
            for (int m = 0; m < n; m++)
            {
                if (m != i)
                { Poly.Add(LINE[m]); }
            }
            line.Polygon = Poly;
            line.n = Poly.Count;
        }
    }


}
