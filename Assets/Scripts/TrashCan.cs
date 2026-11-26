using System;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] private float _delay;

    private float _currentDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentDelay >= _delay)
        {
            MonoBehaviour[] eraseArray = gameObject.GetComponentsInChildren<MonoBehaviour>(true);

            foreach (MonoBehaviour obj in eraseArray)
            {
                if (obj.gameObject != this.gameObject)
                    Destroy(obj,_delay);
            }
            _currentDelay = 0;
        }

        _currentDelay += Time.deltaTime;
    }
}
