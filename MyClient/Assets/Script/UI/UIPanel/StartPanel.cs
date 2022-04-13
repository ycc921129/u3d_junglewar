using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StartPanel : BasePanel
{
    private Button bt;
    private void Start()
    {
        bt = GetComponentInChildren<Button>();
        bt.onClick.AddListener(() =>
        {
            PlayBtSource();
            mUIManager.PushPanel(UIPanelType.LoginPanel);
        });
        facade.PlayBgSound(AudioManager.Sound_Bg_Fast);
    }
    public override void OnPause()
    {
        base.OnPause();
    }
    public override void OnResume()
    {
        base.OnResume();
    }
}
