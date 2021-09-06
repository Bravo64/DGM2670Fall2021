using System.Collections;
using GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonBehaviour : MonoBehaviour
{
    public DraggableMenuItemBehaviour levelSelectMenu;
    public TextMeshProUGUI myLevelNumberLabel;
    public VoidEvent transitionToNone;
    
    
    public void ActivateButton()
    {
        if (levelSelectMenu._closeToSnapPoint)
        {
            transitionToNone.Raise();
            StartCoroutine(WaitAndLoad());
        }
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(int.Parse(myLevelNumberLabel.text) + 1);
    }
}
