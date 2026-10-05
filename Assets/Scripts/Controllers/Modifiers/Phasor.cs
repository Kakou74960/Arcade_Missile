using UnityEngine;

public class Phasor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController>())
        {
            CharacterManager.Instance.CharacterController.PhasorState = true;
        }
    }
}
