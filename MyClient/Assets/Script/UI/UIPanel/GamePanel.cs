using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class GamePanel : BasePanel
{
    public Text mTimer;
    public Button FailButton;
    public Button ExitButton;
    private int mTimeNum = -1;
    public Button SuccessButton;
    private QuitBattleRequest quitBattleRequest;
    private void Start()
    {
        Debug.Log(1);
        if (mTimer == null)
        {
            mTimer = transform.Find("Timer").GetComponent<Text>();
        }
        if (SuccessButton == null)
        {
            SuccessButton = transform.Find("SuccessButton").GetComponent<Button>();
        }
        if (FailButton == null)
        {
            FailButton = transform.Find("FailButton").GetComponent<Button>();
        }
        if (ExitButton == null)
        {
            ExitButton = transform.Find("ExitButton").GetComponent<Button>();
        }
        mTimer.gameObject.SetActive(false);
        SuccessButton.onClick.AddListener(ResultOnclick);
        FailButton.onClick.AddListener(ResultOnclick);
        ExitButton.onClick.AddListener(ExitButtonOnclick);
        quitBattleRequest = GetComponent<QuitBattleRequest>();
        CloseBt();
    }
    private void OnEnable()
    {
        CloseBt();
    }
    private void Update()
    {
        if (mTimeNum != -1)
        {
            ShowTime(mTimeNum);
            mTimeNum = -1;
        }
    }
    private void CloseBt()
    {
        SuccessButton.gameObject.SetActive(false);
        FailButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
    }
    public void ShowTimeAsync(int time)
    {
        this.mTimeNum = time;        
    }
    public void ShowTime(int timeNum)
    {
        if (timeNum == 3)
        {
            ExitButton.gameObject.SetActive(true);
        }
        mTimer.gameObject.SetActive(true);
        mTimer.transform.localScale = Vector3.one;
        Color color = new Color();
        color.a = 1;
        mTimer.color = color;
        mTimer.text = timeNum.ToString();
        mTimer.transform.DOScale(2.0f, 0.3f).SetDelay(0.4f);
        mTimer.transform.DOScale(0, 0.3f).SetDelay(0.4f);
        facade.PlayNormalSound(AudioManager.Sound_Alert);
    }
    public void SetResult(ReturnUserCode returnCode)
    {
        Button tmpBt = null;
        switch (returnCode)
        {
            case ReturnUserCode.Success:
                tmpBt = SuccessButton;
                break;
            case ReturnUserCode.Fail:
                tmpBt = FailButton;
                break;
            default:
                break;
        }
        tmpBt.gameObject.SetActive(true);
        tmpBt.transform.localScale = Vector3.zero;
        tmpBt.transform.DOScale(1, 0.5f);
    }
    public void ResultOnclick()
    {
        mUIManager.PushPanel(UIPanelType.RoomListPanel);
        facade.ResultOver();
        CloseBt();
    }
    public void QuitBattle()
    {
        ResultOnclick();
    }
    public void ExitButtonOnclick()
    {
        quitBattleRequest.SendRequest();
    }
}

