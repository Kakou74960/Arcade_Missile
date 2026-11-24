using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _shakeStrength = 3;
    [SerializeField] private float _loseShakeSpeed = 1;

    private float _currentShakeStrength = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_camera == null)
            _camera = gameObject.GetComponent<Camera>();

        LevelManager.Instance.CurrentCamera = this;
    }

    // Update is called once per frame
    void Update()
    {
        ShakeLogic();
    }

    private void ShakeLogic()
    {
        Vector3 randomPosition = Random.insideUnitCircle * _currentShakeStrength;
        _camera.transform.position = _offset + randomPosition;
        _currentShakeStrength -= _loseShakeSpeed * Time.deltaTime;
        _currentShakeStrength = Mathf.Clamp(_currentShakeStrength, 0, _shakeStrength);
    }

    public void Shake()
    {
        _currentShakeStrength = _shakeStrength;
    }
}
