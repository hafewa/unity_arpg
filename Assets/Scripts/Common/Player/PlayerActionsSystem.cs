using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionsSystem : ActionsSystem
{
    public string[] inputs;
    public override void Start()
    {
        base.Start();
    }

    void Update()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            if (Input.GetButton(inputs[i])) 
            {
                switch (Actions[i].actionTarget)
                {
                    case ActionTarget.None:
                    {
                        ActionHandlerNoneTarget(Actions[i]);
                        break;
                    }
                    case ActionTarget.Point:
                    {
                        ActionHandlerPointTarget(Actions[i]);
                        break;
                    }
                    case ActionTarget.Enemy:
                    {
                        ActionHandlerEnemyTarget(Actions[i]);
                        break;
                    }
                    default: break;
                }
                
            }
        }
    }

    void ActionHandlerNoneTarget(UnitAction action)
    {
        action.Run();
    }

    void ActionHandlerPointTarget(UnitAction action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layer = LayerMask.GetMask("Walkable");
        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            pointTarget = hit.point;
            objectTarget = hit.transform.gameObject;
            action.Run();
        }
    }

    void ActionHandlerEnemyTarget(UnitAction action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layer = LayerMask.GetMask("Enemy");
        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            pointTarget = hit.point;
            objectTarget = hit.transform.gameObject;
            action.Run();
        }
    }

}
