using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _speed;

    [Header("Nav Mesh")]
    [SerializeField] private NavMeshAgent _navAgent;

    private bool _dead;
    public Transform SpawnPoint
    {
        get { return _spawnPoint; }
        set { _spawnPoint = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null && CharacterManager.Instance.CharacterController != null)
            _target = CharacterManager.Instance.CharacterController.transform;

        if (_target && !_dead)
        {
            NavMeshPath path = new NavMeshPath();
            _navAgent.CalculatePath(_target.position, path);

            if (path.status == NavMeshPathStatus.PathComplete)
                _navAgent.SetDestination(_target.position);
            else
            {
                _navAgent.SetDestination(SpawnPoint.position);
            }

        }

        /*
        if (_target)
        {
            Vector2 direction = _target.position;
            direction -= _rb.position;

            if (Application.isEditor)
                _rb.AddForce(direction.normalized * _speed);
            else
                _rb.AddForce(direction.normalized * _speed * Time.deltaTime);
        }*/
    }

    public void Dead(Vector2 explosionDir)
    {
        _dead = true;
        _navAgent.enabled = false;
        _rb.gravityScale = 2.0f;
        _rb.AddForce(explosionDir * 10);
        _rb.AddTorque(Random.Range(0, 180));
        StartCoroutine(DestroyDrone(1));
    }

    private IEnumerator DestroyDrone(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }
}
