using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progeto.Geometry;
using Progeto.Geometry.Models;

namespace Progeto.Geometry.Tests
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void Vector_Magnitude_IsCorrect()
        {
            var v = new Vector(3, 4);
            Assert.AreEqual(5.0, v.Magnitude, 0.0001);
        }

        [TestMethod]
        public void Vector_Operators_Addition_Subtraction()
        {
            var v1 = new Vector(1, 2);
            var v2 = new Vector(3, 4);
            var sum = v1 + v2;
            var diff = v1 - v2;

            Assert.AreEqual(4, sum.x);
            Assert.AreEqual(6, sum.y);
            Assert.AreEqual(-2, diff.x);
            Assert.AreEqual(-2, diff.y);
        }

        [TestMethod]
        public void Vector_CrossProduct_IsCorrect()
        {
            var v1 = new Vector(1, 0);
            var v2 = new Vector(0, 1);
            double cross = Vector.CrossProduct(v1, v2);

            Assert.AreEqual(1, cross);
        }

        [TestMethod]
        public void Vector_Normalize_Works()
        {
            var v = new Vector(3, 4);
            v.Normalize();

            Assert.AreEqual(0.6, v.x, 0.01);
            Assert.AreEqual(0.8, v.y, 0.01);
        }
    }
}

