using UnityEngine;

public class SoundParticleSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _particleAudio;
    [SerializeField] private float _minPitch = 0.5f;
    [SerializeField] private float _maxPitch = 2;

    private void OnParticleCollision(GameObject other)
    {
        _particleAudio.pitch = Random.Range(_minPitch,_maxPitch);
        _particleAudio.PlayOneShot(DatabaseManager.Instance.GetSFXByID("MeatImpact1").AudioClip);
    }
}
