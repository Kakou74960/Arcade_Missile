using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>, IManager
{

    private CharacterController _characterController = null;
    private int _missileUsed = 0;
    private float _timeSpent = 0;

    public CharacterController CharacterController
    {
        get => _characterController;
        set => _characterController = value;
    }

    public int MissileUsed
    {
        get => _missileUsed;
        set => _missileUsed = value;
    }

    public float TimeSpent
    {
        get => _timeSpent;
        set => _timeSpent = value;
    }
}
