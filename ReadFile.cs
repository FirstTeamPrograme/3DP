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
        public List<CLI> STLFile;
        public static CLI ReadCLI(FileStream myStream)
        {
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
                                        //break;
                                    }
                                    else if (f % 3 == 1)
                                    {
                                        i = j;
                                        p.y = Convert.ToDouble(unt);
                                        unt = "";
                                        f++;

                                        // break;
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
            return Cli;
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
                    line.Lines.Add(Oneline);
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
    }
    class ThreeDimension
    {
        public double x, y, z;
    }

    class CLI
    {
        public int LayerNumber;//总层数
        public string FileStyle;//文件风格二进制或ASCII
        public double Units;//单位
        public int Version;//版本号
        public double LayerThickness;//层厚
        public List<ThreeDimension> Dimension = new List<ThreeDimension>();//实体最小外接立方体的对角点（x，y，z）
        public List<Layer> LayerLine = new List<Layer>();//层数据链表
    }
    class Layer
    {
        public double z;//每层的高度
        public List<Line> LineList = new List<Line>();//层线集合
    }

    class Line
    {
        public int id, n, dir;//id模型，n:munber of points,dir:direction of counter
        public List<List<MyPoint>> Lines = new List<List<MyPoint>>();//counter

    }

}
