using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Collider2D _explosionTrigger;
    [SerializeField] private float _radiusExplosion = 10f;
    [SerializeField] private float _timeExplosion = 1f;
    [SerializeField] private float _delayExplosion = 0.5f;
    [SerializeField] private string _explosionSound = "Explosion";

    private float _currentTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.localScale = Vector3.one * _radiusExplosion;
        AudioManager.Instance.PlaySFXOneShot(_explosionSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime >= _delayExplosion && !_explosionTrigger.enabled)
            _explosionTrigger.enabled = true;

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
            heart.transform.rotation = this.transform.rotation;
            heart.Die();
        }
        else
            Destroy(collision.gameObject);
    }
}
