using UnityEngine;

public class SpawnPointDrone : MonoBehaviour
{
    [SerializeField] private GameObject _droneToSpawn;
    [SerializeField] private Animator _dispenserAnimator;
    [SerializeField] private string _spawnSound;
    [SerializeField] private float _delaySpawn = 0.5f;

    private GameObject _currentDrone;
    private float _spawnTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_currentDrone == null)
        {
            if (_spawnTime >= _delaySpawn)
            {
                _currentDrone = Instantiate(_droneToSpawn, gameObject.transform.position, Quaternion.identity);
                _currentDrone.GetComponent<Drone>().SpawnPoint = this.transform;
                _dispenserAnimator.SetTrigger("Close");
                _spawnTime = 0;
            }
            else
            {
                if (_spawnTime == 0)
                    _dispenserAnimator.SetTrigger("Open");
                _spawnTime += Time.deltaTime;
            }
        }
    }
}
