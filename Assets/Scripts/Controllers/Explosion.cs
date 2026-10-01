using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radiusExplosion = 10f;
    [SerializeField] private float _timeExplosion = 1f;

    private float _currentTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.localScale = Vector3.one * _radiusExplosion;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime < _timeExplosion)
            _currentTime += Time.deltaTime;

        if (_currentTime >= _timeExplosion)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
