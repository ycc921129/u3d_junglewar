using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Common;
using GameServer.Tools;
using MySql.Data.MySqlClient;
using GameServer.Model;
using GameServer.DAO;

namespace GameServer.Servers
{
    class Client
    {
        private Server server;
        private Socket clientSocket;
        private Message msg = new Message();
        private MySqlConnection connection;
        public MySqlConnection GetConnection { get { return connection; } }

        private User user;
        private Result result;
        private Room room;
        private ResultDAO resultDAO = new ResultDAO();
        public int HP { get; set; }
        public bool TakeDamage(int damage)
        {
            HP -= damage;
            HP = Math.Max(HP, 0);
            if (HP <= 0) return true;
            return false;
        }
        public bool IsDie()
        {
            return HP <= 0;
        }

        public void Setuserdata(User _user, Result _result)
        {
            this.user = _user;
            this.result = _result;
        }
        public string GetUserdata()
        {
            return user.id+ "-" + user.username + "-" + result.resultCount + "-" + result.resultWinCount;
        }
        public Room Room
        {
            get { return room; }
            set { room = value; }
        }
        public Client() { }
        public Client(Socket _clientSocket, Server _server)
        {
            this.server = _server;
            this.clientSocket = _clientSocket;
            connection = ConnHelper.ConnectDOA();
        }
        public void Start()
        {
            if (clientSocket == null || clientSocket.Connected == false) {
                return;
            } 
            clientSocket.BeginReceive(msg.GetData, msg.StartIndex, msg.RemoveIndex, SocketFlags.None, ReceiveCallBack, null);
        }
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                if (clientSocket == null || clientSocket.Connected == false)
                {
                    return;
                }
                int length = clientSocket.EndReceive(ar);
                if (length == 0)
                {
                    CloseClient();
                }
                msg.ReadByte(length, ProcessDataCallback);
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CloseClient();
            }
        }
        private void CloseClient()
        {
            ConnHelper.CloseDOA(connection);
            if (clientSocket != null)
            {
                Console.WriteLine("client close");
                clientSocket.Close();
            }
            if (room != null)
            {
                room.QuitRoom(this);
            }
            server.RemoveClientList(this);
            if (room != null)
            {
                room.Close(this);
            }
        }
        public void ProcessDataCallback(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }
        public void Send(ActionCode actionCode, string data)
        {
            try
            {
                byte[] bytes = Message.PickedMessage(actionCode, data);
                clientSocket.Send(bytes);
            }
            catch (Exception e)
            {
                Console.WriteLine("server send message false");
            }
        }
        public int GetUserid()
        {
            return user.id;
        }
        public bool IsHouseOwner()
        {
            return room.IsHouseOwner(this);
        }
        public void UpdateResult(bool isVictory)
        {
            UpdateResultToDAO(isVictory);
            UpdateResultToClient();
        }
        private void UpdateResultToDAO(bool isVictory)
        {
            result.resultCount++;
            if (isVictory)
            {
                result.resultWinCount++;
            }
            resultDAO.UpdateOrAddResultValue(connection, result);
        }
        private void UpdateResultToClient()
        {
            Send(ActionCode.UpdateResult, string.Format("{0}_{1}", result.resultCount, result.resultWinCount));
        }
    }
}
