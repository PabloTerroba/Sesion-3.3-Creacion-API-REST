using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using Progeto.Lua;
using Progeto.Geometry.Models;
using Point = Progeto.Geometry.Models.Point;

namespace Progeto.Lua.Tests
{
    [TestClass]
    public class LuaInterpreterTests
    {
        [TestMethod]
        public void RunProgram_PrintsPoint()
        {
            //Se verifica que el punto se imprima correctamente
            var code = @"
                p = Point(1, 2)
                print(p)
            ";

            var interpreter = new LuaInterpreter();
            var result = interpreter.RunProgram(code).ToArray();

            Assert.IsTrue(result[0].Contains("Point"));
        }

        [TestMethod]
        public void RunProgram_DrawsCircle_GeneratesSVG()
        {
            //Se verifica que el círculo se dibuje correctamente
            var code = @"
                c = Circle(Point(50, 50), 25)
                draw(c)
            ";

            var interpreter = new LuaInterpreter();
            var result = interpreter.RunProgram(code).ToArray();

            Assert.IsTrue(result[1].Contains("<circle"));
            Assert.IsTrue(result[1].Contains("r=\"25\""));
        }

        [TestMethod]
        public void RunProgram_DrawsMultiplePrimitives()
        {
            //Se verifica que se dibujen múltiples primitivas correctamente
            var code = @"
                draw(Point(0,0))
                draw(Segment(Point(0,0), Point(100,100)))
                draw(Circle(Point(50,50), 10))
            ";

            var interpreter = new LuaInterpreter();
            var result = interpreter.RunProgram(code).ToArray();

            Assert.IsTrue(result[1].Contains("<ellipse"));
            Assert.IsTrue(result[1].Contains("<line"));
            Assert.IsTrue(result[1].Contains("<circle"));
        }

        [TestMethod]
        public void RunProgram_SetsColorAndWidth()
        {
            //Se verifica que se establezcan el color y el ancho correctamente
            var code = @"
                color(255, 0, 0)
                width(5)
                draw(Segment(Point(0,0), Point(10,10)))
            ";

            var interpreter = new LuaInterpreter();
            var result = interpreter.RunProgram(code).ToArray();

            Assert.IsTrue(result[1].Contains("stroke-width:5"));
            Assert.IsTrue(result[1].Contains("stroke:rgb(255,0,0)"));
        }

        [TestMethod]
        public void RunProgram_WithInvalidCode_ReturnsException()
        {
            //Se verifica que se maneje correctamente el código inválido
            var code = "this is not lua code";

            var interpreter = new LuaInterpreter();
            var result = interpreter.RunProgram(code).ToArray();

            Assert.IsTrue(result[0].Contains("Exception"));
        }

        [TestMethod]
        public void Draw_NullPrimitive_DoesNotThrow()
        {
            // Se verifica que no se lance una excepción al dibujar una primitiva nula
            var interpreter = new LuaInterpreter();
            interpreter.Draw(null); // no debe lanzar excepción
        }

        [TestMethod]
        public void SetColor_SetsCorrectly()
        {
            // Verificamos que el color se establece correctamente
            var interpreter = new LuaInterpreter();
            interpreter.SetColor(10, 20, 30);

            // Forzamos un dibujo para ver que se usa el color
            var code = @"
                draw(Circle(Point(0,0), 5))
            ";
            var result = interpreter.RunProgram(code).ToArray();
            Assert.IsTrue(result[1].Contains("rgb(10,20,30)"));
        }

        [TestMethod]
        public void SetWidth_SetsCorrectly()
        {
            // Verificamos que el ancho se establece correctamente
            var interpreter = new LuaInterpreter();
            interpreter.SetWidth(2.5);

            var code = @"
                draw(Circle(Point(0,0), 5))
            ";
            var result = interpreter.RunProgram(code).ToArray();
            Assert.IsTrue(result[1].Contains("stroke-width:2.5"));
        }

        [TestMethod]
        public void Print_AppendsToOutput()
        {
            // Verificamos que la función print agrega correctamente al output
            var code = @"
                print('Hello World')
            ";

            var interpreter = new LuaInterpreter();
            var result = interpreter.RunProgram(code).ToArray();

            Assert.IsTrue(result[0].Contains("Hello World"));
        }

        [TestMethod]
        public void RunProgram_CreatesSegment_AndSvg()
        {
            var interpreter = new LuaInterpreter();

            string luaScript = @"
                p1 = Point(0, 0)
                p2 = Point(100, 100)
                s = Segment(p1, p2)
                draw(s)
                print('Segmento creado')
            ";

            var result = interpreter.RunProgram(luaScript).ToArray();

            Console.WriteLine("Output:");
            Console.WriteLine(result[0]);
            Console.WriteLine("SVG:");
            Console.WriteLine(result[1]);

            Assert.IsTrue(result[0].Contains("Segmento creado")); // output textual
            Assert.IsTrue(result[1].Contains("<line"));           // contenido SVG
        }

        [TestMethod]
        public void RunProgram_DrawCircle_WithColorAndWidth()
        {
            var interpreter = new LuaInterpreter();

            string script = @"
                color(0, 255, 0)
                width(3)
                draw(Circle(Point(50,50), 25))
                print('Circulo verde') 
            ";
            // Puede llegar a fallar con la tilde en la palabra "Círculo"

            var result = interpreter.RunProgram(script).ToArray();

            Console.WriteLine("Output:");
            Console.WriteLine(result[0]);
            Console.WriteLine("SVG:");
            Console.WriteLine(result[1]);

            Assert.IsTrue(result[0].Contains("Círculo verde"));
            Assert.IsTrue(result[1].Contains("<circle"));
            Assert.IsTrue(result[1].Contains("stroke-width:3"));
        }

        [TestMethod]
        public void RunProgram_InvalidCode_ReturnsException()
        {
            var interpreter = new LuaInterpreter();

            string script = @"
                draw(Linea(Point(0,0), Point(1,1))) -- tipo que no existe
            ";

            var result = interpreter.RunProgram(script).ToArray();

            Console.WriteLine("Output:");
            Console.WriteLine(result[0]);
            Console.WriteLine("SVG:");
            Console.WriteLine(result[1]);

            Assert.IsTrue(result[0].Contains("Exception"));
            Assert.IsTrue(result[1].Contains("<svg"));
        }

        [TestMethod]
        public void RunProgram_MultipleFigures_OutputsAll()
        {
            var interpreter = new LuaInterpreter();

            string script = @"
                for i = 0, 2 do
                    draw(Segment(Point(i*10, i*10), Point(i*10+10, i*10+10)))
                end
            ";

            var result = interpreter.RunProgram(script).ToArray();

            Console.WriteLine("Output:");
            Console.WriteLine(result[0]);
            Console.WriteLine("SVG:");
            Console.WriteLine(result[1]);

            Assert.IsTrue(result[1].Split("<line").Length >= 3); // hay al menos 2 líneas dibujadas
        }

        // Test unitario que comprueba únicamente la parte que nos daba problemas: constructor de Line(Vector, Point)
        [TestMethod]
        public void CreatePerpendicularLine_ShouldNotThrow()
        {
            var p1 = new Point(0, 0);
            var p2 = new Point(10, 0);
            var seg = new Segment(p1, p2);

            var middle = seg.MiddlePoint;
            var normal = seg.Line.Normal; // esto es un Vector

            // Prueba la creación de la línea
            var l = new Line(normal, middle); // Vector, Point

            Assert.IsNotNull(l);
        }

    }
}

