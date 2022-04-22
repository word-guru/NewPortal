using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using NewPortal.Server.Lib.DataBase;

namespace NewPortal.Server
{
    class Program
    {
        private static async Task Main()
        {
            var server = new TcpListener(IPAddress.Parse("127.0.0.1"), 65003);
            server.Start();

            while (true)
            {
                var client = await server.AcceptTcpClientAsync();
                var clientStream = client.GetStream();

                var requestRaw = await Message.GetRequest(clientStream);
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

            Message.SendData(news, stream);
        }

       
    }
}