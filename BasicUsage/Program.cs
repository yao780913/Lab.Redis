using ConsoleApp1;

var options = new ConfigurationOptions
{
    EndPoints = { "localhost:6379" },
    AbortOnConnectFail = false,
    AllowAdmin = true
};
var muxer = await ConnectionMultiplexer.ConnectAsync(options);

// var example1 = new Example1(muxer);
// example1.Run();

// var example2 = new Example2(muxer);
// example2.Run2();
// example2.Run3();

// var example3 = new Example3(muxer);
// example3.Run();

