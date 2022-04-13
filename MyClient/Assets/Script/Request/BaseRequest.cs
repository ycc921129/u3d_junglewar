using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class BaseRequest : MonoBehaviour
{
    protected ActionCode actionCode = ActionCode.None;
    protected RequestCode requestCode = RequestCode.None;
    protected GameFace _gameface; 
    protected GameFace gameface
    {
        get
        {
            if (_gameface == null)
            {
                _gameface = GameFace.Instance;
            }
            return _gameface;
        }
    }
	public virtual void Awake ()
    {
        GameFace.Instance.AddRequestCode(actionCode, this);
    }
    protected void SendRequest(string msg)//发送消息
    {
        gameface.SendRequest(requestCode, actionCode, msg);
    }
    public virtual void SendRequest() { }
    public virtual void OnRespon(string data) { }//接触数据
	public void OnDestroy()
    {
        if (gameface != null)
            GameFace.Instance.RemoveRequest(actionCode);
    }
}
