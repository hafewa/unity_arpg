using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionsSystem : MonoBehaviour
{
    private UnitAction[] _actions;
    public UnitAction[] actions { get { return _actions; } }
    public Vector3 pointTarget { get; set; }
    public GameObject objectTarget { get; set; }
    public UnitAction currentAction { get; set; }

    public bool isPreparing { get; set; }

    public float movementSpeed = 5f;
    public float rotationSpeed = 20f;

    public virtual void Start()
    {
        _actions = gameObject.GetComponentsInChildren<UnitAction>();
    }

    void FixedUpdate()
    {
        if (isPreparing)
        {
            MovementAndRotationControl();
        }
        AnimatorControl();
    }

    void MovementAndRotationControl()
    {
        if (currentAction == null)
            return;

        //Movement
        Vector3 unitPos = currentAction.unitGameObject.transform.position;
        Vector3 targetPos = pointTarget;
        Vector3 pos = Vector3.MoveTowards(unitPos, targetPos, movementSpeed * Time.deltaTime);

        if ((unitPos - targetPos).magnitude <= currentAction.range) {
            isPreparing = false;
            currentAction.Run();
            return;
        }

        currentAction.unitRigidbody.MovePosition(pos);

        //Rotation
        Vector3 targetPosY = new Vector3(pointTarget.x, currentAction.unitGameObject.transform.position.y, pointTarget.z);
        currentAction.unitGameObject.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetPosY - unitPos, rotationSpeed * Time.deltaTime, 0f));

        //Debug
        Debug.DrawLine(unitPos, targetPos, Color.red);
    }

    void AnimatorControl()
    {
        if (currentAction == null)
            return;
        float speedh = currentAction.unitAnimator.GetFloat("speedh");
        if (!isPreparing)
        {
            if (speedh > 0.02f)
                currentAction.unitAnimator.SetFloat("speedh", 0f, 0.1f, Time.deltaTime);
            else if (speedh > 0f)
                currentAction.unitAnimator.SetFloat("speedh", 0f);
        }
        else
        {
            if (speedh < 0.98f)
                currentAction.unitAnimator.SetFloat("speedh", 1f, 0.1f, Time.deltaTime);
            else if (speedh < 1f)
                currentAction.unitAnimator.SetFloat("speedh", 1f);
        }
        
    }
}
