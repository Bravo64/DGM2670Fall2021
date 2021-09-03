using System.Collections;
using UnityEngine;

public class ShapeErrorSensorBehaviour : MonoBehaviour
{
    public Transform shapeParent;
    public SceneLoader sceneLoader;

    private Collider2D _myBlock;
    private bool _checkingForGameOver = true;

    private void Start()
    {
        StartCoroutine(CheckForGameOver());
    }

    IEnumerator CheckForGameOver()
    {
        yield return new WaitForSeconds(0.1f);
        _checkingForGameOver = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == _myBlock) { return; }
        
        if (_myBlock == null)
        {
            _myBlock = other;
        }
        else if (other != _myBlock)
        {
            shapeParent.position = new Vector2(shapeParent.position.x, Mathf.FloorToInt(shapeParent.position.y + 1.0f));
            if (_checkingForGameOver)
            {
                sceneLoader.ReloadScene();
            }
        }
    }
}
