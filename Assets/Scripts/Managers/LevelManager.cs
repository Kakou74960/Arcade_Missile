using UnityEngine;

public class LevelManager : Singleton<LevelManager>, IManager
{
    private void CountTimeLevel()
    {
        CharacterManager.Instance.TimeSpent += Time.deltaTime;
    }
}
