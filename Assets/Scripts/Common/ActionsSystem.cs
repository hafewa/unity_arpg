using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionsSystem : MonoBehaviour
{
    private UnitAction[] _actions;
    public UnitAction[] Actions { get { return _actions; } }
    public Vector3 pointTarget { get; set; }
    public GameObject objectTarget { get; set; }

    public virtual void Start()
    {
        _actions = gameObject.GetComponentsInChildren<UnitAction>();
    }
}
