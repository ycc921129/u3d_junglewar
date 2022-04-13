using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RoleData
{
    public RoleType mRoleType { get; private set; }
    public GameObject mAromPrefab { get; private set; }
    public GameObject mAjian { get; private set; }
    public GameObject mParticle { get; private set; }
    public Vector3 mRolePos { get; private set; }
    public RoleData(RoleType roleType,string arompath,string jianpath,string particlePath,Vector3 rolepos)
    {
        mRoleType = roleType;
        mAromPrefab = Resources.Load<GameObject>(arompath);
        mAjian = Resources.Load<GameObject>(jianpath);
        mParticle = Resources.Load<GameObject>(particlePath); ;
        mAjian.GetComponent<Ariam>().particle = mParticle;
        mRolePos = rolepos;
    }
}
