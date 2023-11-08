namespace Bencoding;

public class BencoderTests
{
    [Theory]
    [InlineData("spam", "4:spam")]
    [InlineData("", "0:")]
    public void ShouldSerialiseByteStrings(string input, string expected)
    {
        var output = Bencoder.Serialise(input);

        Assert.Equal(expected, output);
    }

    [Theory]
    [InlineData(0, "i0e")]
    [InlineData(255, "i255e")]
    [InlineData(-20, "i-20e")]
    public void ShouldSerialiseIntegers(long input, string expected)
    {
        var output = Bencoder.Serialise(input);

        Assert.Equal(expected, output);
    }

    [Fact]
    public void ShouldSerialiseObjectOfStringAndIntegers()
    {
        var input = new
        {
            String1 = "spam",
            Int1 = 1,
            String = "",
            Int2 = -10
        };
        var output = Bencoder.Serialise(input);

        Assert.Equal("4:spami1e0:i-10e", output);
    }

    [Fact]
    public void ShouldSerialiseListOfStringAndIntegers()
    {
        var input = new object[]
        {
            "spam",
            1,
            "",
            -10
        };
        var output = Bencoder.Serialise(input);

        Assert.Equal("l4:spami1e0:i-10ee", output);
    }

    [Fact]
    public void ShouldSerialiseEmptyDictionary()
    {
        var input = new Dictionary<string, int>();
        var output = Bencoder.Serialise(input);

        Assert.Equal("de", output);
    }

    [Fact]
    public void ShouldSerialiseDictionaryOfStrings()
    {
        var input = new Dictionary<string, string>
        {
            ["cow"] = "moo",
            ["spam"] = "eggs"
        };
        var output = Bencoder.Serialise(input);

        Assert.Equal("d3:cow3:moo4:spam4:eggse", output);
    }
    
    [Fact]
    public void ShouldSerialiseObjectWithFullRangeOfValues()
    {
        var input = new
        {
            String1 = "value1",
            Int1 = 1,
            Dic1 = new Dictionary<string, object>
            {
                ["string2"] = "value2",
                ["int2"] = 2,
                ["obj1"] = new
                {
                    String3 = "value3"
                },
                ["list2"] = new[] { 1, 2, 3 }
            },
            List1 = new object[] { "string", 1 }
        };
        var output = Bencoder.Serialise(input);

        Assert.Equal("6:value1i1ed7:string26:value24:int2i2e4:obj16:value35:list2li1ei2ei3eeel6:stringi1ee", output);
    }
}