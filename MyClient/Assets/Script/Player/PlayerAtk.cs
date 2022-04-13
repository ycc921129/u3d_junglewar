using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : MonoBehaviour
{
    public GameObject mArrowBLUE;
    private Animator mAnimator;
    private Transform leftHandTrans;
    private Vector3 shootDir;
    private PlayerManger playerManager;

    void Start () {
        mAnimator = GetComponent<Animator>();
        leftHandTrans = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");
    }
	
	void Update ()
    {
        if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 point = hit.point;
                    point.y = transform.position.y;
                    shootDir = point - transform.position;
                    transform.rotation = Quaternion.LookRotation(shootDir);
                    mAnimator.SetTrigger("Attack");
                    Invoke("Shoot", 0.01f);
                }
            }
        }
	}
    public void SetPlayerMananger(PlayerManger playerManager)
    {
        this.playerManager = playerManager;
    }
    private void Shoot()
    {
        playerManager.Shoot(mArrowBLUE, leftHandTrans.position, Quaternion.LookRotation(shootDir));
    }
}
