using System.Dynamic;
using GiveMeAThing.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GiveMeAThing.Controllers;

[ApiController]
[Route("[controller]")]
public class ThingController : ControllerBase
{
    private readonly IParser _parser;

    public ThingController(IParser parser)
    {
        _parser = parser;
    }

    // GET
    [HttpPost]
    public string Thing([FromBody] dynamic req)
    {
        var converter = new ExpandoObjectConverter();
        var exObjExpandoObject = JsonConvert.DeserializeObject<ExpandoObject>(req.ToString(), converter) as dynamic;

        var parsed = _parser.Parse(exObjExpandoObject);
        // Console.Write(parsed);
        return JsonConvert.SerializeObject(parsed);
    }
    
    [HttpPost]
    [Route("{quantity}")]
    public string Thing(int quantity, [FromBody] dynamic req)
    {
        var result = new List<dynamic>();
        var converter = new ExpandoObjectConverter();
        var exObjExpandoObject = JsonConvert.DeserializeObject<ExpandoObject>(req.ToString(), converter) as dynamic;

        for (int i = 0; i <= quantity; i++)
        {
            result.Add(        _parser.Parse(exObjExpandoObject));            
        }
        return JsonConvert.SerializeObject(result);
    }
}