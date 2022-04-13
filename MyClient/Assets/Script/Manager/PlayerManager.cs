using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class PlayerManger : BaseManager
{
    private Transform rolePos;
    private UserData userdata;
    private Dictionary<RoleType, RoleData> mRoleDic = new Dictionary<RoleType, RoleData>();
    private RoleType currentRoleType;
    private GameObject currentRole;
    private GameObject playerSyncRequest;
    private GameObject otherRole;
    private ShootRequest shootRequest;
    private AtkRequest atkRequest;

    public UserData userData
    {
        get { return userdata; }
        set { userdata = value; }
    }
    public PlayerManger(GameFace _gameface) : base(_gameface)
    {

    }
    public override void OnInit()
    {
        rolePos = GameObject.Find("RolePosition").transform;
        InitArom();
    }
    public void InitArom()
    {
        mRoleDic.Add(RoleType.Blue, new RoleData(RoleType.Blue, "Prefabs/Hunter_BLUE", "Prefabs/Arrow_BLUE", "Prefabs/Explosion_BLUE", rolePos.GetChild(0).position));
        mRoleDic.Add(RoleType.Red, new RoleData(RoleType.Red, "Prefabs/Hunter_RED", "Prefabs/Arrow_RED", "Prefabs/Explosion_RED", rolePos.GetChild(1).position));
    }
    public void SpawnRoles() 
    {
        Debug.Log("mRoleDic.count = " + mRoleDic.Count);
        foreach (RoleData rd in mRoleDic.Values)
        {
            GameObject go = GameObject.Instantiate(rd.mAromPrefab, rd.mRolePos, Quaternion.identity);
            go.tag = "Player";
            if (currentRoleType == rd.mRoleType)
            {
                currentRole = go;
                currentRole.GetComponent<PlayerInfo>().isLocal = true;
            }
            else
            {
                otherRole = go;
            }
        }
    }
    public void UpdateResult(int resultcount,int resultWincount)
    {
        userdata.resultCount = resultcount;
        userdata.resultWinCount = resultWincount;
    }
    public void SetCurrentRole(RoleType roleType)
    {
        currentRoleType = roleType;
    }
    public GameObject GetCurrentRole()
    {
        return currentRole;
    }
    public RoleData GetRoleData(RoleType roleType)
    {
        RoleData roleData = null;
        mRoleDic.TryGetValue(roleType, out roleData);
        return roleData;
    }
    public void AddRoleScript()
    {
        currentRole.AddComponent<PlayerMove>();
        PlayerAtk playerAtk = currentRole.AddComponent<PlayerAtk>();
        RoleType roleType = currentRole.GetComponent<PlayerInfo>().roleType;
        RoleData roleData = GetRoleData(roleType);
        playerAtk.mArrowBLUE = roleData.mAjian;
        playerAtk.SetPlayerMananger(this);
    }
    public void PlayerSyncRequest()
    {
        playerSyncRequest = new GameObject("playerSyncRequest");
        playerSyncRequest.AddComponent<MoveRequest>().SetTransformAndPlayerMove(currentRole.transform, currentRole.GetComponent<PlayerMove>())
            .SetOtherTransformAndPlayerMove(otherRole.transform);
        shootRequest = playerSyncRequest.AddComponent<ShootRequest>();
        shootRequest.playerManager = this;
        atkRequest = playerSyncRequest.AddComponent<AtkRequest>();
    }
    public void Shoot(GameObject arrowPrefab, Vector3 pos,Quaternion rot)
    {
        gameface.PlayNormalSound(AudioManager.Sound_Timer);
        GameObject.Instantiate(arrowPrefab, pos, rot).GetComponent<Ariam>().isLocal = true;
        shootRequest.SendRequest(arrowPrefab.GetComponent<Ariam>().roleType, pos, rot.eulerAngles);
    }
    public void RemoteShoot(RoleType roleType,Vector3 pos,Vector3 rot)
    {
        GameObject arrowPrefab = GetRoleData(roleType).mAjian;
        Transform tran = GameObject.Instantiate(arrowPrefab).transform;
        tran.transform.position = pos;
        tran.transform.eulerAngles = rot;
    }
    public void SendAtk(int damage)
    {
        atkRequest.SendRequest(damage);
    }

    public void DestroyGameObject()
    {
        GameObject.Destroy(currentRole);
        GameObject.Destroy(playerSyncRequest);
        GameObject.Destroy(otherRole);
        shootRequest = null;
        atkRequest = null;
    }
}
