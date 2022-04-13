using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcp客户端
{
    class Message
    {
        public static byte[] GetByte(string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            int bytelength = data.Length;
            byte[] databyte = BitConverter.GetBytes(bytelength);
            byte[] newByte = databyte.Concat(data).ToArray();
            return newByte;
        }
    }
}
