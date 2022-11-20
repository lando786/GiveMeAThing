using System.Dynamic;
using ChanceNET;

namespace GiveMeAThing.Utils;

public interface IParser
{
    ExpandoObject Parse(ExpandoObject str);
}

public class Parser : IParser
{
    private readonly Chance _chance;

    public Parser()
    {
        _chance = new Chance();
    }

    public ExpandoObject Parse(ExpandoObject str)
    {
        var result = new ExpandoObject() as IDictionary<string, object?>;
        foreach (var pair in str)
        {
            result[pair.Key] = GenerateValue(pair);
        }

        return result as ExpandoObject;
    }

    private object GenerateValue(KeyValuePair<string, object> kvp)
    {
        var type = kvp.Value.ToString();
        if (type == Types.STRING)
        {
            return _chance.String(20);
        }
        if (type == Types.NAME)
        {
            return _chance.FullName();
        }

        if (type == Types.AGE)
        {
            return _chance.Age();
        }
        if (type == Types.NUMBER)
        {
            return _chance.Integer(0, 10000);
        }

        if (type == Types.CREDITCARD)
        {
            return _chance.CreditCardNumber();
        }
        if (type == Types.DECIMAL)
        {
            return _chance.Float();
        }
        if (type == Types.BOOL || type == Types.BOOLEAN)
        {
            return _chance.Bool();
        }
        
        return string.Empty;
    }
}