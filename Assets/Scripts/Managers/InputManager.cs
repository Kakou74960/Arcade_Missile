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
    private InputActionMap _scoreBoardActions;
    private float _boost;
    private float _brake;
    private InputAction _autodestruct;
    private InputAction _nextLetter;
    private InputAction _previousLetter;
    private InputAction _selectRight;
    private InputAction _selectLeft;
    private InputAction _submit;
    #endregion Fields

    #region Properties
    public Vector2 MoveDir => _moveDir;
    public Vector2 NormalizedMoveDir => _moveDir.normalized;
    public float Boost => _boost;
    public float Brake => _brake;
    public InputAction Autodestruct => _autodestruct;
    public InputAction NextLetter { get => _nextLetter; set => _nextLetter = value; }
    public InputAction PreviousLetter { get => _previousLetter; set => _previousLetter = value; }
    public InputAction SelectRight { get => _selectRight; set => _selectRight = value; }
    public InputAction SelectLeft { get => _selectLeft; set => _selectLeft = value; }
    public InputAction Submit { get => _submit; set => _submit = value; }
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
        _scoreBoardActions = InputSystem.actions.FindActionMap("scoreBoard");
        //Active le mapping
        _arcadeActions.Enable();
        _scoreBoardActions.Enable();

        _autodestruct = _arcadeActions.FindAction("Explode");
        _nextLetter = _scoreBoardActions.FindAction("Next");
        _previousLetter = _scoreBoardActions.FindAction("Previous");
        _selectLeft = _scoreBoardActions.FindAction("Left");
        _selectRight = _scoreBoardActions.FindAction("Right");
        _submit = _scoreBoardActions.FindAction("Submit");
    }

    // Update is called once per frame
    void Update()
    {
        _moveDir = _arcadeActions.FindAction("Move").ReadValue<Vector2>();
        //Debug.Log("Vecteur moveDir " + _moveDir);
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
