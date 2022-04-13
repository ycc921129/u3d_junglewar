using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class StartPlayRequest : BaseRequest
{
    private bool isPlaying = false;
    public override void Awake()
    {
        actionCode = ActionCode.StartPlay;
        base.Awake();
    }
    private void Update()
    {
        if (isPlaying)
        {
            gameface.StartPlaying();
            isPlaying = false;
        }
    }
    public override void OnRespon(string data)
    {
        isPlaying = true;
    }
}
