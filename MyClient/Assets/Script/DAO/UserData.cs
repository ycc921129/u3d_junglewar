using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public int id { get; private set; }
    public string username { get; private set; }
    public int resultCount { get; set; }
    public int resultWinCount { get; set; }
    public UserData(string userData)
    {
        string[] str = userData.Split('-');
        this.id = int.Parse(str[0]);
        this.username = str[1];
        this.resultCount = int.Parse(str[2]); 
        this.resultWinCount = int.Parse(str[3]); 
    }
    public UserData( string _username, int _resultCount, int _resultWinCount)
    {
        this.username = _username;
        this.resultCount = _resultCount;
        this.resultWinCount = _resultWinCount;
    }
    public UserData(int _id,string _username,int _resultCount,int _resultWinCount)
    {
        this.id = _id;
        this.username = _username;
        this.resultCount = _resultCount;
        this.resultWinCount = _resultWinCount;
    }
}
