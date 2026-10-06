using UnityEngine;

public class Electricity : MonoBehaviour
{
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

        if (collision.gameObject == missile.gameObject)
        {
            missile.ElectrifiedState = true;
        }
    }
}
