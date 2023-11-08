using System.Text;

namespace Bencoding;

public static class Bencoder
{
    public static string Serialise(object input)
    {
        var sb = new StringBuilder();
        Serialise(input, sb);
        return sb.ToString();
    }
    
    public static string Serialise(IEnumerable<object> input)
    {
        var sb = new StringBuilder();
        Serialise(input, sb);
        return sb.ToString();
    }
    
    public static string Serialise(string input)
    {
        var sb = new StringBuilder();
        Serialise(input, sb);
        return sb.ToString();
    }

    public static string Serialise(long input)
    {
        var sb = new StringBuilder();
        Serialise(input, sb);
        return sb.ToString();
    }

    private static void SerialiseItems<T>(IEnumerable<T> input, StringBuilder sb)
    {
        foreach (var item in input)
        {
            SerialiseItem(item, sb);
        }
    }

    private static void SerialiseItem<T>(T item, StringBuilder sb)
        => Serialise((dynamic)item!, sb);

    public static string Serialise<T>(IDictionary<string, T> input)
    {
        var sb = new StringBuilder();
        Serialise(input, sb);
        return sb.ToString();
    }

    private static void Serialise<T>(IDictionary<string, T> input, StringBuilder sb)
    {
        sb.Append("d");
        foreach (var (k, v) in input)
        {
            Serialise(k, sb);
            SerialiseItem(v, sb);
        }

        sb.Append("e");
    }


    private static void Serialise<T>(IEnumerable<T> input, StringBuilder sb)
    {
        sb.Append("l");
        SerialiseItems(input, sb);
        sb.Append("e");
    }

    private static void Serialise(object input, StringBuilder sb)
    {
        var type = input.GetType();
        var items = type.GetProperties()
            .Select(prop => prop.GetValue(input)!)
            .ToArray();

        SerialiseItems(items, sb);
    }

    private static void Serialise(string input, StringBuilder sb)
        => sb.Append($"{input.Length}:{input}");


    private static void Serialise(long input, StringBuilder sb)
        => sb.Append($"i{input}e");
}