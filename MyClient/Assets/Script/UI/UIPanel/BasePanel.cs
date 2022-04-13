using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour
{
    protected UIManager mUIManager;
    protected static bool isTitleActive = false;//判断提示栏是否存在
    protected CanvasGroup canvasGroup;
    protected GameFace facade;

    protected void Awake()
    {
        InitCanvasGroup();
    }
    private void InitCanvasGroup()
    {
        if (GetComponent<CanvasGroup>() == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public UIManager UIMng
    {
        set { mUIManager = value; }
    }

    public GameFace Facade
    {
        set { facade = value; }
    }
    public void PlayBtSource()
    {
        //facade.PlayNormalSound(AudioManager.Sound_ButtonClick);
    }
    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关系
    /// </summary>
    public virtual void OnExit()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
