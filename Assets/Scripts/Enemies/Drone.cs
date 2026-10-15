using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null && CharacterManager.Instance.CharacterController != null)
            _target = CharacterManager.Instance.CharacterController.transform;

        if (_target)
        {
            Vector2 direction = _target.position;
            direction -= _rb.position;

            if (Application.isEditor)
                _rb.AddForce(direction.normalized * _speed);
            else
                _rb.AddForce(direction.normalized * _speed * Time.deltaTime);
        }
    }
}
