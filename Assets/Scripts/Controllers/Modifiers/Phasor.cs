using UnityEngine;

public class Phasor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController missile = CharacterManager.Instance.CharacterController;

        if (collision.gameObject == missile.gameObject && !missile.PhasorState)
        {
            missile.PhasorState = true;
        }
    }
}
