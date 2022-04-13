using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TCP协议
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServerAsync();
        }
        private static void StartServerTongbu()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ip, 9000);
            serverSocket.Bind(localEndPoint);
            serverSocket.Listen(50);
            Socket clientSocket = serverSocket.Accept();
            string msg = "client success";
            clientSocket.Send(Encoding.UTF8.GetBytes(msg));

            byte[] data = new byte[1024];
            int length = clientSocket.Receive(data);
            string msgs = Encoding.UTF8.GetString(data, 0, length);
            Console.WriteLine(msgs);

            serverSocket.Close();
            clientSocket.Close();
        }

        static Socket serverSocket = null;
        private static void StartServerAsync()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ip, 9000);
            serverSocket.Bind(localEndPoint);
            serverSocket.Listen(50);
            serverSocket.BeginAccept(AcceptCallBack, serverSocket);
            Console.ReadKey();
        }
        static Message msss = new Message();
        //static byte[] data = new byte[1024];
        private static void AcceptCallBack(IAsyncResult ar)
        {
            Socket serverSocket = ar.AsyncState as Socket;
            Socket clientSocket = serverSocket.EndAccept(ar);
            string msg = "client success";
            clientSocket.Send(Encoding.UTF8.GetBytes(msg));

            clientSocket.BeginReceive(msss.GetData, msss.StartIndex, msss.RemoveIndex, SocketFlags.None, ReceiveCallback, clientSocket);
            serverSocket.BeginAccept(AcceptCallBack, serverSocket);
        }
        static void ReceiveCallback(IAsyncResult ar)
        {
            Socket clientSocket = ar.AsyncState as Socket;
            int length = clientSocket.EndReceive(ar);
            msss.AddAcount(length);
            //string msg = Encoding.UTF8.GetString(data, 0, length);
            //Console.WriteLine(msg);
            msss.ReadByte();
            clientSocket.BeginReceive(msss.GetData, msss.StartIndex, msss.RemoveIndex, SocketFlags.None, ReceiveCallback, clientSocket);
        }
    }
}
