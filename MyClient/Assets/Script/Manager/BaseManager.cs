using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager
{
    protected GameFace gameface;
    protected BaseManager(GameFace _gameface)
    {
        gameface = _gameface;
    }
    public virtual void OnInit() { }
    public virtual void OnDestory() { }
    public virtual void Update() { }
}
