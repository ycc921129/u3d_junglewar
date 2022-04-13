using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class RegisterPanel : BasePanel
{
    private InputField UsernameInput;
    private InputField PasswordInput;
    private InputField RePasswordInput;
    private RegisterRequest registerRequest;
    
    private void Start()
    {
        registerRequest = GetComponent<RegisterRequest>();
        UsernameInput = transform.Find("UsernameLabel/UsernameInput").GetComponent<InputField>();
        PasswordInput = transform.Find("PasswordLabel/PasswordInput").GetComponent<InputField>();
        RePasswordInput = transform.Find("RePasswordLabel/RePasswordInput").GetComponent<InputField>();
        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayBtSource();
            mUIManager.PopPanel();
            mUIManager.PushPanel(UIPanelType.StartPanel);
        });
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(RegisterSystem);
    }
    private void RegisterSystem()
    {
        PlayBtSource();
        string msg = "";
        if (string.IsNullOrEmpty(UsernameInput.text))
        {
            msg += "账号不能为空";
        }
        if (string.IsNullOrEmpty(RePasswordInput.text) || string.IsNullOrEmpty(PasswordInput.text))
        {
            msg += "密码不能为空";
        }
        if (string.Compare(RePasswordInput.text, PasswordInput.text) == 1)
        {
            msg += "密码不一致";
        }
        if (!string.IsNullOrEmpty(msg))
        {
            mUIManager.ShowText(msg); return;
        }
        registerRequest.SendRequest(UsernameInput.text, PasswordInput.text);
    }
    public void RegisterRequest(ReturnUserCode usercode)
    {
        PlayBtSource();
        Debug.Log("注册");                                                                                                                 
        if (usercode == ReturnUserCode.Success)
        {
            mUIManager.ShowMessageSync("注册成功");
            mUIManager.PushPanelSync(UIPanelType.LoginPanel);
        }
        else
        {
            mUIManager.ShowMessageSync("此账号已经存在");
        }
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnPause()
    {
        base.OnPause();
    }
    public override void OnResume()
    {
        base.OnResume();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}
