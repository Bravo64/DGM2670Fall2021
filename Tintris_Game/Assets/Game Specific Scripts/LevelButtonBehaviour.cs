using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonBehaviour : MonoBehaviour
{
    public DraggableMenuItemBehaviour levelSelectMenu;
    public TextMeshProUGUI myLevelNumberLabel;
    
    public void ActivateButton()
    {
        if (levelSelectMenu._closeToSnapPoint)
        {
            SceneManager.LoadScene(int.Parse(myLevelNumberLabel.text) + 1);
        }
    }
}
