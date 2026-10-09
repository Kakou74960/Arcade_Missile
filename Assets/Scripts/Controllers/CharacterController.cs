using System;
using System.ComponentModel;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region Fields
    [Header("Character Settings")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private Color _forcedBoostColor;
    [SerializeField] private Color _forcedBrakeColor;
    [SerializeField] private Color _invertedColor;

    [Header("State Sprite")]
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _phasedStateSprite;

    [Header("Controls Settings")]
    [SerializeField] private float _highSpeed = 1.5f;
    [SerializeField] private float _highDampening = 1;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _mediumDampening = 1.5f;
    [SerializeField] private float _slowSpeed = 0.5f;
    [SerializeField] private float _slowDampening = 2;
    [SerializeField,Range(0,360)] private float _rotation = 0;
    //[SerializeField] private bool _boutonFast = false;
    //[SerializeField] private bool _boutonSlow = false;

    private bool _electrifiedState = false;
    private bool _phasorState = false;
    private bool _forcedBoost;
    private bool _forcedBrake;
    private bool _inverted;
    private bool _die = false;
    #endregion Fields

    #region Properties
    public bool PhasorState
    {
        get => _phasorState;
        set
        {
            _phasorState = value;
            if (_phasorState)
            {
                _collider.isTrigger = true;
                _spriteRenderer.sprite = _phasedStateSprite;
            }
            else
            {
                _collider.isTrigger = false;
                _spriteRenderer.sprite = _normalSprite;
            }
        }

    }
    public bool ForcedBoost
    {
        get => _forcedBoost;
        set
        {
            _forcedBoost = value;
            if (_forcedBoost)
                _spriteRenderer.color = _forcedBoostColor;
        }
    }

    public bool ForceBrake
    {
        get => _forcedBrake;
        set
        {
            _forcedBrake = value;
            if (_forcedBrake)
                _spriteRenderer.color = _forcedBrakeColor;
        }
    }

    public bool Inverted
    {
        get => _inverted;
        set
        {
            _inverted = value;
            if (_inverted)
                _spriteRenderer.color = _invertedColor;
        }
    }

    public bool ElectrifiedState { get => _electrifiedState; set => _electrifiedState = value; }
    #endregion Properties

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputManager.Instance.Autodestruct.performed += ctx => Die();
        CharacterManager.Instance.CharacterController = this;
    }


    // Update is called once per frame
    void Update()
    {
        SpeedLogic();

        //calcul de direction du missile
        if (InputManager.Instance.MoveDir != Vector2.zero)
        {
            if (_inverted)
                _rotation = Vector2.SignedAngle(Vector2.up, -InputManager.Instance.MoveDir);
            else
                _rotation = Vector2.SignedAngle(Vector2.up, InputManager.Instance.MoveDir);
            Debug.Log(_rotation);
        }

        //rotation du missile
        _rb.rotation = _rotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_phasorState)
            Die();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            PhasorState = false;
        }
    }

    #region Speed
    private void SpeedLogic()
    {
        // Force de propulsion du missile
        float targetSpeed = 0;
        if (_forcedBoost)
            targetSpeed = HighSpeed();
        else if (_forcedBrake)
            targetSpeed = SlowSpeed();
        else if (_inverted)
        {
            if (InputManager.Instance.Boost == 0 && InputManager.Instance.Brake != 0)
            {
                targetSpeed = HighSpeed();
                //_boutonFast = true;
            }
            else if (InputManager.Instance.Brake == 0 && InputManager.Instance.Boost != 0)
            {
                targetSpeed = SlowSpeed();
                //_boutonSlow = true;
            }
            else
            {
                targetSpeed = NormalSpeed();
                //_boutonFast = false;
                //_boutonSlow = false;
            }
        }
        else
        {
            if (InputManager.Instance.Boost != 0 && InputManager.Instance.Brake == 0)
            {
                targetSpeed = HighSpeed();
                //_boutonFast = true;
            }
            else if (InputManager.Instance.Brake != 0 && InputManager.Instance.Boost == 0)
            {
                targetSpeed = SlowSpeed();
                //_boutonSlow = true;
            }
            else
            {
                targetSpeed = NormalSpeed();
                //_boutonFast = false;
                //_boutonSlow = false;
            }
        }
        Debug.Log("Missile speed : " + targetSpeed);

        //Change le calcul si dans l'editeur ou non
        if (Application.isEditor)
            _rb.AddForce(_rb.transform.up * targetSpeed);
        else
            _rb.AddForce(_rb.transform.up * targetSpeed * Time.deltaTime);
    }

    private float HighSpeed()
    {
        _rb.linearDamping = _highDampening;
        return _highSpeed;
    }

    private float NormalSpeed()
    {
        _rb.linearDamping = _mediumDampening;
        return _speed;
    }

    private float SlowSpeed()
    {
        _rb.linearDamping = _slowDampening;
        return _slowSpeed;
    }
    #endregion Speed

    public void Die()
    {
        if(!_die)
        {
            Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            _die = true;
        }
    }
}
