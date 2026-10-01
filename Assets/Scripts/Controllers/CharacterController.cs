using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private GameObject _explosion;

    [Header("Controls Settings")]
    [SerializeField] private float _highSpeed = 1.5f;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _slowSpeed = 0.5f;
    [SerializeField,Range(0,360)] private float _rotation = 0;
    [SerializeField] private bool _boutonFast = false;
    [SerializeField] private bool _boutonSlow = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Force de propulsion du missile
        float targetSpeed = 0;
        if (InputManager.Instance.Boost != 0 && InputManager.Instance.Brake == 0)
        {
            targetSpeed = _highSpeed;
            _boutonFast = true;
        }
        else if (InputManager.Instance.Brake != 0 && InputManager.Instance.Boost == 0)
        {
            targetSpeed = _slowSpeed;
            _boutonSlow = true;
        }
        else
        {
            targetSpeed = _speed;
            _boutonFast = false;
            _boutonSlow = false;
        }
        Debug.Log("Missile speed : " + targetSpeed);

        //Change le calcul si dans l'editeur ou non
        if (Application.isEditor)
            _rb.AddForce(_rb.transform.up * targetSpeed);
        else
            _rb.AddForce(_rb.transform.up * targetSpeed * Time.deltaTime);

        //calcul de direction du missile
        if (InputManager.Instance.MoveDir != Vector2.zero)
        {
            _rotation = Vector2.SignedAngle(Vector2.up, InputManager.Instance.MoveDir);
            Debug.Log(_rotation);
        }

        //rotation du missile
        _rb.rotation = _rotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    public void Die()
    {
        Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
