using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFor : MonoBehaviour
{
    private float time = 1;
    void Start()
    {
        Destroy(this.gameObject, time);
    }
}
