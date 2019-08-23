using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAction : UnitAction
{
    public float rage = 0.2f;
    protected Vector3 target;

    public override ActionTarget actionTarget { get { return ActionTarget.Point; } }

    public override bool Condition()
    {
        return true;
    }

    void FixedUpdate()
    {
        if (isRunning)
        {
            Stop();
        }
    }
}
