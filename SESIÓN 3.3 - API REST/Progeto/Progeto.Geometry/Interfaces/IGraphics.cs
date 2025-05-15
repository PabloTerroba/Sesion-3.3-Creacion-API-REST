using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using Progeto.Geometry.Models;
using Point = Progeto.Geometry.Models.Point;

namespace Progeto.Geometry.Interfaces
{
    public interface IGraphics
    {
        void Draw(Segment s, Color color, double width);
        void Draw(Circle s, Color color, double width);
        void Draw(Point s, Color color, double width);
    }
}

