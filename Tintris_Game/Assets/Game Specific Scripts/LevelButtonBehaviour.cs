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

    private AudioSource _myAudioSource;


    private void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
    }

    public void ActivateButton()
    {
        if (levelSelectMenu._closeToSnapPoint)
        {
            transitionToNone.Raise();
            StartCoroutine(WaitAndLoad());
            _myAudioSource.Play();
        }
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(int.Parse(myLevelNumberLabel.text) + 1);
    }
}
