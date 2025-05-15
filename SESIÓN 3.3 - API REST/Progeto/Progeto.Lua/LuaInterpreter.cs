using System;
using System.Collections.Generic;
using System.Text;

using Progeto.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Progeto.Geometry.Interfaces;

using NLua;
using LuaEngine = NLua.Lua;
using Progeto.Geometry.Models;
using Point = Progeto.Geometry.Models.Point;


namespace Progeto.Lua
{
    public class LuaInterpreter
    {
        private List<DrawingPrimitive> _primitives;
        private Color _color;
        private double _width;
        private StringBuilder _output;

        public LuaInterpreter()
        {
            _primitives = new List<DrawingPrimitive>();
            _color = Color.Black;
            _width = 1;
            _output = new StringBuilder();
        }

        private LuaEngine CreateInterpreter()
        {
            LuaEngine lua = new LuaEngine();

            try
            {
                lua.LoadCLRPackage();
            }
            catch (Exception ex)
            {
                _output.AppendLine("WARNING: LoadCLRPackage failed - " + ex.Message);
            }

            lua.RegisterFunction("print", this, GetType().GetMethod("Print"));
            lua.RegisterFunction("draw", this, GetType().GetMethod("Draw"));
            lua.RegisterFunction("width", this, GetType().GetMethod("SetWidth"));
            lua.RegisterFunction("color", this, GetType().GetMethod("SetColor"));

            // 🔄 Esta es la parte crítica: importamos los tipos .NET como funciones Lua
            // lua["Point"] = typeof(Progeto.Geometry.Models.Point);
            // lua["Segment"] = typeof(Progeto.Geometry.Models.Segment);
            // lua["Line"] = typeof(Progeto.Geometry.Models.Line);
            // lua["Circle"] = typeof(Progeto.Geometry.Models.Circle);
            // lua["Vector"] = typeof(Progeto.Geometry.Models.Vector);

            // Método que funciona - se expone un constructor explícito
            lua.RegisterFunction("Point", null, typeof(Point).GetConstructor(new[] { typeof(double), typeof(double) }));
            lua.RegisterFunction("Circle", null, typeof(Circle).GetConstructor(new[] { typeof(Point), typeof(double) }));
            lua.RegisterFunction("Segment", null, typeof(Segment).GetConstructor(new[] { typeof(Point), typeof(Point) }));
            lua.RegisterFunction("Line", null, typeof(Line).GetConstructor(new[] { typeof(Point), typeof(Point) }));
            lua.RegisterFunction("Vector", null, typeof(Vector).GetConstructor(new[] { typeof(double), typeof(double) }));

            // Añadimos esta línea para registrar una función que crea Línea a partir de un Vector y un Punto porque falla el constructor
            lua.RegisterFunction("lineFrom", typeof(LuaExtensions).GetMethod("CreateLineFromVectorAndPoint"));

            // Debemos hacer algo equivalente para la función de intersección
            lua.RegisterFunction("intersection", typeof(LuaExtensions).GetMethod("Intersection"));



            /*
            try
            {
                lua.DoString(@"
                    luanet.load_assembly('Progeto.Geometry')

                    Segment = luanet.import_type('Progeto.Geometry.Models.Segment')
                    Point = luanet.import_type('Progeto.Geometry.Models.Point')
                    Line = luanet.import_type('Progeto.Geometry.Models.Line')
                    Circle = luanet.import_type('Progeto.Geometry.Models.Circle')
                    Vector = luanet.import_type('Progeto.Geometry.Models.Vector')
                ", "Init");
            }
            catch (Exception ex)
            {
                _output.AppendLine("WARNING: Lua init failed - " + ex.Message);
            }
            */

            return lua;
        }

        /*
        private LuaEngine CreateInterpreter()
        {
            LuaEngine lua = new LuaEngine();

            // Registrar funciones accesibles desde Lua
            lua.RegisterFunction("print", this, GetType().GetMethod("Print"));
            lua.RegisterFunction("draw", this, GetType().GetMethod("Draw"));
            lua.RegisterFunction("width", this, GetType().GetMethod("SetWidth"));
            lua.RegisterFunction("color", this, GetType().GetMethod("SetColor"));

            // IMPORTACIÓN DIRECTA DE CLASES .NET DESDE C#
            lua["Point"] = typeof(Point);
            lua["Segment"] = typeof(Segment);
            lua["Line"] = typeof(Line);
            lua["Circle"] = typeof(Circle);
            lua["Vector"] = typeof(Vector);

            return lua;
        }
        */

        /*
        private LuaEngine CreateInterpreter()
        {
            LuaEngine lua = new LuaEngine();

            lua.LoadCLRPackage(); // Esta línea es importante y ahora debe funcionar

            lua.RegisterFunction("print", this, GetType().GetMethod("Print"));
            lua.RegisterFunction("draw", this, GetType().GetMethod("Draw"));
            lua.RegisterFunction("width", this, GetType().GetMethod("SetWidth"));
            lua.RegisterFunction("color", this, GetType().GetMethod("SetColor"));

            // Ahora volvemos a usar luanet correctamente
            lua.DoString(@"
                luanet.load_assembly('Progeto.Geometry')

                Segment = luanet.import_type('Progeto.Geometry.Segment')
                Point = luanet.import_type('Progeto.Geometry.Point')
                Line = luanet.import_type('Progeto.Geometry.Line')
                Circle = luanet.import_type('Progeto.Geometry.Circle')
                Vector = luanet.import_type('Progeto.Geometry.Vector')
            ");

            return lua;
        }
        */

        // Funciones expuestas a Lua
        public void SetColor(int r, int g, int b)
        {
            _color = Color.FromArgb(r, g, b);
        }

        public void SetWidth(double width)
        {
            _width = width;
        }

        public void Print(object o)
        {
            _output.AppendLine(o?.ToString());
        }

        public void Draw(IPrimitive primitive)
        {
            if (primitive != null)
                _primitives.Add(new DrawingPrimitive(primitive, _color, _width));
        }

        // Ejecuta código Lua y devuelve resultado y SVG
        public IEnumerable<string> RunProgram(string program)
        {
            _primitives.Clear();
            _output.Clear();

            try
            {
                LuaEngine lua = CreateInterpreter();
                lua.DoString(program, "Program");
            }
            catch (Exception ex)
            {
                _output.AppendLine("Exception: " + ex.Message);
            }

            SvgGraphics svg = new SvgGraphics();
            foreach (var p in _primitives)
                p.Draw(svg);
            string svgText = svg.Text();

            return new string[] { _output.ToString(), svgText };
        }
    }
}
