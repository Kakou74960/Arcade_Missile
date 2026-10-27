using UnityEngine;

public class Phasor : MonoBehaviour
{
    [SerializeField] private string _phasorSound = "Phasing";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController>())
        {
            CharacterManager.Instance.CharacterController.PhasorState = true;
            AudioManager.Instance.PlaySFXOneShot(_phasorSound);
        }
    }
}
