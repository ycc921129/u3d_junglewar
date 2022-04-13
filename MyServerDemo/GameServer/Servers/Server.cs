using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using GameServer.Controller;
using Common;

namespace GameServer.Servers
{
    class Server
    {
        private Socket serverSocket;
        private IPEndPoint localEndPoint;
        private List<Client> clientList = new List<Client>();//客户端容器
        private List<Room> roomList = new List<Room>();//房间容器
        private ControllerManager controllerManager;
        public Server()
        {

        }
        public Server(string ipStr,int port)
        {
            controllerManager = new ControllerManager(this);
            SetIpAndPort(ipStr, port);
        }
        public void SetIpAndPort(string ipStr, int port)
        {
            localEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }
        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(localEndPoint);
            serverSocket.Listen(0);
            Console.WriteLine("server start");
            serverSocket.BeginAccept(AcceptCallBack, null);
        }
        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientsocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientsocket, this);
            client.Start();
            clientList.Add(client);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }
        public void RemoveClientList(Client client)
        {
            lock (clientList)
            {
                clientList.Remove(client);
            }
        }
        public void CreateRoom(Client client)
        {
            Room room = new Room(this);
            room.AddClient(client);
            roomList.Add(room);
        }
        public void RemoveRoom(Room room)
        {
            if (roomList != null && room != null)
            {
                roomList.Remove(room);
            }
        }
        public void SendRequest(Client client,ActionCode actionCode,string data)
        {
            client.Send(actionCode, data);
        }
        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            controllerManager.HandleRequest(requestCode, actionCode, data, client);
        }
        public List<Room> GetRoomList()
        {
            return roomList;
        }
        public Room GetRoomById(int id)
        {
            foreach (Room room in roomList)
            {
                if (room.GetRoomId() == id)
                {
                    return room;
                }
            }
            return null;
        }
    }
}
