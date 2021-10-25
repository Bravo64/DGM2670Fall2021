using System.Collections;
using GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class LevelButtonBehaviour : MonoBehaviour
{
    public DraggableMenuItemBehaviour levelSelectMenu;
    public TextMeshProUGUI myLevelNumberLabel;
    public VoidEvent transitionToNone;
    public IntData highestLevelUnlocked;

    private AudioSource _myAudioSource;
    private int _levelToSceneIndexShift = 2;
    private int levelIntNumber;


    private void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
        levelIntNumber = int.Parse(myLevelNumberLabel.text);
    }

    public void ActivateButton()
    {
        if (levelSelectMenu._closeToSnapPoint && levelIntNumber <= highestLevelUnlocked.value)
        {
            transitionToNone.Raise();
            StartCoroutine(WaitAndLoad());
            _myAudioSource.Play();
        }
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelIntNumber + _levelToSceneIndexShift);
    }
}
