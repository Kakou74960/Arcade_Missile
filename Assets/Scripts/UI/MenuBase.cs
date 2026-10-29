using UnityEngine;
using UnityEngine.EventSystems;

public class MenuBase : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] protected Animator _baseMenuAnimator;
    [SerializeField] protected float _animSpeed = 1;
    [SerializeField] protected GameObject _defaultSelected;
    [SerializeField] protected string _selectedSound;
    [SerializeField] protected string _pressedSound;

    public enum Direction
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

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

    public void TransitionSlide(Direction Slidedirection, bool slideOut)
    {
        _baseMenuAnimator.SetBool("Out", slideOut);
        _baseMenuAnimator.SetFloat("Speed", _animSpeed);

        switch (Slidedirection)
        {
            case Direction.UP:
                _baseMenuAnimator.SetTrigger("SlideUp");
                break;
            case Direction.DOWN:
                _baseMenuAnimator.SetTrigger("SlideDown");
                break;
            case Direction.RIGHT:
                _baseMenuAnimator.SetTrigger("SlideRight");
                break;
            case Direction.LEFT:
                _baseMenuAnimator.SetTrigger("SlideLeft");
                break;
            default:
                break;
        }
    }

    public void OnSelected()
    {
        AudioManager.Instance.PlaySFXOneShot(_selectedSound);
    }

    public void OnPressed()
    {
        AudioManager.Instance.PlaySFXOneShot(_pressedSound);
    }
}
