using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Tcp客户端
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ip, 9000);
            clientSocket.Connect(localEndPoint);

            byte[] data = new byte[1024];
            int length = clientSocket.Receive(data);
            string msg = Encoding.UTF8.GetString(data, 0, length);
            Console.WriteLine(msg);
            for (int i = 0; i < 6; i++)
            {
                clientSocket.Send(Message.GetByte("客户端" + i));
            }
            Console.ReadKey();
            clientSocket.Close();
        }
    }
}
