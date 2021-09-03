using System.Collections;
using UnityEngine;

public class GameOverDetectionBehaviour : MonoBehaviour
{
    public SceneLoader sceneLoaderObj;

    private WaitForSeconds _waitForSecondsObj;

    private void Awake()
    {
        _waitForSecondsObj = new WaitForSeconds(0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(Wait(other));
    }

    IEnumerator Wait(Collider2D other)
    {
        for (int i = 0; i < 10; i++)
        {
            if (other.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Kinematic) { yield break; }
            yield return _waitForSecondsObj;
        }
        sceneLoaderObj.ReloadScene();
    }
}
