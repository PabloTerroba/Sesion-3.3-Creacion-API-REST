using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progeto.Geometry;
using Progeto.Geometry.Models;

namespace Progeto.Geometry.Tests
{
    [TestClass]
    public class SegmentTests
    {
        [TestMethod]
        public void Segment_Length_IsCorrect()
        {
            var p1 = new Point(0, 0);
            var p2 = new Point(3, 4);
            var segment = new Segment(p1, p2);

            Assert.AreEqual(5.0, segment.Length, 0.0001);
        }

        [TestMethod]
        public void Segment_MiddlePoint_IsCorrect()
        {
            var p1 = new Point(2, 2);
            var p2 = new Point(4, 6);
            var segment = new Segment(p1, p2);
            var middle = segment.MiddlePoint;

            Assert.AreEqual(3.0, middle.x, 0.0001);
            Assert.AreEqual(4.0, middle.y, 0.0001);
        }
    }
}
