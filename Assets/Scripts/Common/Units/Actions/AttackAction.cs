using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : UnitAction
{
    protected GameObject target;
    public override ActionTarget actionTarget { get { return ActionTarget.Enemy; } }

    public override bool Condition()
    {
        return true;
    }

    void FixedUpdate()
    {
        if (isRunning)
        {
            
        }
    }
}
