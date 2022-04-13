using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFace : MonoBehaviour
{
    private static GameFace _instance;
    public static GameFace Instance { get { return _instance; } }
    private UIManager uimanager;
    private PlayerManger playermanager;
    private AudioManager audiomanager;
    private RequestManager requestmanager;
    private ClientManager clientmanager;
    private CameraManager cameramanager;
    private bool isEntingPlay = false;
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        Screen.SetResolution(800, 600, false);
    }
	void Start ()
    {
        uimanager = new UIManager(this);
        requestmanager = new RequestManager(this);
        playermanager = new PlayerManger(this);
        audiomanager = new AudioManager(this);
        clientmanager = new ClientManager(this);
        cameramanager = new CameraManager(this);
        uimanager.OnInit();
        clientmanager.OnInit();
        audiomanager.OnInit();
        requestmanager.OnInit();
        playermanager.OnInit();
        cameramanager.OnInit();
    }
    private void Update()
    {
        OnUpdate();
        if (isEntingPlay)
        {
            EntingPlay();
            isEntingPlay = false;
        }
    }
    public void ShowText(string _text)
    {
        uimanager.ShowText(_text);
    }
    public void AddRequestCode(ActionCode actionCode, BaseRequest baseRequest)
    {
        requestmanager.AddRequest(actionCode, baseRequest);
    }
    public void PlayBgSound(string soundName)
    {
        audiomanager.PlayBgSound(soundName);
    }
    public void PlayNormalSound(string soundName)
    {
        audiomanager.PlayNormalSound(soundName);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        requestmanager.RemoveRequest(actionCode);
    }
    public void HandleRequest(ActionCode actionCode, string data)
    {
        requestmanager.HandleRequest(actionCode, data);
    }
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string msg)
    {
        clientmanager.SendRequest(requestCode, actionCode, msg);
    }
    public UserData GetUserData()
    {
        return playermanager.userData;
    }
    public void SetUserdata(UserData userdata)
    {
        playermanager.userData = userdata;
    }
    public void SetCurrentRole(RoleType roleType)
    {
        playermanager.SetCurrentRole(roleType);
    }
    public GameObject GetCurrentRole()
    {
        return playermanager.GetCurrentRole();
    }
    public void EntingPlaySync()
    {
        isEntingPlay = true;
    }
    public void EntingPlay()
    {
        playermanager.SpawnRoles();
        cameramanager.FollowRole();
    }
    public void StartPlaying()
    {
        playermanager.AddRoleScript();
        playermanager.PlayerSyncRequest();
    }
    public void SendAtk(int damage)
    {
        playermanager.SendAtk(damage);
    }
    public void ResultOver()
    {
        cameramanager.WalkthroughScene();
        playermanager.DestroyGameObject();
    }
    public void UpdateResult(int resultcount, int resultWincount)
    {
        playermanager.UpdateResult(resultcount, resultWincount);
    }
    private void OnDestory()
    {
        uimanager.OnDestory();
        clientmanager.OnDestory();
        audiomanager.OnDestory();
        requestmanager.OnDestory();
        playermanager.OnDestory();
        cameramanager.OnDestory();
    }
    private void OnUpdate()
    {
        uimanager.Update();
        clientmanager.Update();
        audiomanager.Update();
        requestmanager.Update();
        playermanager.Update();
        cameramanager.Update();
    }
}
