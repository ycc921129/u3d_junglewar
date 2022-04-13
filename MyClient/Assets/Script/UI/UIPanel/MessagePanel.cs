using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    private Text text;
    private string message = null;
    public override void OnEnter()
    {
        base.OnEnter();
        text = GetComponent<Text>();
        mUIManager.SetMessagePanel(this);
    }
    public override void OnExit()
    {
        base.OnExit();
        text.enabled = false;
    }
    private void Update()
    {
        if (message != null)
        {
            Debug.Log("message=" + message);
            ShowText(message);
            message = null;
        }
    }
    public void ShowText(string _text)
    {
        text.text = _text;
        isTitleActive = true;
        base.OnEnter();
        Invoke("HideText", 2.0f);
    }
    public void ShowMessageSync(string msg)
    {
        message = msg;
    }
    public void HideText()
    {
        isTitleActive = false;
        base.OnExit();
    }
}
