using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAction : UnitAction
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 20f;
    protected Vector3 target;

    public override ActionTarget actionTarget { get { return ActionTarget.Point; } }

    public override bool Condition()
    {
        Vector3 unitPos = unitGameObject.transform.position;
        Vector3 targetPos = actionSystem.pointTarget;

        if ((unitPos - targetPos).magnitude < 1f) {
            return false;
        }

        return true;
    }

    void FixedUpdate()
    {
        if (isRunning)
        {
            MovementAndRotationControl();
        }
        AnimatorControl();
    }

    void MovementAndRotationControl()
    {
        //Movement
        Vector3 unitPos = unitGameObject.transform.position;
        Vector3 targetPos = actionSystem.pointTarget;
        Vector3 pos = Vector3.MoveTowards(unitPos, targetPos, movementSpeed * Time.deltaTime);

        if ((unitPos - targetPos).magnitude < 0.2f) {
            Stop();
            return;
        }

        unitRigidbody.MovePosition(pos);

        //Rotation
        Vector3 targetPosY = new Vector3(actionSystem.pointTarget.x, unitGameObject.transform.position.y, actionSystem.pointTarget.z);
        unitGameObject.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetPosY - unitPos, rotationSpeed * Time.deltaTime, 0f));

        //Debug
        Debug.DrawLine(unitPos, targetPos, Color.red);
    }

    void AnimatorControl()
    {
        float speedh = unitAnimator.GetFloat("speedh");
        if (!isRunning)
        {
            if (speedh > 0.02f)
                    unitAnimator.SetFloat("speedh", 0f, 0.1f, Time.deltaTime);
            else if (speedh > 0f)
                    unitAnimator.SetFloat("speedh", 0f);
        }
        else
        {
            if (speedh < 0.98f)
                    unitAnimator.SetFloat("speedh", 1f, 0.1f, Time.deltaTime);
            else if (speedh < 1f)
                    unitAnimator.SetFloat("speedh", 1f);
        }
        
    }
}
