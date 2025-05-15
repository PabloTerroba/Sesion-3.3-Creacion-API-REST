﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using Progeto.Geometry.Interfaces;
using System.Numerics;

namespace Progeto.Geometry.Models
{
    public class Segment : IPrimitive
    {
        private readonly Point _p0;
        private readonly Point _p1;

        public Segment(Point p0, Point p1)
        {
            _p0 = p0;
            _p1 = p1;
        }

        public Point InitialPoint
        {
            get { return _p0; }
        }

        public Point FinalPoint
        {
            get { return _p1; }
        }

        public Point MiddlePoint
        {
            get
            {
                Vector v = _p1 - _p0;
                return _p0 + v * 0.5;
            }
        }

        public double Length
        {
            get { return _p0.Distance(_p1); }
        }

        public Line Line
        {
            get { return new Line(_p0, _p1); }
        }

        /*
        // Nuevo método para el ejercicio del Apartado 5)
        public Vector Normal()
        {
            double dx = FinalPoint.x - InitialPoint.x;
            double dy = FinalPoint.y - InitialPoint.y;
            return new Vector(-dy, dx); // Vector perpendicular (normal)
        }
        */

        public void Draw(IGraphics g, Color color, double width)
        {
            g.Draw(this, color, width);
        }
    }
}
