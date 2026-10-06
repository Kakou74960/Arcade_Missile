using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float _speedForce = 5;
    [SerializeField] private List<Collider2D> _objectsAffectedByWind = new List<Collider2D>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PushObjects();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody)
        {
            _objectsAffectedByWind.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody)
        {
            _objectsAffectedByWind.Remove(collision);
        }
    }

    private void PushObjects()
    {
        foreach (Collider2D obj in _objectsAffectedByWind)
        {
            if (Application.isEditor)
                obj.attachedRigidbody.AddForce(Vector2.right * _speedForce);
            else
                obj.attachedRigidbody.AddForce(Vector2.right * _speedForce * Time.deltaTime);
        }
    }
}
