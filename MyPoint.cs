using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetPoint
{
    class MyPoint
    {
        public double x,y;

        public static MyPoint operator +(MyPoint  b, MyPoint c)
        {
           MyPoint box = new MyPoint();
            box.x = b.x + c.x;
            box.y = b.y + c.y;
            return box;
        }

        public static MyPoint operator -(MyPoint b, MyPoint c)
        {
            MyPoint box = new MyPoint();
            box.x = b.x - c.x;
            box.y = b.y - c.y;
            return box;
        }

        public static bool operator ==(MyPoint lhs, MyPoint rhs)
        {
            bool status = false;
            if (lhs.x== rhs.x && lhs.y == rhs.y)
            {
                status = true;
            }
            return status;
        }
        public static bool operator !=(MyPoint lhs, MyPoint rhs)
        {
            bool status = false;
            if (lhs.x != rhs.x || lhs.y != rhs.y)
            {
                status = true;
            }
            return status;
        }
        //public static bool operator <(MyPoint lhs, MyPoint rhs)
        //{
        //    bool status = false;
        //    if (lhs.x < rhs.x && lhs.y < rhs.y )
        //    {
        //        status = true;
        //    }
        //    return status;
        //}

        //public static bool operator >(MyPoint lhs, MyPoint rhs)
        //{
        //    bool status = false;
        //    if (lhs.x > rhs.x && lhs.y > rhs.y)
        //    {
        //        status = true;
        //    }
        //    return status;
        //}

        //public static bool operator <=(MyPoint lhs, MyPoint rhs)
        //{
        //    bool status = false;
        //    if (lhs.x <= rhs.x && lhs.y<= rhs.y )
        //    {
        //        status = true;
        //    }
        //    return status;
        //}

        //public static bool operator >=(MyPoint lhs, MyPoint rhs)
        //{
        //    bool status = false;
        //    if (lhs.x >= rhs.x && lhs.y >= rhs.y)
        //    {
        //        status = true;
        //    }
        //    return status;
        //}
    }
}
