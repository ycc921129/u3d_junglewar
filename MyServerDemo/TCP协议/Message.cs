using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP协议
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
        public void AddAcount(int acount)
        {
            startIndex += acount;
        }
        public int RemoveIndex
        {
            get { return data.Length - startIndex; }
        }
        public void ReadByte()
        {
            while (true)
            {
                if (startIndex <= 4)
                {
                    return;
                }
                int count = BitConverter.ToInt32(data, 0);
                if (startIndex - 4 >= count)
                {
                    string ms = Encoding.UTF8.GetString(data, 4, count);
                    Console.WriteLine("解析出一条数据：" + ms);
                    Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                    startIndex -= (count + 4);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
