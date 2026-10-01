using Unity.VisualScripting;
using UnityEngine;

public class PlaygroundArea : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision);

        CharacterController missile = collision.gameObject.GetComponent<CharacterController>();
        if (!missile.IsDestroyed())
            missile.Die();
    }

    private void OnApplicationQuit()
    {
        gameObject.SetActive(false);
    }
}
