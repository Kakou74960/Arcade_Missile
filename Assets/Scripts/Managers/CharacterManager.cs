using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>, IManager
{

    private CharacterController _characterController = null;

    public CharacterController CharacterController
    {
        get => _characterController;
        set => _characterController = value;
    }
}
