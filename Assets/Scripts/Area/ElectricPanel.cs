using UnityEngine;

public class ElectricPanel : MonoBehaviour
{
    [SerializeField] private bool _activated = false;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _panelSprites;

    public bool Activated
    {
        get => _activated;
        set
        {
            _activated = value;
            if (_activated)
            {
                _spriteRenderer.sprite = _panelSprites[1];
            }
            else
                _spriteRenderer.sprite = _panelSprites[0];
        }
    }

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

        if (collision.gameObject == missile.gameObject && missile.ElectrifiedState)
        {
            Activated = true;
            missile.ElectrifiedState = false;
        }
    }
}
