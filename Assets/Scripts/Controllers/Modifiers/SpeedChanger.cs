using UnityEngine;

public class SpeedChanger : MonoBehaviour
{
    [SerializeField] private bool _isBooster = true;
    [SerializeField] private string _acceleratorSound = "Accelerator";
    [SerializeField] private string _deceleratorSound = "Decelerator";

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
        if (collision.gameObject.GetComponent<CharacterController>())
        {
            CharacterManager.Instance.CharacterController.ForcedBoost = _isBooster;
            CharacterManager.Instance.CharacterController.ForceBrake = !_isBooster;
            if (_isBooster)
            {
                AudioManager.Instance.PlaySFXOneShot(_acceleratorSound);
            }
            else
                AudioManager.Instance.PlaySFXOneShot(_deceleratorSound);
        }
    }
}
