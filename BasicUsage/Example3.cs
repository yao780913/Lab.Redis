using System.Net;

namespace ConsoleApp1;

/// <summary>
/// Accessing individual servers
/// </summary>
public class Example3
{
    private readonly ConnectionMultiplexer _muxer;

    public Example3 (ConnectionMultiplexer muxer)
    {
        _muxer = muxer;
    }

    public void Run ()
    {
        var server = _muxer.GetServer("localhost", 6379);
        
        var endpoints = _muxer.GetEndPoints();
        var lastSave = server.LastSave();
        var clients = server.ClientList();

        Console.WriteLine(lastSave);
        foreach (var e in endpoints)
        {
            if (e is DnsEndPoint dnsEndPoint)
            {
                Console.WriteLine((dnsEndPoint.Host, dnsEndPoint.Port));
            }
        }
        
        foreach (var client in clients)
        {
            Console.WriteLine((client.Name, client.Address, client.Host, client.Port));
        }
    }
}