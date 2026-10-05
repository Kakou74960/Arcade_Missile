using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _elementToTurnToTarget;
    //[SerializeField] private Transform _target;
    [SerializeField] private LineRenderer _lineLock;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _lockTime = 3;
    [SerializeField] private Color _startLockColor;
    [SerializeField] private Color _endLockColor;

    private float _currentLockTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterManager.Instance.CharacterController != null)
            CheckIfMissileVisible();
        //_elementToTurnToTarget.transform.rotation = Quaternion.FromToRotation(gameObject.transform.position, CharacterManager.Instance.CharacterController.transform.position);

        if(_currentLockTime >= _lockTime)
        {
            CharacterManager.Instance.CharacterController.Die();
            _currentLockTime = 0;
        }
    }

    private void CheckIfMissileVisible()
    {
        RaycastHit2D turretRay = Physics2D.Linecast(_elementToTurnToTarget.transform.position, CharacterManager.Instance.CharacterController.transform.position, _layerMask);


        if (turretRay)
        {
            LineLock(turretRay.collider.gameObject == CharacterManager.Instance.CharacterController.gameObject);
            if (turretRay.collider.gameObject == CharacterManager.Instance.CharacterController.gameObject)
            {
                // Tourne vers le missile
                _elementToTurnToTarget.transform.up = CharacterManager.Instance.CharacterController.transform.position - gameObject.transform.position;
                _currentLockTime += Time.deltaTime;
            }
            else
                _currentLockTime = 0;
            Debug.Log("Turret raycast :" + turretRay.collider.gameObject);
        }
    }

    private void LineLock(bool locking)
    {
        Color lineColor = Color.Lerp(_startLockColor, _endLockColor, _currentLockTime / _lockTime);
        if (locking)
        {
            _lineLock.SetPosition(0,_elementToTurnToTarget.transform.position);
            _lineLock.SetPosition(1,CharacterManager.Instance.CharacterController.transform.position);
            _lineLock.startColor = lineColor;
            _lineLock.endColor = lineColor;
            _lineLock.enabled = true;
        }
        else
            _lineLock.enabled = false;
    }
}
