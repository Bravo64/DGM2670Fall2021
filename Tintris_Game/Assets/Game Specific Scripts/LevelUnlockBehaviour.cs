using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUnlockBehaviour : MonoBehaviour
{
    public IntData highestLevelUnlocked;
    public int sceneToNextLevelIndexShift = -1;
    
    public void UnlockNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + sceneToNextLevelIndexShift > highestLevelUnlocked.value)
        {
            highestLevelUnlocked.value = SceneManager.GetActiveScene().buildIndex + sceneToNextLevelIndexShift;
        }
    }
}
