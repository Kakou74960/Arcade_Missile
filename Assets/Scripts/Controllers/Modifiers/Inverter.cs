using UnityEngine;

public class Inverter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController>())
        {
            CharacterManager.Instance.CharacterController.Inverted = true;
        }
    }
}
