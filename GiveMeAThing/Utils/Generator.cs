using System.Dynamic;
using AutoFixture;

namespace GiveMeAThing.Utils;

public interface IParser
{
    ExpandoObject Parse(ExpandoObject str);
}

public class Parser : IParser
{
    private readonly Fixture _fixture;

    public Parser()
    {
        _fixture = new Fixture();
    }

    public ExpandoObject Parse(ExpandoObject str)
    {
        var result = new ExpandoObject() as IDictionary<string, object?>;
        foreach (var pair in str)
        {
            result[pair.Key] = GenerateValue(pair.Value?.ToString().ToLower());
        }

        return result as ExpandoObject;
    }

    private object GenerateValue(string type)
    {
        if (type == string.Empty) return "";
        if (type == Types.STRING)
        {
            return _fixture.Create<string>();
        }

        if (type == Types.NUMBER)
        {
            return _fixture.Create<int>();
        }

        if (type == Types.DECIMAL)
        {
            return _fixture.Create<decimal>();
        }

        if (type == Types.BOOL || type == Types.BOOLEAN)
        {
            return _fixture.Create<bool>();
        }

        return string.Empty;
    }
}