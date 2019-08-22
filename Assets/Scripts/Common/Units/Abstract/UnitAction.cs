using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAction : MonoBehaviour
{
    #region Cache
    public Rigidbody unitRigidbody
    {
        get
        {
            if (_rigidbody == null) {
                _rigidbody = unitGameObject.GetComponent<Rigidbody>();
            }
            return _rigidbody;
        }
    }
    public GameObject unitGameObject
    {
        get
        {
            if (_gameObject == null) {
                _gameObject = actionSystem.gameObject;
            }
            return _gameObject;
        }
    }
    public Animator unitAnimator
    {
        get
        {
            if (_animator == null) {
                _animator = unitGameObject.GetComponent<Animator>();
            }
            return _animator;
        }
    }
    private Rigidbody _rigidbody;
    private GameObject _gameObject;
    private Animator _animator;
    #endregion

    public abstract bool Condition();

    private ActionsSystem _actionSystem;
    public ActionsSystem actionSystem { get { return _actionSystem; }}

    public bool isRunning { get; set; }
    public abstract ActionTarget actionTarget { get; }

    public void Run()
    {
        if (Condition())
            isRunning = true;
    }

    public void Stop()
    {
        isRunning = false;
    }

    void Start()
    {
        _actionSystem = GetComponentInParent<ActionsSystem>();
    }
}

public enum ActionTarget
{
    None,
    Point,
    Enemy,
}