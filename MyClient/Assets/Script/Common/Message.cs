using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using System.Linq;
using System.Text;

public class Message
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
    public void ReadByte(int Amount, Action<ActionCode, string> processDataCallback)
    {
        startIndex += Amount;
        while (true)
        {
            if (startIndex <= 4)
            {
                return;
            }
            int count = BitConverter.ToInt32(data, 0);
            if ((startIndex - 4) >= count)
            {
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
                string ms = Encoding.UTF8.GetString(data, 8, count - 4);
                processDataCallback(actionCode, ms);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= (count + 4);
            }
            else
            {
                break;
            }
        }
    }
    //public static byte[] PickedMessage(RequestCode requestCode, string ms)
    //{
    //    byte[] requestData = BitConverter.GetBytes((int)requestCode);
    //    byte[] messageData = Encoding.UTF8.GetBytes(ms);
    //    byte[] lengthData = BitConverter.GetBytes(requestData.Length + messageData.Length);
    //    byte[] newDatas = lengthData.Concat(requestData).ToArray<byte>();
    //    return lengthData.Concat(messageData).ToArray<byte>();
    //}
    public static byte[] PickedMessage(RequestCode requestCode,ActionCode actionCode, string ms)
    {
        byte[] requestData = BitConverter.GetBytes((int)requestCode);
        byte[] actionData = BitConverter.GetBytes((int)actionCode);
        byte[] messageData = Encoding.UTF8.GetBytes(ms);
        int length = requestData.Length + messageData.Length + actionData.Length;
        byte[] lengthData = BitConverter.GetBytes(length);
        
        return lengthData.Concat(requestData).ToArray<byte>().Concat(actionData).ToArray<byte>()
            .Concat(messageData).ToArray<byte>(); 
    }
}
