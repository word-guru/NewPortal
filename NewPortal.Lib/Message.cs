using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewPortal.Server
{
    public static class Message
    {
        public static async Task SendData(object value, Stream stream)
        {
            var sendRaw = JsonSerializer.Serialize(value,value.GetType());
            var data = Encoding.Unicode.GetBytes(sendRaw);
            await stream.WriteAsync(data, 0, data.Length);
        }
        
        public static async Task<string> GetRequest(NetworkStream stream)
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