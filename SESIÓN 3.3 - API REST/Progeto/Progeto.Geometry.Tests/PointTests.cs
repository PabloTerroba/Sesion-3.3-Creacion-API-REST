using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progeto.Geometry;
using Progeto.Geometry.Models;
using Point = Progeto.Geometry.Models.Point;

namespace Progeto.Geometry.Tests
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void Distance_BetweenTwoPoints_IsCorrect()
        {
            var p1 = new Point(0, 0);
            var p2 = new Point(3, 4);

            double expected = 5.0;
            double actual = Point.Distance(p1, p2);

            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestMethod]
        public void Operator_Minus_ReturnsCorrectVector()
        {
            var p1 = new Point(2, 3);
            var p2 = new Point(1, 1);
            var v = p1 - p2;

            Assert.AreEqual(1, v.x);
            Assert.AreEqual(2, v.y);
        }
    }
}
