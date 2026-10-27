using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _open = false;
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private ElectricPanel _electricPanel;
    [SerializeField] private string _openingSound = "Door_Opening";
    [SerializeField] private string _PressureSound = "Depressurization";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_electricPanel.Activated)
            _open = true;

        if (_open)
            _doorAnimator.SetTrigger("OpenDoor");
    }

    public void OpenSound()
    {
        AudioManager.Instance.PlaySFXOneShot(_openingSound);
    }

    public void PressureSound()
    {
        AudioManager.Instance.PlaySFXOneShot(_PressureSound);
    }
}
