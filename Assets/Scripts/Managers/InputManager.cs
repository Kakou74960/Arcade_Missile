using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>, IManager
{

    #region Fields
    private Vector2 _moveDir = Vector3.zero;
    private InputActionMap _arcadeActions;
    private float _boost;
    private float _brake;
    #endregion Fields

    #region Properties
    public Vector2 MoveDir => _moveDir;
    public Vector2 NormalizedMoveDir => _moveDir.normalized;
    public float Boost => _boost;
    public float Brake => _brake;
    #endregion Properties

    #region Event
    private event Action _jumped = null;
    public event Action Jumped
    {
        add
        {
            _jumped -= value;
            _jumped += value;
        }
        remove
        {
            _jumped -= value;
        }
    }

    #endregion Event

    /*public void Init()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }*/
    private void Start()
    {
        //récupère le mapping d'actions "Arcade"
        _arcadeActions = InputSystem.actions.FindActionMap("Arcade");
        //Active le mapping
        _arcadeActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        _moveDir = _arcadeActions.FindAction("Move").ReadValue<Vector2>();
        Debug.Log("Vecteur moveDir " + _moveDir);
        _boost = _arcadeActions.FindAction("Accelerate").ReadValue<float>();
        _brake = _arcadeActions.FindAction("Decelerate").ReadValue<float>();
        //MOVEDIR
        //_moveDir.x = Input.GetAxis("Horizontal");
        //_moveDir.y = Input.GetAxis("Vertical");

        //JUMP
        if (_jumped != null && Input.GetButtonDown("Jump"))
        {
            _jumped();
        }
    }
}
