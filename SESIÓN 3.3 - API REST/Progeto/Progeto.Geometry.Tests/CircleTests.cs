using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progeto.Geometry;
using Progeto.Geometry.Models;

namespace Progeto.Geometry.Tests
{
    [TestClass]
    public class CircleTests
    {
        [TestMethod]
        public void Circle_Perimeter_IsCorrect()
        {
            var center = new Point(0, 0);
            var circle = new Circle(center, 2.0);

            double expected = 2 * System.Math.PI * 2;
            double actual = circle.Perimeter;

            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestMethod]
        public void Circle_Center_IsStoredCorrectly()
        {
            var center = new Point(1, 1);
            var circle = new Circle(center, 5.0);

            Assert.AreEqual(1, circle.Center.x);
            Assert.AreEqual(1, circle.Center.y);
            Assert.AreEqual(5.0, circle.Radius);
        }
    }
}
