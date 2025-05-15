using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

namespace Progeto.Geometry.Interfaces
{
    public interface IDraw
    {
        void Draw(IGraphics g, Color color, double width);
    }
}

