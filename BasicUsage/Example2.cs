namespace ConsoleApp1;

/// <summary>
/// Using redis pub/sub
/// </summary>
public class Example2
{
    private readonly ConnectionMultiplexer _muxer;
    private readonly ISubscriber _sub;
    private const string ChannelName = "messages";

    public Example2 (ConnectionMultiplexer muxer)
    {
        _muxer = muxer;
        _sub = _muxer.GetSubscriber();
    }

    
    public void Run1 ()
    {
        _sub.Subscribe(
            ChannelName,
            (channel, message) =>
            {
                Console.WriteLine((string)message);
            });
    }

    /// <summary>
    /// Synchronous Handler
    /// </summary>
    public void Run2 ()
    {
        _sub.Subscribe(ChannelName).OnMessage(
            channelMessage =>
            {
                Console.WriteLine($"Synchronous handler: {(string) channelMessage.Message}");
            });

        _sub.Publish(ChannelName, "hello");
        _sub.Publish(ChannelName, "hello2");
    }
    
    /// <summary>
    /// Asynchronous Handler
    /// </summary>
    public void Run3 ()
    {
        _sub.Subscribe(ChannelName).OnMessage(
            channelMessage =>
            {
                Task.Run(async () =>
                {
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - {Task.CurrentId}");
                    Console.WriteLine($"Asynchronous handler: {(string) channelMessage.Message}");
                    await Task.Delay(1000);
                });
            });
    
        _sub.Publish(ChannelName, "hello");
        _sub.Publish(ChannelName, "hello2");
    }
}
