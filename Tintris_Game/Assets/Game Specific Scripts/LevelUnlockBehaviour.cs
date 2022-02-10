using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUnlockBehaviour : MonoBehaviour
{
    public IntData highestLevelUnlocked;
    public IntData currentLevel;
    public int sceneToNextLevelIndexShift = -1;
    public int nextLevelNumber = 999;
    
    public void UnlockNextLevel()
    {
        if (nextLevelNumber > highestLevelUnlocked.value)
        {
            highestLevelUnlocked.value = nextLevelNumber;
        }
    }
}
