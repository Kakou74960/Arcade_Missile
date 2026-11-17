using UnityEngine;

public class OverviewLogic : MonoBehaviour
{
    [SerializeField] private Animator _overviewAnim;
    [SerializeField] private float _animSpeed;
    private float _motionTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _motionTime = _overviewAnim.GetFloat("MotionTime");
    }

    // Update is called once per frame
    void Update()
    {
        if (_overviewAnim.GetBool("isOpen"))
        {
            _motionTime += Time.deltaTime * _animSpeed;
        }
        else
            _motionTime -= Time.deltaTime * _animSpeed;

        _motionTime = Mathf.Clamp(_motionTime, 0.0f, 1.0f);
        _overviewAnim.SetFloat("MotionTime", _motionTime);
    }
}
