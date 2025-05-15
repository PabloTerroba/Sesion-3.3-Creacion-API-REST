using Microsoft.AspNetCore.Mvc;
using Progeto.API.Models;
using Progeto.Lua;
using System.Diagnostics;
using System.Linq;

namespace Progeto.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LuaController : ControllerBase
    {
        [HttpPost]
        public ActionResult<LuaResponse> Execute([FromBody] LuaRequest request)
        {
            var interpreter = new LuaInterpreter();
            var stopwatch = Stopwatch.StartNew();

            var result = interpreter.RunProgram(request.Code);
            stopwatch.Stop();

            return Ok(new LuaResponse
            {
                Output = result.ElementAt(0),
                Svg = result.ElementAt(1),
                ExecutionTimeMs = stopwatch.ElapsedMilliseconds
            });
        }
    }
}
