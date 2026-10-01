using UnityEngine;

public class SpawnPointMissile : MonoBehaviour
{
    [SerializeField] private GameObject _missileToSpawn;
    private GameObject _currentMissile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (FindAnyObjectByType<CharacterController>())
            _currentMissile = FindAnyObjectByType<CharacterController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentMissile == null)
        {
            _currentMissile = Instantiate(_missileToSpawn, gameObject.transform.position, Quaternion.identity);
        }
    }
}
