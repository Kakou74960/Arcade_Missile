using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _rendererHeart;
    [SerializeField] private Collider2D _colliderHeart;
    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem _smallParticle;
    [SerializeField] private ParticleSystem _bigParticle;
    [SerializeField] private ParticleSystem _smokeParticle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelManager.Instance.Objective++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        _colliderHeart.enabled = false;
        _rendererHeart.enabled = false;
        _smallParticle.Play();
        _smallParticle.transform.parent = null;
        _bigParticle.Play();
        _bigParticle.transform.parent = null;
        _smokeParticle.Play();
        _smokeParticle.transform.parent = null;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        LevelManager.Instance.Objective--;
    }
}
