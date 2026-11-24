using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region Fields
    [Header("Character Settings")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private Color _forcedBoostColor;
    [SerializeField] private string _acceleratorSound = "Accelerator";
    [SerializeField] private Color _forcedBrakeColor;
    [SerializeField] private string _deceleratorSound = "Decelerator";
    [SerializeField] private AudioSource _flySource;
    [SerializeField] private string _soundFlying;
    [SerializeField] private string _soundFastFlying;
    [SerializeField] private string _soundSlowFlying;

    [Header("Normal State")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _missileAnimator;

    [Header("Inverted State")]
    [SerializeField] private Color _invertedColor;
    [SerializeField] private string _invertedSound = "Inverted";

    [Header("Phased State")]
    [SerializeField] private SpriteRenderer _changeSpriteRenderer;
    [SerializeField] private Animator _invertedMissileAnim;
    [SerializeField] private string _phasorSound = "Phasing";
    [SerializeField] private string _UnphasingSound = "Unphasing";

    [Header("Electrified State")]
    [SerializeField] private string _electricSound = "Electricity";

    [Header("Controls Settings")]
    [SerializeField] private ParticleSystem _highSpeedParticle;
    [SerializeField] private float _highSpeed = 1.5f;
    [SerializeField] private float _highDampening = 1;
    [SerializeField] private ParticleSystem _mediumSpeedParticle;
    [SerializeField] private float _mediumSpeed = 1;
    [SerializeField] private float _mediumDampening = 1.5f;
    [SerializeField] private ParticleSystem _lowSpeedParticle;
    [SerializeField] private float _slowSpeed = 0.5f;
    [SerializeField] private float _slowDampening = 2;
    [SerializeField] private ParticleSystem _smokeParticle;
    [SerializeField,Range(0,360)] private float _rotation = 0;
    //[SerializeField] private bool _boutonFast = false;
    //[SerializeField] private bool _boutonSlow = false;

    private bool _electrifiedState = false;
    private bool _phasorState = false;
    private bool _forcedBoost;
    private bool _forcedBrake;
    private bool _inverted;
    private bool _die = false;

    private List<Collider2D> _walls = new List<Collider2D>();
    #endregion Fields

    #region Properties
    public bool PhasorState
    {
        get => _phasorState;
        set
        {
            if (value != _phasorState)
            {
                _phasorState = value;
                if (_phasorState)
                {
                    _collider.isTrigger = true;
                    _missileAnimator.SetBool("Phased", true);
                    AudioManager.Instance.PlaySFXOneShot(_phasorSound);
                }
                else
                {
                    _collider.isTrigger = false;
                    _missileAnimator.SetBool("Phased", false);
                    AudioManager.Instance.PlaySFXOneShot(_UnphasingSound);
                }
            }
        }

    }
    public bool ForcedBoost
    {
        get => _forcedBoost;
        set
        {
            if (value != _forcedBoost)
            {
                _forcedBoost = value;
                if (_forcedBoost)
                {
                    _spriteRenderer.color = _forcedBoostColor;
                    _changeSpriteRenderer.color = _forcedBoostColor;
                    AudioManager.Instance.PlaySFXOneShot(_acceleratorSound);
                }
            }
        }
    }

    public bool ForceBrake
    {
        get => _forcedBrake;
        set
        {
            if (value != _forcedBrake)
            {
                _forcedBrake = value;
                if (_forcedBrake)
                {
                    _spriteRenderer.color = _forcedBrakeColor;
                    _changeSpriteRenderer.color = _forcedBrakeColor;
                    AudioManager.Instance.PlaySFXOneShot(_deceleratorSound);
                }
            }
        }
    }

    public bool Inverted
    {
        get => _inverted;
        set
        {
            if (value != _inverted)
            {
                _inverted = value;
                if (_inverted)
                {
                    _spriteRenderer.color = _invertedColor;
                    _changeSpriteRenderer.color = _invertedColor;
                    //AudioManager.Instance.PlaySFXOneShot(_invertedSound);
                }
                AudioManager.Instance.ReverseMusic(_inverted);
            }
        }
    }

    public bool ElectrifiedState
    {
        get => _electrifiedState;
        set
        {
            if (value != _electrifiedState)
            {
                _electrifiedState = value;
                if (_electrifiedState)
                {
                    _missileAnimator.SetBool("Electrified", true);
                    _invertedMissileAnim.SetBool("Electrified", true);
                    AudioManager.Instance.PlaySFXOneShot(_electricSound);
                }
                else
                {
                    _missileAnimator.SetBool("Electrified", false);
                    _invertedMissileAnim.SetBool("Electrified", false);
                }
            }
        }
    }
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
        /*if (_flySoundTime >= 5f)
        {
            AudioManager.Instance.PlaySFXOneShot(_soundFlying);
            _flySoundTime = 0;
        }
        else
            _flySoundTime += Time.deltaTime;
        */
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _walls.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _walls.Remove(collision);
            if (_walls.Count == 0)
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
            _rb.AddForce(_rb.transform.up * targetSpeed);
    }

    private float HighSpeed()
    {
        _rb.linearDamping = _highDampening;
        StopSpeedParticles();
        PlayRocketSound(_soundFastFlying);
        _highSpeedParticle.gameObject.SetActive(true);
        _highSpeedParticle.Play();
        return _highSpeed;
    }

    private float NormalSpeed()
    {
        _rb.linearDamping = _mediumDampening;
        StopSpeedParticles();
        PlayRocketSound(_soundFlying);
        _mediumSpeedParticle.gameObject.SetActive(true);
        _mediumSpeedParticle.Play();
        return _mediumSpeed;
    }

    private float SlowSpeed()
    {
        _rb.linearDamping = _slowDampening;
        StopSpeedParticles();
        PlayRocketSound(_soundSlowFlying);
        _lowSpeedParticle.gameObject.SetActive(true);
        _lowSpeedParticle.Play();
        return _slowSpeed;
    }

    private void StopSpeedParticles()
    {
        _highSpeedParticle.Stop();
        _mediumSpeedParticle.Stop();
        _lowSpeedParticle.Stop();
    }
    #endregion Speed

    private void DetachParticles()
    {
        StopSpeedParticles();
        _smokeParticle.Stop();
        _highSpeedParticle.transform.parent = null;
        _mediumSpeedParticle.transform.parent = null;
        _lowSpeedParticle.transform.parent = null;
        _smokeParticle.transform.parent = null;
    }

    private void PlayRocketSound(string id)
    {
        if (_flySource.clip != DatabaseManager.Instance.GetSFXByID(id).AudioClip)
        {
            _flySource.clip = DatabaseManager.Instance.GetSFXByID(id).AudioClip;
            _flySource.volume = DatabaseManager.Instance.GetSFXByID(id).Volume;
            _flySource.Play();
        }
    }

    public void Die()
    {
        if(!_die)
        {
            InputManager.Instance.Autodestruct.performed -= ctx => Die();
            DetachParticles();
            Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
            LevelManager.Instance.CurrentCamera.Shake();
            AudioManager.Instance.ReverseMusic(false);
            _die = true;
            Destroy(gameObject);
        }
    }
}
