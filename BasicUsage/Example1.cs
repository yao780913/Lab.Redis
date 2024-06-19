namespace ConsoleApp1;

/// <summary>
/// Using a redis database
/// </summary>
public class Example1
{
    private readonly ConnectionMultiplexer _muxer;

    public Example1 (ConnectionMultiplexer muxer)
    {
        _muxer = muxer;
    }
    public void Run ()
    {
        var db = _muxer.GetDatabase();
        
        var value = "abcdefg";
        var redisKey = "key1";
        db.StringSet(redisKey, value);

        Console.WriteLine(db.KeyExists(redisKey));
        Console.WriteLine(db.StringGet(redisKey));
        db.KeyDelete(redisKey);
        Console.WriteLine(db.KeyExists(redisKey));
    }
}