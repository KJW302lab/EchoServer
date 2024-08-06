using System;
using WebSocketSharp;
using WebSocketSharp.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var wssv = new WebSocketServer("ws://172.31.54.242:6666");

        wssv.AddWebSocketService<Echo>("/Echo", () =>
        {
            var service = new Echo();
            service.SetCORSHeaders();
            return service;
        });
        
        wssv.Start();
        Console.WriteLine("WebSocket Server started on ws://172.31.54.242:6666/Echo. Press any key to exit...");
        Console.ReadKey(true);
        wssv.Stop();
    }
}

public class Echo : WebSocketBehavior
{
    public void SetCORSHeaders()
    {
        Context.Headers.Add("Access-Control-Allow-Origin", "*");
    }

    protected override void OnOpen()
    {
        Console.WriteLine("Connection opened from: " + Context.UserEndPoint);
        
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine("Received message from client: " + e.Data);
        Send(e.Data);
    }

    protected override void OnClose(CloseEventArgs e)
    {
        Console.WriteLine("Connection closed: " + e.Reason);
    }

    protected override void OnError(WebSocketSharp.ErrorEventArgs e)
    {
        Console.WriteLine($"Error occurred: {e.Message}");
    }
}