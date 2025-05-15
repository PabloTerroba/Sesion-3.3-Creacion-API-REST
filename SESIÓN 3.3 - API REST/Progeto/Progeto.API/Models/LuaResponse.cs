using System;
using System.Collections.Generic;
using System.Text;

namespace Progeto.API.Models
{
    public class LuaResponse
    {
        public string Output { get; set; }
        public string Svg { get; set; }
        public long ExecutionTimeMs { get; set; }
    }
}
