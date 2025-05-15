using System;
using System.Collections.Generic;
using System.Text;
using Progeto.Geometry.Models;

namespace Progeto.Lua
{
    public static class LuaExtensions
    {
        public static Line CreateLineFromVectorAndPoint(Vector v, Point p)
        {
            return new Line(v, p);
        }

        public static Point Intersection(Line l1, Line l2)
        {
            return Line.Intersection(l1, l2);
        }

    }
}
