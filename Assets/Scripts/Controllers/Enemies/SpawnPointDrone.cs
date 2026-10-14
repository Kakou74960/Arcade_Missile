using UnityEngine;

public class SpawnPointDrone : MonoBehaviour
{
    [SerializeField] private GameObject _droneToSpawn;
    private GameObject _currentDrone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_currentDrone == null)
        {
            _currentDrone = Instantiate(_droneToSpawn, gameObject.transform.position, Quaternion.identity);
        }
    }
}
