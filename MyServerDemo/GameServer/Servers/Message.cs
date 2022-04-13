using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServer.Servers
{
    class Message
    {
        private byte[] data = new byte[1024];
        private int startIndex = 0;
        public int StartIndex
        {
            get { return startIndex; }
        }
        public byte[] GetData
        {
            get { return data; }
        }
        public int RemoveIndex
        {
            get { return data.Length - startIndex; }
        }
        public void ReadByte(int Amount,Action<RequestCode,ActionCode,string> processDataCallback)
        {
            startIndex += Amount;
            while (true)
            {
                if (startIndex <= 4)
                {
                    return;
                }
                int count = BitConverter.ToInt32(data, 0);
                if (startIndex - 4 >= count)
                {
                    //string ms = Encoding.UTF8.GetString(data, 4, count);
                    RequestCode requestCode = (RequestCode)BitConverter.ToInt32(data, 4);
                    ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);
                    string ms = Encoding.UTF8.GetString(data, 12, count - 8);
                    processDataCallback(requestCode, actionCode, ms);
                    Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                    startIndex -= (count + 4);
                }
                else
                {
                    break;
                }
            }
        }
        public static byte[] PickedMessage(ActionCode actionCode,string ms)
        {
            byte[] requestData = BitConverter.GetBytes((int)actionCode);
            byte[] messageData = Encoding.UTF8.GetBytes(ms);
            int dataAmount = requestData.Length + messageData.Length;
            byte[] lengthData = BitConverter.GetBytes(dataAmount);
            byte[] newDatas = lengthData.Concat(requestData).ToArray<byte>();
            return newDatas.Concat(messageData).ToArray<byte>();
        }
    }
}
