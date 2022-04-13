using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class Ariam : MonoBehaviour
{
    private Rigidbody mRigibody;
    private float speed = 10.0f;
    public RoleType roleType;
    public GameObject particle;
    public bool isLocal = false;
    void Start ()
    {
        mRigibody = GetComponent<Rigidbody>();	
	}
	
	void Update ()
    {
        mRigibody.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameFace.Instance.PlayNormalSound(AudioManager.Sound_ShootPerson);
            if (isLocal)
            {
                bool playerIslocal = other.GetComponent<PlayerInfo>().isLocal;
                if (isLocal != playerIslocal)
                {
                    GameFace.Instance.SendAtk(Random.Range(10, 20));
                }
            }
        }
        else
        {
            GameFace.Instance.PlayNormalSound(AudioManager.Sound_Miss);
        }
        GameObject.Instantiate(particle, transform.position, transform.rotation);
        GameObject.Destroy(this.gameObject);
    }
}
