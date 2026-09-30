using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>, IManager
{

    #region Fields
    private Vector3 _moveDir = Vector3.zero;
    #endregion Fields

    #region Properties
    public Vector3 MoveDir => _moveDir;
    public Vector3 NormalizedMoveDir => _moveDir.normalized;
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

    // Update is called once per frame
    void Update()
    {
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
