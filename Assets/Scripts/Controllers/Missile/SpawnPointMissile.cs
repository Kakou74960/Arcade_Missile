using UnityEngine;

public class SpawnPointMissile : MonoBehaviour
{
    [SerializeField] private GameObject _missileToSpawn;
    [SerializeField] private Animator _launcherAnimator;
    [SerializeField] private string _spawnSound = "Launching_Missile";
    [SerializeField] private float _delaySpawn = 0.5f;

    private GameObject _currentMissile;
    private float _spawnTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (FindAnyObjectByType<CharacterController>())
            _currentMissile = FindAnyObjectByType<CharacterController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentMissile == null && !LevelManager.Instance.LevelFinished)
        {
            if (_spawnTime >= _delaySpawn)
            {
                AudioManager.Instance.PlaySFXOneShot(_spawnSound);
                _currentMissile = Instantiate(_missileToSpawn, gameObject.transform.position, Quaternion.identity);
                LevelManager.Instance.MissileUsed++;
                _launcherAnimator.SetTrigger("Close");
                _spawnTime = 0;
            }
            else
            {
                if (_spawnTime == 0)
                    _launcherAnimator.SetTrigger("Open");
                _spawnTime += Time.deltaTime;
            }
        }
    }
}
