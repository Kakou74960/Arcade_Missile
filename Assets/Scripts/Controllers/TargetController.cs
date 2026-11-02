using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _rendererHeart;
    [SerializeField] private Collider2D _colliderHeart;
    [SerializeField] private string _soundAtDeath;
    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem[] _heartParticles;

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
        AudioManager.Instance.PlaySFXOneShot(_soundAtDeath);
        foreach (ParticleSystem particle in _heartParticles)
        {
            particle.Play();
            particle.transform.parent = null;
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        LevelManager.Instance.Objective--;
        if (LevelManager.Instance.Objective == 0 && LevelManager.Instance.LevelMenu != null)
        {
            LevelManager.Instance.LevelFinished = true;
            LevelManager.Instance.LevelMenu.gameObject.SetActive(true);
            LevelManager.Instance.LevelMenu.ShowScore();
        }
    }
}
