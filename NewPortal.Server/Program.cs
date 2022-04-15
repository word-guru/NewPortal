using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NewPortal.Server.Lib.DataBase;
using NewPortal.Server.Lib.Models;

namespace NewPortal.Server
{
    class Program
    {
        private static async Task Main()
        {
            var server = new TcpListener(IPAddress.Loopback, 8005);
            server.Start();

            while (true)
            {
                var client = await server.AcceptTcpClientAsync();
                var clientStream = client.GetStream();

                var requestRaw = await GetRequest(clientStream);
                var request = JsonSerializer.Deserialize<Request>(requestRaw);

                switch (request?.Type)
                {
                    case "news_all":
                        await GetAllNews(clientStream);
                        break;
                    case "user_all":
                        break;
                    case "news":
                        break;
                    case "insert_news":
                        break;
                }
            }
        }

        private static async Task GetAllNews(Stream stream)
        {
            var db = new DB();
            var news = await db.GetAllNews();

            SendData(news, stream);
        }

        private static async Task SendData(object value, Stream stream)
        {
            var sendRaw = JsonSerializer.Serialize(value,value.GetType());
            var data = Encoding.Unicode.GetBytes(sendRaw);
            await stream.WriteAsync(data, 0, data.Length);
        }
        
        private static async Task<string> GetRequest(NetworkStream stream)
        {
            var builder = new StringBuilder();
            int bytes = 0;
            var data = new byte[64];
            do
            {
                bytes = await stream.ReadAsync(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (stream.DataAvailable);
            
            return builder.ToString();
        }
    }
}