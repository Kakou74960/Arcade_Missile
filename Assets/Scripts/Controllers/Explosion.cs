using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radiusExplosion = 10f;
    [SerializeField] private float _timeExplosion = 1f;
    [SerializeField] private float _delayExplosion = 0.5f;
    [SerializeField] private Collider2D _explosionTrigger;

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
        if (collision.GetComponent<TargetController>())
        {
            TargetController heart = collision.GetComponent<TargetController>();
            heart.transform.up = heart.transform.position - this.gameObject.transform.position;
            heart.Die();
        }
        else
            Destroy(collision.gameObject);
    }
}
