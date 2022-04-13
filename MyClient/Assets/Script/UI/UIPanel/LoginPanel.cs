using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class LoginPanel : BasePanel
{
    private InputField UsernameInput;
    private InputField PasswordInput;
    private LoginRequest loginRequest;
    private void Start()
    {
        loginRequest = GetComponent<LoginRequest>();
        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayBtSource();
            if (!isTitleActive)
            {
                mUIManager.PopPanel();
                mUIManager.PushPanel(UIPanelType.StartPanel);
            }
        });
        UsernameInput = transform.Find("UsernameLabel/UsernameInput").GetComponent<InputField>();
        PasswordInput = transform.Find("PasswordLabel/PasswordInput").GetComponent<InputField>();
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayBtSource();
            mUIManager.PushPanel(UIPanelType.RegisterPanel);
        });
        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayBtSource();
            string msg = "";
            if (string.IsNullOrEmpty(UsernameInput.text))
            {
                msg += "账号不能为空";
            }
            if (string.IsNullOrEmpty(PasswordInput.text))
            {
                msg += "密码不能为空";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                mUIManager.ShowText(msg); return;
            }
            loginRequest.SendRequest(UsernameInput.text, PasswordInput.text);
        });
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public void LoginRequest(ReturnUserCode usercode)
    {
        PlayBtSource();
        if (usercode == ReturnUserCode.Fail)
        {
            mUIManager.ShowMessageSync("登录密码错误");
            Debug.Log("登录密码错误");
        }
        else
        {
            mUIManager.ShowMessageSync("登录成功");
            mUIManager.PushPanelSync(UIPanelType.RoomListPanel);
            Debug.Log("登录成功");
        }
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
