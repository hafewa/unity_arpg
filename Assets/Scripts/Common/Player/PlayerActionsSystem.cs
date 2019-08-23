using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionsSystem : ActionsSystem
{
    public string[] inputs;
    
    private UnitAction _movementAction;

    public UnitAction movementAction { get { return _movementAction; } }

    public override void Start()
    {
        base.Start();
        foreach (UnitAction action in actions)
        {
            if (action.GetType() == typeof(MovementAction))
            {
                _movementAction = action;
                break;
            }
        }
        if (movementAction == null)
            throw new Exception("Movement Action is missing!");
    }

    void Update()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            if (Input.GetButton(inputs[i])) 
            {
                switch (actions[i].actionTarget)
                {
                    case ActionTarget.None:
                    {
                        ActionHandlerNoneTarget(actions[i]);
                        break;
                    }
                    case ActionTarget.Point:
                    {
                        ActionHandlerPointTarget(actions[i]);
                        break;
                    }
                    case ActionTarget.Enemy:
                    {
                        if (!ActionHandlerEnemyTarget(actions[i])) {
                            ActionHandlerPointTarget(movementAction);
                        }
                        break;
                    }
                    default: break;
                }
                
            }
        }
    }

    bool ActionHandlerNoneTarget(UnitAction action)
    {
        currentAction = action;
        return true;
    }

    bool ActionHandlerPointTarget(UnitAction action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layer = LayerMask.GetMask("Walkable");
        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            pointTarget = hit.point;
            objectTarget = hit.transform.gameObject;
            currentAction = action;
            isPreparing = true;
            return true;
        }
        return false;
    }

    bool ActionHandlerEnemyTarget(UnitAction action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layer = LayerMask.GetMask("Enemy");
        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            pointTarget = hit.point;
            objectTarget = hit.transform.gameObject;
            currentAction = action;
            isPreparing = true;
            return true;
        }
        return false;
    }

}
