### Prepare Environment: Run redis server in docker
`docker run -d --rm --name redis-server -p 6379:6379 redis redis-server --save 60 1 --loglevel warning`
```
CONTAINER ID   IMAGE     COMMAND                   CREATED          STATUS          PORTS                    NAMES
fc7c3683ce2f   redis     "docker-entrypoint.s…"   16 minutes ago   Up 16 minutes   0.0.0.0:6379->6379/tcp   redis-server
```

### Examples
- Example1: `Using a redis database`
- Example2: `Using redis pub/sub`
- Example3: `Accessing individual servers` 

### 錯誤用法 1
Find potential problems in the code:
```csharp
_sub.Subscribe(ChannelName)
    .OnMessage(async channelMessage =>
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - {Task.CurrentId}");
        Console.WriteLine($"Asynchronous handler: {(string) channelMessage.Message}");
        await Task.Delay(1000);
    });
```

以下是問 AI Assistant 的回答
1. **Use of async void in the callback of OnMessage Using `async void` can lead to unforeseen exceptions as they cannot be awaited, making the parent unaware of any exceptions that may occur.** Instead, consider keeping the callback synchronous and spinning off work that needs to be done asynchronously.
    ```csharp
    public void Run3 ()
    {
        _sub.Subscribe(ChannelName).OnMessage(
            channelMessage =>
            {
               Task.Run(async () => 
               {
                   Console.WriteLine($"Asynchronous handler: {(string) channelMessage.Message}");
                   await Task.Delay(1000);
               });
           });
    
        _sub.Publish(ChannelName, "hello");
        _sub.Publish(ChannelName, "hello2");
    }
    ```
Please note that potential problems depend on the larger context of your code and the requirements. There may be problems that aren't visible from the provided code.

### References
- [StackExchange.Redis](https://stackexchange.github.io/StackExchange.Redis/Server)
  - [Running a Redis Server](https://stackexchange.github.io/StackExchange.Redis/Server)
  - [Basic Usage](https://stackexchange.github.io/StackExchange.Redis/Basics)
- [Run redis server in docker](https://hub.docker.com/_/redis/)
