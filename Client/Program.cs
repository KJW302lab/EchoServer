using System;
using WebSocketSharp;

public class Program
{
    public static void Main(string[] args)
    {
        string url = "ws://43.202.161.57:6666/Echo";
        using (var ws = new WebSocket(url))
        {
            ws.OnOpen += (sender, e) =>
            {
                Console.WriteLine("Connected to the server");
            };

            ws.OnMessage += (sender, e) =>
            {
                Console.WriteLine("Received from server: " + e.Data);
            };

            ws.OnClose += (sender, e) =>
            {
                Console.WriteLine("Connection closed: " + e.Reason);
            };

            ws.OnError += (sender, e) =>
            {
                Console.WriteLine("Error occurred: " + e.Message);
            };

            ws.Connect();
            Console.WriteLine("Press any key to send a message...");
            Console.ReadKey(true);

            ws.Send("Hello, Server!");
            Console.WriteLine("Message sent to server");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}