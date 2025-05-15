using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progeto.Geometry;
using Progeto.Geometry.Models;

namespace Progeto.Geometry.Tests
{
    [TestClass]
    public class LineTests
    {
        [TestMethod]
        public void Line_Intersection_WorksCorrectly()
        {
            var l1 = new Line(new Point(0, 0), new Point(1, 1)); // y = x
            var l2 = new Line(new Point(0, 1), new Point(1, 0)); // y = -x + 1

            var inter = Line.Intersection(l1, l2);

            Assert.AreEqual(0.5, inter.x, 0.0001);
            Assert.AreEqual(0.5, inter.y, 0.0001);
        }

        [TestMethod]
        public void Line_Parallel_ReturnsNullIntersection()
        {
            var l1 = new Line(new Point(0, 0), new Point(1, 1));
            var l2 = new Line(new Point(0, 1), new Point(1, 2)); // paralela

            var inter = Line.Intersection(l1, l2);

            Assert.IsNull(inter);
        }
    }
}
