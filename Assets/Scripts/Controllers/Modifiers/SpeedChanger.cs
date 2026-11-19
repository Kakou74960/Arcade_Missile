using UnityEngine;

public class SpeedChanger : MonoBehaviour
{
    [SerializeField] private bool _isBooster = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController missile = CharacterManager.Instance.CharacterController;

        if (collision.gameObject == missile.gameObject && !missile.PhasorState)
        {
            CharacterManager.Instance.CharacterController.ForcedBoost = _isBooster;
            CharacterManager.Instance.CharacterController.ForceBrake = !_isBooster;
        }
    }
}
