using UnityEngine;

public class PlaygroundArea : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision);
        collision.gameObject.GetComponent<CharacterController>().Die();
    }

    private void OnApplicationQuit()
    {
        gameObject.SetActive(false);
    }
}
