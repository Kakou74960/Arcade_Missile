using UnityEngine;

public class Inverter : MonoBehaviour
{
    [SerializeField] private string _invertedSound = "Inverted";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController>())
        {
            CharacterManager.Instance.CharacterController.Inverted = true;
            AudioManager.Instance.PlaySFXOneShot(_invertedSound);
        }
    }
}
