using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RequestManager : BaseManager
{
    private Dictionary<ActionCode, BaseRequest> RequestDic = new Dictionary<ActionCode, BaseRequest>();
    public RequestManager(GameFace _gameface) : base(_gameface)
    {
    }
    public void AddRequest(ActionCode actionCode,BaseRequest baseRequest)
    {
        RequestDic.Add(actionCode, baseRequest);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        RequestDic.Remove(actionCode);
    }
    public void HandleRequest(ActionCode actionCode, string data)
    {
        BaseRequest baseRequest = RequestDic.TryGet<ActionCode, BaseRequest>(actionCode);
        if (baseRequest == null)
        {
            Debug.LogWarning("无法得到ActionCode[" + actionCode + "]对应的Request类");return;
        }
        baseRequest.OnRespon(data);
    }
}
