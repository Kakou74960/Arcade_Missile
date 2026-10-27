using UnityEngine;
using UnityEngine.EventSystems;

public class MenuBase : MonoBehaviour
{
    [SerializeField] protected GameObject _defaultSelected;
    [SerializeField] protected string _selectedSound;
    [SerializeField] protected string _pressedSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /// Si on perd la sélection
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(_defaultSelected);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            EventSystem.current.SetSelectedGameObject(_defaultSelected);
        }
    }

    private void OnSelected()
    {
        AudioManager.Instance.PlaySFXOneShot(_selectedSound);
    }

    private void OnPressed()
    {
        AudioManager.Instance.PlaySFXOneShot(_pressedSound);
    }
}
